import { defineStore } from 'pinia'
import { ref } from 'vue'
import axios from 'axios'

export const useErrorTicketStore = defineStore('errorTicket', () => {
  const errorTickets = ref([])

  const api = axios.create({
    withCredentials: true,
  })

  const fetchErrorTicketsOfStation = async (id) => {
    try {
      const response = await api.get(
        `https://localhost:7020/api/errorTicket/TicketsOfChargingStation/${id}`,
      )
      errorTickets.value = response.data
    } catch (error) {
      console.error('Error fetching error tickets:', error.message)
    }
  }

  const newErrorTicket = async (ticket) => {
    try {
      await api.post(`https://localhost:7020/api/errorTicket/`, ticket)
      await fetchErrorTicketsOfStation(ticket.chargingStationId)
    } catch (error) {
      console.error('Error creating error ticket:', error.message)
    }
  }

  return {
    errorTickets,
    
    fetchErrorTicketsOfStation,
    newErrorTicket,
  }
})
