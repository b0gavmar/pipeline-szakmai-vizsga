<template>
  <ion-modal :is-open="isOpen" @didDismiss="emitClose">
    <ion-content>
      <ion-grid class="grid">
        <ion-row>
          <ion-col size="12" size-md="6" size-lg="4">
            <ion-card color="dark">
              <ion-header>
                <ion-toolbar color="success">
                  <ion-title>Add Device</ion-title>
                  <ion-buttons slot="end">
                    <ion-button @click="emitClose">Close</ion-button>
                  </ion-buttons>
                </ion-toolbar>
              </ion-header>
              <ion-card-content>
                <ion-item color="dark">
                  <ion-select
                    v-model="newDevice.deviceType"
                    label="DeviceType"
                    label-placement="stacked"
                    required
                    interface="action-sheet"
                  >
                    <ion-select-option
                      v-for="(label, value) in deviceTypes"
                      :key="value"
                      :value="Number(value)"
                      >{{ label }}</ion-select-option
                    >
                  </ion-select>
                </ion-item>
                <ion-item color="dark" v-if="newDevice.deviceType == 0">
                  <ion-select
                    v-model="newDevice.detachableBattery"
                    label="Its battery can be detached:"
                    label-placement="stacked"
                    interface="action-sheet"
                    required
                  >
                    <ion-select-option :value="true">Yes</ion-select-option>
                    <ion-select-option :value="false">No</ion-select-option>
                  </ion-select>
                </ion-item>
                <ion-item color="dark" v-if="newDevice.deviceType == 1">
                  <ion-select
                    v-model="newDevice.isFoldable"
                    label="It can be folded:"
                    label-placement="stacked"
                    interface="action-sheet"
                    required
                  >
                    <ion-select-option :value="true">Yes</ion-select-option>
                    <ion-select-option :value="false">No</ion-select-option>
                  </ion-select>
                </ion-item>
                <ion-item color="dark" v-if="newDevice.deviceType == 2">
                  <ion-select
                    v-model="newDevice.canBeLocked"
                    label="It can be locked remotely:"
                    label-placement="stacked"
                    interface="action-sheet"
                    required
                  >
                    <ion-select-option :value="true">Yes</ion-select-option>
                    <ion-select-option :value="false">No</ion-select-option>
                  </ion-select>
                </ion-item>
                <ion-item color="dark">
                  <ion-input
                    v-model="newDevice.name"
                    label="Name"
                    maxlength="15"
                    label-placement="stacked"
                    postition="floating"
                  ></ion-input>
                </ion-item>
                <ion-item color="dark">
                  <ion-input
                    v-model="newDevice.manufacturer"
                    label="Manufacturer"
                    maxlength="10"
                    label-placement="stacked"
                    postition="floating"
                    required
                  ></ion-input>
                </ion-item>
                <ion-item color="dark">
                  <ion-input
                    v-model="newDevice.model"
                    label="Model"
                    maxlength="25"
                    label-placement="stacked"
                    postition="floating"
                    required
                  ></ion-input>
                </ion-item>
                <ion-item color="dark">
                  <ion-input
                    v-model="newDevice.batteryCapacity"
                    label="Battery Capacity (mAh)"
                    label-placement="stacked"
                    postition="floating"
                    required
                  ></ion-input>
                </ion-item>
                <ion-item color="dark">
                  <ion-input
                    v-model="newDevice.batteryVoltage"
                    label="Battery Voltage (V)"
                    label-placement="stacked"
                    postition="floating"
                    required
                  ></ion-input>
                </ion-item>
                <ion-item color="dark">
                  <ion-input
                    v-model="newDevice.maxChargingSpeed"
                    label="Max Charging Speed (kWh)"
                    label-placement="stacked"
                    postition="floating"
                    required
                  ></ion-input>
                </ion-item>
              </ion-card-content>
              <ion-button expand="full" color="success" @click="addDevice"
                >Add Device</ion-button
              >
            </ion-card>
          </ion-col>
        </ion-row>
      </ion-grid>
    </ion-content>
  </ion-modal>
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
  IonSelect,
  IonSelectOption,
} from "@ionic/vue";
import { ref } from "vue";

const props = defineProps({
  isOpen: Boolean,
});

const newDevice = ref({
  name: "",
  manufacturer: "",
  model: "",
  deviceType: 0,
  batteryVoltage: 0,
  batteryCapacity: 0,
  maxChargingSpeed: 0,
  detachableBattery: null,
  isFoldable: null,
  canBeLocked: null,
});
const deviceTypes = {
  0: "EBike",
  1: "EScooter",
  2: "ESkateboard",
};

const emit = defineEmits("close", "addDevice");
const emitClose = () => {
  emit("close");
  newDevice.value = {
    name: "",
    manufacturer: "",
    model: "",
    deviceType: 0,
    batteryVoltage: 0,
    batteryCapacity: 0,
    maxChargingSpeed: 0,
    detachableBattery: null,
    isFoldable: null,
    canBeLocked: null,
  };
};
const addDevice = () => emit("addDevice", newDevice.value);
</script>
