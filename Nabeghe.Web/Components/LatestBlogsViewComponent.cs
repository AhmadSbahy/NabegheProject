using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Infra.Data.Context;

namespace Nabeghe.Web.Components
{
	public class LatestBlogsViewComponent(IBlogService blogService) : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(int courseId)
		{
			var blogs = await blogService.GetLatestBlogsAsync(4);

			return View("/Views/Shared/Components/LatestBlogs.cshtml", blogs);
		}
	}	
}
