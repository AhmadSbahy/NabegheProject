using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Domain.ViewModels.User
{
	public class EditUserProfileViewModel
	{
		#region Properties

		public int? UserId { get; set; }
		public string? PhoneNumber { get; set; }
		public string? currentPassword { get; set; }

		[Display(Name = "نام")]
		[MaxLength(150, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
		public string? FirstName { get; set; }

		[Display(Name = "نام خانوادگی")]
		[MaxLength(150, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
		public string? LastName { get; set; }

		[Display(Name = "ایمیل")]
		[MaxLength(350, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
		[EmailAddress(ErrorMessage = "فرمت وارد شده صحیح نمی باشد.")]
		public string? Email { get; set; }

		[Display(Name = "شماره تماس")]
		[MaxLength(350, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
		[RegularExpression(@"^([0-9]{11})$", ErrorMessage = "موبایل وارد شده معتبر نمی باشد")]
		public string? Mobile { get; set; }

		[Display(Name = "کلمه عبور")]
		[MaxLength(350, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
		public string? Password { get; set; }

		public string? Avatar { get; set; }

		[Display(Name = "تصویر")]
		public IFormFile? NewAvatar { get; set; }

		#endregion
	}

	public enum EditUserProfileResult
	{
		Success,
		UserNotFound,
		CurrentPasswordNotCorrect
	}
}
