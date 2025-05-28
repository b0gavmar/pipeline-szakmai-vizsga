import { defineStore } from "pinia";
import axios from "axios";
import { ref } from "vue";
import { useToast } from "vue-toastification";

export const api = axios.create({
  withCredentials: true,
});

export const useDeviceStore = defineStore("device", () => {
  const devices = ref([]);
  const toast = useToast();

  const fetchDevices = async (query) => {
    try {
      const response = await api.get(
        `https://localhost:7020/api/Device/myFilteredDevices`,
        {
          params: { input: query },
        }
      );
      devices.value = response.data;
      console.log(devices.value);
    } catch (error) {
      console.error("error:", error);
    }
  };
  const fetchNonChargingDevices = async (query) => {
    try {
      const response = await api.get(
        `https://localhost:7020/api/Device/myNonChargingDevices`,
        {
          params: { input: query },
        }
      );
      devices.value = response.data;
      console.log(devices.value);
    } catch (error) {
      console.error("error:", error);
    }
  };
  const saveDevice = async (deviceToSave) => {
    try {
      await api.put(`https://localhost:7020/api/Device`, deviceToSave);
      toast.success("Device data was successfully saved!");
      await fetchDevices();
    } catch (error) {
      console.error("error:", error);
    }
  };
  const deleteDevice = async (deviceToDelete) => {
    try {
      await api.delete(`https://localhost:7020/api/Device/${deviceToDelete}`);
      toast.success("Device was successfully deleted!");
      await fetchDevices();
    } catch (error) {
      console.error("error:", error);
    }
  };
  const addDevice = async (deviceToAdd) => {
    try {
      await api.post(`https://localhost:7020/api/Device`, deviceToAdd);
      toast.success("New device successfully added!");
      await fetchDevices();
    } catch (error) {
      console.error("error:", error);
    }
  };
  return {
    devices,
    fetchDevices,
    fetchNonChargingDevices,
    saveDevice,
    deleteDevice,
    addDevice,
  };
});
