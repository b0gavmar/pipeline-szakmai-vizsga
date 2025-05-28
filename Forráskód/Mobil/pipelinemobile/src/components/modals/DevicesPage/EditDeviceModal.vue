<template>
  <ion-modal :is-open="isOpen" @didDismiss="emitClose">
    <ion-content>
      <ion-grid class="grid">
        <ion-row>
          <ion-col size="12" size-md="6" size-lg="4">
            <ion-card>
              <ion-header>
                <ion-toolbar color="success">
                  <ion-title>Modify Device</ion-title>
                  <ion-buttons slot="end">
                    <ion-button @click="emitClose">Close</ion-button>
                  </ion-buttons>
                </ion-toolbar>
              </ion-header>
              <ion-card-content>
                <ion-item>
                  <ion-input
                    v-model="localSelectedDevice.name"
                    label="Name"
                    maxlength="15"
                    label-placement="stacked"
                    postition="floating"
                  ></ion-input>
                </ion-item>
                <ion-item>
                  <ion-input
                    v-model="localSelectedDevice.manufacturer"
                    label="Manufacturer"
                    label-placement="stacked"
                    postition="floating"
                    maxlength="10"
                  ></ion-input>
                </ion-item>
                <ion-item>
                  <ion-input
                    v-model="localSelectedDevice.model"
                    label="Model"
                    label-placement="stacked"
                    postition="floating"
                    maxlength="25"
                  ></ion-input>
                </ion-item>
                <ion-item>
                  <ion-input
                    v-model="localSelectedDevice.batteryVoltage"
                    label="Battery Voltage (V)"
                    label-placement="stacked"
                    postition="floating"
                  ></ion-input>
                </ion-item>
                <ion-item>
                  <ion-input
                    v-model="localSelectedDevice.batteryCapacity"
                    label="Battery Capacity (mAh)"
                    label-placement="stacked"
                    postition="floating"
                  ></ion-input>
                </ion-item>
                <ion-item>
                  <ion-input
                    v-model="localSelectedDevice.maxChargingSpeed"
                    label="Max Charging Speed (kWh)"
                    label-placement="stacked"
                    postition="floating"
                  ></ion-input>
                </ion-item>
                <ion-item>
                  <ion-input
                    v-model="localSelectedDevice.id"
                    disabled
                    label="ID"
                    label-placement="stacked"
                    postition="floating"
                  ></ion-input>
                </ion-item>
                <ion-item v-if="localSelectedDevice.deviceType == 0">
                  <ion-select
                    v-model="localSelectedDevice.detachableBattery"
                    label="Its battery can be detached:"
                    label-placement="stacked"
                    interface="action-sheet"
                    required
                  >
                    <ion-select-option :value="true">Yes</ion-select-option>
                    <ion-select-option :value="false">No</ion-select-option>
                  </ion-select>
                </ion-item>
                <ion-item v-if="localSelectedDevice.deviceType == 1">
                  <ion-select
                    v-model="localSelectedDevice.isFoldable"
                    label="It can be folded:"
                    label-placement="stacked"
                    interface="action-sheet"
                    required
                  >
                    <ion-select-option :value="true">Yes</ion-select-option>
                    <ion-select-option :value="false">No</ion-select-option>
                  </ion-select>
                </ion-item>
                <ion-item v-if="localSelectedDevice.deviceType == 2">
                  <ion-select
                    v-model="localSelectedDevice.canBeLocked"
                    label="It can be locked remotely:"
                    label-placement="stacked"
                    interface="action-sheet"
                    required
                  >
                    <ion-select-option :value="true">Yes</ion-select-option>
                    <ion-select-option :value="false">No</ion-select-option>
                  </ion-select>
                </ion-item>
              </ion-card-content>
              <ion-button expand="full" color="success" @click="saveDevice"
                >Save Changes</ion-button
              >
            </ion-card>
            <br />

            <ion-button expand="full" color="danger" @click="deleteDeviceAlert"
              >Delete Device Permanently</ion-button
            >
          </ion-col>
        </ion-row>
      </ion-grid>
    </ion-content>
  </ion-modal>

  <ion-alert
    :is-open="isAlertOpen"
    header="Confirm Deletion"
    message="Are you sure you want to delete this device? This action is irreversible!"
    :buttons="alertButtons"
  ></ion-alert>
</template>
<script setup>
import {
  IonToolbar,
  IonButton,
  IonButtons,
  IonHeader,
  IonCard,
  IonContent,
  IonCardContent,
  IonModal,
  IonTitle,
  IonItem,
  IonInput,
  IonGrid,
  IonRow,
  IonCol,
  IonAlert,
  IonSelect,
  IonSelectOption,
} from "@ionic/vue";
import { ref, watch } from "vue";

const props = defineProps({
  isOpen: Boolean,
  selectedDevice: Object,
});
const localSelectedDevice = ref({ ...props.selectedDevice });
const isAlertOpen = ref(false);
watch(
  () => props.selectedDevice,
  (newVal) => {
    localSelectedDevice.value = { ...newVal };
    //console.log(localSelectedDevice.value);
  },
  { immediate: true, deep: true }
);

const alertButtons = [
  {
    text: "Cancel",
    role: "cancel",
    handler: () => {
      isAlertOpen.value = false;
    },
  },
  {
    text: "Delete",
    role: "destructive",
    color: "danger",
    handler: () => {
      deleteDevice();
      isAlertOpen.value = false;
    },
  },
];
const deleteDeviceAlert = () => {
  isAlertOpen.value = true;
};
const emit = defineEmits(["close", "saveDevice", "deleteDevice"]);

const emitClose = () => emit("close");
const saveDevice = () => emit("saveDevice", localSelectedDevice.value);
const deleteDevice = () => emit("deleteDevice", localSelectedDevice.value);
</script>
