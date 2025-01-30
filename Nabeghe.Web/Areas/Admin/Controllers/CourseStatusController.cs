using Microsoft.AspNetCore.Mvc;
using Nabeghe.Domain.Shared;
using Nabeghe.Domain.ViewModels.Course;
using Nabeghe.Web.Contracts;
using Nabeghe.Web.Models.CourseStatus;
using Nabeghe.Web.Utilities;

namespace Nabeghe.Web.Areas.Admin.Controllers
{
	public class CourseStatusController : AdminBaseController
	{
		private readonly ICourseStatusService _courseStatusService;

		#region Constructor

		public CourseStatusController(ICourseStatusService courseStatusService)
		{
			_courseStatusService = courseStatusService;
		}

		#endregion

		#region List

		public async Task<IActionResult> List()
		{
			var courseStatusList = await _courseStatusService.GetCourseStatusListAsync();
			return View(courseStatusList);
		}

		#endregion

		#region Detail

		public async Task<IActionResult> Detail(int id)
		{
			var courseStatus = await _courseStatusService.GetCourseStatusDetailAsync(id);
			return View(courseStatus);
		}

		#endregion

		#region Create

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateCourseStatusViewModel model)
		{
			var response = await _courseStatusService.CreateCourseStatusAsync(model);

			switch (response.IsSuccess)
			{
				case true:
					TempData[SuccessMessage] = response.Message;
					return RedirectToAction(nameof(List));

				case false:
					TempData[ErrorMessage] = response.ValidationErrors;
					return View(model);
			}
		}

		#endregion

		#region Update
		[HttpGet]
		public async Task<IActionResult> Update(int id)
		{
			var courseStatus = await _courseStatusService.GetCourseStatusDetailAsync(id);
			return View(courseStatus);
		}

		[HttpPost]
		public async Task<IActionResult> Update(CourseStatusViewModel model)
		{
			var response = await _courseStatusService.UpdateCourseStatusAsync(model);

			switch (response.IsSuccess)
			{
				case true:
					TempData[SuccessMessage] = response.Message;
					return RedirectToAction(nameof(List));

				case false:
					TempData[ErrorMessage] = response.ValidationErrors;
					return View(model);
			}
		}
		#endregion

		#region Delete

		public async Task<IActionResult> Delete(int id)
		{
			var response = await _courseStatusService.DeleteCourseStatusAsync(id);
			switch (response.IsSuccess)
			{
				case true:
					TempData[SuccessMessage] = response.Message;
					return RedirectToAction(nameof(List));

				case false:
					TempData[ErrorMessage] = response.ValidationErrors;
					break;
			}
            return RedirectToAction(nameof(List));
        }

		#endregion
	}
}
