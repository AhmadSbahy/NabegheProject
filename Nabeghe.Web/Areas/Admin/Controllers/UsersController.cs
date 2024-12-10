using Microsoft.AspNetCore.Mvc;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.Shared;
using Nabeghe.Domain.ViewModels.User;
using Nabeghe.Web.Utilities;

namespace Nabeghe.Web.Areas.Admin.Controllers
{
	public class UsersController(IRoleService _roleService, IUserService _userService) : AdminBaseController
    {
		[CheckPermission("ManageUser")]
        public async Task<IActionResult> Index(FilterUserViewModel filter)
        {
	        var model = await _userService.FilterUserAsync(filter);
			return View(model);
		}

		#region Create

		[CheckPermission("AddUser")]
		public async Task<IActionResult> Create()
        {
	        ViewData["Roles"] = await _roleService.GetAllAsync();
	        return View();
        }

        // POST: Admin/Users/Create
        [HttpPost,CheckPermission("AddUser"),ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminSideCreateUserViewModel model)
		{
			#region Validations

			if (!ModelState.IsValid)
			{
				ViewData["Roles"] = await _roleService.GetAllAsync(); 
				return View(model);
			}

			#endregion
			var result = await _userService.AdminSideCreateAsync(model);

			switch (result)
			{
				case AdminSideCreateUserResult.Success:
					TempData[SuccessMessage] = SuccessMessages.CreateUserSuccessfullyDone;
					return RedirectToAction(nameof(Index));
					break;

				case AdminSideCreateUserResult.MobileDuplicated:
					TempData[ErrorMessage] = ErrorMessages.DuplicatedMobile;
					break;

				case AdminSideCreateUserResult.NotSelectedRole:
					TempData[ErrorMessage] = ErrorMessages.NotSelectedRole;
					break;
			}

			ViewData["Roles"] = await _roleService.GetAllAsync();
			return View(model);
		}

		#endregion

		#region Update

		[CheckPermission("EditUser")]
		public async Task<IActionResult> Edit(int id)
		{
			var user = await _userService.AdminSideGetForEditAsync(id);
			
			if (user == null)
				return NotFound();

			ViewData["Roles"] = await _roleService.GetAllAsync();
			return View(user);
		}

		// POST: Admin/Users/Edit/5
		[HttpPost,CheckPermission("EditUser"), ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(AdminSideEditUserViewModel model)
		{
			#region Validations

			if (!ModelState.IsValid)
			{
				ViewData["Roles"] = await _roleService.GetAllAsync();
				return View();
			}

			#endregion

			var result = await _userService.AdminSideUpdateAsync(model);


			switch (result)
			{
				case AdminSideEditUserResult.Success:
					TempData[SuccessMessage] = SuccessMessages.EditUserSuccessfullyDone;
					return RedirectToAction(nameof(Index));
					break;

				case AdminSideEditUserResult.MobileDuplicated:
					TempData[ErrorMessage] = ErrorMessages.DuplicatedMobile;
					break;

				case AdminSideEditUserResult.NotSelectedRole:
					TempData[ErrorMessage] = ErrorMessages.NotSelectedRole;
					break;

				case AdminSideEditUserResult.UserNotFound:
					TempData[ErrorMessage] = ErrorMessages.UserNotFound;
					break;
			}

			ViewData["Roles"] = await _roleService.GetAllAsync();
			return View(model);
		}



		#endregion

		#region Delete
		// GET: Admin/Users/Delete/5
		[CheckPermission("DeleteUser")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await _userService.DeleteAsync(id);
			switch (result)
			{
				case DeleteUserResult.Success:
					TempData[SuccessMessage] = SuccessMessages.DeleteUserSuccessfullyDone;
					return RedirectToAction(nameof(Index));

				case DeleteUserResult.UserNotFound:
					TempData[ErrorMessage] = ErrorMessages.UserNotFound;
					break;
			}

			return RedirectToAction(nameof(Index));
		}
		#endregion
    }
}
