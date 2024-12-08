using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nabeghe.Domain.Enums.Course;
using Nabeghe.Infra.Data.Context;

namespace Nabeghe.Web.Components
{
    public class CourseCommentViewComponent(NabegheContext context) : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(int courseId)
		{
			var courseComments = await context.CourseComments
				.Where(cc => cc.CourseId == courseId && cc.Status == CourseCommentStatus.Confirmed)
				.ToListAsync();

			return View("/Views/Shared/Components/CourseComment.cshtml", courseComments);
		}
	}
}
