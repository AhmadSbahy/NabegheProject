using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nabeghe.Domain.Models.Course
{
    public class CommentLike
    {
        #region Properties
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CommentId { get; set; }
        #endregion

        #region Relations
        [ForeignKey(nameof(UserId))]
        public User.User User { get; set; }

        [ForeignKey(nameof(CommentId))]
        public CourseComment Comment { get; set; }
        #endregion

    }
}
