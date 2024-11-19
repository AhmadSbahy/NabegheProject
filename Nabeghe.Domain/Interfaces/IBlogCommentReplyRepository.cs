using Nabeghe.Domain.Models.Blog;

namespace Nabeghe.Domain.Interfaces;

public interface IBlogCommentReplyRepository
{
    Task AddReplyAsync(BlogCommentReply reply);
    Task<IEnumerable<BlogCommentReply>> GetRepliesByCommentIdAsync(int commentId);
}