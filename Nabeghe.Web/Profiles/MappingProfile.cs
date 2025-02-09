using AutoMapper;
using Nabeghe.Domain.Models.Course;
using Nabeghe.Domain.Models.NewsLetter;
using Nabeghe.Domain.ViewModels.CourseStatus;
using Nabeghe.Domain.ViewModels.NewsLetter;
using Nabeghe.Web.Models.CourseStatus;
using Nabeghe.Web.Services.Base;
using CourseStatusViewModel = Nabeghe.Web.Models.CourseStatus.CourseStatusViewModel;

namespace Nabeghe.Web.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Course Status

            CreateMap<CourseStatusViewModel, CourseStatusDto>().ReverseMap();
            CreateMap<CourseStatusViewModel, UpdateCourseStatusDto>().ReverseMap();
            CreateMap<CreateCourseStatusViewModel, CreateCourseStatusDto>().ReverseMap();


            CreateMap<CourseStatusFilterDto, CourseStatusFilterViewModel>().ReverseMap();
            CreateMap<FilterCourseStatusViewModel, CourseStatusFilterDto>().ReverseMap();

			#endregion

			#region NewsLetter

			CreateMap<NewsLetter, NewsLetterViewModel>().ReverseMap();

			#endregion
		}
	}
}
