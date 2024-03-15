
using GraduationProject.Data.Context;
using GraduationProject.Data.Models;
using GraduationProject_Api.TRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GraduationProject_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<GP_Db>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("con1"));
            });

            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;

                options.User.RequireUniqueEmail = true;

                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
            })
                  .AddEntityFrameworkStores<GP_Db>();
            //verify token

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "default";
                options.DefaultChallengeScheme = "default";
            }).
                 AddJwtBearer("default", options =>
                 {
                     var secretKey = builder.Configuration.GetValue<string>("SecretKey")!;
                     var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKey);
                     var key = new SymmetricSecurityKey(secretKeyInBytes);

                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = false,
                         ValidateAudience = false,
                         IssuerSigningKey = key
                     };
                 });


            //builder.Services.AddScoped<ITIUserRepository, TUserRepository>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
