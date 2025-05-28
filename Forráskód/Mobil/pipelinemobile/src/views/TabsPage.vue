<template>
  <ion-page>
    <ion-tabs>
      <ion-router-outlet />
      <ion-tab-bar slot="bottom">
        <ion-tab-button tab="profile" href="/tabs/profile">
          <ion-icon :icon="person" />
        </ion-tab-button>
        <ion-tab-button tab="map" href="/tabs/map">
          <ion-icon :icon="map" />
        </ion-tab-button>
        <ion-tab-button tab="devices" href="/tabs/devices">
          <ion-icon :icon="bicycle" />
        </ion-tab-button>
      </ion-tab-bar>
    </ion-tabs>
  </ion-page>
</template>

<script setup>
import { IonTabs, IonTabBar, IonTabButton, IonRouterOutlet } from "@ionic/vue";
import { bicycle, map, person } from "ionicons/icons";
</script>
<style scoped>
ion-tab-button {
  --color-selected: white;
}
</style>
