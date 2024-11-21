using Microsoft.AspNetCore.Mvc;
using Nabeghe.Application.Services.Implementation;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.ViewModels.BlogComment;

namespace Nabeghe.Web.Areas.Admin.Controllers
{
	public class BlogCommentController(IBlogCommentService blogCommentService) : AdminBaseController
	{
		#region List

		public async Task<IActionResult> Index(FilterBlogCommentViewModel filter)
		{
			var model = await blogCommentService.FilterBlogCommentAsync(filter);
			return View(model);
		}

		#endregion

		#region Confirm

		public async Task<IActionResult> ConfirmComment(int commentId)
		{
			var result = await blogCommentService.ConfirmComment(commentId);

			if (!result.Success)
				return BadRequest(new { message = result.Message, status = result.StatusCode });

			return Ok(new { message = result.Message, status = result.StatusCode });
		}

		#endregion

		#region Reject

		public async Task<IActionResult> RejectComment(int commentId)
		{
			var result = await blogCommentService.RejectComment(commentId);

			if (!result.Success)
				return BadRequest(new { message = result.Message, status = result.StatusCode });

			return Ok(new { message = result.Message, status = result.StatusCode });
		}

		#endregion
	}
}
