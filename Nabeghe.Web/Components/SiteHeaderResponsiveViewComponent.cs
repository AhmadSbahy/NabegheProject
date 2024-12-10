using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Infra.Data.Context;

namespace Nabeghe.Web.Components
{
	public class SiteHeaderResponsiveViewComponent(ICourseCategoryService courseCategoryService) : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var categories = await courseCategoryService.GetAllCategoriesAsync();

			return View("/Views/Shared/Components/SiteHeaderResponsive.cshtml", categories);
		}
	}
}
