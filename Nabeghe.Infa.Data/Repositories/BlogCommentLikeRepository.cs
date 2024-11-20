using Microsoft.EntityFrameworkCore;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Domain.Models.Blog;
using Nabeghe.Infra.Data.Context;

namespace Nabeghe.Infra.Data.Repositories;

public class BlogCommentLikeRepository : IBlogCommentLikeRepository
{
    private readonly NabegheContext _context;

    public BlogCommentLikeRepository(NabegheContext context)
    {
        _context = context;
    }

    public async Task AddLikeAsync(BlogCommentLike like)
    {
        await _context.BlogCommentLikes.AddAsync(like);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveLikeAsync(int userId, int commentId)
    {
		var like = await _context.BlogCommentLikes.FirstOrDefaultAsync(l=>l.UserId == userId && l.CommentId == commentId);
		if (like != null)
		{
			 _context.BlogCommentLikes.Remove(like);
			await _context.SaveChangesAsync();
		}
	}

    public async Task<int> GetLikesCountByCommentIdAsync(int commentId)
    {
        return await _context.BlogCommentLikes
            .CountAsync(l => l.CommentId == commentId);
    }

    public bool IsUserLikedBlogComment(int userId, int commentId)
    {
		return _context.BlogCommentLikes.Any(c => c.UserId == userId && c.CommentId == commentId);
	}
}