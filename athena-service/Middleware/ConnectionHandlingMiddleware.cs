using System.Net.WebSockets;
using System.IdentityModel.Tokens.Jwt;
using AthenaService.Logger;

namespace AthenaService.Middleware
{
    public class ConnectionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogManager _logger;

        public ConnectionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IConfiguration configuration, IServiceProvider services)
        {
            _logger = services.GetService<ILogManager>();

            _logger.Information("Connection Handling Middleware", "AthenaService");

            _logger.Information("Dummy Authorization Like In Settings", "TODO");
            context.Request.Headers["Authorization"] = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWUsImlhdCI6MTY1OTMzMTg1NywiZXhwIjoxNjU5MzM1NDU3fQ.limjEJduois7TNk2NNIz5fDWvmCGNYZgrTrRg4Fytd0";

            if (context.Request.Path == "/ws")
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    _logger.Information("we're in /ws");
                    var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    await Listen(webSocket);
                }
                else
                {
                    _logger.Information("Need WebSocket Connection Instead Of Http(s)", "AthenaService");
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                }
            }
            else
            {
                var token = context.Request.Headers["Authorization"].First();

                if (token.StartsWith("Bearer "))
                {
                    token = token.Split(" ").ElementAtOrDefault(1);
                }

                // TODO - there has a mechanism to get the the connection string here.
                // because we have a specific database for a tenant so we get the tenant id from jwt >> connection string and store in cache using MemoryCache
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);

                await _next(context);
            }
        }

        private async Task Listen(WebSocket socket)
        {
            // this param is got from the websocket config
            var bufferSize = 2048;
            WebSocket webSocket = socket;
            var buffer = new byte[bufferSize];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                buffer = new byte[bufferSize];
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            _logger.Information("Disconnected socket might be invoked here", "TODO");
        }
    }
}
