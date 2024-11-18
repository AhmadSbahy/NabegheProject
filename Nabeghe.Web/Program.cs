using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Nabeghe.Infra.Data.Context;
using Nabeghe.Infra.IoC.Container;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region IOC

builder.Services.RegisterServices();

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
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
