<script setup>
import { useDeviceStore } from '@/stores/device'
import router from '@/router'

import { useRoute } from 'vue-router'

const deviceStore = useDeviceStore()
const route = useRoute()

const deleteDevice = async()=>{
  await deviceStore.deleteDevice(route.params.deviceId)
  router.push(`/devices`)
}

</script>

<template>
  <div class="modal" id="modal-deleteDevice">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header plgreen">
          <h4 class="modal-title text-dark">Are you sure?</h4>
          <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
        </div>

        <div class="modal-body">
          <div>
            This will permanently delete the device and affect the charging history associated with it!
          </div>
        </div>

        <div class="modal-footer dark">
          <button type="button" class="btn btn-success" data-bs-dismiss="modal">
            Cancel
          </button>
          <button type="button" class="btn btn-danger" data-bs-dismiss="modal" @click="deleteDevice">Delete</button>
        </div>
      </div>
    </div>
  </div>
</template>
