using Microsoft.EntityFrameworkCore;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Domain.Models.Blog;
using Nabeghe.Infra.Data.Context;

namespace Nabeghe.Infra.Data.Repositories;

public class BlogCommentRepository : IBlogCommentRepository
{
    private readonly NabegheContext _context;

    public BlogCommentRepository(NabegheContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BlogComment>> GetCommentsByBlogIdAsync(int blogId)
    {
        return await _context.BlogComments
            .Where(c => c.BlogId == blogId)
            .Include(c => c.CommentLikes)
			.Include(c => c.User)
            .Include(c => c.Replies)
            .ThenInclude(b=>b.User)
            
            .ToListAsync();
    }

    public async Task AddCommentAsync(BlogComment comment)
    {
        await _context.BlogComments.AddAsync(comment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCommentAsync(BlogComment comment)
    {
        _context.BlogComments.Update(comment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCommentAsync(int id)
    {
        var comment = await _context.BlogComments.FindAsync(id);
        if (comment != null)
        {
            _context.BlogComments.Remove(comment);
            await _context.SaveChangesAsync();
        }
    }
}