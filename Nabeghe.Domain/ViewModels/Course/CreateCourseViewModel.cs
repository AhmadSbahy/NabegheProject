using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Nabeghe.Domain.ViewModels.Course
{
	public class CreateCourseViewModel
	{
		[Display(Name = "استاد")]
		public int? TeacherId { get; set; }

		[Display(Name = "دسته بندی")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public int CategoryId { get; set; }

		[Display(Name = "وضعیت دوره")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public int StatusId { get; set; }

        [Display(Name = "Slug")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Slug { get; set; }

        [Display(Name = "عنوان دوره")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
		public string CourseTitle { get; set; }

		[Display(Name = "شرح عنوان دوره")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
		public string CourseSubTitle { get; set; }

		[Display(Name = "شرح دوره")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public string CourseDescription { get; set; }

		[Display(Name = "قیمت دوره")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public int CoursePrice { get; set; }

		[Display(Name = "عکس دوره")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public IFormFile CourseImage { get; set; }

		[Display(Name = "حذف شده")]
		public bool IsDeleted { get; set; }
	}

	public enum CreateCourseResult
	{
		Success
	}
}
