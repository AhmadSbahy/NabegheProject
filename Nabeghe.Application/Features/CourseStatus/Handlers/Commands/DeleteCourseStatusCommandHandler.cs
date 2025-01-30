using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using MediatR;
using Nabeghe.Application.Exceptions;
using Nabeghe.Application.Features.CourseStatus.Request.Commands;
using Nabeghe.Application.Response;
using Nabeghe.Domain.Interfaces;

namespace Nabeghe.Application.Features.CourseStatus.Handlers.Commands
{
	public class DeleteCourseStatusCommandHandler : IRequestHandler<DeleteCourseStatusCommand>
	{
		private readonly ICourseStatusRepository _courseStatusRepository;

		public DeleteCourseStatusCommandHandler(ICourseStatusRepository courseStatusRepository)
		{
			_courseStatusRepository = courseStatusRepository;
		}

		public async Task Handle(DeleteCourseStatusCommand request, CancellationToken cancellationToken)
		{
			var courseStatus = await _courseStatusRepository.GetAsync(request.Id);
			if (courseStatus == null)
			{
				throw new NotFoundException("وضعیت دوره پیدا نشد", typeof(Domain.Models.Course.CourseStatus));
			}
			await _courseStatusRepository.DeleteAsync(courseStatus);
		}
	}
}
