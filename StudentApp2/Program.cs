using Microsoft.AspNetCore.Builder;
using Data;
using Microsoft.EntityFrameworkCore;
using Data.Models;
using Microsoft.OpenApi.Models;
using AutoMapper;
using StudentApp2;
using Service;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.Identity;
using Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public class Program {
    
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var configuration = builder.Configuration;


        builder.Services.AddAutoMapper(typeof(MappingProfile));

        builder.Services.AddHttpClient();
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
    /*    builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        });*/

        // Set up EntityFramework with Sql Server
        builder.Services.AddDbContext<UniversityDbContext>(options =>
        options.UseSqlServer((@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog= UniversityContext; Integrated Security=True;")));

        // Set up Identity
        builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<UniversityDbContext>()
            .AddDefaultTokenProviders();

        //Set up JWT Authentication
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

            // ?? ????????? ????????? Bearer ??????
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "??????? ????? ? ???????: Bearer {???_?????}"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
        });


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

        app.UseAuthentication(); 

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}