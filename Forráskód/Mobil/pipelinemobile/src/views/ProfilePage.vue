<template>
  <ion-page>
    <div v-if="loading" class="loading-screen">
      <ion-img src="/pipeLine_logo_black1.svg" class="loading-img"></ion-img>
    </div>
    <ion-header>
      <ion-toolbar color="light">
        <ion-item color="success">
          <ion-label
            ><h1>Welcome, {{ store.user?.firstName || "User" }}!</h1></ion-label
          >
          <ion-button @click="logout" slot="end" color="warning">
            Log Out
            <ion-icon :icon="logOutOutline"></ion-icon>
          </ion-button>
        </ion-item>
      </ion-toolbar>
    </ion-header>
    <ion-content>
      <div v-if="!loading">
        <ion-card class="profile-card">
          <ion-card-header>
            <ion-card-title class="fw-bold">Profile Information</ion-card-title>
          </ion-card-header>
          <ion-card-content>
            <ion-item>
              <ion-input
                v-model="updatedUser.firstName"
                type="text"
                label="First Name"
                label-placement="stacked"
                value=""
              ></ion-input>
            </ion-item>

            <ion-item>
              <ion-input
                v-model="updatedUser.lastName"
                label="Last Name"
                label-placement="stacked"
                type="text"
              ></ion-input>
            </ion-item>

            <ion-item>
              <ion-input
                v-model="updatedUser.email"
                label="Email Address"
                label-placement="stacked"
                type="email"
              ></ion-input>
            </ion-item>

            <ion-item>
              <ion-input
                v-model="updatedUser.phoneNumber"
                label="Phone Number"
                label-placement="stacked"
                postition="floating"
                type="text"
              ></ion-input>
            </ion-item>
            <!-- <ion-item>
              <ion-input
                v-model="updatedUser.id"
                label="User ID"
                disabled
                label-placement="stacked"
                postition="floating"
                type="text"
              ></ion-input>
            </ion-item> -->
            <ion-button @click="saveUser" expand="full" color="success"
              >Save Changes
              <ion-icon slot="start" :icon="saveOutline"></ion-icon>
            </ion-button>
            <ion-button
              @click="openPwChangeModal"
              expand="full"
              color="tertiary"
              >Change Password
              <ion-icon slot="start" :icon="keyOutline"></ion-icon>
            </ion-button>
          </ion-card-content>
        </ion-card>
        <ChangePasswordModal
          :is-open="isPasswordChangeModalOpen"
          @close="isPasswordChangeModalOpen = false"
          @changePW="changePassword"
        />
        <ion-card>
          <ion-card-header>
            <ion-card-title>Active Charging Instances</ion-card-title>
          </ion-card-header>
          <ion-card-content>
            <ion-list>
              <ion-item
                v-for="instance in instanceStore.ongoingChargingInstances"
                :key="instance.id"
              >
                <ion-label>
                  <p>
                    <strong>Connected Port's Number:</strong>
                    {{ instance.portNumber }}
                  </p>
                  <p>
                    <strong>Connected Station's Name:</strong>
                    {{ instance.chargingStationName }}
                  </p>
                  <p>
                    <strong>Connected Device's ID:</strong>
                    {{ instance.deviceId }}
                  </p>
                  <p>
                    <strong>Starting Percentage:</strong>
                    {{ instance.startingPercentage }}%
                  </p>
                  <p v-if="instance.end === null">
                    <strong>Current Percentage:</strong>
                    {{ instance.endPercentage }}%
                  </p>
                  <p v-else>
                    <strong>End Percentage:</strong>
                    {{ instance.endPercentage }}%
                  </p>

                  <p>
                    <strong>Desired End Percentage:</strong>
                    {{ instance.desiredEndPercentage }}%
                  </p>
                  <p>
                    Start of Instance:
                    {{ new Date(instance.start).toLocaleString() }}
                  </p>
                  <ion-button
                    expand="full"
                    color="warning"
                    @click="finishInstanceAlert(instance)"
                  >
                    Finish Charging Instance
                    <ion-icon slot="start" :icon="checkboxOutline"></ion-icon>
                  </ion-button>
                </ion-label>
              </ion-item>
            </ion-list>
          </ion-card-content>
        </ion-card>

        <ion-alert
          :is-open="isFinishInstanceAlertOpen"
          header="Confirm Completion"
          message="Are you sure you want to end this instance?"
          :buttons="alertButtons"
        ></ion-alert>

        <ion-card>
          <ion-card-header>
            <ion-card-title>Finished Charging Instances</ion-card-title>
          </ion-card-header>
          <ion-card-content>
            <ion-list>
              <ion-item
                v-for="instance in instanceStore.chargingInstances"
                :key="instance.id"
              >
                <ion-label>
                  <p>
                    <strong>Connected Port's Number:</strong>
                    {{ instance.portNumber }}
                  </p>
                  <p>
                    <strong>Connected Staion's Name:</strong>
                    {{ instance.chargingStationName }}
                  </p>
                  <p>
                    <strong>Connected Device's ID:</strong>
                    {{ instance.deviceId }}
                  </p>
                  <p>
                    <strong>Starting Percentage:</strong>
                    {{ instance.startingPercentage }}%
                  </p>
                  <p v-if="instance.end === null">
                    <strong>Current Percentage:</strong>
                  </p>
                  <p v-else>
                    <strong>End Percentage:</strong>
                    {{ instance.endPercentage }}%
                  </p>
                  <p>
                    <strong>Desired End Percentage:</strong>
                    {{ instance.desiredEndPercentage }}%
                  </p>
                  <p>
                    Start of Instance:
                    {{ new Date(instance.start).toLocaleString() }}
                  </p>
                  <p>
                    End of Instance:
                    {{ new Date(instance.end).toLocaleString() }}
                  </p>
                </ion-label>
              </ion-item>
            </ion-list>
          </ion-card-content>
        </ion-card>
      </div>
    </ion-content>
  </ion-page>
</template>
<script setup>
import {
  IonAlert,
  IonButton,
  IonCard,
  IonCardContent,
  IonCardHeader,
  IonCardTitle,
  IonContent,
  IonHeader,
  IonIcon,
  IonImg,
  IonInput,
  IonItem,
  IonLabel,
  IonList,
  IonPage,
  IonToolbar,
  onIonViewDidEnter,
} from "@ionic/vue";
import { useUserStore } from "@/stores/user.js";
import { useChargingInstanceStore } from "@/stores/chargingInstances.js";
import { useChargingProgressWebSocket } from "@/stores/chargingProgressWebSocket";
import {
  checkboxOutline,
  keyOutline,
  logOutOutline,
  saveOutline,
} from "ionicons/icons";
import { useRouter } from "vue-router";
import { nextTick, onMounted, ref } from "vue";
import ChangePasswordModal from "@/components/modals/ProfilePage/ChangePasswordModal.vue";
const store = useUserStore();
const instanceStore = useChargingInstanceStore();
const sockets = new Map();
const router = useRouter();
const updatedUser = ref({ ...store.user });
const loading = ref(true);
const isDark = ref(false);
const selectedInstance = ref(null);
const isFinishInstanceAlertOpen = ref(false);
const isPasswordChangeModalOpen = ref(false);

onMounted(async () => {
  await store.fetchUser();
  await instanceStore.fetchOngoingChargingInstances();
  await instanceStore.fetchFinishedChargingInstances({
    start: null,
    end: null,
  });
  initWebSockets();
  setTimeout(() => {
    loading.value = false;
  }, 2000);
  updatedUser.value = { ...store.user };
  isDark.value = document.body.classList.contains("dark");
});
onIonViewDidEnter(() => {
  initWebSockets();
});
const logout = async () => {
  await store.logout();
  router.replace("/login");
  router.go(0);
};
const saveUser = async () => {
  await store.updateUser(updatedUser.value);
};
const finishInstanceAlert = (instance) => {
  selectedInstance.value = instance;
  initWebSockets();
  isFinishInstanceAlertOpen.value = true;
};
const alertButtons = [
  {
    text: "Cancel",
    role: "cancel",
    handler: () => {
      isFinishInstanceAlertOpen.value = false;
      selectedInstance.value = null;
    },
  },
  {
    text: "Finish",
    role: "confirm",
    handler: () => {
      finishInstance();
    },
  },
];
const initWebSockets = () => {
  instanceStore.ongoingChargingInstances.forEach((ci) => {
    if (!sockets.has(ci.id)) {
      const socket = useChargingProgressWebSocket(ci.id, (data) => {
        instanceStore.fetchOngoingChargingInstances();
      });
      sockets.set(ci.id, socket);
    }
  });
};
const finishInstance = async () => {
  if (!selectedInstance.value) {
    return;
  }
  await instanceStore.finishChargingInstance(selectedInstance.value);
  await nextTick();
  await instanceStore.fetchOngoingChargingInstances();
  await instanceStore.fetchFinishedChargingInstances({
    start: null,
    end: null,
  });
  const socket = sockets.get(selectedInstance.value.id);
  if (socket) {
    socket.close();
    sockets.delete(selectedInstance.value.id);
  }
  isFinishInstanceAlertOpen.value = false;
  selectedInstance.value = null;
};
const openPwChangeModal = () => {
  isPasswordChangeModalOpen.value = true;
};
const changePassword = async (currentPw, newPw) => {
  try {
    await store.changePassword(currentPw, newPw);
  } catch (error) {
    alert(`There was a problem setting your password: ${error}`);
  }
};
</script>
