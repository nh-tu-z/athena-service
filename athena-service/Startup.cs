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
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
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
    }
}