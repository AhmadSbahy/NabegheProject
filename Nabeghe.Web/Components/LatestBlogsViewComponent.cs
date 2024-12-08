using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nabeghe.Infra.Data.Context;

namespace Nabeghe.Web.Components
{
	public class LatestBlogsViewComponent(NabegheContext context) : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(int courseId)
		{
			var blogs = await context.Blogs
				.Include(c => c.User)
				.Include(c => c.BlogLikes)
				.OrderByDescending(c => c.CreateDate)
				.Take(4)
				.ToListAsync();

			return View("/Views/Shared/Components/LatestBlogs.cshtml", blogs);
		}
	}	
}
