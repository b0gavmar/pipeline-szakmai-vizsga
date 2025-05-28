import { onUnmounted } from "vue";

export function useChargingProgressWebSocket(instanceId, onMessageReceived) {
  const socket = new WebSocket(
    `wss://localhost:7020/ws/charging-progress?instanceId=${instanceId}`
  );

  socket.onopen = () => {
    console.log("WebSocket connected");
  };

  socket.onmessage = (event) => {
    const data = JSON.parse(event.data);
    console.log("Update:", data);
    onMessageReceived(data);
  };

  socket.onerror = (error) => {
    console.error("WebSocket error:", error);
  };

  socket.onclose = (event) => {
    console.log("WebSocket closed", event);
    if (event.wasClean) {
    } else {
      setTimeout(() => {
        useChargingProgressWebSocket(instanceId, onMessageReceived);
      }, 3000);
    }
  };

  onUnmounted(() => {
    if (socket.readyState === WebSocket.OPEN) {
      socket.close();
    }
  });

  return socket;
}
