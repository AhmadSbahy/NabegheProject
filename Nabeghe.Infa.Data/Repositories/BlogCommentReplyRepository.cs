using Microsoft.EntityFrameworkCore;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Domain.Models.Blog;
using Nabeghe.Infra.Data.Context;

namespace Nabeghe.Infra.Data.Repositories;

public class BlogCommentReplyRepository : IBlogCommentReplyRepository
{
    private readonly NabegheContext _context;

    public BlogCommentReplyRepository(NabegheContext context)
    {
        _context = context;
    }

    public async Task AddReplyAsync(BlogCommentReply reply)
    {
        await _context.BlogCommentReplies.AddAsync(reply);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<BlogCommentReply>> GetRepliesByCommentIdAsync(int commentId)
    {
        return await _context.BlogCommentReplies
            .Where(r => r.CommentId == commentId)
            .Include(r => r.User)
            .ToListAsync();
    }
}