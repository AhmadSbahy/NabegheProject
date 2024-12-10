using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.ViewModels.CourseComment;
using Nabeghe.Domain.Models.Course;

namespace Nabeghe.Domain.Interfaces
{
    public interface ICourseCommentRepository
    {
        Task<FilterCourseCommentViewModel> FilterCourseCommentAsync(FilterCourseCommentViewModel filterCourseCommentViewModel);
		Task AddCommentAsync(CourseComment comment);
        Task<CourseComment> GetCommentWithRepliesAsync(int commentId);
        Task AddReplyAsync(CommentReply reply);
        bool IsUserLikedComment(int userId,int commentId);
        Task AddCommentLikeAsync(int userId, int commentId);
        Task DeleteCommentLikeAsync(int userId,int commentId);
        Task <List<CourseComment>> GetCourseCommentsAsync(int courseId);
        Task<CourseComment?> GetCommentByIdAsync(int commentId);
        void UpdateComment(CourseComment model);
        Task SaveAsync();
        Task<List<CourseComment>> GetCommentsByCourseIdAsync(int courseId);
        Task<List<CourseComment>> GetLatestCommentsAsync(int count);
	}
}
