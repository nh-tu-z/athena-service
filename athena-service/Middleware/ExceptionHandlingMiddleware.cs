using AthenaService.Logger;

namespace AthenaService.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, IServiceProvider serviceProvider, IWebHostEnvironment webHostEnvironment)
        {
            var logManager = serviceProvider.GetService<ILogManager>();
            try
            {
                logManager.Information("ExceptionHandling Middleware", "AthenaService");

                await next(context);
            }
            catch (Exception ex)
            {
                logManager.Error(ex.Message, "AthenaService");
            }
        }
    }
}
