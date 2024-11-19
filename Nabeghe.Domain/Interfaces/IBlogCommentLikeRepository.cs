using Nabeghe.Domain.Models.Blog;

namespace Nabeghe.Domain.Interfaces;

public interface IBlogCommentLikeRepository
{
    Task AddLikeAsync(BlogCommentLike like);
    Task RemoveLikeAsync(int userId, int commentId);
    Task<int> GetLikesCountByCommentIdAsync(int commentId);
    bool IsUserLikedBlogComment(int userId, int commentId);

}