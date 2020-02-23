using MongoDB.Driver;
using MyBlog.DataAccessLayer.Models;
using MyBlog.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBlog.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetCommentsAsync();
        Task<IEnumerable<Comment>> GetUserCommentsAsync(string userId);
        Task<Comment> GetCommentAsync(string id);
        Task<Comment> AddCommentAsync(CommentRequest request, Entry entry);
    }
}
