<template>
  <ion-app>
    <ion-router-outlet />
  </ion-app>
</template>

<script setup lang="js">
import { IonApp, IonRouterOutlet } from '@ionic/vue';
</script>
<style>
.loading-img {
  animation: fadeInOut 2.5s ease-in-out infinite;
}
@keyframes fadeInOut {
  0% {
    opacity: 0;
  }
  50% {
    opacity: 1;
  }
  100% {
    opacity: 0;
  }
}
.loading-screen {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1;
}
.grid {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100vh;
}
.background-img {
  position: absolute;
  width: 170%;
  height: 120%;
  opacity: 0.4;
  z-index: 0;
  animation: fadeInOut 3s infinite;
}
.loading-img {
  animation: fadeInOut 1.5s infinite;
}
.card {
  z-index: 1;
}
.routerlink{
  color: white;
  font-style: italic;
}
</style>
