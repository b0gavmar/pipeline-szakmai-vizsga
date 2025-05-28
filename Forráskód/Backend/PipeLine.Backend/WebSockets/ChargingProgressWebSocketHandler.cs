using System.Net.WebSockets;
using System.Text.Json;
using System.Text;
using PipeLine.Backend.Repos.ChargingInstanceRepos;
using PipeLine.Backend.Services.ChargingInstanceServices;
using PipeLine.Shared.Dtos.ChargingInstance;
using PipeLine.Shared.Models.ChargingInstanceModels;
using PipeLine.Backend.Assemblers.ChargingInstanceAssemblers;
using PipeLine.Shared.Responses;

namespace PipeLine.Backend.WebSockets
{
    public class ChargingProgressWebSocketHandler : IChargingProgressWebSocketHandler
    {
        private readonly IChargingInstanceService _service;
        private readonly IChargingInstanceAssembler _assembler;

        public ChargingProgressWebSocketHandler(
            IChargingInstanceService service,
            IChargingInstanceAssembler assembler)
        {
            _service = service;
            _assembler = assembler;
        }

        public async Task HandleAsync(HttpContext context, WebSocket webSocket)
        {
            try
            {
                var instanceIdStr = context.Request.Query["instanceId"];
                if (!Guid.TryParse(instanceIdStr, out var instanceId))
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.InvalidPayloadData, "Invalid instanceId", CancellationToken.None);
                    return;
                }

                while (webSocket.State == WebSocketState.Open)
                {
                    var entity = await _service.GetInstanceByIdAsync(instanceId);
                    if (entity != null)
                    {
                        if (entity.End != null)
                        {
                            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Charging already finished", CancellationToken.None);
                            break;
                        }

                        var dto = _assembler.ToDto(entity);
                        var json = JsonSerializer.Serialize(dto);
                        var buffer = Encoding.UTF8.GetBytes(json);
                        await webSocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None); 
                    }

                    ChargingProgressResponse response = await _service.UpdateChargingProgressAsync(instanceId);

                    if (response.IsFinished)
                    {
                        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Charging completed", CancellationToken.None);
                        break;
                    }

                    await Task.Delay(3000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"WebSocket error: {ex.Message}");
            }
        }
    }

}
