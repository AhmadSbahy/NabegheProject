using Nabeghe.Domain.ViewModels.Common;
using System.ComponentModel.DataAnnotations;

namespace Nabeghe.Web.Models.CourseStatus
{
	public class FilterCourseStatusViewModel : BasePaging<CourseStatusViewModel>
	{
		[Display(Name = "جستجو")]
		public string Param { get; set; }

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
