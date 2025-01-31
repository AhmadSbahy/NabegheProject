using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Nabeghe.Application.DTOs.CourseStatus.Validators;
using Nabeghe.Application.Features.CourseStatus.Request.Commands;
using Nabeghe.Application.Response;
using Nabeghe.Domain.Interfaces;

namespace Nabeghe.Application.Features.CourseStatus.Handlers.Commands
{
	public class CreateCourseStatusCommandHandler : IRequestHandler<CreateCourseStatusCommand, BaseCommandResponse>
	{
		private readonly ICourseStatusRepository _courseStatusRepository;
		private readonly IMapper _mapper;

		public CreateCourseStatusCommandHandler(ICourseStatusRepository courseStatusRepository, IMapper mapper)
		{
			_courseStatusRepository = courseStatusRepository;
			_mapper = mapper;
		}

		public async Task<BaseCommandResponse> Handle(CreateCourseStatusCommand request, CancellationToken cancellationToken)
		{
			var response = new BaseCommandResponse();
			var validator = new CreateCourseStatusDtoValidator();
			var validate = await validator.ValidateAsync(request.CreateCourseStatusDto, cancellationToken);
			if (validate.IsValid)
			{
				var courseStatus = _mapper.Map<Domain.Models.Course.CourseStatus>(request.CreateCourseStatusDto);
				courseStatus = await _courseStatusRepository.AddAsync(courseStatus);
				
				response.Id = courseStatus.Id;
				response.IsSuccess = true;
				response.Message = "وضعیت دوره با موفقیت ساخته شد";
			}
			else
			{
				response.ErroresList = validate.Errors.Select(e => e.ErrorMessage).ToList();
				response.IsSuccess = false;
				response.Message = validate.Errors.First().ToString();
			}
			return response;
		}
	}
}
