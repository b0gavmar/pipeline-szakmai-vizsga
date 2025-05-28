import { defineStore } from 'pinia'
import { ref, watch } from 'vue'

export const useThemeStore = defineStore('theme', () => {
  const mode = ref('dark')

  watch(
    mode,
    (newMode) => {
      document.body.classList.remove('light-mode')
      document.body.classList.add(`${newMode}`)
    },
    { immediate: true },
  )

  const toggleTheme = () => {
    mode.value = mode.value === 'dark' ? 'light-mode' : 'dark'
  }

  return { mode, toggleTheme }
})
