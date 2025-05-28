import { defineStore } from "pinia";
import axios from "axios";
import { ref } from "vue";

export const api = axios.create({
  withCredentials: true,
});

export const useChargingPortStore = defineStore("chargingPort", () => {
  const chargingPorts = ref([]);

  const fetchPorts = async (stationid) => {
    try {
      const response = await api.get(
        `https://localhost:7020/api/ChargingPort/FilteredPortsOfChargingStation/${stationid}`
      );
      chargingPorts.value = response.data;
      console.log(chargingPorts.value);
    } catch (error) {
      console.error("error:", error);
    }
  };
  return { chargingPorts, fetchPorts };
});
