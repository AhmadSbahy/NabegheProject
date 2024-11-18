using System.ComponentModel.DataAnnotations;
using Nabeghe.Domain.ViewModels.Common;

namespace Nabeghe.Domain.ViewModels.CourseDiscount;

public class FilterCourseDiscountViewModel : BasePaging<CourseDiscountViewModel>
{
	[Display(Name = "جستجو")]
	public string Param { get; set; }

	[Display(Name = "وضعیت")] 
	public FilterCourseDiscountStatus FilterCourseDiscountStatus { get; set; }
}