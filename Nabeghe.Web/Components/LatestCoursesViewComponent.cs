using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nabeghe.Domain.Enums.Course;
using Nabeghe.Infra.Data.Context;

namespace Nabeghe.Web.Components
{
	public class LatestCoursesViewComponent(NabegheContext context) : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(int courseId)
		{
			var courses = await context.Courses
				.Include(c=>c.CourseStatus)
				.Include(c=>c.CourseCategory)
				.Include(c=>c.CourseDiscount)
				.Include(c=>c.CourseLikes)
				.Include(c=>c.User)
				.OrderByDescending(c=>c.CreateDate)
				.Take(15)
				.ToListAsync();

			return View("/Views/Shared/Components/LatestCourses.cshtml", courses);
		}
	}
}
