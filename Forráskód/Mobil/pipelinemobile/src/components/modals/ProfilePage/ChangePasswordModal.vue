<template>
  <ion-modal :is-open="isOpen" @didDismiss="emitClose">
    <ion-content>
      <ion-card>
        <ion-header>
          <ion-toolbar>
            <ion-title>Reset Password</ion-title>
            <ion-buttons slot="end">
              <ion-button @click="emitClose">Return</ion-button>
            </ion-buttons>
          </ion-toolbar>
        </ion-header>
        <ion-card-content>
          <ion-item>
            <ion-input
              type="password"
              v-model="currentPw"
              label="Current Password"
              label-placement="stacked"
              postition="floating"
              placeholder="xxxxxxxxxx"
            ></ion-input>
          </ion-item>
          <ion-item>
            <ion-input
              type="password"
              v-model="newPw"
              label="New Password"
              label-placement="stacked"
              position="floating"
              placeholder="xxxxxxxxxxx"
            ></ion-input>
          </ion-item>
          <ion-button
            @click="changePw(currentPw, newPw)"
            expand="block"
            color="danger"
          >
            Change Password
          </ion-button>
        </ion-card-content>
      </ion-card>
    </ion-content>
  </ion-modal>
</template>
<script setup>
import { ref } from "vue";
import {
  IonButton,
  IonButtons,
  IonCard,
  IonCardContent,
  IonContent,
  IonHeader,
  IonItem,
  IonInput,
  IonModal,
  IonTitle,
  IonToolbar,
} from "@ionic/vue";
const currentPw = ref("");
const newPw = ref("");

const props = defineProps({
  isOpen: Boolean,
});

const emit = defineEmits(["close", "changePW"]);

const emitClose = () => emit("close");
const changePw = (currentPw, newPw) => emit("changePW", currentPw, newPw);
</script>
