using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.Enums.Course;
using Nabeghe.Infra.Data.Context;

namespace Nabeghe.Web.Components
{
    public class CourseCommentViewComponent(ICourseCommentService commentService) : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(int courseId)
		{
			var courseComments = await commentService.GetCommentsByCourseIdAsync(courseId);

			return View("/Views/Shared/Components/CourseComment.cshtml", courseComments);
		}
	}
}
