using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace SwaggerDemo
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
           
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter(true));
                });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info()
                {
                    Version = "v1",
                    Title = "在线商城api-v1",
                    Description = "为在线商城提供完成的订单流程接口",
                });
                c.SwaggerDoc("v2",new Info
                {
                    Version = "v2",
                    Title = "在线商城api-v2",
                    Description = "为在线商城提供完成的订单流程接口",
                });
                c.DescribeAllEnumsAsStrings();
                c.DescribeStringEnumsInCamelCase();
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "SwaggerDemo.xml"));
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Model.xml"));
                //c.DocInclusionPredicate((docName, apiDesc) =>
                //{
                //    if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                //    //var versions1 = methodInfo.GetCustomAttributes(typeof(ApiVersionAttribute), true)
                //    //    .OfType<ApiVersionAttribute>().ToList();

                //    //var versions2 = methodInfo.DeclaringType.GetCustomAttributes(true)
                //    //    .OfType<ApiVersionAttribute>().ToList();

                //    //versions1.AddRange(versions2);
                //    //var versions = versions1.SelectMany(attr => attr.Versions);

                //    //apiDesc.RelativePath = apiDesc.RelativePath.Replace("{version}",
                //    //    string.Join(",", versions.Select(a => a.MajorVersion)));
                //    return true;
                //});
                c.CustomSchemaIds((type) => type.FullName);

                c.DocInclusionPredicate((docName, apiDesc) => apiDesc.RelativePath.Contains(docName));
                // Add security definitions
                //c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                //{
                //    Description = "Please enter into field the word 'Bearer' followed by a space and the JWT value",
                //    Name = "Authorization",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.ApiKey,
                //});
                //c.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    { new OpenApiSecurityScheme
                //    {
                //        Reference = new OpenApiReference()
                //        {
                //            Id = "Bearer",
                //            Type = ReferenceType.SecurityScheme
                //        }
                //    }, Array.Empty<string>() }
                //});

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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((document, request) =>
                {
                    var paths = document.Paths.ToDictionary(item =>
                    {
                        return string.Join('/', item.Key.Split('/').Select(x => x.Contains("{") || x.Length < 2 ? x : char.ToLowerInvariant(x[0]) + x.Substring(1)));

                    }, item => item.Value);
                    document.Paths.Clear();
                    foreach (var pathItem in paths)
                    {
                        document.Paths.Add(pathItem.Key, pathItem.Value);
                    }
                });
                
            });

            app.UseSwaggerUI(c =>
            {
                c.DefaultModelExpandDepth(-1);
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "在线商城api-v1.0");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "在线商城api-v2.0");
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                c.InjectStylesheet("/swagger-ui/custom.css");
                
            });


            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
