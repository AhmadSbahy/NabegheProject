using Microsoft.EntityFrameworkCore;
using Nabeghe.Domain.Enums.Blog;
using Nabeghe.Domain.Enums.Course;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Domain.Models.Blog;
using Nabeghe.Domain.ViewModels.BlogComment;
using Nabeghe.Domain.ViewModels.CourseComment;
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
            .Where(c=>c.Status == BlogCommentStatus.Confirmed)
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

    public async Task<FilterBlogCommentViewModel> FilterBlogCommentAsync(FilterBlogCommentViewModel model)
    {
		var query = _context.BlogComments.Where(c => c.BlogId == model.BlogId).AsQueryable();

		#region Filter

		if (!string.IsNullOrEmpty(model.Param))
		{
			query = query.Where(c => c.CommentText.Contains(model.Param));
		}

		switch (model.Status)
		{
			case FilterBlogCommentStatus.All:
				query = query;
				break;

			case FilterBlogCommentStatus.Pending:
				query = query.Where(c => c.Status == BlogCommentStatus.Pending);
				break;

			case FilterBlogCommentStatus.Confirmed:
				query = query.Where(c => c.Status == BlogCommentStatus.Confirmed);
				break;

			case FilterBlogCommentStatus.Rejected:
				query = query.Where(c => c.Status == BlogCommentStatus.Rejected);
				break;
		}


		#endregion

		query = query.OrderByDescending(c => c.CreateDate);

		await model.Paging(query.Select(c => new BlogCommentViewModel()
		{
			Id = c.Id,
			CreateDate = c.CreateDate,
			CommentText = c.CommentText,
			BlogId = c.BlogId,
			UserId = c.UserId,
			Status = c.Status
		}));

		return model;
	}

    public async Task<BlogComment?> GetCommentByIdAsync(int commentId)
    {
	    return await _context.BlogComments.FirstOrDefaultAsync(comment => comment.Id == commentId);
    }
}