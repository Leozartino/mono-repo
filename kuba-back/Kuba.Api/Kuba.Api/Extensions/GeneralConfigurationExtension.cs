using Kuba.Api.Errors;
using Kuba.Api.Helpers;
using Kuba.Domain.Interfaces;
using Kuba.Domain.Models;
using Kuba.Infra.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Kuba.Api.Extensions
{
    public static class GeneralConfigurationExtension
    {
        public static IServiceCollection AddGeneralConfigurationExtensions
            (
            this IServiceCollection services, 
            IConfiguration configuration
            )
        {

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(entry => entry.Value.Errors.Count() > 0)
                        .SelectMany(entry => entry.Value.Errors)
                        .Select(entry => entry.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult( errorResponse );

                };
            });

            services.AddScoped<IJwtSettingsHelper, JwtSetttingsHelper>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<ApiDbContext>();

            services.AddCors(o => o.AddPolicy(name: "KubaAppOrigins",
                policy =>
                {
                    policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
                }));

            services.AddRouting(options => options.LowercaseUrls = true);

            //DbContext
            services.Configure<ApplicationSettingsConfig>(configuration);

            var applicationConfig = configuration.Get<ApplicationSettingsConfig>();

            services.AddDbContext<ApiDbContext>(options =>
            {
                options.UseSqlServer(applicationConfig!.ConnectionStrings.DefaultConnection);
            });

            // IdentityUser -> represents the logged user
            // IdentityRole -> user profile (employee, supervisor and adm)
            services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApiDbContext>();

            //JwtAuthentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                byte[] key = Encoding.ASCII.GetBytes(applicationConfig!.JwtSettings.Secret);

                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = applicationConfig.JwtSettings.Issuer,
                    ValidAudience = applicationConfig.JwtSettings.Audience,
                };
            });

            return services;
        }
    }
}
