using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.ViewModels.Common;

namespace Nabeghe.Application.DTOs.CourseStatus
{
	public class CourseStatusFilterDto : BasePaging<CourseStatusDto>
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
