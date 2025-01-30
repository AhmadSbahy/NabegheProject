using MediatR;
using Nabeghe.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Application.DTOs.CourseStatus;

namespace Nabeghe.Application.Features.CourseStatus.Request.Commands
{
	public class CreateCourseStatusCommand : IRequest<BaseCommandResponse>
	{
		public CreateCourseStatusDto CreateCourseStatusDto { get; set; }
	}
}
