using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using App.Api.ActionFilters;
using App.Api.Extensions;
using App.Api.Middlewares;
using Autofac.Extensions.DependencyInjection;
using App.Service;
using App.Data.Models.ViewModels;
using App.Data.Infrastructure.Infrastructure;

namespace App.Api
{
    public class Startup
    {
        public IContainer _container { get; private set; }
        public IConfigurationRoot _configuration { get; }
        private static IHttpContextAccessor _httpContextAccessor { get; set; }
        private readonly string _allowCors = "AllowCors";
        private string AppConnectionString
        {
            get
            {
                return _configuration.GetConnectionString("AppConnection");
            }
        }
        private string BackofficeConnectionString
        {
            get
            {
                return _configuration.GetConnectionString("BackofficeConnection");
            }
        }
        private string AuthConnectionString
        {
            get
            {
                return _configuration.GetConnectionString("AuthConnection");
            }
        }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var jwtTokenConfig = _configuration.GetSection("jwtTokenConfig").Get<JwtTokenConfig>();
            services.SetDbContext(_configuration);
            services.AddSingleton<IConfiguration>(_configuration);
            services.AddSingleton(jwtTokenConfig);
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddCors(options =>
            {
                options.AddPolicy(name: _allowCors,
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin();
                                      builder.AllowAnyHeader();
                                      builder.AllowAnyMethod();
                                  });
            });
            services.AddMvc(o => o.Filters.Add(typeof(ValidateModelStateAttribute)))
                    .AddJsonOptions(o =>
                    {
                        o.JsonSerializerOptions.PropertyNamingPolicy = null;
                        o.JsonSerializerOptions.DictionaryKeyPolicy = null;
                    });
            services.AddRazorPages().AddMvcOptions(options => options.EnableEndpointRouting = false);
            services.AddSession();
            services.SetAuthentication(_configuration);
            services.SetScopedService();

            // create a Autofac container builder
            var builder = new ContainerBuilder();
            builder.Register(ctx => GetCaller()).As<ICallerService>().InstancePerDependency();
            _container = builder.CreateContainer(services);
            return new AutofacServiceProvider(_container);
        }

        public void Configure(IApplicationBuilder app, Data.Context.AppContext appContext, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<Data.Context.AppContext>()
                    .Database.Migrate();
            }

            app.UseStaticFiles();
            app.UseCors(_allowCors);
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseSession();
            app.UseMvc();
            DbInitializer<App.Data.Context.AppContext>.Initialize(appContext);
        }

        private static ICallerService GetCaller()
        {
            if (_httpContextAccessor == null)
            {
                return (ICallerService)new AnonymousCaller();
            }

            var principal = _httpContextAccessor.HttpContext?.User;
            var caller = principal?.Identity == null || !principal.Identity.IsAuthenticated
                ? (ICallerService)new AnonymousCaller()
                : (ICallerService)new AuthenticatedCaller(principal);

            return caller;
        }
    }
}
