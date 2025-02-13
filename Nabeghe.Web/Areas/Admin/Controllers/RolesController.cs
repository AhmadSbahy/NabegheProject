﻿using Microsoft.AspNetCore.Mvc;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.Shared;
using Nabeghe.Domain.ViewModels.Role;
using Nabeghe.Web.Filters;
using Nabeghe.Web.Utilities;

namespace Nabeghe.Web.Areas.Admin.Controllers
{
	public class RolesController(IRoleService roleService) : AdminBaseController
	{
		#region List
		[CheckPermission("ManageRole")]
		public async Task<IActionResult> Index(FilterRoleViewModel filter)
		{
			var model = await roleService.FilterRoleAsync(filter);
			return View(model);
		}
		#endregion

		#region Create
		[CheckPermission("AddRole")]
		public async Task<IActionResult> Create()
		{
			ViewData["Permissions"] = await roleService.GetAllPermissionsAsync();
			return View();
		}

		// POST: Admin/Roles/Create
		[HttpPost, ValidateModel, CheckPermission("AddRole"), ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateRoleViewModel model)
		{
			var result = await roleService.CreateAsync(model);
			switch (result)
			{
				case CreateRoleResult.Success:
					TempData[SuccessMessage] = SuccessMessages.CreateRoleSuccessfullyDone;
					return RedirectToAction(nameof(Index));

				case CreateRoleResult.NotSelectedPermission:
					TempData[ErrorMessage] = ErrorMessages.NotSelectedPermission;
					break;
			}

			return View();
		}
		#endregion

		#region Update
		[CheckPermission("EditRole")]
		public async Task<IActionResult> Edit(int id)
		{
			var role = await roleService.GetForEditAsync(id);

			if (role == null)
				return NotFound();

			ViewData["Permissions"] = await roleService.GetAllPermissionsAsync();
			ViewData["SelectedPermissions"] = await roleService.GetSelectedRolePermissionAsync(id);
			return View(role);
		}

		// POST: Admin/Roles/Edit/5
		[HttpPost, ValidateModel, CheckPermission("EditRole"), ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(UpdateRoleViewModel model)
		{
			var result = await roleService.UpdateAsync(model);
			switch (result)
			{
				case UpdateRoleResult.Success:
					TempData[SuccessMessage] = SuccessMessages.EditRoleSuccessfullyDone;
					return RedirectToAction(nameof(Index));

				case UpdateRoleResult.NotSelectedPermission:
					TempData[ErrorMessage] = ErrorMessages.NotSelectedPermission;
					break;

				case UpdateRoleResult.RoleNotFound:
					TempData[ErrorMessage] = ErrorMessages.RoleNotFound;
					break;
			}

			return View();
		}
		#endregion

		#region Delete
		// GET: Admin/Roles/Delete/5
		[CheckPermission("DeleteRole")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await roleService.DeleteAsync(id);
			switch (result)
			{
				case DeleteRoleResult.Success:
					TempData[SuccessMessage] = SuccessMessages.DeleteRoleSuccessfullyDone;
					return RedirectToAction(nameof(Index));

				case DeleteRoleResult.NotFound:
					TempData[ErrorMessage] = ErrorMessages.RoleNotFound;
					break;
			}

			return RedirectToAction(nameof(Index));
		}
		#endregion
	}
}
