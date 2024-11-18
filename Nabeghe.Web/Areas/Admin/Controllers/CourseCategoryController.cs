using Microsoft.AspNetCore.Mvc;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.Shared;
using Nabeghe.Domain.ViewModels.CourseCategory;
using Nabeghe.Domain.ViewModels.User;
using System.Collections.Generic;

namespace Nabeghe.Web.Areas.Admin.Controllers
{
	public class CourseCategoryController(ICourseCategoryService _courseCategoryService) : AdminBaseController
	{
		#region Index

		public async Task<IActionResult> Index(FilterCourseCategoryViewModel filter)
		{
			var model = await _courseCategoryService.FilterCourseCategoryAsync(filter);
			return View(model);
		}

		#endregion

		#region Create

		// GET: Admin/Users/Create
		public async Task<IActionResult> Create()
		{
			ViewData["ParentCategories"] = await _courseCategoryService.GetAllParentsAsync();
			return View();
		}

		// POST: Admin/Users/Create
		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateCourseCategoryViewModel model)
		{
			#region Validations

			if (!ModelState.IsValid)
			{
				ViewData["ParentCategories"] = await _courseCategoryService.GetAllParentsAsync();
				return View(model);
			}

			#endregion
			var result = await _courseCategoryService.CreateAsync(model);

			switch (result)
			{
				case CreateCourseCategoryResult.Success:
					TempData[SuccessMessage] = SuccessMessages.CreateCourseCategorySuccessfullyDone;
					return RedirectToAction(nameof(Index));
					
			}
			ViewData["ParentCategories"] = await _courseCategoryService.GetAllParentsAsync();
			return View(model);
		}

		#endregion

		#region Update
		public async Task<IActionResult> Edit(int id)
		{
			var courseCategory = await _courseCategoryService.GetForEditAsync(id);
			if (courseCategory == null)
				return NotFound();

			ViewData["ParentCategories"] = await _courseCategoryService.GetAllParentsAsync();
			return View(courseCategory);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(UpdateCourseCategoryViewModel model)
		{
			#region Validations

			if (!ModelState.IsValid)
			{
				ViewData["ParentCategories"] = await _courseCategoryService.GetAllParentsAsync();
				return View(model);
			}

			#endregion

			var result = await _courseCategoryService.UpdateAsync(model);
			switch (result)
			{
				case UpdateCourseCategoryResult.Success:
					TempData[SuccessMessage] = SuccessMessages.UpdateCourseCategorySuccessfullyDone;
					return RedirectToAction(nameof(Index));

				case UpdateCourseCategoryResult.CourseCategoryNotFound:
					TempData[ErrorMessage] = ErrorMessages.CourseCategoryNotFound;
					break;
			}

			ViewData["ParentCategories"] = await _courseCategoryService.GetAllParentsAsync();
			return View(model);
		}
		#endregion

		#region Delete

		public async Task<IActionResult> Delete(int id)
		{
			var result = await _courseCategoryService.DeleteAsync(id);
			switch (result)
			{
				case DeleteCourseCategoryResult.Success:
					TempData[SuccessMessage] = SuccessMessages.DeleteCourseCategorySuccessfullyDone;
					return RedirectToAction(nameof(Index));

				case DeleteCourseCategoryResult.CourseCategoryNotFound:
					TempData[ErrorMessage] = ErrorMessages.CourseCategoryNotFound;
					break;
			}

			return RedirectToAction(nameof(Index));
		}
		#endregion
	}
}
