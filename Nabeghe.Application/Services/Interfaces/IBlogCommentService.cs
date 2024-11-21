using Nabeghe.Domain.Models.Blog;
using Nabeghe.Domain.ViewModels.BlogComment;

namespace Nabeghe.Application.Services.Interfaces;

public interface IBlogCommentService
{
    Task<IEnumerable<BlogComment>> GetBlogCommentsAsync(int blogId);
    Task AddCommentAsync(BlogComment comment);
    Task AddReplyToCommentAsync(BlogCommentReply reply);
    Task LikeCommentAsync(BlogCommentLike like);
    bool IsUserLikedBlogComment(int userId, int commentId);
    Task DeleteBlogCommentLike(int userId, int commentId);
    Task<FilterBlogCommentViewModel> FilterBlogCommentAsync(FilterBlogCommentViewModel model);
    Task<(bool Success, string Message, int StatusCode)> ConfirmComment(int commentId);
    Task<(bool Success, string Message, int StatusCode)> RejectComment(int commentId);
}