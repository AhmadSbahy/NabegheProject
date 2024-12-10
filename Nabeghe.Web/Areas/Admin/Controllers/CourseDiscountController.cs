using Microsoft.AspNetCore.Mvc;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.Shared;
using Nabeghe.Domain.ViewModels.CourseDiscount;
using Nabeghe.Web.Utilities;

namespace Nabeghe.Web.Areas.Admin.Controllers
{
    public class CourseDiscountController(ICourseDiscountService discountService) : AdminBaseController
    {
		#region Create
		// GET: Admin/CourseDiscount/Create/1
		[CheckPermission("AddCourseDiscount")]
		public IActionResult Create(int id)
		{
			return View(new CreateCourseDiscountViewModel()
			{
				CourseId = id
			});
		}

		// POST: Admin/CourseDiscount/Create
		[HttpPost , CheckPermission("AddCourseDiscount") ,ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateCourseDiscountViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var result = await discountService.CreateAsync(model);
			switch (result)
			{
				case CreateCourseDiscountResult.Success:
					TempData[SuccessMessage] = SuccessMessages.CreateCourseDiscountSuccessfullyDone;
					return RedirectToAction("Index","Course");

			}
			return View();
		}
		#endregion

		#region Update
		// GET: Admin/CourseDiscount/Edit/5
		[CheckPermission("EditCourseDiscount")]
		public async Task<IActionResult> Edit(int id)
		{
			var courseDiscount = await discountService.GetForEditAsync(id);

			if (courseDiscount == null)
				return NotFound();

			return View(courseDiscount);
		}

		// POST: Admin/CourseDiscount/Edit/5
		[HttpPost, CheckPermission("EditCourseDiscount"), ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(UpdateCourseDiscountViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			var result = await discountService.UpdateAsync(model);
			switch (result)
			{
				case UpdateCourseDiscountResult.Success:
					TempData[SuccessMessage] = SuccessMessages.UpdateCourseDiscountSuccessfullyDone;
					return RedirectToAction("Index", "Course");

				case UpdateCourseDiscountResult.NotFound:
					TempData[ErrorMessage] = ErrorMessages.CourseDiscountNotFound;
					break;
			}

			return View();
		}
		#endregion

		#region Delete
		// GET: Admin/CourseDiscount/Delete/5
		[CheckPermission("DeleteCourseDiscount")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await discountService.DeleteAsync(id);
			switch (result)
			{
				case DeleteCourseDiscountResult.Success:
					TempData[SuccessMessage] = SuccessMessages.DeleteCourseDiscountSuccessfullyDone;
					return RedirectToAction("Index", "Course");

				case DeleteCourseDiscountResult.NotFound:
					TempData[ErrorMessage] = ErrorMessages.CourseDiscountNotFound;
					break;
			}

			return RedirectToAction("Index", "Course");
		}
		#endregion
	}
}
