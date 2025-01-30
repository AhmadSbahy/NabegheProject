using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MyEshop_Clean.MVC.Base;
using Nabeghe.Web.Contracts;
using Nabeghe.Web.Services;

namespace Nabeghe.Web
{
    public static class WebServicesRegistration
    {
        public static void ConfigureWebRegisterServices(this IServiceCollection services)
        {
	        services.AddSingleton<ILocalStorageService, LocalStorageService>();
	        services.AddScoped<ICourseStatusService, CourseStatusService>();
		}
	}
}
