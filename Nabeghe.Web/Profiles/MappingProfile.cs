using AutoMapper;
using Nabeghe.Domain.Models.Course;
using Nabeghe.Web.Models.CourseStatus;
using Nabeghe.Web.Services.Base;


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

            #endregion
        }
    }
}
