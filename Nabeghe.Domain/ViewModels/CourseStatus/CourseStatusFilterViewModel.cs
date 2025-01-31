using Nabeghe.Domain.ViewModels.Common;
using System.ComponentModel.DataAnnotations;

namespace Nabeghe.Domain.ViewModels.CourseStatus
{
	public class CourseStatusFilterViewModel : BasePaging<CourseStatusViewModel>
	{
		[Display(Name = "جستجو")]
		public string Param { get; set; }

		[Display(Name = "وضعیت")]
		public FilterCourseStatusOrder FilterCourseStatusOrder { get; set; }
	}
	public enum FilterCourseStatusOrder
	{
		[Display(Name = "جدید ترین")]
		Newest,
		[Display(Name = "قدیمی ترین")]
		Oldest,
	}
}
