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
        /// �˷���������ʱ���ã��˷���������ע�뵽������ע�����ֵ���һ��IOC�����Ʒ�ת�ĵ�˼�룩
        /// </summary>
        /// <param name="services">����ע������</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // ��services.AddTransient<IFoo, Foo>();��������ע��ӿں�ʵ��[����ע��(DI)]
            services.AddControllers();
            services.AddTransient<IUserService, UserService>();
            //ע��Ӧ�÷���
            //services.RegisterAppServices(Assembly.Load("Template.BLL"));

            // ע��Swagger
            services.AddSwaggerGen(Swagger =>
            {
                Swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Template.API", Version = "v1" });
                //��ӿ�������ע�ͣ�true��ʾ��ʾ������ע�ͣ�
                Swagger.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"), true);
            });

            //���巵�ص�jsonԭ�����أ���ʹ���շ������淶
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

            // �����м��Ϊ���ɵ� JSON �ĵ�
            app.UseSwagger();
            // ΪSwaggerUI�ṩ����
            app.UseSwaggerUI(Swagger =>
            {
                Swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "Template.API V1");
            });
        }
    }
}
