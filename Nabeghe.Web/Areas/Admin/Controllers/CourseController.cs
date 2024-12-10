using Microsoft.AspNetCore.Mvc;
using Nabeghe.Application.Extensions;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.Shared;
using Nabeghe.Domain.ViewModels.Course;
using Nabeghe.Web.Utilities;

namespace Nabeghe.Web.Areas.Admin.Controllers
{
	public class CourseController(ICourseService _courseService, ICourseCategoryService _courseCategoryService) : AdminBaseController
	{
		#region Index
		[CheckPermission("ManageCourse")]
		public async Task<IActionResult> Index(AdminSideFilterCourseViewModel filter)
		{
			var model = await _courseService.FilterCourseAsync(filter);
			return View(model);
		}


		#endregion

		#region Create

		// GET: Admin/Users/Create
		[CheckPermission("AddCourse")]
		public async Task<IActionResult> Create()
		{
			ViewData["Categories"] = await _courseCategoryService.GetAllChildCategoriesAsync();
			ViewData["CourseStatus"] = await _courseCategoryService.GetAllCourseStatusAsync();
			
            return View(new CreateCourseViewModel()
            {
				TeacherId = User.GetUserId()
            });
		}

		// POST: Admin/Users/Create
		[HttpPost, CheckPermission("AddCourse"), ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateCourseViewModel model)
		{

			#region Validations

			if (!ModelState.IsValid)
			{
                ViewData["Categories"] = await _courseCategoryService.GetAllChildCategoriesAsync();
                ViewData["CourseStatus"] = await _courseCategoryService.GetAllCourseStatusAsync();
                return View(model);
			}

			#endregion
			
			var result = await _courseService.CreateAsync(model);

			switch (result)
			{
				case CreateCourseResult.Success:
					TempData[SuccessMessage] = SuccessMessages.CreateCourseSuccessfullyDone;
					return RedirectToAction(nameof(Index));

			}
            ViewData["Categories"] = await _courseCategoryService.GetAllChildCategoriesAsync();
            ViewData["CourseStatus"] = await _courseCategoryService.GetAllCourseStatusAsync();
            return View(model);
		}

		#endregion

		#region Update
		[CheckPermission("EditCourse")]
		public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseService.GetForEditAsync(id);
            if (course == null)
                return NotFound();

            ViewData["Categories"] = await _courseCategoryService.GetAllChildCategoriesAsync();
            ViewData["CourseStatus"] = await _courseCategoryService.GetAllCourseStatusAsync();
            return View(course);
        }

        [HttpPost, CheckPermission("EditCourse"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateCourseViewModel model)
        {
            #region Validations

            if (!ModelState.IsValid)
            {
                ViewData["Categories"] = await _courseCategoryService.GetAllChildCategoriesAsync();
                ViewData["CourseStatus"] = await _courseCategoryService.GetAllCourseStatusAsync();
                return View(model);
            }

            #endregion

            var result = await _courseService.UpdateAsync(model);
            switch (result)
            {
                case UpdateCourseResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.UpdateCourseCategorySuccessfullyDone;
                    return RedirectToAction(nameof(Index));

                case UpdateCourseResult.CourseNotFound:
                    TempData[ErrorMessage] = ErrorMessages.CourseCategoryNotFound;
                    break;
            }

            ViewData["Categories"] = await _courseCategoryService.GetAllChildCategoriesAsync();
            ViewData["CourseStatus"] = await _courseCategoryService.GetAllCourseStatusAsync();
            return View(model);
        }
		#endregion

		#region Delete
		// GET: Admin/Users/Delete/5
		[CheckPermission("DeleteCourse")]
		public async Task<IActionResult> Delete(int id)
        {
            var result = await _courseService.DeleteAsync(id);
            switch (result)
            {
                case DeleteCourseResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.DeleteCourseSuccessfullyDone;
                    return RedirectToAction(nameof(Index));

                case DeleteCourseResult.CourseNotFound:
                    TempData[ErrorMessage] = ErrorMessages.CourseNotFound;
                    break;
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
