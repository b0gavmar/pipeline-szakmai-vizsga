import { defineStore } from 'pinia'
import { ref } from 'vue'
import axios from 'axios'
import { useToast } from 'vue-toastification'

export const useDeviceStore = defineStore('device', () => {
  const toast = useToast()
  const devices = ref([])

  const api = axios.create({
    withCredentials: true,
  })

  const fetchDevices = async (query) => {
    try {
      const response = await api.get('https://localhost:7020/api/device/myFilteredDevices', {
        params: { input: query },
      })
      devices.value = response.data
    } catch (error) {
      console.error('error:', error)
    }
  }

  const fetchNonChargingDevices = async () => {
    try {
      const response = await api.get('https://localhost:7020/api/device/myNonChargingDevices')
      devices.value = response.data
    } catch (error) {
      console.error('error:', error)
    }
  }

  const getDeviceById = async (deviceId) => {
    try {
      const response = await api.get(`https://localhost:7020/api/device/${deviceId}`)
      return response.data
    } catch (error) {
      console.error('error:', error)
    }
  }

  const numberOfDevices = async () => {
    try {
      const response = await api.get('https://localhost:7020/api/device/myNumberOfDevices')
      return response.data
    } catch (error) {
      console.error('error:', error)
    }
  }

  const saveDevice = async (deviceToSave) => {
    try {
      await api.put('https://localhost:7020/api/device', deviceToSave)
      toast.success('Device saved successfully!')
      await fetchDevices()
    } catch (error) {
      console.error('error:', error)
    }
  }

  const deleteDevice = async (deviceToDelete) => {
    try {
      await api.delete(`https://localhost:7020/api/device/${deviceToDelete}`)
      toast.success('Device deleted successfully!')
      await fetchDevices()
    } catch (error) {
      console.error('error:', error)
    }
  }

  const addDevice = async (deviceToAdd) => {
    try {
      await api.post('https://localhost:7020/api/device', deviceToAdd)
      toast.success('Device added successfully!')
      await fetchDevices()
    } catch (error) {
      console.error('error:', error)
    }
  }

  return {
    devices,

    fetchDevices,
    getDeviceById,
    numberOfDevices,
    saveDevice,
    deleteDevice,
    fetchNonChargingDevices,
    addDevice,
  }
})
