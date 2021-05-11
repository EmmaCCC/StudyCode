using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CookieAuthSample
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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(opts =>
            {
                opts.LoginPath = "/home/login"; //未登录访问页面 [Authorize] 跳转到该路径
                opts.AccessDeniedPath = "/home/denied";//登录后访问权限不够的页面 [Authorize] 跳转到该路径
                opts.LogoutPath = "/home/logout";
                // opts.Cookie = new CookieBuilder() //配置Cookie的信息，如:domain,age,path.etc.
                // {

                // };
                // opts.ClaimsIssuer = "ss";//声明的签发人
                // opts.CookieManager = new ChunkingCookieManager();//定义怎么获取cookie，写入cookie，删除cookie
                // opts.DataProtectionProvider =null;//cookie 加密解密的提供器，可自定义加密解密逻辑
                // opts.ExpireTimeSpan = TimeSpan.FromHours(2);//指定cookie的过期时间，默认不指定是session级别
                // opts.Events = new CookieAuthenticationEvents();//cookie 的各种事件，可以自定事件，在事件里边做一些操作，比如登入前，登入后，登出前，登出后，类似钩子函数。
                // opts.EventsType = typeof(Object);//指定上边events的类型，可以用注入的方式获取
                // opts.TicketDataFormat = null;//指定票据的类型格式
                // opts.SlidingExpiration = true;//是否是滑动过期时间方式
                // opts.SessionStore = null;//自定cookie的存储获取位置，比如可以存到redis里边，
                // opts.ReturnUrlParameter = "/home/index";//登录跳转附带的参数名称 默认是returnUrl
                // opts.ForwardAuthenticate = "JwtBearer";//转发到其他认证方式
                // opts.ForwardSignIn =  "JwtBearer"; //登入时转换成其他认证方式，其他类推，貌似没什么用
            });
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("RequiredAmin", policy => policy.RequireRole("Admin"));
            });
            services.AddHttpContextAccessor();
            // services.AddSingleton<IAuthorizationHandler, SignatureHandler>();

            services.AddControllersWithViews();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
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
