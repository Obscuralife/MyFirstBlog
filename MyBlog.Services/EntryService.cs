using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyBlog.DataAccessLayer;
using MyBlog.DataAccessLayer.Models;
using MyBlog.Services.Abstract;
using MyBlog.Services.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyBlog.Services
{
    public class EntryService : IEntryService
    {
        private readonly EntryContext context;
        private readonly ICommentService commentService;
        private readonly IMapper mapper;
        private readonly IUserService userService;
        public EntryService(IOptions<Settings> options, IMapper mapper, ICommentService commentService, IUserService userService)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            context = new EntryContext(options);
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.commentService = commentService ?? throw new ArgumentNullException(nameof(commentService));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<Entry> CreateEntryAsync(EntryRequest request)
        {
            var entries = await context.Entries.Find(i => i.Article == request.Article).FirstOrDefaultAsync();
            if (entries != null)
            {
                throw new RequestedResourceHasConflictException(nameof(request.Article));
            }
            var entry = mapper.Map<EntryRequest, Entry>(request);
            await context.Entries.InsertOneAsync(entry);
            return entry;
        }

        public async Task<IEnumerable<Entry>> GetEntriesAsync()
        {
            var entries = await context.Entries.Find(x => true).ToListAsync();
            if (entries is null)
            {
                throw new RequestedResourceNotFoundException(nameof(context.Entries));
            }

            return entries;
        }

        public async Task<IEnumerable<Entry>> GetEntriesAsync(string category)
        {
            ValidateRequestLength(category, nameof(category));
            var entries = await this.context.Entries.Find(x => x.Category.ToLower() == category.ToLower()).ToListAsync();
            if (entries is null)
            {
                throw new RequestedResourceNotFoundException(nameof(category));
            }

            return entries;
        }

        public async Task<Entry> GetEntryAsync(string article)
        {
            ValidateRequestLength(article, nameof(article));
            var entries = await context.Entries.Find(x => x.Article.ToLower() == article.ToLower()).FirstOrDefaultAsync();
            if (entries is null)
            {
                throw new RequestedResourceNotFoundException(nameof(article));
            }

            return entries;
        }

        public async Task<Entry> RemoveEntryAsync(string id)
        {
            ValidateIdLength(id, nameof(id));
            var entry = await context.Entries.FindOneAndDeleteAsync(x => x.Id == id);
            if (entry.Comments != null)
            {
                await commentService.RemoveCommentsAsync(entry.Comments);
            }
            if (entry is null)
            {
                throw new RequestedResourceNotFoundException(nameof(id));
            }
            return entry;
        }

        public async Task<Entry> UpdateEntryAsync(string id, UpdateRequest request)
        {
            ValidateIdLength(id, nameof(id));
            var entry = await context.Entries.FindOneAndUpdateAsync((i) => i.Id == id,
                                Builders<Entry>.Update
                                .Set(j => j.Body, request.NewContent)
                                .Set(k => k.UpdatedOn, DateTime.Now));
            if (entry is null)
            {
                throw new RequestedResourceNotFoundException(nameof(id));
            }

            return entry;
        }

        public async Task<Comment> AddCommentAsync(string entryId, CommentRequest request)
        {
            var entry = await this.GetEntryByIdAsync(entryId);
            if (entry is null)
            {
                throw new RequestedResourceNotFoundException(nameof(entryId));
            }

            var comment = await commentService.AddCommentAsync(request, entry);

            if (entry.Comments is null)
            {
                entry.Comments = new List<Comment>() { comment };
            }
            else
            {
                entry.Comments.Add(comment);
            }
            var newCommentList = entry.Comments;
            await context.Entries.UpdateOneAsync((i) => i.Id == entry.Id,
                                    Builders<Entry>.Update
                                    .Set(j => j.Comments, newCommentList));
            return comment;
        }

        public async Task<Entry> GetEntryByIdAsync(string entryId)
        {
            var entry = await context.Entries.Find(x => x.Id == entryId).FirstOrDefaultAsync();
            if (entry is null)
            {
                throw new RequestedResourceNotFoundException(nameof(entryId));
            }
            return entry;
        }

        private void ValidateRequestLength(string value, string argumentsName)
        {
            if (value.Length < 2 || value.Length > 10)
            {
                throw new RequestedResourceHasBadRequest($"Wrong {argumentsName} length");
            }
        }

        private void ValidateIdLength(string id, string idName)
        {
            if (id.Length != 24)
            {
                throw new RequestedResourceHasBadRequest($"Wrong {idName} length");
            }
        }
    }
}
