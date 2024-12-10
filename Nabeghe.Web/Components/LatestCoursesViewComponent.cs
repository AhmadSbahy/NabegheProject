using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.Enums.Course;
using Nabeghe.Infra.Data.Context;

namespace Nabeghe.Web.Components
{
	public class LatestCoursesViewComponent(ICourseService courseService) : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(int courseId)
		{
			var courses = await courseService.GetLatestCoursesAsync(15);

			return View("/Views/Shared/Components/LatestCourses.cshtml", courses);
		}
	}
}
