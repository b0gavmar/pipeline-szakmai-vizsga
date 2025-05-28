using PipeLine.Backend.WebSockets;

namespace PipeLine.Backend.Extensions
{
    public static class WebSocketExtension
    {
        public static void UseWebSocket(this IApplicationBuilder app)
        {
            app.UseWebSockets();
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/ws/charging-progress")
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        var handler = context.RequestServices.GetRequiredService<IChargingProgressWebSocketHandler>();
                        await handler.HandleAsync(context, webSocket);
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                }
                else
                {
                    await next();
                }
            });

        }
    }
}
