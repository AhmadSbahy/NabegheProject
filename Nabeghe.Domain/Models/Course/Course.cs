using Nabeghe.Domain.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nabeghe.Domain.Models.Course
{
    public class Course : BaseEntity<int>
	{
		#region Properties
		[Display(Name = "استاد")]
		[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
		public int TeacherId { get; set; }

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
		public string CourseImageName { get; set; }

		[Display(Name = "حذف شده")]
		public bool IsDeleted { get; set; } = false;

		#endregion

		#region Relations

		[ForeignKey(nameof(StatusId))]
		public CourseStatus? CourseStatus { get; set; }

		public ICollection<CourseEpisode>? CourseEpisodes { get; set; }

		[ForeignKey(nameof(CategoryId))]
		public CourseCategory.CourseCategory? CourseCategory { get; set; }

		[ForeignKey(nameof(TeacherId))]
		public User.User? User { get; set; }

		public ICollection<Order.OrderDetail>? OrderDetails { get; set; }

		public ICollection<CourseComment>? CourseComments { get; set; }

        public ICollection<CourseLike>? CourseLikes { get; set; }

        public CourseDiscount? CourseDiscount { get; set; }

        #endregion
    }
}
