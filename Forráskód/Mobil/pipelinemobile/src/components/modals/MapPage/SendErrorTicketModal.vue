<template>
  <ion-modal :is-open="isOpen" @didDismiss="emitClose">
    <ion-content>
      <ion-card>
        <ion-header>
          <ion-toolbar color="success">
            <ion-title>Make a Ticket</ion-title>
            <ion-buttons slot="end">
              <ion-button @click="emitClose">Close</ion-button>
            </ion-buttons>
          </ion-toolbar>
        </ion-header>
        <ion-card-content>
          <ion-label>List of Tickets:</ion-label>
          <ion-item v-for="ticket in tickets">
            Ticket: {{ ticket.description }}
          </ion-item>
          <br />
          <ion-item color="light">
            <ion-label position="stacked">Description</ion-label>
            <ion-textarea
              v-model="localTicket.description"
              auto-grow
              rows="5"
              required
            ></ion-textarea>
          </ion-item>
          <ion-button @click="emitSendTicket(newTicket)" color="primary"
            >Make New Ticket</ion-button
          >
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
  IonTextarea,
  IonModal,
  IonTitle,
  IonToolbar,
} from "@ionic/vue";
import { ref, watch } from "vue";

const props = defineProps({
  isOpen: Boolean,
  newTicket: Object,
  tickets: Array,
});
const localTicket = ref({ ...props.newTicket });

watch(
  () => props.newTicket,
  (newVal) => {
    localTicket.value = { ...newVal };
    //console.log(localTicket.value);
  },
  { immediate: true, deep: true }
);

const emit = defineEmits(["close", "sendTicket"]);

const emitClose = () => emit("close");
const emitSendTicket = () => emit("sendTicket", localTicket.value);
</script>
