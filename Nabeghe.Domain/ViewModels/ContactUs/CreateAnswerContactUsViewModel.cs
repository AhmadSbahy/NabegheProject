using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Domain.ViewModels.ContactUs
{
	public class CreateAnswerContactUsViewModel
	{
		public int Id { get; set; }

		[Display(Name = "پاسخ")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(800, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
		public string Answer { get; set; }
	}
}
