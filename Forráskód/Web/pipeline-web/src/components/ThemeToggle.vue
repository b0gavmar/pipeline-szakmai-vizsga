<template>
  <button @click="theme.toggleTheme()" :class="theme.mode === 'dark' ? 'btn btn-dark' : 'btn btn-light'">
    <div :class="theme.mode === 'dark' ? 'bi-moon-fill' : 'bi-sun-fill'"></div>
  </button>
</template>

<script setup>
import { useThemeStore } from '@/stores/theme'
const theme = useThemeStore()
</script>
