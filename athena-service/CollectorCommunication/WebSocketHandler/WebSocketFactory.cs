using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace AthenaService.CollectorCommunication.WebSocketHandler
{
    public interface IWebSocketFactory
    {
        void Add(WebSocket socket);
        void Remove(WebSocket socket);
        IEnumerable<WebSocket> All();
    }

    public class WebSocketFactory : IWebSocketFactory
    {
        ConcurrentDictionary<int, WebSocket> _sockets = new ConcurrentDictionary<int, WebSocket>();

        public void Add(WebSocket socket)
        {
            _sockets.TryAdd(socket.GetHashCode(), socket);
        }

        public void Remove(WebSocket socket)
        {
            _sockets.Remove(socket.GetHashCode(), out _);
        }

        public IEnumerable<WebSocket> All()
        {
            return _sockets.Values;
        }
    }
}
