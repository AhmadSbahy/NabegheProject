using Nabeghe.Domain.Models.Blog;

namespace Nabeghe.Domain.Interfaces;

public interface IBlogCommentRepository
{
    Task<IEnumerable<BlogComment>> GetCommentsByBlogIdAsync(int blogId);
    Task AddCommentAsync(BlogComment comment);
    Task UpdateCommentAsync(BlogComment comment);
    Task DeleteCommentAsync(int id);
}