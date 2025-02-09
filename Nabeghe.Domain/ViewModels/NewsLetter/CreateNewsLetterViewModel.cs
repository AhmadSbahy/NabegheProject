using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Domain.ViewModels.NewsLetter
{
	public class CreateNewsLetterViewModel
	{
		[Display(Name = "ایمیل")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(350, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
		public string Email { get; set; }

		[MaxLength(350)]
		public string? Ip { get; set; }

		public DateTime CreateDate { get; set; }
	}

	public enum CreateNewsLetterStatus
	{
		Success,
		Duplicated
	}
}
