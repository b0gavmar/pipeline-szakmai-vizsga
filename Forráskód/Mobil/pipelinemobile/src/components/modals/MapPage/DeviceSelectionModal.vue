<template>
  <ion-modal :is-open="isOpen" @didDismiss="emitClose">
    <ion-content>
      <ion-card>
        <ion-header>
          <ion-toolbar>
            <ion-title>Choose a Device</ion-title>
            <ion-buttons slot="end">
              <ion-button @click="emitClose">Close</ion-button>
            </ion-buttons>
          </ion-toolbar>
        </ion-header>
        <ion-card-content>
          <ion-list>
            <ion-item
              v-for="device in devices"
              :key="device.id"
              @click="selectDevice(device)"
            >
              <ion-label>
                <h2>{{ device.name }}</h2>
                <p>{{ device.manufacturer }} - {{ device.model }}</p>
              </ion-label>
            </ion-item>
          </ion-list>
        </ion-card-content>
      </ion-card>
    </ion-content>
  </ion-modal>
</template>

<script setup>
import {
  IonButton,
  IonButtons,
  IonCard,
  IonCardContent,
  IonContent,
  IonHeader,
  IonItem,
  IonLabel,
  IonList,
  IonModal,
  IonTitle,
  IonToolbar,
} from "@ionic/vue";
const props = defineProps({
  isOpen: Boolean,
  devices: Array,
});
const emit = defineEmits(["close", "selectDevice"]);

const emitClose = () => emit("close");
const selectDevice = (device) => emit("selectDevice", device);
</script>
