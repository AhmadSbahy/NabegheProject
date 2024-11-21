using Microsoft.AspNetCore.Mvc;
using Nabeghe.Application.Services.Implementation;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.Shared;
using Nabeghe.Domain.ViewModels.Blog;
using Nabeghe.Domain.ViewModels.Course;
using Nabeghe.Domain.ViewModels.Role;

namespace Nabeghe.Web.Areas.Admin.Controllers
{
    public class BlogController(IBlogService blogService) : AdminBaseController
    {
	    #region List

	    public async Task<IActionResult> Index(AdminFilterBlogViewModel filter)
	    {
		    var model = await blogService.FilterBlogAsync(filter);
		    return View(model);
		}


		#endregion

		#region Create
		// GET: Admin/Blog/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Admin/Blog/Create
		[HttpPost,ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(AdminCreateBlogViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var result = await blogService.AddNewBlogAsync(model);
			switch (result)
			{
				case AdminCreateBlogStatus.Success:
					TempData[SuccessMessage] = SuccessMessages.CreateRoleSuccessfullyDone;
					return RedirectToAction(nameof(Index));

			}

			return View();
		}
		#endregion

		#region Update

		// GET: Admin/Blog/Edit/5
		public async Task<IActionResult> Edit(int id)
		{
			var role = await blogService.GetBlogDetailsAsync(id);

			if (role == null)
				return NotFound();

			return View(role);
		}

		// POST: Admin/Blog/Edit/5
		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(AdminEditBlogViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			var result = await blogService.EditBlogAsync(model);
			switch (result)
			{
				case AdminEditBlogStatus.Success:
					TempData[SuccessMessage] = SuccessMessages.UpdateBlogSuccessfullyDone;
					return RedirectToAction(nameof(Index));

				case AdminEditBlogStatus.NotFound:
					TempData[ErrorMessage] = ErrorMessages.BlogNotFound;
					break;
			}

			return View();
		}

		#endregion

		#region Delete
		// GET: Admin/Roles/Delete/5
		public async Task<IActionResult> Delete(int id)
		{
			var result = await blogService.DeleteBlogAsync(id);
			switch (result)
			{
				case AdminDeleteBlogStatus.Success:
					TempData[SuccessMessage] = SuccessMessages.DeleteBlogSuccessfullyDone;
					return RedirectToAction(nameof(Index));

				case AdminDeleteBlogStatus.NotFound:
					TempData[ErrorMessage] = ErrorMessages.BlogNotFound;
					break;
			}

			return RedirectToAction(nameof(Index));
		}
		#endregion
	}
}
