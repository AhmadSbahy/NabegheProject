using MediatR;
using Nabeghe.Application.DTOs.CourseStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Application.Features.CourseStatus.Request.Queries
{
	public class GetCourseStatusDetailRequest : IRequest<CourseStatusDto>
	{
		public int Id { get; set; }
	}
}
