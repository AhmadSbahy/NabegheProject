using Nabeghe.Domain.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nabeghe.Domain.Models.User
{
	public class UserRole:BaseEntity<int>
    {
		#region Properties

		public int RoleId { get; set; }
		public int UserId { get; set; }

		#endregion

		#region Relations

		[ForeignKey("RoleId")]
		public Role? Role { get; set; }
		[ForeignKey("UserId")]
		public User? User { get; set; }

		#endregion
	}
}
