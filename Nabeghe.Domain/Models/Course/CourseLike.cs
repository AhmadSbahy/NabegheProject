using System.ComponentModel.DataAnnotations.Schema;

namespace Nabeghe.Domain.Models.Course
{
    public class CourseLike
    {
        #region Properties

        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }

        #endregion

        #region Relations

        [ForeignKey(nameof(UserId))]
        public User.User User { get; set; }
        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }

        #endregion
    }
}
