<template>
  <ion-modal
    :is-open="isOpen"
    @didDismiss="emitClose"
    :breakpoints="[0, 0.5, 0.8]"
    :initial-breakpoint="0.5"
    handle-behavior="cycle"
    interface="action-sheet"
  >
    <ion-content color="">
      <ion-card color="success">
        <ion-card-header>
          <ion-card-title>Name: {{ station?.name }}</ion-card-title>
          <ion-card-subtitle>Address: {{ station?.address }}</ion-card-subtitle>
        </ion-card-header>
        <ion-card-content>
          <ion-list v-if="ports.length > 0">
            <ion-item v-for="port in ports" :key="port.id">
              <ion-label>
                <h2>Port Number {{ port.portNumber }}</h2>
                <p>
                  {{ port.isBeingUsed || port.isCharging ? "In Use" : "Free" }}
                </p>
                <p>Status: {{ port.isDisabled ? "Offline" : "Online" }}</p>
              </ion-label>
              <ion-button
                v-if="!port.isBeingUsed && !port.isDisabled && !port.isCharging"
                color="secondary"
                @click="selectPort(port)"
                >Select</ion-button
              >
            </ion-item>
          </ion-list>
          <ion-item v-else>
            <i>No ports to show...</i>
          </ion-item>
          <ion-button @click="emitErrorTicket(station)" color="danger">
            <ion-icon slot="start" :icon="ticketOutline"></ion-icon>
            Send Error Ticket
          </ion-button>
        </ion-card-content>
      </ion-card>
    </ion-content>
  </ion-modal>
</template>

<script setup>
import { ticketOutline } from "ionicons/icons";
import {
  IonButton,
  IonCard,
  IonCardContent,
  IonContent,
  IonItem,
  IonLabel,
  IonList,
  IonModal,
  IonIcon,
  IonCardTitle,
  IonCardSubtitle,
  IonCardHeader,
} from "@ionic/vue";
const props = defineProps({
  isOpen: Boolean,
  station: Object,
  ports: Array,
});

const emit = defineEmits(["close", "selectPort", "errorTicket"]);

const emitClose = () => emit("close");
const selectPort = (port) => emit("selectPort", port);
const emitErrorTicket = () => emit("errorTicket", props.station);
</script>
