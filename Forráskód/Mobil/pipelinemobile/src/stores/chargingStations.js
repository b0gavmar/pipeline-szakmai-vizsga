import { defineStore } from "pinia";
import axios from "axios";
import { ref } from "vue";

export const api = axios.create({
  withCredentials: true,
});

export const useChargingStationStore = defineStore("chargingStation", () => {
  const chargingStations = ref([]);

  const fetchStations = async (query) => {
    try {
      const response = await api.get(
        `https://localhost:7020/api/chargingStation/FilteredStations`,
        { params: { input: query } }
      );
      chargingStations.value = response.data;
      console.log(chargingStations.value);
    } catch (error) {
      console.error("error:", error);
    }
  };

  return { chargingStations, fetchStations };
});
