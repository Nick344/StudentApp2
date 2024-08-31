using Microsoft.AspNetCore.Builder;
using Data;
using Microsoft.EntityFrameworkCore;
using Data.Models;
using Microsoft.OpenApi.Models;
using AutoMapper;
using StudentApp2;
using Service;
using Newtonsoft.Json.Converters;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

builder.Services.AddDbContext<UniversityDbContext>(options =>
options.UseSqlServer((@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog= UniversityContext; Integrated Security=True;")));

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddControllers(o => o.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
.AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    options.SerializerSettings.Converters.Add(new StringEnumConverter());
});


var app = builder.Build();


var user = new Student();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI((c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1")));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
