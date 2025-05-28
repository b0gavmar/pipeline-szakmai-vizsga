<script setup>
import { useUserStore } from '@/stores/user'
import router from '@/router'

const userStore = useUserStore()

const logOut = async () => {
  await userStore.logout()
  router.push('/login')
}
</script>

<template>
  <div class="modal" id="modal-logOut">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header plgreen">
          <h4 class="modal-title text-dark fw-bold">Are you sure?</h4>
          <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
        </div>

        <div class="modal-body">
          <div>You will have to re-enter your credentials</div>
        </div>

        <div class="modal-footer dark">
          <button type="button" class="btn btn-success" data-bs-dismiss="modal">Cancel</button>
          <button type="button" class="btn btn-danger" data-bs-dismiss="modal" @click="logOut">
            Log out
          </button>
        </div>
      </div>
    </div>
  </div>
</template>
