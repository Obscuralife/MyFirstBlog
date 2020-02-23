using MyBlog.DataAccessLayer.Models;
using MyBlog.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBlog.Services
{
    public interface IEntryService
    {
        Task<IEnumerable<Entry>> GetEntriesAsync();
        Task<IEnumerable<Entry>> GetEntriesAsync(string category);
        Task<Entry> GetEntryAsync(string article);
        Task<Entry> GetEntryByIdAsync(string entryId);
        Task<Entry> CreateEntryAsync(EntryRequest entry);
        Task<Entry> RemoveEntryAsync(string id);
        Task<Entry> UpdateEntryAsync(string id, UpdateRequest updateRequest);
        Task<Comment> AddCommentAsync(string entryId, CommentRequest request);
    }
}
