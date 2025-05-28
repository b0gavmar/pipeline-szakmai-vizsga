<script setup>
import { useUserStore } from '@/stores/user'
import router from '@/router'
import { onMounted, ref } from 'vue'
import { useRoute } from 'vue-router'

const userStore = useUserStore()
const route = useRoute()

const curPw = ref('')
const newPw = ref('')

const changePw = async () => {
  userStore.changePassword(curPw.value, newPw.value)
  router.push('/profile')
}

onMounted(async () => {
  await userStore.fetchUser()
})
</script>

<template>
  <div class="modal" id="modal-changePw">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header plgreen">
          <h4 class="modal-title text-dark fw-bold">Password change</h4>
          <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
        </div>

        <div class="modal-body">
              <div class="mb-3">
                <label class="form-label">Current password</label>
                <input type="password" v-model="curPw" class="form-control" required />
              </div>

              <div class="mb-3">
                <label class="form-label">New password</label>
                <input type="password" v-model="newPw" class="form-control" required />
              </div>
        </div>

        <div class="modal-footer dark">
          <button type="button" class="btn btn-success" data-bs-dismiss="modal" @click="changePw()">
            Change password
          </button>
          <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Cancel</button>
        </div>
      </div>
    </div>
  </div>
</template>
