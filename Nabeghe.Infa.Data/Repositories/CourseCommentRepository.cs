using Microsoft.EntityFrameworkCore;
using Nabeghe.Domain.Enums.Course;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Domain.Models.Course;
using Nabeghe.Domain.ViewModels.CourseComment;
using Nabeghe.Infra.Data.Context;

namespace Nabeghe.Infra.Data.Repositories
{
    public class CourseCommentRepository : ICourseCommentRepository
    {
        private readonly NabegheContext _context;

        public CourseCommentRepository(NabegheContext context)
        {
            _context = context;
        }

        public async Task AddReplyAsync(CommentReply reply)
        {
            await _context.CommentReplies.AddAsync(reply);
            await _context.SaveChangesAsync();
        }

        public bool IsUserLikedComment(int userId, int commentId)
        {
	        return _context.CommentLikes.Any(c => c.UserId == userId && c.CommentId == commentId);
        }

        public async Task AddCommentLikeAsync(int userId, int commentId)
        {
	        CommentLike commentLike = new CommentLike()
	        {
                CommentId = commentId,
                UserId = userId
	        };

	        await _context.CommentLikes.AddAsync(commentLike);
	        await _context.SaveChangesAsync();
        }

        public async Task DeleteCommentLikeAsync(int userId, int commentId)
        {
	        var commentLike = _context.CommentLikes.FirstOrDefault(c => c.CommentId == commentId && c.UserId == userId);
	        if (commentLike != null)
	        {
				_context.CommentLikes.Remove(commentLike);
				await _context.SaveChangesAsync();
			}
		}

        public async Task<List<CourseComment>> GetCourseCommentsAsync(int courseId)
        {
	        return await _context.CourseComments.Where(c=>c.CourseId == courseId)
		        .ToListAsync();
        }

        public async Task<CourseComment?> GetCommentByIdAsync(int commentId)
        {
	        return await _context.CourseComments.FirstOrDefaultAsync(cc => cc.Id == commentId);
        }

        public void UpdateComment(CourseComment model)
        {
	        _context.CourseComments.Update(model);
        }

        public async Task SaveAsync()
        {
	        await _context.SaveChangesAsync();
        }

        public async Task<FilterCourseCommentViewModel> FilterCourseCommentAsync(FilterCourseCommentViewModel filterCourseCommentViewModel)
        {
			var query = _context.CourseComments.Where(c=>c.CourseId == filterCourseCommentViewModel.CourseId).AsQueryable();

			#region Filter

			if (!string.IsNullOrEmpty(filterCourseCommentViewModel.Param))
			{
				query = query.Where(c => c.CommentText.Contains(filterCourseCommentViewModel.Param));
			}
			switch (filterCourseCommentViewModel.Status)
			{
				case FilterCourseCommentStatus.All:
					query = query;
					break;

				case FilterCourseCommentStatus.Pending:
					query = query.Where(c => c.Status == CourseCommentStatus.Pending);
					break;

				case FilterCourseCommentStatus.Confirmed:
					query = query.Where(c => c.Status == CourseCommentStatus.Confirmed);
					break;

				case FilterCourseCommentStatus.Rejected:
					query = query.Where(c => c.Status == CourseCommentStatus.Rejected);
					break;
			}

			#endregion

			query = query.OrderByDescending(c => c.CreateDate);

			await filterCourseCommentViewModel.Paging(query.Select(c => new CourseCommentViewModel()
			{
				Id = c.Id,
				CreateDate = c.CreateDate,
                CommentText = c.CommentText,
                CourseId = c.CourseId,
                UserId = c.UserId,
                Status = c.Status
			}));

			return filterCourseCommentViewModel;
		}

        public async Task AddCommentAsync(CourseComment comment)
        {
            await _context.CourseComments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<CourseComment> GetCommentWithRepliesAsync(int commentId)
        {
            return await _context.CourseComments
                .Include(c => c.Replies)
                .FirstOrDefaultAsync(c => c.Id == commentId);
        }
    }
}
