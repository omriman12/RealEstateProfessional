using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientDataSyncAPI.WebAPI.Logic.DI;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace SouthRealEstate
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc(o =>
            {
                /* addind defautl policy(require authentication) to all controllers*/
                //var policy = new AuthorizationPolicyBuilder()
                //    .RequireAuthenticatedUser()
                //    .Build();
                //o.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.SlidingExpiration = true;
                options.AccessDeniedPath = "/Administrator/LogIn";
                options.LoginPath = "/Administrator/Login";
                options.LogoutPath = "/Home";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
            });

            //IoC
            ScopeModule.Load(services);

            ////filters
            //services.AddScoped<ThreadContextAttribute>();

            services.AddAuthorization(options =>
            {
                /* default policy*/
                //options.AddPolicy("defaultpolicy", b =>
                //{
                //    b.RequireAuthenticatedUser();
                //});


                options.AddPolicy("Administrator", policy => policy.RequireClaim("Administrator"));

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
