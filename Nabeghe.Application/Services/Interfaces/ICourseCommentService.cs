using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.ViewModels.CourseComment;

namespace Nabeghe.Application.Services.Interfaces
{
    public interface ICourseCommentService
    {
	    Task<FilterCourseCommentViewModel> FilterCourseCommentAsync(FilterCourseCommentViewModel filter);
	    Task<(bool Success, string Message, int StatusCode)> ConfirmComment(int commentId);
		Task<object> RejectComment(int commentId);
    }
}
