using Nabeghe.Domain.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nabeghe.Domain.Models.Course
{
	public class CourseEpisode : BaseEntity<int>
	{
		#region Properties

		[Display(Name = "دوره")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public int CourseId { get; set; }

		[Display(Name = "عنوان بخش")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		[MaxLength(400, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
		public string EpisodeTitle { get; set; }

		[Display(Name = "زمان")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public TimeSpan EpisodeTime { get; set; }

		[Display(Name = "نام ویدیو")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public string EpisodeFileName { get; set; }

		[Display(Name = "حذف شده")]
		public bool IsDeleted { get; set; } = false;

		#endregion

		#region Relations

		[ForeignKey(nameof(CourseId))]
		public Course? Course { get; set; }

		#endregion
	}
}
