using Nabeghe.Domain.Models.User;
using Nabeghe.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.ViewModels.Role;

namespace Nabeghe.Domain.Interfaces
{
	public interface IRoleRepository
	{
		Task<int> InsertAsync(Role role);
		Task SaveAsync();
		void Update(Role role);
		Task<Role?> GetByIdAsync(int id);
		Task<List<Role>> GetAllAsync();
		Task<List<Permission>> GetAllPermissionsAsync();
		Task<List<int>> GetSelectedRolePermissionAsync(int roleId);
		Task<FilterRoleViewModel> FilterRoleAsync(FilterRoleViewModel model);
		void DeleteAsync(Role role);
		Task InsertRolePermissionAsync(RolePermission rolePermission);
		void DeleteRolePermission(int roleId);
		Task DisposeAsync();
	}
}
