using System.Formats.Asn1;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Nabeghe.Application.Profiles;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Infra.Data.Context;
using Nabeghe.Infra.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NabegheContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("NabegheConnectionStrings"));
});
builder.Services.AddScoped<ICourseStatusRepository,CourseStatusRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MappingProfile).Assembly));
builder.Services.AddCors(options =>
{
	options.AddPolicy("CorsPolicy", policyBuilder =>
		policyBuilder.AllowAnyOrigin()
			.AllowAnyHeader()
			.AllowAnyMethod());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
