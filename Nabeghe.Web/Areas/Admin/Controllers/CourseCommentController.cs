using Microsoft.AspNetCore.Mvc;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.ViewModels.CourseComment;
using Nabeghe.Web.Utilities;

namespace Nabeghe.Web.Areas.Admin.Controllers
{
	public class CourseCommentController(ICourseCommentService courseCommentService) : AdminBaseController
	{
		#region List
		[CheckPermission("ManageCourseComment")]
		public async Task<IActionResult> Index(FilterCourseCommentViewModel filter)
		{
			if (filter.CourseId < 1)
				return NotFound();

			FilterCourseCommentViewModel comments = await courseCommentService.FilterCourseCommentAsync(filter);
			
			return View(comments);
		}

		#endregion

		#region Confirm

		[CheckPermission("ConfirmComment")]
		public async Task<IActionResult> ConfirmComment(int commentId)
		{
			var result = await courseCommentService.ConfirmComment(commentId);

			if (!result.Success)
				return BadRequest(new { message = result.Message, status = result.StatusCode });

			return Ok(new { message = result.Message, status = result.StatusCode });
		}

		#endregion

		#region Reject
		[CheckPermission("RejectComment")]
		public async Task<IActionResult> RejectComment(int commentId)
		{
			var result = await courseCommentService.RejectComment(commentId);

			return Ok(result);
		}

		#endregion  
	}
}
