using MediatR;
using Nabeghe.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Application.Features.CourseStatus.Request.Commands
{
	public class DeleteCourseStatusCommand : IRequest
	{
		public int Id { get; set; }
	}
}
