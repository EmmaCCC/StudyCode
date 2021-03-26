using IdentityServer4.Stores;
using Ids4.Data;
using Ids4Server.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ids4Server
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

            services.AddControllersWithViews();

            services.AddSingleton<GwlDbContext>();

            services.AddHttpContextAccessor();

            services.AddIdentityServer(opts =>
            {
                
            })
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(Ids4MemoryDatas.GetApiResources())
                .AddClientStore<DbClientStore>()
                .AddSecretValidator<MySecretValidator>()
                .AddInMemoryApiScopes(Ids4MemoryDatas.GetApiScopes())
                .AddResourceOwnerValidator<DbResourceOwnerPasswordValidator>()
                .AddProfileService<MyProfileService>()
                .AddExtensionGrantValidator<WeChatLoginGrantValidator>()
                .AddExtensionGrantValidator<SmsLoginGrantValidator>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseIdentityServer();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
