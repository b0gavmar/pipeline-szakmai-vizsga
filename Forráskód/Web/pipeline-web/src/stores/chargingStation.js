import { defineStore } from 'pinia'
import { ref } from 'vue'
import axios from 'axios'

export const useChargingStationStore = defineStore('chargingStation', () => {
  const chargingStations = ref([])
  const pageNumber = ref(1)
  const pageSize = ref(20)
  const hasMore = ref(true)

  const api = axios.create({
    withCredentials: true,
  })

  const fetchChargingStations = async (query) => {
    try {
      const response = await api.get(
        'https://localhost:7020/api/chargingStation/FilteredStations',
        { params: { input: query } },
      )
      chargingStations.value = response.data
    } catch (error) {
      console.error('Error fetching stations:', error.message)
    }
  }

  const fetchChargingStationById = async (id) => {
    try {
      const response = await api.get(`https://localhost:7020/api/chargingStation/${id}`)
      return response.data
    } catch (error) {
      console.error('Error fetching station by id:', error.message)
    }
  }

  const numberOfStations = async () => {
    try {
      const response = await api.get(`https://localhost:7020/api/chargingStation/NumberOfStations`)
      return response.data
    } catch (error) {
      console.error('Error fetching station count:', error)
    }
  }

  const fetchPagedChargingStations = async (query, reset = false) => {
    if (reset) {
      pageNumber.value = 1
      hasMore.value = true
    }
    
    if (!hasMore.value) return

    try {
      const response = await api.get(
        'https://localhost:7020/api/chargingStation/FilteredPagedStations',
        {
          params: {
            input: query,
            pageNumber: pageNumber.value,
            pageSize: pageSize.value,
          },
        },
      )

      const newData = response.data

      if (reset) {
        chargingStations.value = [...newData]
      } else {
        chargingStations.value.push(...newData)
      }

      if (newData.length < pageSize.value) {
        hasMore.value = false
      } else {
        pageNumber.value++
      }
    } catch (error) {
      console.error('Error fetching stations:', error.message)
    }
  }

  return {
    chargingStations,
    hasMore,
    pageNumber,

    fetchChargingStations,
    fetchChargingStationById,
    numberOfStations,
    fetchPagedChargingStations,
  }
})
