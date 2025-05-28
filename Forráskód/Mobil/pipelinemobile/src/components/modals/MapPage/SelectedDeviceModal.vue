<template>
  <ion-modal :is-open="isOpen" @didDismiss="emitClose">
    <ion-content>
      <ion-grid class="grid">
        <ion-row>
          <ion-col size="12" size-md="6" size-lg="4">
            <ion-item color="danger">
              <ion-label
                ><strong
                  >Trying to input invalid values into the percentage inputs
                  will automatically set starting percentage to 90%, and desired
                  end percentage to 100%.</strong
                ></ion-label
              >
            </ion-item>
            <ion-card>
              <ion-header>
                <ion-toolbar color="success">
                  <ion-title>Device Information</ion-title>
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
                    maxlength="13"
                    label-placement="stacked"
                    postition="floating"
                    disabled
                  ></ion-input>
                </ion-item>
                <ion-item>
                  <ion-input
                    v-model="localSelectedDevice.manufacturer"
                    label="Manufacturer"
                    label-placement="stacked"
                    postition="floating"
                    disabled
                  ></ion-input>
                </ion-item>
                <ion-item>
                  <ion-input
                    v-model="localSelectedDevice.model"
                    label="Model"
                    label-placement="stacked"
                    postition="floating"
                    disabled
                  ></ion-input>
                </ion-item>
                <ion-item>
                  <ion-input
                    v-model="localSelectedDevice.id"
                    label="ID"
                    label-placement="stacked"
                    postition="floating"
                    disabled
                  ></ion-input>
                </ion-item>
                <ion-item>
                  <ion-input
                    type="number"
                    v-model="localInstance.startingPercentage"
                    label="Starting Percentage"
                    label-placement="stacked"
                    postition="floating"
                    min="0"
                    max="90"
                    required
                  ></ion-input>
                </ion-item>
                <ion-item>
                  <ion-input
                    type="number"
                    v-model="localInstance.desiredEndPercentage"
                    label="Desired End Percentage"
                    label-placement="stacked"
                    postition="floating"
                    min="0"
                    max="100"
                    required
                  ></ion-input>
                </ion-item>
                <ion-button color="success" @click="startInstance"
                  >Confirm</ion-button
                >
              </ion-card-content>
            </ion-card>
          </ion-col>
        </ion-row>
      </ion-grid>
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
  IonInput,
  IonModal,
  IonTitle,
  IonToolbar,
  IonLabel,
  IonGrid,
  IonRow,
  IonCol,
} from "@ionic/vue";
import { ref, watch } from "vue";
const props = defineProps({
  isOpen: Boolean,
  selectedDevice: Object,
  newInstance: Object,
});
const localSelectedDevice = ref({ ...props.selectedDevice });
const localInstance = ref({ ...props.newInstance });

watch(
  () => props.selectedDevice,
  (newVal) => {
    localSelectedDevice.value = { ...newVal };
    //console.log(localSelectedDevice.value);
  },
  { immediate: true, deep: true }
);
watch(
  () => props.newInstance,
  (newVal) => {
    localInstance.value = { ...newVal };
    //console.log(localInstance.value);
  },
  { immediate: true, deep: true }
);
watch(
  () => localInstance.value.startingPercentage,
  (newValue) => {
    if (newValue > 90) {
      localInstance.value.startingPercentage = 90;
    } else if (newValue < 0) {
      localInstance.value.startingPercentage = 0;
    }
  }
);

watch(
  () => localInstance.value.desiredEndPercentage,
  (newValue) => {
    if (newValue > 100) {
      localInstance.value.desiredEndPercentage = 100;
    } else if (newValue < localInstance.value.startingPercentage) {
      localInstance.value.desiredEndPercentage =
        localInstance.value.startingPercentage + 1;
    }
  }
);

const emit = defineEmits(["close", "startInstance"]);
const emitClose = () => emit("close");
const startInstance = () => emit("startInstance", localInstance.value);
</script>
