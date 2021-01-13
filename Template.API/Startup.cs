using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Reflection;
using Template.BLL;
using Template.NuGet;
using Microsoft.AspNetCore.Http;

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
            // 瞬时模式AddTransient
            // 如services.AddTransient<IFoo, Foo>();向容器中注入接口和实现[依赖注入(DI)]
            services.AddControllers();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICommonService, CommonService>();
            // 可删除？
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //注册应用服务
            //services.RegisterAppServices(Assembly.Load("Template.BLL"));

            // 注册token
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        // Token验证参数
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            // 是否验证发行人
                            ValidateIssuer = true,
                            // 是否验证接收人

                            ValidateAudience = true,
                            // 是否验证 超时?
                            ValidateLifetime = true,
                            // 是否验证密匙
                            ValidateIssuerSigningKey = true,

                            // 发行人
                            ValidIssuer = Configuration["Authorization:Issuer"],
                            // 接收人
                            ValidAudience = Configuration["Authorization:Audience"],
                            // 密匙
                            IssuerSigningKey = JwtSecurityKey.Create(Configuration["Authorization:SecretKey"])
                        };

                        options.Events = new JwtBearerEvents
                        {
                            OnAuthenticationFailed = context =>
                            {
                                return Task.CompletedTask;
                            },
                            OnTokenValidated = context =>
                            {
                                return Task.CompletedTask;
                            }
                        };
                    });


            //分割成字符串数组
            string[] origins = Configuration["AllowedOrigins:Urls"].Split('|');
            //配置跨域处理
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigin", builder =>
                {
                    builder.WithOrigins(origins) //允许指定来源的主机访问
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });

            // 注入Swagger
            services.AddSwaggerGen(Swagger =>
            {
                //Swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Template.API", Version = "v1" });
                Swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "EmptyDream Template Project",
                    Description = "RESTful API for Template.API",
                    Contact = new OpenApiContact { Name = "EmptyDream", Email = "764799539@qq.com" }
                });
                // 添加控制器层注释（true表示显示控制器注释）
                Swagger.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"), true);
                // 处理复杂名称
                Swagger.CustomSchemaIds((type) => type.FullName);
                Swagger.OperationFilter<HttpHeaderOperation>(); // 添加httpHeader参数
                Swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
            });
            //services.AddMvc(options =>
            //{
            //    options.Filters.Add(typeof(GlobalExceptionFilter));
            //});


            services.AddControllers().AddJsonOptions(options =>
            {
                // 定义返回的json原样返回，不使用驼峰命名规范
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
        }

        /// <summary>
        /// 此方法由Runtime调用。使用此方法配置HTTP请求管道
        /// 1、这里的配置对所有请求生效
        /// </summary>
        /// 
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 判断当前环境是否为开发模式
            if (env.IsDevelopment())
            {
                // 注册一个开发模式异常页面
                app.UseDeveloperExceptionPage();
            }

            // 使用跨域配置
            app.UseCors("AllowAllOrigin");

            // 调用HTTPS重定向中间件, 所有HTTP请求转换为HTTPS
            //app.UseHttpsRedirection();

            // 路由中间件
            app.UseRouting();

            // 认证中间件
            app.UseAuthorization();

            // 终端中间件 和路由中间件是一套东西
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

            // IApplicationBuilder一定有一个地方去调用build方法，得到Application

        }
    }
}
