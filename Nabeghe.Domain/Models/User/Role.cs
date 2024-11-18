using Nabeghe.Domain.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Nabeghe.Domain.Models.User
{
	public class Role : BaseEntity<int>
	{
		#region Properties

		[Display(Name = "عنوان نقش")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
		[MaxLength(400, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
		public string RoleTitle { get; set; }

		#endregion

		#region Relations

		public List<UserRole>? UserRoles { get; set; }

		#endregion
	}
}
