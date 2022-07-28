namespace AthenaService
{
    public class Startup
    {
        #region App Config Variables

        protected const string _version = "v1";
        protected const string _appName = "Athena Serice";
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
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{_appName} {_version}"); });

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
            });
        }
    }
}