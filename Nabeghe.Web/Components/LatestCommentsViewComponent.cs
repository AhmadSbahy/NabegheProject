using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.Enums.Course;
using Nabeghe.Infra.Data.Context;

namespace Nabeghe.Web.Components
{
	public class LatestCommentsViewComponent(ICourseCommentService commentService) : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var comments = await commentService.GetLatestCommentsAsync(5);

			return View("/Views/Shared/Components/LatestComments.cshtml", comments);
		}
	}
}
