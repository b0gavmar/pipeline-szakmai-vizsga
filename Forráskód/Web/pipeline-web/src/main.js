import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/js/bootstrap.bundle.min.js'
import 'bootstrap-icons/font/bootstrap-icons.css'
import '@/assets/main.css'

import 'vue-toastification/dist/index.css'
import '@maptiler/sdk/dist/maptiler-sdk.css'

import Toast from 'vue-toastification'

const app = createApp(App)

app.use(createPinia())
app.use(router)

app.use(Toast, {
  position: 'top-right',
  timeout: 2000,
  closeOnClick: true,
  pauseOnHover: true,
})

app.mount('#app')
