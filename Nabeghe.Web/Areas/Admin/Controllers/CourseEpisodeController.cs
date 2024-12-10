using Microsoft.AspNetCore.Mvc;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.Shared;
using Nabeghe.Domain.ViewModels.CourseEpisode;
using Nabeghe.Web.Utilities;

namespace Nabeghe.Web.Areas.Admin.Controllers
{
	public class CourseEpisodeController(ICourseEpisodeService _courseEpisodeService, ICourseService _courseService) : AdminBaseController
	{

		#region List
		[CheckPermission("ManageEpisode")]
		public async Task<IActionResult> Index(FilterCourseEpisodeViewModel filter)
		{
			if (filter.CourseId == 0 || !await _courseService.IsCourseExist(filter.CourseId))
				return NotFound();

			var model = await _courseEpisodeService.FilterCourseAsync(filter);
			return View(model);
		}

		#endregion

		#region Create

		// GET: Admin/CourseEpisode/Create
		[CheckPermission("AddEpisode")]
		public async Task<IActionResult> Create(int id)
		{
			if (id == 0 || !await _courseService.IsCourseExist(id))
				return NotFound();


			return View(new CreateCourseEpisodeViewModel()
			{
				CourseId = id
			});
		}

		// POST: Admin/CourseEpisode/Create
		[HttpPost,CheckPermission("AddEpisode"), ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateCourseEpisodeViewModel model)
		{

			#region Validations

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			#endregion

			var result = await _courseEpisodeService.CreateAsync(model);

			switch (result)
			{
				case CreateCourseEpisodeResult.Success:
					TempData[SuccessMessage] = SuccessMessages.CreateCourseEpisodeSuccessfullyDone;
					return RedirectToAction(nameof(Index), new{ CourseId = model.CourseId});

			}
			return View(model);
		}

		#endregion

		#region Update

		// GET: Admin/CourseEpisode/Edit
		[CheckPermission("EditEpisode")]
		public async Task<IActionResult> Edit(int id)
		{

			var courseEpisode = await _courseEpisodeService.GetForEditAsync(id);
			if (courseEpisode == null)
				return NotFound();

			return View(courseEpisode);
		}

		// POST: Admin/CourseEpisode/Edit
		[HttpPost, CheckPermission("EditEpisode"), ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(UpdateCourseEpisodeViewModel model)
		{
			#region Validations

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			#endregion

			var result = await _courseEpisodeService.UpdateAsync(model);

			switch (result)
			{
				case UpdateCourseEpisodeResult.Success:
					TempData[SuccessMessage] = SuccessMessages.UpdateCourseEpisodeSuccessfullyDone;
					return RedirectToAction(nameof(Index), new { CourseId = model.CourseId });

				case UpdateCourseEpisodeResult.NotFound:
					TempData[SuccessMessage] = ErrorMessages.CourseEpisodeNotFound;
					return RedirectToAction(nameof(Index), new { CourseId = model.CourseId });

			}
			return View(model);
		}

		#endregion

		#region Delete
		// GET: Admin/CourseEpisode/Delete/5
		[CheckPermission("DeleteEpisode")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await _courseEpisodeService.DeleteAsync(id);
			switch (result)
			{
				case DeleteCourseEpisodeResult.Success:
					TempData[SuccessMessage] = SuccessMessages.DeleteCourseEpisodeSuccessfullyDone;
					return RedirectToAction(nameof(Index));

				case DeleteCourseEpisodeResult.NotFound:
					TempData[ErrorMessage] = ErrorMessages.CourseEpisodeNotFound;
					break;
			}

			return RedirectToAction(nameof(Index), new { CourseId = id });
		}
		#endregion
	}
}
