using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Domain.ViewModels.NewsLetter
{
	public class SendEmailForNewsLetterViewModel
	{
		[Display(Name = "عنوان")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(350, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
		public string Subject { get; set; }

		[Display(Name = "متن")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public string Message { get; set; }
		
		[Display(Name = "html هست")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public bool IsHtml { get; set; }
	}
}
