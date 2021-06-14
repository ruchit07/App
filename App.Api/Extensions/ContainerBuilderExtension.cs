using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using App.Data.Repositories;
using App.Service;

namespace App.Api.Extensions
{
    public static class ContainerBuilderExtension
    {
        public static IContainer CreateContainer(this ContainerBuilder builder, IServiceCollection services)
        {
            builder.Populate(services);
            builder.RegisterType();
            return builder.Build();
        }

        public static void RegisterType(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(AppService<>).Assembly)
                 .Where(t => t.Name.EndsWith("Service"))
                 .AsImplementedInterfaces().InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(AppRepository<>).Assembly)
                .Where(t => t.Name.EndsWith("Repository"));

            builder.RegisterAssemblyTypes(typeof(LeadRepository).Assembly)
               .Where(t => t.Name.EndsWith("Repository"));

            builder.RegisterType<IFormFile>()
                .AsImplementedInterfaces().InstancePerDependency();
        }
    }
}
