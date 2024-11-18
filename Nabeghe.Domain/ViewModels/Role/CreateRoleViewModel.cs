using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Domain.ViewModels.Role
{
	public class CreateRoleViewModel
	{
		[Display(Name = "عنوان نقش")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
		[MaxLength(400, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
		public string RoleTitle { get; set; }

		public List<int>? selectedPermissions { get; set; }
	}

	public enum CreateRoleResult
	{
		Success,
		NotSelectedPermission
	}
}
