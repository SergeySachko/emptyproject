using DAL;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Server.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomDevDbContext(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                string useSqLite = Startup.Configuration["Data:useSqLite"];
                if (useSqLite.ToLower() == "true")
                {
                    options.UseSqlite(Startup.Configuration["Data:Dev:SqlLiteConnectionString"]);
                }
                else
                {
                    options.UseSqlServer(Startup.Configuration["Data:Dev:SqlServerConnectionString"], b => b.MigrationsAssembly("Application.DAL"));
                }
            });
            return services;
        }

        public static IServiceCollection AddCustomProdDbContext(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                string useSqLite = Startup.Configuration["Data:useSqLite"];
                if (useSqLite.ToLower() == "true")
                {
                    options.UseSqlite(Startup.Configuration["Data:Prod:SqlLiteConnectionString"]);
                }
                else
                {
                    options.UseSqlServer(Startup.Configuration["Data:Prod:SqlServerConnectionString"], b => b.MigrationsAssembly("Application.DAL"));
                }
            });
            return services;
        }

        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            return services;
        }

        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            services.AddScoped<IDbContextFactory, DbContextFactory>();            

            return services;
        }



    }
}
