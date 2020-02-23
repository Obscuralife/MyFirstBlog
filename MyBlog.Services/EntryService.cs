using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyBlog.DataAccessLayer;
using MyBlog.DataAccessLayer.Models;
using MyBlog.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBlog.Services
{
    public class EntryService : IEntryService
    {
        private readonly EntryContext context;
        private readonly ICommentService commentService;
        private readonly IMapper mapper;
        public EntryService(IOptions<Settings> options, IMapper mapper, ICommentService commentService)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (mapper is null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            if (commentService is null)
            {
                throw new ArgumentNullException(nameof(commentService));
            }

            context = new EntryContext(options);
            this.mapper = mapper;
            this.commentService = commentService;
        }

        public async Task<Entry> CreateEntryAsync(EntryRequest request)
        {
            var entries = await context.Entries.Find(i => i.Article == request.Article).FirstOrDefaultAsync();
            if (entries != null)
            {
                return null;
            }
            var entry = mapper.Map<EntryRequest, Entry>(request);
            await context.Entries.InsertOneAsync(entry);
            return entry;
        }

        public async Task<IEnumerable<Entry>> GetEntriesAsync()
        {
            return await context.Entries.Find(x => true).ToListAsync();
        }

        public async Task<IEnumerable<Entry>> GetEntriesAsync(string category)
        {
            return await this.context.Entries.Find(x => x.Category.ToLower() == category.ToLower()).ToListAsync();
        }

        public async Task<Entry> GetEntryAsync(string article)
        {
            return await context.Entries.Find(x => x.Article.ToLower() == article.ToLower()).FirstOrDefaultAsync();

        }

        public async Task<Entry> RemoveEntryAsync(string id)
        {
            return await context.Entries.FindOneAndDeleteAsync(x => x.Id == id);
        }

        public async Task<Entry> UpdateEntryAsync(string id, UpdateRequest request)
        {
            return await context.Entries.FindOneAndUpdateAsync((i) => i.Id == id,
                                Builders<Entry>.Update
                                .Set(j => j.Body, request.NewContent)
                                .Set(k => k.UpdatedOn, DateTime.Now));
        }

        public async Task<Comment> AddCommentAsync(string entryId, CommentRequest request)
        {
            var entry = await this.GetEntryByIdAsync(entryId);
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
            return await context.Entries.Find(x => x.Id == entryId).FirstOrDefaultAsync();
        }
    }
}
