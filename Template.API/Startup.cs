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
            services.AddTransient<ICommonService, CommonService>();
            //ע��Ӧ�÷���
            //services.RegisterAppServices(Assembly.Load("Template.BLL"));

            // ע��token
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,

                            ValidIssuer = Configuration["Authorization:Issuer"],
                            ValidAudience = Configuration["Authorization:Audience"],
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

            //�ָ���ַ�������
            string[] origins = Configuration["AllowedOrigins:Urls"].Split('|');
            //���ÿ�����
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigin", builder =>
                {
                    builder.WithOrigins(origins) //����ָ����Դ����������
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });

            // ע��Swagger
            services.AddSwaggerGen(Swagger =>
            {
                Swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Template.API", Version = "v1" });
                // ��ӿ�������ע�ͣ�true��ʾ��ʾ������ע�ͣ�
                Swagger.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"), true);
                // ����������
                Swagger.CustomSchemaIds((type) => type.FullName);
                Swagger.OperationFilter<HttpHeaderOperation>(); // ���httpHeader����
                Swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
            });

            // ���巵�ص�jsonԭ�����أ���ʹ���շ������淶
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

            // ʹ�ÿ�������
            app.UseCors("AllowAllOrigin");

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
