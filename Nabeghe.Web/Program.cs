using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Nabeghe.Application.Statics;
using Nabeghe.Infra.Data.Context;
using Nabeghe.Infra.IoC.Container;
using Nabeghe.Web;
using Nabeghe.Web.Services.Base;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Unicode;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());


#region IOC

builder.Services.RegisterServices();
builder.Services.ConfigureWebRegisterServices();
builder.Services.AddHttpClient();

var apiAddress = builder.Configuration["ApiAddress"];

builder.Services.AddHttpClient<IClient, Client>(client =>

{
	client.BaseAddress = new Uri(apiAddress);
});

#endregion

#region DbContext

builder.Services.AddDbContext<NabegheContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("NabegheConnectionStrings"));
});

#endregion

#region HtmlEncoder

builder.Services.AddSingleton<HtmlEncoder>(
	HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin,
		UnicodeRanges.Arabic }));

#endregion

#region Authentication

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(60);
    });

#endregion

#region Config kavenegar api

builder.Configuration.GetSection("KavenegarSms").Get<KavenegarStatics>();

#endregion

#region AppConfigs

builder.Services.Configure<FormOptions>(o =>
{
	o.ValueLengthLimit = int.MaxValue;
	o.MultipartBodyLengthLimit = 4L * 1024L * 1024L * 1024L;
	o.MultipartBoundaryLengthLimit = int.MaxValue;
	o.MultipartHeadersCountLimit = int.MaxValue;
	o.MultipartHeadersLengthLimit = int.MaxValue;
	o.BufferBodyLengthLimit = 4L * 1024L * 1024L * 1024L;
	o.BufferBody = true;
	o.ValueCountLimit = int.MaxValue;
});

builder.Services.Configure<IISServerOptions>(options =>
{
	options.MaxRequestBodySize = Int64.MaxValue;
});

builder.Services.Configure<KestrelServerOptions>(options =>
{
	options.Limits.MaxRequestBodySize = Int64.MaxValue;
});

#endregion

var app = builder.Build();
app.Use(async (context, next) =>
{
	await next();
	if (context.Response.StatusCode == 404)
	{
		context.Response.Redirect("/Home/Error404");
	}
});
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
