using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nabeghe.Domain.Models.User
{
	public class Permission
	{
		#region Properties

		[Key]
		public int PermissionId { get; set; }
		[MaxLength(150)]
		public string PermissionName { get; set; }
		[MaxLength(150)]
		public string PermissionTitle { get; set; }
		public int? ParentId { get; set; }

		#endregion

		#region Relations

		[ForeignKey(nameof(ParentId))]
		public Permission? CheckPermission { get; set; }
		public ICollection<Permission>? Permissions { get; set; }
		public ICollection<RolePermission>? RolePermissions { get; set; }

		#endregion
	}
}
