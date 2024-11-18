using System.ComponentModel.DataAnnotations;

namespace Nabeghe.Domain.ViewModels.CourseDiscount;

public enum FilterCourseDiscountStatus
{
	[Display(Name = "همه")]
	All,
	[Display(Name = "به اتمام رسیده")]
	Finished,
	[Display(Name = "شروع نشده")]
	NotStarted,
	[Display(Name = "بیشترین تخفیف")]
	BiggestDiscount,
	[Display(Name = "کمترین تخفیف")]
	LowestDiscount
}