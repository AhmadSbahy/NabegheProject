using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Domain.ViewModels.CourseCategory
{
	public class CreateCourseCategoryViewModel
	{
		[Display(Name = "عنوان")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(150, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
		public string Title { get; set; }

		[Display(Name = "دسته بندی والد")]
		public int? ParentId { get; set; }
	}

	public enum CreateCourseCategoryResult
	{
		Success
	}
}
