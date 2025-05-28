import { onUnmounted, ref } from 'vue'

export function useChargingProgressWebSocket(
    instanceId,
    { onMessage = () => {}, reconnect = true, reconnectInterval = 3000 } = {},
  )
{
  const socket = ref(null)
  let reconnectTimeout = null

  const connect = () => {
    const ws = new WebSocket(`wss://localhost:7020/ws/charging-progress?instanceId=${instanceId}`)
    socket.value = ws

    ws.onopen = () => {
      console.log(`[WebSocket] Connected to charging-progress (${instanceId})`)
    }

    ws.onmessage = (msg) => {
      try {
        const data = JSON.parse(msg.data)
        onMessage(data)
      } catch (err) {
        console.error('Failed to parse WebSocket message:', err)
      }
    }

    ws.onerror = (err) => {
      console.error('[WebSocket] Error:', err)
    }

    ws.onclose = (msg) => {
      console.log(`[WebSocket] Closed (${instanceId})`, msg.reason || msg.code)
      if (!msg.wasClean && reconnect) {
        reconnectTimeout = setTimeout(connect, reconnectInterval)
      }
    }
  }

  connect()

  const close = () => {
    if (socket.value?.readyState === WebSocket.OPEN) {
      socket.value.close()
    }
    if (reconnectTimeout) {
      clearTimeout(reconnectTimeout)
    }
  }

  onUnmounted(close)

  return {
    socket,
    close,
    readyState: () => socket.value?.readyState ?? WebSocket.CLOSED,
  }
}
