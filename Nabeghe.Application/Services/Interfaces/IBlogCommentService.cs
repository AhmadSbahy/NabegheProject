using Nabeghe.Domain.Models.Blog;

namespace Nabeghe.Application.Services.Interfaces;

public interface IBlogCommentService
{
    Task<IEnumerable<BlogComment>> GetBlogCommentsAsync(int blogId);
    Task AddCommentAsync(BlogComment comment);
    Task AddReplyToCommentAsync(BlogCommentReply reply);
    Task LikeCommentAsync(BlogCommentLike like);
    bool IsUserLikedBlogComment(int userId, int commentId);
    void DeleteBlogCommentLike(int userId, int commentId);
}