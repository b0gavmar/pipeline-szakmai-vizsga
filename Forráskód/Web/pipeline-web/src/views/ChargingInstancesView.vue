<script setup>
import { ref, onMounted, onUnmounted, watch } from 'vue'
import { useChargingInstanceStore } from '@/stores/chargingInstance'
import { useChargingProgressWebSocket } from '@/websockets/chargingProgressWebSocket'

import ChargingInstanceCard from '@/components/instance/ChargingInstanceCard.vue'
import DateRangeFilter from '@/components/instance/DateRangeFilter.vue'

const chargingInstanceStore = useChargingInstanceStore()

const numberOfInstances = ref(0)
const startDate = ref('')
const endDate = ref('')

const sockets = new Map()

const endChargingInstance = async (instance) => {
  await chargingInstanceStore.finishChargingInstance(instance)
  await chargingInstanceStore.fetchOngoingChargingInstances({ start: null, end: null })
  await chargingInstanceStore.fetchFinishedChargingInstances({ start: null, end: null })

  const ws = sockets.get(instance.id)
  if (ws) {
    ws.close()
    sockets.delete(instance.id)
  }
}


const initWebSockets = () => {
  chargingInstanceStore.ongoingChargingInstances.forEach((ci) => {
    if (!sockets.has(ci.id)) {
      const ws = useChargingProgressWebSocket(ci.id, {
        onMessage: () => {
          chargingInstanceStore.fetchOngoingChargingInstances({ start: null, end: null })
        }
      })
      sockets.set(ci.id, ws)
    }
  })
}


onMounted(async () => {
  await chargingInstanceStore.fetchFinishedChargingInstances({ start: null, end: null })
  await chargingInstanceStore.fetchOngoingChargingInstances()
  numberOfInstances.value = await chargingInstanceStore.numberOfInstances()

  initWebSockets()
})

onUnmounted(() => {
  sockets.forEach((s) => s.close())
  sockets.clear()
})

watch([startDate, endDate], async () => {
  await chargingInstanceStore.fetchFinishedChargingInstances({
    start: startDate.value,
    end: endDate.value,
  })
})
</script>

<template>
  <div class="container-fluid my-3">
    <h3 class="text-center text-light">My number of charging instances: {{ numberOfInstances }}</h3>

    <DateRangeFilter v-model:startDate="startDate" v-model:endDate="endDate" />

    <hr class="bg-white" />

    <div
      class="row justify-content-center gap-3 mb-3"
      v-if="chargingInstanceStore.ongoingChargingInstances.length > 0"
    >
      <ChargingInstanceCard
        v-for="instance in chargingInstanceStore.ongoingChargingInstances"
        :key="instance.id"
        :instance="instance"
        :onFinish="endChargingInstance"
      />
    </div>

    <hr class="bg-white" v-if="chargingInstanceStore.ongoingChargingInstances.length > 0" />

    <div
      class="row justify-content-center gap-3 mb-3"
      v-if="chargingInstanceStore.chargingInstances.length > 0"
    >
      <ChargingInstanceCard
        v-for="instance in chargingInstanceStore.chargingInstances"
        :key="instance.id"
        :instance="instance"
        :onFinish="endChargingInstance"
      />
    </div>

    <div v-else class="text-center text-light mt-4">
      <p>No charging instances found.</p>
    </div>
  </div>
</template>
