using Microsoft.Extensions.DependencyInjection;
using Nabeghe.Application.Senders.Implementation;
using Nabeghe.Application.Senders.Interfaces;
using Nabeghe.Application.Services.Implementation;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Infra.Data.Repositories;

namespace Nabeghe.Infra.IoC.Container
{
	public static class IOCContainer
	{
		public static void RegisterServices(this IServiceCollection services)
		{
			#region Services

			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IAccountService, AccountService>();
			services.AddSingleton<ISmsSender, SmsSender>();
			services.AddScoped<ICourseCategoryService, CourseCategoryService>();
			services.AddScoped<ICourseService, CourseService>();
			services.AddScoped<ICourseCommentService, CourseCommentService>();
			services.AddScoped<ICourseEpisodeService, CourseEpisodeService>();
			services.AddScoped<IRoleService, RoleService>();
			services.AddScoped<IContactUsService, ContactUsService>();
			services.AddScoped<ICourseDiscountService, CourseDiscountService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IBlogCommentService, BlogCommentService>();

            #endregion

            #region Repositories

            services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IRoleRepository, RoleRepository>();
			services.AddScoped<ICourseCategoryRepository, CourseCategoryRepository>();
			services.AddScoped<ICourseRepository, CourseRepository>();
			services.AddScoped<ICourseCommentRepository, CourseCommentRepository>();
			services.AddScoped<ICourseEpisodeRepository, CourseEpisodeRepository>();
			services.AddScoped<IUserRoleRepository, UserRoleRepository>();
			services.AddScoped<ICourseDiscountRepository, CourseDiscountRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IBlogCommentRepository, BlogCommentRepository>();
            services.AddScoped<IBlogCommentReplyRepository, BlogCommentReplyRepository>();
            services.AddScoped<IBlogCommentLikeRepository, BlogCommentLikeRepository>();

            #endregion
        }
    }
}	
