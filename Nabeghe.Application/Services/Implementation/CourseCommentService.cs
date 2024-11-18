using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.Enums.Course;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Domain.ViewModels.CourseComment;

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
	}
}
