using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Domain.ViewModels.CourseComment
{
    public class AddBlogCommentViewModel
	{
        public int BlogId { get; set; }
        public string CommentText { get; set; }
    }
}
