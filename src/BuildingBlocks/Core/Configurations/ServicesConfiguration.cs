using Core.Controllers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.Configurations
{
    public static class ServicesConfiguration
    {
        public static IMvcBuilder AddTestController(this IMvcBuilder builder)
        {
            var assembly = Assembly.GetAssembly(typeof(TestController));

            builder
                .AddApplicationPart(assembly)
                .AddControllersAsServices();

            return builder;
        }
    }
}
