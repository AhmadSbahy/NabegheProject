using AutoMapper;
using Nabeghe.Application.DTOs.CourseStatus;
using Nabeghe.Domain.Models.Course;

namespace Nabeghe.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Course Status

            CreateMap<CourseStatus, CourseStatusDto>().ReverseMap();
            CreateMap<CourseStatus, CreateCourseStatusDto>().ReverseMap();
            CreateMap<CourseStatus, UpdateCourseStatusDto>().ReverseMap();

            #endregion
        }
    }
}
