using Nabeghe.Domain.Models.User;
using Nabeghe.Domain.ViewModels.Role;

namespace Nabeghe.Application.Services.Interfaces
{
	public interface IRoleService
	{
		Task<List<Role>> GetAllAsync();
		Task<List<Permission>> GetAllPermissionsAsync();
		Task<FilterRoleViewModel> FilterRoleAsync(FilterRoleViewModel model);
		Task<CreateRoleResult> CreateAsync(CreateRoleViewModel model);
		Task<UpdateRoleViewModel?> GetForEditAsync(int id);
		Task<UpdateRoleResult> UpdateAsync(UpdateRoleViewModel model); 
		Task<DeleteRoleResult> DeleteAsync(int id);
		Task<List<int>> GetSelectedRolePermissionAsync(int roleId);
	}
}
