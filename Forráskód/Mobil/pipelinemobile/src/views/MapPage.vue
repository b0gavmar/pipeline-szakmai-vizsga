<template>
  <ion-page>
    <ion-header>
      <ion-toolbar color="success">
        <ion-searchbar v-model="searchQuery" debounce="250"></ion-searchbar>
      </ion-toolbar>
      <ion-list v-if="chargingStations.length != 0 && searchQuery.length != 0">
        <ion-item
          v-for="station in chargingStations"
          :key="station.id"
          @click="focusStationOnMap(station)"
        >
          <ion-label> {{ station.name }} - {{ station.address }} </ion-label>
        </ion-item>
      </ion-list>
    </ion-header>
    <ion-content>
      <div id="map"></div>
    </ion-content>
    <StationModal
      :isOpen="chargingStationStore.selectedStation !== null"
      :station="chargingStationStore.selectedStation"
      :ports="chargingPortStore.chargingPorts"
      @close="closeStationModal"
      @selectPort="openDeviceSelection"
      @errorTicket="openErrorTicket"
    />
    <DeviceSelectionModal
      :isOpen="chargingPortStore.selectedPort !== null"
      :devices="devices"
      @close="closeDeviceSelection"
      @selectDevice="selectedDeviceModal"
    />
    <SelectedDeviceModal
      :isOpen="isSelectedDeviceModalOpen"
      :selectedDevice="selectedDevice"
      :newInstance="newInstance"
      @close="closeSelectedDeviceModal"
      @startInstance="startChargingInstance"
    />

    <SendErrorTicketModal
      :isOpen="isErrorTicketModalOpen"
      :newTicket="newTicket"
      :tickets="tickets"
      @close="closeErrorTicket"
      @sendTicket="sendErrorTicket"
    />
  </ion-page>
</template>

<script setup>
import { computed, nextTick, ref, toRaw, watch } from "vue";
import {
  IonPage,
  IonHeader,
  IonToolbar,
  IonContent,
  onIonViewDidEnter,
  IonSearchbar,
  IonItem,
  IonList,
  IonLabel,
} from "@ionic/vue";
import * as maptilersdk from "@maptiler/sdk";
import { useChargingStationStore } from "@/stores/chargingStations.js";
import { useChargingPortStore } from "@/stores/chargingPorts.js";
import { useDeviceStore } from "@/stores/devices.js";
import { useChargingInstanceStore } from "@/stores/chargingInstances.js";
import { useErrorTicketStore } from "@/stores/errorTickets.js";
import StationModal from "@/components/modals/MapPage/StationModal.vue";
import DeviceSelectionModal from "@/components/modals/MapPage/DeviceSelectionModal.vue";
import SendErrorTicketModal from "@/components/modals/MapPage/SendErrorTicketModal.vue";
import SelectedDeviceModal from "@/components/modals/MapPage/SelectedDeviceModal.vue";

let map;
const chargingStationStore = useChargingStationStore();
const chargingInstanceStore = useChargingInstanceStore();
const chargingPortStore = useChargingPortStore();
const deviceStore = useDeviceStore();
const ticketStore = useErrorTicketStore();
const chargingStations = computed(() => chargingStationStore.chargingStations);
const devices = computed(() => deviceStore.devices);
const tickets = computed(() => ticketStore.errorTickets);
const markers = ref([]);
const selectedDevice = ref({});
chargingStationStore.selectedStation = ref(null);
chargingPortStore.selectedPort = ref(null);
const isSelectedDeviceModalOpen = ref(false);
const isErrorTicketModalOpen = ref(false);
//const isFilteringListOpen = ref(false);
const searchQuery = ref("");
watch(searchQuery, async (newValue) => {
  await chargingStationStore.fetchStations(newValue);
});

const focusStationOnMap = (station) => {
  if (!map || !station) return;
  map.flyTo({
    center: [station.longitude, station.latitude],
    zoom: 15,
    speed: 1.2,
    curve: 1.42,
    easing: (t) => t,
  });
  //isFilteringListOpen.value = false;
};
const newInstance = ref({
  chargingPortId: "",
  deviceId: "",
  start: null,
  startingPercentage: ref(null),
  desiredEndPercentage: ref(null),
});

const newTicket = ref({
  chargingStationid: null,
  description: ref(""),
});

const openDeviceSelection = (port) => {
  chargingPortStore.selectedPort = port;
  newInstance.value.chargingPortId = chargingPortStore.selectedPort.id;
};
const closeDeviceSelection = () => {
  chargingPortStore.selectedPort = null;
};
const selectedDeviceModal = async (device) => {
  try {
    selectedDevice.value = device;
    newInstance.value.deviceId = device.id;
    await nextTick();
    isSelectedDeviceModalOpen.value = true;
    
  } catch (error) {
    alert("Error starting proccess, please try again later.")
  }
};
const closeSelectedDeviceModal = () => {
  isSelectedDeviceModalOpen.value = false;
};

const startChargingInstance = async (newInstance) => {
  console.log(newInstance);
  newInstance.start = new Date().toISOString();
  await nextTick();
  await chargingInstanceStore.newChargingInstance(newInstance);
  await nextTick();
  closeSelectedDeviceModal();
  await nextTick();
  closeDeviceSelection();
  chargingStationStore.selectedStation = null;
};
const selectStation = (station) => {
  chargingStationStore.selectedStation = station;
  chargingPortStore.fetchPorts(station.id);
};
const closeStationModal = () => {
  chargingStationStore.selectedStation = null;
  chargingPortStore.ports = [];
};
const fetchMarkers = async () => {
  markers.value = chargingStations.value;
};
const openErrorTicket = async (station) => {
  console.log(station);
  isErrorTicketModalOpen.value = true;
  newTicket.value.chargingStationid = station.id;
  tickets.value = await ticketStore.fetchErrorTicketsOfStation(station.id);
  console.log(newTicket);
};
const closeErrorTicket = () => {
  isErrorTicketModalOpen.value = false;
};
const sendErrorTicket = async (newTicket) => {
  console.log(newTicket);
  await ticketStore.newErrorTicket(newTicket);
  newTicket.description = "";
  closeErrorTicket();
};

onIonViewDidEnter(async () => {
  await chargingStationStore.fetchStations();
  await deviceStore.fetchNonChargingDevices();
  maptilersdk.config.apiKey = "3K6z7QIsAjfjT4juVPVR";
  map = new maptilersdk.Map({
    container: "map",
    style: maptilersdk.MapStyle.STREETS,
    center: [14.047, 48.4979],
    zoom: 4,
  });
  await fetchMarkers();

  markers.value.forEach((station) => {
    const rawStation = toRaw(station);
    new maptilersdk.Marker({ draggable: false })
      .setLngLat([rawStation.longitude, rawStation.latitude])
      .addTo(map)
      .getElement()
      .addEventListener("click", () => selectStation(station));
  });

  setTimeout(() => {
    map.resize();
  }, 200);
});
</script>
<style scoped>
#map {
  width: 100vw;
  height: 100vh;
  overflow: hidden;
  position: relative;
}
</style>
