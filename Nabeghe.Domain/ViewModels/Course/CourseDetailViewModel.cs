using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.Models.Course;

namespace Nabeghe.Domain.ViewModels.Course
{
    public class CourseDetailViewModel
    {
        public int Id { get; set; }

        public string CourseTitle { get; set; }

        public string CourseSubTitle { get; set; }

        public string CourseDescription { get; set; }

        public int TeacherId { get; set; }

        public int CategoryId { get; set; }

        public int StatusId { get; set; }

        public string Slug { get; set; }

        public int CoursePrice { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreateDate { get; set; }

        public string ImageName { get; set; }

        public TimeSpan totalCourseTime { get; set; } = TimeSpan.Zero;

        [ForeignKey(nameof(StatusId))]
        public Models.Course.CourseStatus? CourseStatus { get; set; }

        public ICollection<Models.Course.CourseEpisode>? CourseEpisodes { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Models.CourseCategory.CourseCategory? CourseCategory { get; set; }

        [ForeignKey(nameof(TeacherId))]
        public Models.User.User? User { get; set; }


        public ICollection<Models.Course.CourseComment>? CourseComments { get; set; }

        public ICollection<Models.Course.CourseLike>? CourseLikes { get; set; }

        public Models.Course.CourseDiscount? CourseDiscount { get; set; }
    }

}
