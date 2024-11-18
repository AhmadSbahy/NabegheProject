using Nabeghe.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.ViewModels.Common;

namespace Nabeghe.Domain.ViewModels.Course
{
	public class AdminSideFilterCourseViewModel : BasePaging<CourseViewModel>
	{
		[Display(Name = "جستجو")]
		public string Param { get; set; }
		
		[Display(Name = "قیمت")]
		public int Price { get; set; }

		[Display(Name = "وضعیت")]
		public FilterCourseStatus Status { get; set; }
	}
	public enum FilterCourseStatus
	{
		[Display(Name = "همه")]
		All,

		[Display(Name = "حذف نشده ها")]
		NotDeleted,

		[Display(Name = "حذف شده ها")]
		Deleted
	}
}
