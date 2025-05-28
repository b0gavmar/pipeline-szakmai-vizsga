<script setup>
import { useErrorTicketStore } from '@/stores/errorTicket'
import router from '@/router'
import { onMounted, ref } from 'vue'
import { useRoute } from 'vue-router'
import { useToast } from 'vue-toastification'

const errorTicketStore = useErrorTicketStore()
const route = useRoute()
const toast = useToast()

const ticket = ref({
  chargingStationId: route.params.chargingStationId,
  description: "",
  isSolved: false
})
const list = ref(true)

const submitTicket = async () => {
  if (description.value.trim() === '') {
    toast.error('Please enter a description.')
    return
  }

  await errorTicketStore.newErrorTicket(ticket.value)

  ticket.value.description = ''
  list.value = true
}

onMounted(async () => {
  await errorTicketStore.fetchErrorTicketsOfStation(route.params.chargingStationId)
})
</script>

<template>
  <div class="modal" id="modal-tickets">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header plgreen">
          <h4 class="modal-title text-dark">Known issues</h4>
          <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
        </div>

        <div class="modal-body">
          <div v-if="list">
            <div
              v-if="errorTicketStore.errorTickets.length !== 0"
              v-for="ticket in errorTicketStore.errorTickets"
            >
              - {{ ticket.description }}
            </div>
            <div v-else>Everything works as intended.</div>
          </div>
          <div v-else>
            <label for="description" class="form-label text-light">Describe the issue:</label>
            <textarea
              id="description"
              v-model="ticket.description"
              class="form-control"
              rows="3"
              placeholder="Write the issue description here..."
            ></textarea>
            <button class="btn btn-primary mt-2" @click="submitTicket">Submit</button>
          </div>
        </div>

        <div class="modal-footer dark">
          <button v-if="list" type="button" class="btn btn-success" @click="list = false">
            Submit a ticket
          </button>
          <button v-else type="button" class="btn btn-success" @click="list = true">
            See tickets
          </button>
          <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
</template>
