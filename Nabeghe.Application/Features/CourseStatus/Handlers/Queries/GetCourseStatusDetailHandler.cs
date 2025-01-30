using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Nabeghe.Application.DTOs.CourseStatus;
using Nabeghe.Application.Features.CourseStatus.Request.Queries;
using Nabeghe.Domain.Interfaces;

namespace Nabeghe.Application.Features.CourseStatus.Handlers.Queries
{
	public class GetCourseStatusDetailHandler : IRequestHandler<GetCourseStatusDetailRequest,CourseStatusDto>
	{
		private readonly ICourseStatusRepository _courseStatusRepository;
		private readonly IMapper _mapper;

		public GetCourseStatusDetailHandler(ICourseStatusRepository courseStatusRepository, IMapper mapper)
		{
			_courseStatusRepository = courseStatusRepository;
			_mapper = mapper;
		}

		public async Task<CourseStatusDto> Handle(GetCourseStatusDetailRequest request, CancellationToken cancellationToken)
		{
			var courseStatus = await _courseStatusRepository.GetAsync(request.Id);
			return _mapper.Map<CourseStatusDto>(courseStatus);
		}
	}
}
