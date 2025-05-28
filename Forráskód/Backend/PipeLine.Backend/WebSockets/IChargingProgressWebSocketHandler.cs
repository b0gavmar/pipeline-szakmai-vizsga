using System.Net.WebSockets;

namespace PipeLine.Backend.WebSockets
{
    public interface IChargingProgressWebSocketHandler
    {
        Task HandleAsync(HttpContext context, WebSocket webSocket);
    }

}
