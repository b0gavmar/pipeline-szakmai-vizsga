import { defineStore } from 'pinia'
import { ref } from 'vue'
import axios from 'axios'

export const useChargingPortStore = defineStore('chargingPort', () => {
  const chargingPorts = ref([])

  const api = axios.create({
    withCredentials: true,
  })

  const fetchChargingPortsOfStation = async (query, id) => {
    try {
      const response = await api.get(
        `https://localhost:7020/api/chargingPort/FilteredPortsOfChargingStation/${id}`,
        { params: { input: query } },
      )
      chargingPorts.value = response.data
    } catch (error) {
      console.error('Error fetching ports of station:', error.message)
    }
  }

  const fetchChargingPortById = async (id) => {
    try {
      const response = await api.get(`https://localhost:7020/api/chargingPort/${id}`)
      return response.data
    } catch (error) {
      console.error('Error fetching port by id:', error.message)
    }
  }

  const fetchChargingStationAndPortNameById = async (id) => {
    try {
      const response = await api.get(
        `https://localhost:7020/api/chargingPort/ChargingStationAndPortName/${id}`,
      )
      return response.data
    } catch (error) {
      console.error('Error fetching station/port name:', error.message)
    }
  }

  const numberOfPorts = async (id) => {
    try {
      const response = await api.get(
        `https://localhost:7020/api/chargingPort/NumberOfPortsOfChargingStation/${id}`,
      )
      return response.data
    } catch (error) {
      console.error('Error fetching number of ports:', error)
    }
  }

  return {
    chargingPorts,

    fetchChargingPortsOfStation,
    fetchChargingPortById,
    fetchChargingStationAndPortNameById,
    numberOfPorts,
  }
})
