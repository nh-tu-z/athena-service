using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FluentValidation.AspNetCore;
using AthenaService.Services;
using AthenaService.Middleware;
using AthenaService.Swagger;
using AthenaService.CollectorCommunication.Scheduler;

namespace AthenaService
{
    public class Startup
    {
        #region App Config Variables

        protected const string _version = "v1";
        protected const string _appName = "Athena Service";
        protected const string _allowAllCorsPolicyName = "AllowAll";

        #endregion App Config Variables

        public IWebHostEnvironment Env { get; }
        protected IConfiguration Configuration { get; set; }

        public Startup(IWebHostEnvironment env)
        {
            Env = env;
            Configuration = GetBuilder().Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AddSwagger(services);

            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            // Adds Microsoft Identity platform (AAD v2.0) support to protect this Api
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = false,
                        RequireSignedTokens = false,
                        SignatureValidator = delegate (string token, TokenValidationParameters parameters)
                        {
                            var jwt = new JwtSecurityToken(token);

                            return jwt;
                        }
                    };
                });

            services
                .AddControllersWithViews(options =>
                {
                    options.InputFormatters.Insert(0, JsonPatchInputFormatter.GetJsonPatchInputFormatter());
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                })
                .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddRouting(options => options.LowercaseUrls = true);

            services
                .AddServices()
                .AddLogger(Configuration, Env)
                .AddPersistence()
                .AddAutoMapper()
                .AddApiVersioningService()
                .AddWebSocket()
                .AddHostedService<SchedulerService>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{_appName} {_version}"); });
            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
            app.UseWebSockets();
            app.UseMiddleware<ConnectionHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }

        public virtual IConfigurationBuilder GetBuilder()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Env.ContentRootPath)
                .AddJsonFile("appsettings.json",
                    optional: false,
                    reloadOnChange: true)
                .AddJsonFile($"appsettings.{Env.EnvironmentName}.json",
                    optional: true)
                .AddEnvironmentVariables();

            return config;
        }

        private static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(_version, new OpenApiInfo() { Title = _appName, Version = _version });
                c.DocumentFilter<JsonPatchDocumentFilter>();
                c.OperationFilter<SwaggerFileOperationFilter>();
                c.OperationFilter<SwaggerHeaderFilter>();

                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter into field the word 'Bearer' following by space and JWT",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}