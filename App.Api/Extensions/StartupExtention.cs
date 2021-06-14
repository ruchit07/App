using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using App.Data.Repositories;
using App.Data.Infrastructure;
using App.Data.Models.ViewModels;

namespace App.Api.Extensions
{
    public static class StartupExtention
    {
        public static void SetDbContext(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddDbContext<Data.Context.AppContext>(options => options.UseSqlServer(configuration.GetConnectionString("AppConnection")));
        }

        public static void SetScopedService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAppRepository<>), typeof(AppRepository<>));
            services.AddScoped(typeof(ILeadRepository), typeof(LeadRepository));
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped(typeof(IDbFactory<>), typeof(DbFactory<>));
        }

        public static void SetAuthentication(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var jwtTokenConfig = configuration.GetSection("jwtTokenConfig").Get<JwtTokenConfig>();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtTokenConfig.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
                    ValidAudience = jwtTokenConfig.Audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                };
            });
        }
    }
}
