using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.ViewModels.Role;

namespace Nabeghe.Application.Services.Implementation
{
	public class RoleService(IRoleRepository _repository) : IRoleService
	{
		public async Task<List<Role>> GetAllAsync()
		{
			return await _repository.GetAllAsync();
		}

		public async Task<List<Permission>> GetAllPermissionsAsync()
		{
			return await _repository.GetAllPermissionsAsync();
		}

		public async Task<FilterRoleViewModel> FilterRoleAsync(FilterRoleViewModel model)
		{
			return await _repository.FilterRoleAsync(model);
		}

		public async Task<CreateRoleResult> CreateAsync(CreateRoleViewModel model)
		{
			Role role = new Role()
			{
				CreateDate = DateTime.Now,
				RoleTitle = model.RoleTitle,
			};
			if (model.selectedPermissions == null && !model.selectedPermissions.Any())
			{
				return CreateRoleResult.NotSelectedPermission;
			}

			int roleId = await _repository.InsertAsync(role);

			foreach (var item in model.selectedPermissions)
			{
				await _repository.InsertRolePermissionAsync(new RolePermission()
				{
					RoleId = roleId,
					PermissionId = item
				});
			}

			await _repository.SaveAsync();
			return CreateRoleResult.Success;
		}

		public async Task<UpdateRoleViewModel?> GetForEditAsync(int id)
		{
			var role = await _repository.GetByIdAsync(id);

			if (role == null)
			{
				return null;
			}

			return new UpdateRoleViewModel()
			{
				RoleTitle = role.RoleTitle,
				Id = role.Id,
				CreateDate = role.CreateDate
			};
		}

		public async Task<UpdateRoleResult> UpdateAsync(UpdateRoleViewModel model)
		{
			var role = await _repository.GetByIdAsync(model.Id);

			if (role == null) return UpdateRoleResult.RoleNotFound;

			if (model.selectedPermissions == null || !model.selectedPermissions.Any())
				return UpdateRoleResult.NotSelectedPermission;

			// ویرایش مقادیر موجودیت قدیمی به جای ساختن شیء جدید
			role.RoleTitle = model.RoleTitle;

            _repository.Update(role);
            _repository.DeleteRolePermission(role.Id);

			foreach (var item in model.selectedPermissions)
			{
				await _repository.InsertRolePermissionAsync(new RolePermission()
				{
					RoleId = role.Id,
					PermissionId = item
				});
			}

			
			await _repository.SaveAsync();
			return UpdateRoleResult.Success;
		}

		public async Task<DeleteRoleResult> DeleteAsync(int id)
		{
			var role = await _repository.GetByIdAsync(id);

			if (role == null) return DeleteRoleResult.NotFound;

			_repository.DeleteAsync(role);
			await _repository.SaveAsync();

			return DeleteRoleResult.Success;
		}

		public async Task<List<int>> GetSelectedRolePermissionAsync(int roleId)
		{
			return await _repository.GetSelectedRolePermissionAsync(roleId);
		}

		
	}
}
