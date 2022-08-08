using System.Net.WebSockets;
using System.Text;

namespace AthenaService.CollectorCommunication.WebSocketHandler
{
    public interface IWebSocketMessageHandler
    {
        Task BroadcastMessage(string value);
    }

    public class WebSocketMessageHandler : IWebSocketMessageHandler
    {
        private readonly IWebSocketFactory _factory;

        public WebSocketMessageHandler(IWebSocketFactory factory)
        {
            _factory = factory;
        }

        public async Task BroadcastMessage(string value)
        {
            var buffer = Encoding.UTF8.GetBytes(value);
            var sockets = _factory.All();
            foreach (var socket in sockets)
            {
                if (socket.State == WebSocketState.Open)
                {
                    await socket.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
    }
}
