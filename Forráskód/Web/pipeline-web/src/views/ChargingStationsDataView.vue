<script setup>
import { useChargingPortStore } from '@/stores/chargingPort'
import { useErrorTicketStore } from '@/stores/errorTicket'
import { onMounted, watch, ref } from 'vue'
import { useRoute } from 'vue-router'
import router from '@/router'
import Modal from '@/components/_modals/ErrorTicketsModal.vue'

const chargingPortStore = useChargingPortStore()
const errorTicketStore = useErrorTicketStore()
const searchQuery = ref('')
const numberOfPorts = ref(0)
const route = useRoute()

const startNewChargingInstance = (id) => {
  router.push(`/chargingInstances/newChargingInstance/${id}`)
}

watch(searchQuery, async (newValue) => {
  await chargingPortStore.fetchChargingPortsOfStation(newValue, route.params.chargingStationId)
})

onMounted(async () => {
  await chargingPortStore.fetchChargingPortsOfStation('', route.params.chargingStationId)
  await errorTicketStore.fetchErrorTicketsOfStation(route.params.chargingStationId)
  numberOfPorts.value = await chargingPortStore.numberOfPorts(route.params.chargingStationId)
})
</script>

<template>
  <main class="container-fluid mt-4">
    <div class="row justify-content-center mb-3">
      <div class="col-12 col-md-6">
        <div class="d-flex justify-content-between align-items-center">
          <h3 class="text-light m-0">Number of Charging Ports: {{ numberOfPorts }}</h3>
          <button
            class="btn"
            :class="errorTicketStore.errorTickets.length === 0 ? 'btn-secondary' : 'btn-danger'"
            data-bs-toggle="modal"
            data-bs-target="#modal-tickets"
          >
            <i class="bi bi-exclamation-triangle-fill"></i> Issues
          </button>
        </div>
      </div>
    </div>

    <div class="row justify-content-center mb-3">
      <div class="col-12 col-md-6">
        <input
          v-model="searchQuery"
          type="text"
          class="form-control"
          placeholder="Search by Port Number..."
        />
      </div>
    </div>

    <hr class="bg-white" />

    <div
      v-if="chargingPortStore.chargingPorts.length > 0"
      class="row justify-content-center gap-3 mb-3"
    >
      <div
        v-for="port in chargingPortStore.chargingPorts"
        :key="port.id"
        class="col-12 col-md-4 col-lg-4"
      >
        <div class="card">
          <div class="card-body p-0">
            <p class="card-text"><strong>Port Number:</strong> {{ port.portNumber }}</p>
            <button class="btn btn-success w-100" @click="startNewChargingInstance(port.id)">
              <i class="bi bi-battery-charging"></i> Start New Charging Instance
            </button>
          </div>
        </div>
      </div>
    </div>

    <div v-else class="text-center text-light mt-4">
      <p>No matching charging ports found.</p>
    </div>
  </main>
  <Modal />
</template>
