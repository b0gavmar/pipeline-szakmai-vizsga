<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useUserStore } from '@/stores/user'
import { useToast } from 'vue-toastification'

const toast = useToast()

const userStore = useUserStore()
const router = useRouter()

const email = ref('')
const password = ref('')
const stayLoggedIn = ref(true)

const loginFunction = async () => {
  try {
    await userStore.login(email.value, password.value, stayLoggedIn.value)
    router.push('/profile')
  } catch (ex) {
    toast.error('The email and password do not match')
  }
}
</script>

<template>
  <div class="container d-flex justify-content-center align-items-center">
    <div class="col-12 col-md-6 col-lg-5">
      <div class="card2 shadow-lg text-center">
        <h2 class="fw-bold text-light mb-3">Welcome back!</h2>

        <form @submit.prevent="loginFunction" class="d-flex flex-column gap-3 w-100">
          <input type="email" v-model="email" placeholder="Email" required class="form-control" />
          <input type="password" v-model="password" placeholder="Password" class="form-control" />
          <div class="form-check d-flex">
            <input
              type="checkbox"
              v-model="stayLoggedIn"
              class="form-check-input"
              id="stayloggedin"
            />
            <label class="form-check-label px-1" for="stayloggedin"> Keep me logged in</label>
          </div>
          <button type="submit" class="btn btn-success w-100">Login</button>
        </form>

        <p class="text-center mt-3 mb-0 text-light">
          Don't have an account?
          <router-link to="/register" class="text-info fw-bold">Register here</router-link>
        </p>
      </div>
    </div>
  </div>

  <div></div>
</template>
