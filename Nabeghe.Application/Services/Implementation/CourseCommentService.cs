using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.Enums.Course;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Domain.Models.Course;
using Nabeghe.Domain.ViewModels.CourseComment;
using System.ComponentModel.Design;

namespace Nabeghe.Application.Services.Implementation
{
	public class CourseCommentService(ICourseCommentRepository courseCommentRepository) : ICourseCommentService
	{
		public async Task<FilterCourseCommentViewModel> FilterCourseCommentAsync(FilterCourseCommentViewModel filter)
		{
			return await courseCommentRepository.FilterCourseCommentAsync(filter);
		}

		public async Task<(bool Success, string Message, int StatusCode)> ConfirmComment(int commentId)
		{
			var comment = await courseCommentRepository.GetCommentByIdAsync(commentId);

			if (comment == null)
				return (false, "کامنت مشخص شده پیدا نشد.", 101);

			comment.Status = CourseCommentStatus.Confirmed;
			courseCommentRepository.UpdateComment(comment);
			await courseCommentRepository.SaveAsync();

			return (true, "کامنت مدنظر شما تایید شد.", 100);
		}


		public async Task<object> RejectComment(int commentId)
		{
			var comment = await courseCommentRepository.GetCommentByIdAsync(commentId);

			if (comment == null)
				return new
				{
					message = "کامنت مشخص شده پیدا نشد.",
					status = 101
				};

			comment.Status = CourseCommentStatus.Rejected;

			courseCommentRepository.UpdateComment(comment);
			await courseCommentRepository.SaveAsync();

			return new
			{
				message = "کامنت مدنظر شما با موفقیت رد شد.",
				status = 100
			};
		}

		public async Task<(bool Success, string Message, int StatusCode)> ConfirmCommentReply(int replyId)
		{
			var commentReply = await courseCommentRepository.GetCommentReplyByIdAsync(replyId);

			if (commentReply == null)
				return (false, "پاسخ کامنت مشخص شده پیدا نشد.", 101);

			commentReply.Status = CourseCommentStatus.Confirmed;
			courseCommentRepository.UpdateCommentReply(commentReply);
			await courseCommentRepository.SaveAsync();

			return (true, "پاسخ کامنت مدنظر شما تایید شد.", 100);
		}

		public async Task<object> RejectCommentReply(int replyId)
		{
			var commentReply = await courseCommentRepository.GetCommentReplyByIdAsync(replyId);

			if (commentReply == null)
				return new
				{
					message = "پاسخ کامنت مشخص شده پیدا نشد.",
					status = 101
				};

			commentReply.Status = CourseCommentStatus.Rejected;

			courseCommentRepository.UpdateCommentReply(commentReply);
			await courseCommentRepository.SaveAsync();

			return new
			{
				message = "پاسخ کامنت مدنظر شما با موفقیت رد شد.",
				status = 100
			};
		}

		public Task<List<CourseComment>> GetCommentsByCourseIdAsync(int courseId)
		{
			return courseCommentRepository.GetCommentsByCourseIdAsync(courseId);
		}

		public Task<List<CourseComment>> GetLatestCommentsAsync(int count)
		{
			return courseCommentRepository.GetLatestCommentsAsync(count);
		}
	}
}
