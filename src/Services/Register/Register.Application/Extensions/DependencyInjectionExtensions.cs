﻿using Register.Application.Interfaces;
using Register.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Core.DependencyInjectionExtension;

namespace Register.Application.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddCoreLib();

            return services;
        }
    }
}
