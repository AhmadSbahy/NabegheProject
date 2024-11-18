using Nabeghe.Domain.ViewModels.Common;
using Nabeghe.Domain.ViewModels.Course;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Domain.ViewModels.CourseEpisode
{
	public class FilterCourseEpisodeViewModel : BasePaging<CourseEpisodeViewModel>
	{
		public int CourseId { get; set; }

		[Display(Name = "جستجو")] 
		public string Param { get; set; }

		[Display(Name = "وضعیت")] 
		public FilterCourseEpisodeStatus Status { get; set; }
	}

	public enum FilterCourseEpisodeStatus
	{
		[Display(Name = "همه")]
		All,

		[Display(Name = "حذف نشده ها")]
		NotDeleted,

		[Display(Name = "حذف شده ها")]
		Deleted
	}

}
