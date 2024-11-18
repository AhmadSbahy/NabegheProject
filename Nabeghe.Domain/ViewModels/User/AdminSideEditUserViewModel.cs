using Microsoft.AspNetCore.Http;
using Nabeghe.Domain.Enums.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Domain.ViewModels.User
{
	public class AdminSideEditUserViewModel
	{
		public int Id { get; set; }
		[Display(Name = "نام")]
		[MaxLength(150, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
		public string? FirstName { get; set; }

		[Display(Name = "نام خانوادگی")]
		[MaxLength(150, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
		public string? LastName { get; set; }

		[Display(Name = "موبایل")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(150, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
		[RegularExpression(@"^([0-9]{11})$", ErrorMessage = "موبایل وارد شده معتبر نمی باشد")] 
		public string Mobile { get; set; }

		[MaxLength(350, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]

		public string? Email { get; set; }

		public UserStatus Status { get; set; }

		[Display(Name = "نقش ها")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public List<int> RoleIds { get; set; }

		[Display(Name = "کلمه عبور")]
		[MaxLength(400, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
		public string? Password { get; set; }

		public string? Avatar { get; set; }

		[Display(Name = "تصویر")]
		public IFormFile? NewAvatar { get; set; }
	}
	public enum AdminSideEditUserResult
	{
		Success,
		MobileDuplicated,
		NotSelectedRole,
		UserNotFound
	}
}
