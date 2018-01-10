using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api;
using Application.Server.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; set; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDependencies();

            services.AddCustomIdentity();

            services.ConfigureApplicationCookie(config =>
            {
                config.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api"))
                        {
                            ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                        return Task.FromResult(0);
                    }
                };
            });

            services.AddMvc()
               .AddApplicationPart(typeof(ProductController).Assembly)
               .AddDataAnnotationsLocalization()
               .AddViewLocalization();
        }

       
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseDefaultFiles();
            app.UseStaticFiles();

        }
    }
}
