using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Domain.ViewModels.CourseComment
{
    public class AddCommentViewModel
    {
        public int courseId { get; set; }
        public string commentText { get; set; }
    }
}
