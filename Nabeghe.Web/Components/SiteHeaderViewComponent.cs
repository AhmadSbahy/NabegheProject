using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nabeghe.Infra.Data.Context;

namespace Nabeghe.Web.Components
{
	public class SiteHeaderViewComponent(NabegheContext context) : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var categories = await context.CourseCategories
				.Where(c => !c.IsDeleted)
				.ToListAsync();

			return View("/Views/Shared/Components/SiteHeader.cshtml", categories);
		}
	}
}
