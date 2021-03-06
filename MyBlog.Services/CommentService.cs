﻿using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyBlog.DataAccessLayer;
using MyBlog.DataAccessLayer.Models;
using MyBlog.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Services
{
    public class CommentService : ICommentService
    {
        private readonly CommentContext context;
        private readonly IMapper mapper;
        public CommentService(IOptions<Settings> options, IMapper mapper)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            context = new CommentContext(options);
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<Comment> GetCommentAsync(string id)
        {
            return await context.Comments.Find(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsAsync()
        {
            return await context.Comments.Find(i => true).ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetUserCommentsAsync(string userId)
        {
            return await context.Comments.Find(i => i.UserId == userId).ToListAsync();
        }

        public async Task<Comment> AddCommentAsync(CommentRequest request, Entry entry)
        {
            var comment = mapper.Map<CommentRequest, Comment>(request);
            await context.Comments.InsertOneAsync(comment);           
            
            return comment;
        }

        public async Task<Comment> RemoveCommentAsync(string commentId)
        {
            var comment = await context.Comments.FindOneAndDeleteAsync(i => i.Id == commentId);
            return comment;
        }

        public async Task RemoveCommentsAsync(IEnumerable<Comment> comments)
        {
            foreach (var comment in comments)
            {
                await RemoveCommentAsync(comment.Id);
            }
        }
    }
}
