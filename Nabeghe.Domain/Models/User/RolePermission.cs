using System.ComponentModel.DataAnnotations.Schema;

namespace Nabeghe.Domain.Models.User
{
	public class RolePermission
	{
		#region Properties

		public int RolePermissionId { get; set; }
		public int RoleId { get; set; }
		public int PermissionId { get; set; }

		#endregion

		#region Relations

		[ForeignKey("PermissionId")]
		public Permission? Permission { get; set; }
		[ForeignKey("RoleId")]
		public Role? Role { get; set; }

		#endregion
	}
}
