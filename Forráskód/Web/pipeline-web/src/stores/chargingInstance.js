import { defineStore } from 'pinia'
import { ref } from 'vue'
import axios from 'axios'
import { useToast } from 'vue-toastification'

export const useChargingInstanceStore = defineStore('chargingInstance', () => {
  const toast = useToast()

  const chargingInstances = ref([])
  const ongoingChargingInstances = ref([])

  const api = axios.create({
    withCredentials: true,
  })

  const fetchFinishedChargingInstances = async (query) => {
    try {
      const response = await api.get(
        'https://localhost:7020/api/charginginstance/myFilteredFinishedInstances',
        { params: { start: query.start, end: query.end } },
      )
      chargingInstances.value = response.data
    } catch (error) {
      console.error('error:', error.message)
    }
  }

  const fetchOngoingChargingInstances = async () => {
    try {
      const response = await api.get(
        'https://localhost:7020/api/charginginstance/myOngoingInstances',
      )
      ongoingChargingInstances.value = response.data
    } catch (error) {
      console.error('error:', error.message)
    }
  }

  const numberOfInstances = async () => {
    try {
      const response = await api.get(
        'https://localhost:7020/api/charginginstance/myNumberOfInstances',
      )
      return response.data
    } catch (error) {
      console.error('error:', error.message)
      return 0
    }
  }

  const newChargingInstance = async (newChargingInstance) => {
    try {
      await api.post('https://localhost:7020/api/charginginstance', newChargingInstance)
      toast.success('ChargingInstance started successfully!')
      await fetchOngoingChargingInstances({ start: null, end: null })
    } catch (error) {
      console.error('error:', error.message)
    }
  }

  const finishChargingInstance = async (chargingInstance) => {
    try {
      await fetchChargingProgress(chargingInstance.id)
      await api.put('https://localhost:7020/api/charginginstance/finishInstance', chargingInstance)
      toast.success('ChargingInstance ended successfully!')
    } catch (error) {
      console.error('error:', error.message)
    }
  }

  const fetchChargingProgress = async (instanceId) => {
    try {
      await api.put(`https://localhost:7020/api/ChargingInstance/ChargingProgress/${instanceId}`)
    } catch (error) {
      console.error('Error fetching charging progress:', error)
    }
  }

  return {
    chargingInstances,
    ongoingChargingInstances,

    fetchFinishedChargingInstances,
    fetchOngoingChargingInstances,
    numberOfInstances,
    newChargingInstance,
    finishChargingInstance,
    fetchChargingProgress,
  }
})
