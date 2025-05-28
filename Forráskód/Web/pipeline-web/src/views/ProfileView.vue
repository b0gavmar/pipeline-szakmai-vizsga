<script setup>
import { useUserStore } from '@/stores/user'
import { ref, onMounted } from 'vue'
import LogOutModal from '@/components/_modals/LogOutModal.vue'
import ChangePwModal from '@/components/_modals/ChangePasswordModal.vue'

const userStore = useUserStore()

const updatedUser = ref({})

onMounted(async () => {
  await userStore.fetchUser()

   if (!userStore.user) {
    userStore.logout()
  } else {
    updatedUser.value = {
      id: userStore.user.id,
      firstName: userStore.user.firstName,
      lastName: userStore.user.lastName,
      userName: userStore.user.userName,
      email: userStore.user.email,
      normalizedEmail: userStore.user.email.toUpperCase(),
      phoneNumber: userStore.user.phoneNumber,
    }
  }
});

const saveUser = async () => {
  await userStore.updateUser(updatedUser.value)
}
</script>

<template>
  <main class="container d-flex justify-content-center align-items-center">
    <div v-if="isLoading">Loading profile...</div>

    <div v-else class="col-12 col-md-6">
      <div class="card2 p-4">
        <h3 class="fw-bold text-center">Profile Information</h3>

        <div class="mb-3">
          <label class="form-label fs-5">First Name</label>
          <input type="text" v-model="updatedUser.firstName" class="form-control" />
        </div>

        <div class="mb-3">
          <label class="form-label fs-5">Last Name</label>
          <input type="text" v-model="updatedUser.lastName" class="form-control" />
        </div>

        <div class="mb-3">
          <label class="form-label fs-5">Email</label>
          <input type="email" v-model="updatedUser.email" class="form-control" />
        </div>

        <div class="mb-3">
          <label class="form-label fs-5">Phone Number</label>
          <input type="text" v-model="updatedUser.phoneNumber" class="form-control" />
        </div>

        <button
          class="btn btn-info w-100 mb-3"
          data-bs-toggle="modal"
          data-bs-target="#modal-changePw"
        >
          <i class="bi bi-key"></i> Change password
        </button>
        <button class="btn btn-success w-100 mb-3" @click="saveUser">
          <i class="bi bi-save"></i> Save Profile
        </button>
        <button class="btn btn-danger w-100" data-bs-toggle="modal" data-bs-target="#modal-logOut">
          <i class="bi bi-box-arrow-right"></i> Log out
        </button>
      </div>
    </div>
  </main>

  <LogOutModal />
  <ChangePwModal />
</template>
