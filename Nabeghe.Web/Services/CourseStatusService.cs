using AutoMapper;
using Nabeghe.Domain.Shared;
using Nabeghe.Web.Contracts;
using Nabeghe.Web.Models.CourseStatus;
using Nabeghe.Web.Services.Base;
using FilterCourseStatusOrder = Nabeghe.Web.Services.Base.FilterCourseStatusOrder;

namespace Nabeghe.Web.Services
{
	public class CourseStatusService : BaseHttpService, ICourseStatusService
	{
		private readonly IMapper _mapper;
		private readonly IClient _httpClient;
		private readonly ILocalStorageService _localStorage;

		public CourseStatusService(IMapper mapper, IClient httpClient, ILocalStorageService localStorage) : base(httpClient, localStorage)
		{
			_mapper = mapper;
			_httpClient = httpClient;
			_localStorage = localStorage;
		}

		public async Task<FilterCourseStatusViewModel> GetCourseStatusListAsync(FilterCourseStatusViewModel model)
		{
			var courseStatusFilterDto = _mapper.Map<CourseStatusFilterDto>(model);
			var courseStatusList = await _httpClient.CourseStatusPUTAsync(courseStatusFilterDto);
			return _mapper.Map<FilterCourseStatusViewModel>(courseStatusList);
		}

		public async Task<CourseStatusViewModel> GetCourseStatusDetailAsync(int id)
		{
			var courseStatus = await _httpClient.CourseStatusGETAsync(id);
			return _mapper.Map<CourseStatusViewModel>(courseStatus);
		}

		public async Task<Response<int>> CreateCourseStatusAsync(CreateCourseStatusViewModel model)
		{
			try
			{
				var response = new Response<int>();
				CreateCourseStatusDto courseStatusDto =
					_mapper.Map<CreateCourseStatusDto>(model);
				//Todo Auth
				var apiResponse = await _httpClient.CourseStatusPOSTAsync(courseStatusDto);

				if (apiResponse.IsSuccess)
				{
					response.Data = apiResponse.Id;
					response.IsSuccess = true;
				}
				else
				{
					foreach (var err in apiResponse.ErroresList)
					{
						response.ValidationErrors = err + Environment.NewLine;
					}
				}
				return response;
			}
			catch (ApiException e)
			{
				return ConvertApiException<int>(e);
			}
		}

		public async Task<Response<int>> UpdateCourseStatusAsync(CourseStatusViewModel model)
		{
			try
			{
				var response = new Response<int>();
				UpdateCourseStatusDto courseStatusDto =
					_mapper.Map<UpdateCourseStatusDto>(model);
				//Todo Auth
				var apiResponse = await _httpClient.CourseStatusPUT2Async(model.Id,courseStatusDto);

				if (apiResponse.IsSuccess)
				{
					response.Data = apiResponse.Id;
					response.IsSuccess = true;
				}
				else
				{
					foreach (var err in apiResponse.ErroresList)
					{
						response.ValidationErrors = err + Environment.NewLine;
					}
				}
				return response;
			}
			catch (ApiException e)
			{
				return ConvertApiException<int>(e);
			}
		}

		public async Task<Response<int>> DeleteCourseStatusAsync(int id)
		{
			try
			{ 
				await _httpClient.CourseStatusDELETEAsync(id);
				return new Response<int>(){
					IsSuccess = true,
					Message = "عملیات حذف وضعیت دوره با موفقیت انجام شد"
				};
			}
			catch (ApiException e)
			{
				return ConvertApiException<int>(e);
			}
		}

		public async Task<Response<int>> DeleteCourseStatusAsync(CourseStatusViewModel model)
		{
			try
			{
				await _httpClient.CourseStatusDELETEAsync(model.Id);
				return new Response<int>()
				{
					IsSuccess = true,
					Message = "عملیات حذف وضعیت دوره با موفقیت انجام شد"
				};
			}
			catch (ApiException e)
			{
				return ConvertApiException<int>(e);
			}
		}

	}
}
