using Nabeghe.Domain.Models.Blog;
using Nabeghe.Domain.ViewModels.BlogComment;

namespace Nabeghe.Domain.Interfaces;

public interface IBlogCommentRepository
{
    Task<IEnumerable<BlogComment>> GetCommentsByBlogIdAsync(int blogId);
    Task AddCommentAsync(BlogComment comment);
    Task UpdateCommentAsync(BlogComment comment);
    Task DeleteCommentAsync(int id);
    Task<FilterBlogCommentViewModel> FilterBlogCommentAsync(FilterBlogCommentViewModel model);
    Task<BlogComment?> GetCommentByIdAsync(int commentId);
}