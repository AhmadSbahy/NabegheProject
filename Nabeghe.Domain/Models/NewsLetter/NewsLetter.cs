using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.Models.Common;

namespace Nabeghe.Domain.Models.NewsLetter
{
	public class NewsLetter : BaseEntity<int>
	{
		[Display(Name = "ایمیل")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(350, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
		public string Email { get; set; }

		[MaxLength(350)]
		public string Ip { get; set; }
	}
}
