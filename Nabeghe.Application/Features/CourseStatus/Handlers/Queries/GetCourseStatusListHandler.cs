using AutoMapper;
using MediatR;
using Nabeghe.Application.DTOs.CourseStatus;
using Nabeghe.Application.Features.CourseStatus.Request.Queries;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Domain.ViewModels.CourseStatus;

namespace Nabeghe.Application.Features.CourseStatus.Handlers.Queries
{
	public class GetCourseStatusListHandler : IRequestHandler<GetCourseStatusListRequest, CourseStatusFilterDto>
	{
		private readonly ICourseStatusRepository _courseStatusRepository;
		private readonly IMapper _mapper;

		public GetCourseStatusListHandler(ICourseStatusRepository courseStatusRepository, IMapper mapper)
		{
			_courseStatusRepository = courseStatusRepository;
			_mapper = mapper;
		}

		public async Task<CourseStatusFilterDto> Handle(GetCourseStatusListRequest request, CancellationToken cancellationToken)
		{
			var courseStatuses = await _courseStatusRepository.GetAllAsync(_mapper.Map<CourseStatusFilterViewModel>(request.CourseStatusFilterDto));
			return _mapper.Map<CourseStatusFilterDto>(courseStatuses);
		}
	}
}
