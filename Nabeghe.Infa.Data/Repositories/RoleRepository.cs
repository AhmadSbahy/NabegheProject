using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Domain.Models.User;
using Nabeghe.Domain.ViewModels.Role;
using Nabeghe.Infra.Data.Context;

namespace Nabeghe.Infra.Data.Repositories
{
	public class RoleRepository(NabegheContext _context) : IRoleRepository
	{
		public async Task<int> InsertAsync(Role role)
		{
			await _context.AddAsync(role);
			await SaveAsync();
			return role.Id;
		}

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}

		public void Update(Role role)
			=> _context.Roles.Update(role);

		public async Task<Role?> GetByIdAsync(int id)
			=> await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);

		public async Task<List<Role>> GetAllAsync()
			=> await _context.Roles.ToListAsync();

		public async Task<List<Permission>> GetAllPermissionsAsync()
			=> await _context.Permissions.ToListAsync();

		public async Task<List<int>> GetSelectedRolePermissionAsync(int roleId)
		{
			return await _context.RolePermissions
				.Where(r => r.RoleId == roleId)
				.Select(r => r.PermissionId)
				.ToListAsync();
		}

		public async Task<FilterRoleViewModel> FilterRoleAsync(FilterRoleViewModel model)
		{
			var query = _context.Roles.AsQueryable();

			#region Filter

			if (!string.IsNullOrEmpty(model.Param))
			{
				query = query.Where(r => r.RoleTitle.Contains(model.Param));
			}

			#endregion

			query = query.OrderByDescending(r => r.CreateDate);

			await model.Paging(query.Select(r => new RoleViewModel()
			{
				Id = r.Id,
				RoleTitle = r.RoleTitle ,
				CreateDate = r.CreateDate
			}));

			return model;
		}

		public void DeleteAsync(Role role)
		=> _context.Roles.Remove(role);

		public async Task InsertRolePermissionAsync(RolePermission rolePermission)
		{
			await _context.AddAsync(rolePermission);
		}

		public async void DeleteRolePermission(int roleId)
		{
			var rolePermission =  _context.RolePermissions
                .Where(r => r.RoleId == roleId)
				.ToList();
            foreach (var role in rolePermission)
            {
                _context.RolePermissions.Remove(role);
            }
        }

		public int GetPermissionIdByName(string permissionName)
		{
			return _context.Permissions.First(p => p.PermissionName == permissionName).PermissionId;
		}

		public async Task DisposeAsync()
		{
			await _context.DisposeAsync();
		}
		public List<Role> GetRolesInPermission(string permission)
		{
			int permissionId = GetPermissionIdByName(permission);
			return _context.RolePermissions.Include(p => p.Role)
				.Where(r => r.PermissionId == permissionId)
				.Select(r => r.Role).ToList();
		}

		public List<Role> GetRolesUser(int userId)
		{
			return _context.UserRoles.Include(r => r.Role)
				.Where(r => r.UserId == userId)
				.Select(r => r.Role).ToList();
		}
	}
}
