<template>
  <ion-page>
    <ion-content>
      <ion-grid class="grid">
        <ion-img
          src="/pipeLine_logo_black1.svg"
          class="background-img"
        ></ion-img>
        <ion-row>
          <ion-col size="12" size-md="6" size-lg="4">
            <div v-if="loading" class="loading-container">
              <ion-img src="/pipeLine_logo_green1.svg" class="loading-img" />
            </div>
            <ion-card v-else color="secondary">
              <ion-header>
                <ion-toolbar color="success">
                  <ion-title>Register an account for PipeLine</ion-title>
                </ion-toolbar>
              </ion-header>
              <ion-card-content>
                <form @submit.prevent="register">
                  <ion-item>
                    <ion-input
                      v-model="email"
                      label-placement="stacked"
                      label="Email"
                      postion="floating"
                      type="email"
                      placeholder="ex:email@domain.com"
                    >
                      <ion-icon slot="start" :icon="mail"></ion-icon>
                    </ion-input>
                  </ion-item>
                  <ion-item>
                    <ion-input
                      v-model="password"
                      label-placement="stacked"
                      label="Password"
                      postion="floating"
                      type="password"
                      placeholder="ex:P@ssw0rd!"
                    >
                      <ion-input-password-toggle
                        slot="end"
                        color="success"
                      ></ion-input-password-toggle>
                      <ion-icon slot="start" :icon="lockClosed"></ion-icon>
                    </ion-input>
                  </ion-item>
                  <ion-button expand="full" type="submit" color="success"
                    >Register</ion-button
                  >
                </form>
              </ion-card-content>
              <ion-item color="secondary">
                <ion-label slot="start">Already have an account?</ion-label>
                <ion-label slot="end"
                  ><router-link to="/login" class="routerlink">Login here</router-link></ion-label
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
const email = ref("");
const password = ref("");
const router = useRouter();
const loading = ref(false);
const register = async () => {
  loading.value = true;
  try {
    await store.register(email.value, password.value);
    await store.checkAuth();
    await nextTick();
    router.replace("/tabs/profile");
  } catch (error) {
    alert("Register failed! \nPlease check if your password contains at least 1 uppercase character, 1 lowercase character, 1 numeric character, and 1 special character!")
  } finally{
    loading.value = false;
  }
};
</script>
