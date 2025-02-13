﻿using AutoMapper;
using Nabeghe.Application.DTOs.CourseStatus;
using Nabeghe.Domain.Models.Course;
using Nabeghe.Domain.Models.NewsLetter;
using Nabeghe.Domain.ViewModels.CourseStatus;
using Nabeghe.Domain.ViewModels.NewsLetter;

namespace Nabeghe.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
	        #region Course Status

	        // مپینگ اصلی
	        CreateMap<CourseStatus, CourseStatusDto>().ReverseMap();
	        CreateMap<CourseStatus, CreateCourseStatusDto>().ReverseMap();
	        CreateMap<CourseStatus, UpdateCourseStatusDto>().ReverseMap();

	        // مپینگ جدید برای DTO به ViewModel و بالعکس
	        CreateMap<CourseStatusDto, CourseStatusViewModel>().ReverseMap(); // این خط را اضافه کنید

	        // مپینگ فیلترها
	        CreateMap<CourseStatusFilterViewModel, CourseStatusFilterDto>().ReverseMap();



			#endregion

			#region NewsLetter

			CreateMap<NewsLetter, NewsLetterViewModel>().ReverseMap();

			#endregion
		}
	}
}
