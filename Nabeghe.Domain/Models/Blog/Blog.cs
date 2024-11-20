using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.Enums.Course;
using Nabeghe.Domain.Models.Common;
using Nabeghe.Domain.Models.Course;

namespace Nabeghe.Domain.Models.Blog
{
    public class Blog : BaseEntity<int>
    {
        #region Properties

        public int AuthorId { get; set; }

        [Display(Name = "عکس مقاله")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
        public string BlogImage { get; set; }

        [Display(Name = "عنوان مقاله")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(600, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
        public string BlogTitle { get; set; }

        [Display(Name = "عنوان مقاله")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string BlogDescription { get; set; }

        #endregion

        #region Relations

        [ForeignKey(nameof(AuthorId))]
        public User.User? User { get; set; }

        public ICollection<BlogComment>? BlogComments { get; set; }
        public ICollection<BlogLike>? BlogLikes { get; set; }

        #endregion
    }
    public class BlogLike
    {
	    #region Properties

	    public int Id { get; set; }
	    public int UserId { get; set; }
	    public int BlogId { get; set; }

	    #endregion

	    #region Relations

	    [ForeignKey(nameof(UserId))]
	    public User.User User { get; set; }

	    [ForeignKey(nameof(BlogId))]
	    public Blog Blog { get; set; }

	    #endregion
    }
}
