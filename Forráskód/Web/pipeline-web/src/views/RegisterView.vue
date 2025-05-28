<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useUserStore } from '@/stores/user'

const userStore = useUserStore()
const router = useRouter()

const email = ref('')
const password = ref('')

const registerFunction = async () => {
  await userStore.register(email.value, password.value)
  if(userStore.user){
  router.push('/profile')
  }
}
</script>

<template>
  <div class="container d-flex justify-content-center align-items-center">
    <div class="col-12 col-md-6 col-lg-5">
      <div class="card2 shadow-lg text-center">
        <h2 class="fw-bold text-light mb-3">Register</h2>

        <form @submit.prevent="registerFunction" class="d-flex flex-column gap-3 w-100">
          <input type="email" v-model="email" placeholder="Email" required class="form-control" />
          <input
            type="password"
            v-model="password"
            placeholder="Password"
            required
            class="form-control"
          />
          <button type="submit" class="btn btn-success w-100">Register</button>
        </form>

        <p class="text-light mt-3">
          Already have an account?
          <router-link to="/login" class="text-primary fw-bold">Login here</router-link>
        </p>
      </div>
    </div>
  </div>
  <div></div>
</template>

