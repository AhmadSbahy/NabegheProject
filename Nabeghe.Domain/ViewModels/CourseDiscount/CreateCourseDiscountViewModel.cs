using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Domain.ViewModels.CourseDiscount
{
	public class CreateCourseDiscountViewModel
	{

		public int CourseId { get; set; }

		[Display(Name = "درصد تخفیف")]
		[Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
		public int DiscountPercent { get; set; }

		[Display(Name = "تاریخ شروع")]
		[Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
		public string StartDate { get; set; }

		[Display(Name = "تاریخ پایان")]
		[Required(ErrorMessage = "لطفا {0} را وارد كنيد")]
		public string EndDate { get; set; }

		public bool IsDeleted { get; set; }

	}

	public enum CreateCourseDiscountResult
	{
		Success
	}
}
