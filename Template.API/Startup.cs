using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using Template.BLL;
using Template.NuGet;

namespace Template.API
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConfigHelper.Configuration = Configuration;
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 此方法由运行时调用，此方法将服务注入到容器，注入体现的是一个IOC（控制反转的的思想）
        /// </summary>
        /// <param name="services">依赖注入容器</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // 如services.AddTransient<IFoo, Foo>();向容器中注入接口和实现[依赖注入(DI)]
            services.AddControllers();
            services.AddTransient<IUserService, UserService>();
            //注册应用服务
            //services.RegisterAppServices(Assembly.Load("Template.BLL"));

            // 注入Swagger
            services.AddSwaggerGen(Swagger =>
            {
                Swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Template.API", Version = "v1" });
                //添加控制器层注释（true表示显示控制器注释）
                Swagger.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"), true);
            });

            //定义返回的json原样返回，不使用驼峰命名规范
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // 启用中间件为生成的 JSON 文档
            app.UseSwagger();
            // 为SwaggerUI提供服务
            app.UseSwaggerUI(Swagger =>
            {
                Swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "Template.API V1");
            });
        }
    }
}
