using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Domain.ViewModels.User
{
    public class UserFavoriteCourseViewModel
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string CourseImageName { get; set; }
        public string CourseTitle { get; set; }
        public string TeacherFullName { get; set; }
        public string Status { get; set; }
        public int Price { get; set; }
        public string TeacherImage { get; set; }

    }
}
