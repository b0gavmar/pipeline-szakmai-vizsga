<script setup>
import { RouterLink } from 'vue-router'
import { useUserStore } from '@/stores/user'
import ThemeToggle from '@/components/ThemeToggle.vue'
import logo from '@/assets/pipeLine_logo_black1.png'

const userStore = useUserStore()

const shouldShow = () => userStore.user === null
</script>

<template>
  <nav class="navbar navbar-expand-lg plgreen">
    <div class="container">
      <ThemeToggle />
      <RouterLink
        class="navbar-brand d-flex align-items-center justify-content-center fw-bold text-uppercase"
        to="/"
      >
        <img :src="logo" alt="PipeLine Logo" class="logo me-3" />
        <span class="fs-2 text-dark">PipeLine</span>
      </RouterLink>
      <button
        class="navbar-toggler"
        type="button"
        data-bs-toggle="collapse"
        data-bs-target="#navbarNav"
      >
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarNav">
        <ul class="navbar-nav ms-auto">
          <li class="nav-item"><RouterLink class="nav-link" to="/">Home</RouterLink></li>
          <li class="nav-item"><RouterLink class="nav-link" to="/about">About</RouterLink></li>
          <li class="nav-item">
            <RouterLink class="nav-link" to="/chargingstations">Charging Stations</RouterLink>
          </li>
          <li class="nav-item" v-if="!shouldShow()">
            <RouterLink class="nav-link" to="/devices">Devices</RouterLink>
          </li>
          <li class="nav-item" v-if="!shouldShow()">
            <RouterLink class="nav-link" to="/charginginstances">Charging Instances</RouterLink>
          </li>
          <li class="nav-item" v-if="!shouldShow()">
            <RouterLink class="nav-link" to="/profile">Profile</RouterLink>
          </li>
          <li class="nav-item navlogin" v-if="shouldShow()">
            <RouterLink class="nav-link" to="/login">Login</RouterLink>
          </li>
        </ul>
      </div>
    </div>
  </nav>
</template>

<style scoped>
.container {
  min-height: unset !important;
}

.navbar {
  padding: 0.5rem;
}

.logo {
  height: 50px;
}

.navbar-nav .nav-link {
  font-size: 1.4rem;
  font-weight: bold;
  color: black;
  padding: 8px 15px;
  transition:
    color 0.3s ease-in-out,
    transform 0.2s ease-in-out;
}

.navbar-nav .nav-link:hover {
  color: white;
  background-color: rgba(0, 0, 0, 0.788);
  border-radius: 8px;
  transform: scale(1.01);
}

.navbar-nav .nav-link:focus {
  color: white;
  background-color: rgb(0, 0, 0);
  border-radius: 8px;
  transform: scale(1.05);
}

.navbar-nav .router-link-active {
  background-color: rgba(0, 0, 0, 0.788);
  color: white !important;
  border-radius: 8px;
}
</style>
