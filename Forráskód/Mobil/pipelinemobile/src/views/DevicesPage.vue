<template>
  <ion-page>
    <ion-header>
      <ion-toolbar color="light">
        <ion-item color="success">
          <ion-label><h1>My Devices</h1></ion-label>
          <ion-button slot="end" color="warning" @click="openAddModal()">
            <ion-label>Add device</ion-label>
            <ion-icon slot="end" :icon="addCircleOutline"></ion-icon>
          </ion-button>
        </ion-item>
      </ion-toolbar>
    </ion-header>
    <ion-content>
      <AddDeviceModal
        :isOpen="isAddModelOpen"
        @close="closeModal"
        @addDevice="addNewDevice"
      />
      <EditDeviceModal
        :isOpen="isEditModalOpen"
        :selectedDevice="selectedDevice"
        @close="closeModal"
        @saveDevice="saveChanges"
        @deleteDevice="deleteDevice"
      />
      <div class="device-container">
        <ion-card
          v-for="device in devices"
          :key="device.id"
          class="device-card"
          color="success"
          @click="openEditModal(device)"
        >
          <ion-card-content>
            <ion-img
              v-if="device.deviceType == 0"
              src="/ebike.svg"
              class="device-icon"
            ></ion-img>
            <ion-img
              v-if="device.deviceType == 1"
              src="/escooter.svg"
              class="device-icon"
            ></ion-img>
            <ion-img
              v-if="device.deviceType == 2"
              src="/eskateboard.svg"
              class="device-icon"
            ></ion-img>
            <h1>{{ device.name || "Unnamed Device" }}</h1>
            <p>
              {{
                device.deviceType == 0
                  ? "EBike"
                  : device.deviceType == 1
                  ? "EScooter"
                  : "ESkateboard"
              }}
            </p>
            <p>{{ device.manufacturer || "N/A" }}</p>
            <p>{{ device.model || "N/A" }}</p>
          </ion-card-content>
        </ion-card>
      </div>
    </ion-content>
  </ion-page>
</template>
<script setup>
import {
  IonPage,
  IonToolbar,
  IonButton,
  IonHeader,
  IonLabel,
  IonCard,
  IonContent,
  IonCardContent,
  IonIcon,
  IonItem,
  IonImg,
} from "@ionic/vue";
import { addCircleOutline } from "ionicons/icons";
import { computed, onMounted, ref } from "vue";
import { useDeviceStore } from "@/stores/devices.js";
import AddDeviceModal from "@/components/modals/DevicesPage/AddDeviceModal.vue";
import EditDeviceModal from "@/components/modals/DevicesPage/EditDeviceModal.vue";
const store = useDeviceStore();
const devices = computed(() => store.devices);
const selectedDevice = ref({});
const isEditModalOpen = ref(false);
const isAddModelOpen = ref(false);

onMounted(async () => {
  await store.fetchDevices();
  devices.value = store.devices;
});
const openAddModal = () => {
  isAddModelOpen.value = true;
};
const addNewDevice = async (newDevice) => {
  console.log(newDevice);
  await store.addDevice(newDevice);
  await store.fetchDevices();
  closeModal();
};
const openEditModal = (device) => {
  selectedDevice.value = device;
  console.log(selectedDevice.value);
  isEditModalOpen.value = true;
};
const closeModal = () => {
  isEditModalOpen.value = false;
  isAddModelOpen.value = false;
};
const saveChanges = async (device) => {
  await store.saveDevice(device);
  await store.fetchDevices();
  closeModal();
};
const deleteDevice = async (selectedDevice) => {
  await store.deleteDevice(selectedDevice.id);
  await store.fetchDevices();
  closeModal();
};
</script>
<style scoped>
.device-container {
  display: flex;
  align-items: center;
  flex-wrap: wrap;
  padding-top: 5%;
}
.device-card {
  display: flex;
  align-items: center;
  justify-content: center;
  max-width: 77%;
  width: 100%;
  margin-top: auto;
  margin-left: auto;
  margin-right: auto;
  box-shadow: 2px;
  border-radius: 20px;
  font-family: monospace;
  border: solid 3px black;
}
.device-icon {
  height: 25vh;
}
p {
  font-size: larger;
  max-width: 100%;
}
</style>
