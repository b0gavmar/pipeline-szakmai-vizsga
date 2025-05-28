<script setup>
import { useDeviceStore } from '@/stores/device'
import router from '@/router'
import { useRoute } from 'vue-router'
import { computed, ref, onMounted } from 'vue'
import Modal from '@/components/_modals/DeleteDeviceModal.vue'
import { useToast } from 'vue-toastification'

const deviceStore = useDeviceStore()
const route = useRoute()
const toast = useToast()

const currentDevice = ref({
  id: '00000000-00000000-00000000-00000000-00000000',
  deviceType: 3,
  manufacturer: '',
  model: '',
  name: '',
  applicationUserId: '',
  batteryCapacity: 0,
  maxChargingSpeed: 0,
  hasName: true,
  batteryVoltage: 0,
  detachableBattery: null,
  isFoldable: null,
  canBeLocked: null,
})

const isValid = computed(
  () =>
    currentDevice.value.name.trim() !== '' ||
    (currentDevice.value.manufacturer.trim() !== '' && currentDevice.value.model.trim() !== ''),
)

const SaveDevice = async () => {
  if (isValid.value) {
    if (currentDevice.value.name.trim() === '') {
      currentDevice.value.name = currentDevice.value.manufacturer + ' ' + currentDevice.value.model
    }
    await deviceStore.saveDevice(currentDevice.value)
    router.push(`/devices/`)
  } else {
    toast.error('The device must have a visible name!')
  }
}

const deviceTypeText = computed(() => {
  if (currentDevice.value.deviceType == 0) {
    return "EBike's"
  } else if (currentDevice.value.deviceType == 1) {
    return "EScooter's"
  } else if (currentDevice.value.deviceType == 2) {
    return "ESkateBoard's"
  } else {
    return "Device's"
  }
})

const showId = ref(false)
const toggleShowId = () => {
  showId.value = !showId.value
}

onMounted(async () => {
  await deviceStore.fetchDevices()
  currentDevice.value = await deviceStore.getDeviceById(route.params.deviceId)
})
</script>

<template>
  <div class="container d-flex justify-content-center align-items-center">
    <div class="col-12 col-md-8 col-lg-6 p-4 card2 shadow">
      <h2 class="text-center mb-4">{{ deviceTypeText }} Information</h2>
      <button class="btn btn-info w-100 mb-3" @click="toggleShowId">
        <i class="bi bi-eye"></i> {{ showId ? 'Hide Device ID' : 'Show Device ID' }}
      </button>

      <div v-if="showId" class="alert alert-secondary text-center mt-3 mb-3">
        <strong>Device ID:</strong> {{ currentDevice.id }}
      </div>
      <div class="row">
        <div class="col-md-12 mb-3">
          <label class="form-label">Name:</label>
          <input type="text" v-model="currentDevice.name" class="form-control" placeholder="Name" />
        </div>
        <div class="col-md-6 mb-3">
          <label class="form-label">Manufacturer:</label>
          <input
            type="text"
            v-model="currentDevice.manufacturer"
            class="form-control"
            placeholder="Manufacturer"
          />
        </div>
        <div class="col-md-6 mb-3">
          <label class="form-label">Model:</label>
          <input
            type="text"
            v-model="currentDevice.model"
            class="form-control"
            placeholder="Model"
          />
        </div>
      </div>

      <h4 class="mt-4">Battery Information</h4>
      <div class="row">
        <div class="col-md-6 mb-3">
          <label class="form-label">Battery Capacity (mAh):</label>
          <input type="number" v-model="currentDevice.batteryCapacity" class="form-control" />
        </div>
        <div class="col-md-6 mb-3">
          <label class="form-label">Battery Voltage (V):</label>
          <input type="number" v-model="currentDevice.batteryVoltage" class="form-control" />
        </div>
      </div>
      <div class="mb-3">
        <label class="form-label">Max Charging Speed (kWh):</label>
        <input
          type="number"
          v-model="currentDevice.maxChargingSpeed"
          class="form-control"
          required
        />
      </div>

      <div class="mt-3">
        <div v-if="currentDevice.deviceType == 0" class="form-check">
          <input
            type="checkbox"
            v-model="currentDevice.detachableBattery"
            class="form-check-input"
            id="detachablebattery"
          />
          <label class="form-check-label" for="detachablebattery">Detachable battery</label>
        </div>
        <div v-if="currentDevice.deviceType == 1" class="form-check">
          <input
            type="checkbox"
            v-model="currentDevice.isFoldable"
            class="form-check-input"
            id="isfoldable"
          />
          <label class="form-check-label" for="isfoldable">Foldable</label>
        </div>
        <div v-if="currentDevice.deviceType == 2" class="form-check">
          <input
            type="checkbox"
            v-model="currentDevice.canBeLocked"
            class="form-check-input"
            id="canbelocked"
          />
          <label class="form-check-label" for="canbelocked">Lockable</label>
        </div>
      </div>

      <div class="row mt-4">
        <div class="col-md-6">
          <button
            class="btn btn-danger w-100"
            data-bs-toggle="modal"
            data-bs-target="#modal-deleteDevice"
          >
            <i class="bi bi-trash"></i> Delete
          </button>
        </div>
        <div class="col-md-6">
          <button class="btn btn-success w-100" @click="SaveDevice">
            <i class="bi bi-save"></i> Save
          </button>
        </div>
      </div>

      <button class="btn btn-secondary w-100 mt-3" @click="router.push('/devices')">
        <i class="bi bi-arrow-left"></i> Back to Devices
      </button>
    </div>
  </div>
  <Modal />
</template>
