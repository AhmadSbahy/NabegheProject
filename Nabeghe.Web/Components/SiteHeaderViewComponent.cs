using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nabeghe.Application.Services.Implementation;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Infra.Data.Context;

namespace Nabeghe.Web.Components
{
	public class SiteHeaderViewComponent(ICourseCategoryService courseCategoryService) : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var categories = await courseCategoryService.GetAllCategoriesAsync();


			return View("/Views/Shared/Components/SiteHeader.cshtml", categories);
		}
	}
}
