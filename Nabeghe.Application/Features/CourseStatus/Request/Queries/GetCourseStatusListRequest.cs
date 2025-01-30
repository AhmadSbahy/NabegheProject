using MediatR;
using Nabeghe.Application.DTOs.CourseStatus;
using Nabeghe.Domain.ViewModels.Course;

namespace Nabeghe.Application.Features.CourseStatus.Request.Queries
{
	public class GetCourseStatusListRequest : IRequest<CourseStatusFilterDto>
	{
		public CourseStatusFilterDto CourseStatusFilterDto { get; set; }
	}
}
	