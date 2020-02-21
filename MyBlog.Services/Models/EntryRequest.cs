namespace MyBlog.Services.Models
{
    public class EntryRequest
    {
        public string Article { get; set; }
        public string Category { get; set; }
        public string Body { get; set; }
    }
}