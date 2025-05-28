<script setup>
import Navbar from '@/components/Navbar.vue'
import { onMounted } from 'vue'
import { RouterView } from 'vue-router'
import { useUserStore } from './stores/user'

const userStore = useUserStore()

onMounted(async () => {
  await userStore.checkAuth()
  await userStore.fetchUser()
})
</script>

<template>
  <Navbar />
  <RouterView />
</template>
