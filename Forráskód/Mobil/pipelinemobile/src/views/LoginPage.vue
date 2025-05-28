<template>
  <ion-page>
    <ion-content>
      <ion-grid class="grid">
        <ion-img
          src="/pipeLine_logo_black1.svg"
          class="background-img"
        ></ion-img>
        <ion-row>
          <ion-col size="12">
            <div v-if="loading" class="loading-container">
              <ion-img src="/pipeLine_logo_green1.svg" class="loading-img" />
            </div>
            <ion-card v-else color="secondary" class="card">
              <ion-header>
                <ion-toolbar color="success">
                  <ion-title>Welcome to PipeLine, please log in!</ion-title>
                </ion-toolbar>
              </ion-header>
              <ion-card-content>
                <form @submit.prevent="login()">
                  <ion-item>
                    <ion-input
                      v-model="email"
                      label-placement="stacked"
                      label="Email"
                      position="floating"
                      type="email"
                      placeholder="email@domain.com"
                      required
                    >
                      <ion-icon slot="start" :icon="mail"></ion-icon>
                    </ion-input>
                  </ion-item>
                  <ion-item>
                    <ion-input
                      v-model="password"
                      label-placement="stacked"
                      label="Password"
                      position="floating"
                      type="password"
                      placeholder="xxxxxxxxxx"
                      required
                    >
                      <ion-input-password-toggle
                        slot="end"
                        color="success"
                      ></ion-input-password-toggle>
                      <ion-icon slot="start" :icon="lockClosed"></ion-icon>
                    </ion-input>
                  </ion-item>
                  <ion-button expand="full" type="submit" color="success"
                    >Login</ion-button
                  >
                </form>
              </ion-card-content>
              <ion-item color="secondary">
                <ion-label slot="start">Don't have an account yet?</ion-label>
                <ion-label slot="end"
                  ><router-link to="/register" class="routerlink"
                    >Register here</router-link
                  ></ion-label
                >
              </ion-item>
            </ion-card>
          </ion-col>
        </ion-row>
      </ion-grid>
    </ion-content>
  </ion-page>
</template>
<script setup>
import {
  IonButton,
  IonCard,
  IonCardContent,
  IonCol,
  IonContent,
  IonGrid,
  IonHeader,
  IonIcon,
  IonImg,
  IonInput,
  IonInputPasswordToggle,
  IonItem,
  IonLabel,
  IonPage,
  IonRow,
  IonTitle,
  IonToolbar,
} from "@ionic/vue";
import { lockClosed, mail } from "ionicons/icons";
import { useUserStore } from "@/stores/user.js";
import { nextTick, ref } from "vue";
import { useRouter } from "vue-router";
const store = useUserStore();
const loading = ref(false);
const email = ref("");
const password = ref("");
const router = useRouter();
const login = async () => {
  loading.value = true;
  try {
    await store.login(email.value, password.value);
    router.replace("/tabs/profile");
  } catch (error) {
    //console.log(error);
    alert("Invalid Credentials!")
  } finally {
    loading.value = false;
  }
};
</script>
