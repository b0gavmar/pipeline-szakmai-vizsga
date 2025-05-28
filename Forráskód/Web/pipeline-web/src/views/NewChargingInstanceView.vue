<script setup>
import { useRoute, useRouter } from 'vue-router'
import { ref, onMounted, watch } from 'vue'
import { useChargingInstanceStore } from '@/stores/chargingInstance'
import { useDeviceStore } from '@/stores/device'
import { useToast } from 'vue-toastification'

const toast = useToast()
const route = useRoute()
const router = useRouter()
const chargingInstanceStore = useChargingInstanceStore()
const deviceStore = useDeviceStore()

const chargingPortId = route.params.chargingPortId

const newChargingInstance = ref({
  chargingPortId,
  deviceId: '',
  start: null,
  startingPercentage: null,
  desiredEndPercentage: null,
})

const selectedDevice = ref({
  deviceType: 4,
  manufacturer: '',
  model: 'a',
  name: '',
  batteryCapacity: null,
  batteryVoltage: null,
  maxChargingSpeed: 0,
  hasName: false,
  detachableBattery: null,
  isFoldable: null,
  canBeLocked: null,
})

const startChargingInstance = async () => {
  if (!newChargingInstance.value.deviceId) {
    toast.error('You must choose a device')
  } else {
    await chargingInstanceStore.newChargingInstance(newChargingInstance.value)
    router.push('/chargingInstances')
  }
}

watch(
  () => newChargingInstance.value.startingPercentage,
  (newValue) => {
    if (newValue > 100) {
      newChargingInstance.value.startingPercentage = 100
    } else if (newValue < 0) {
      newChargingInstance.value.startingPercentage = 0
    }
  },
)

watch(
  () => newChargingInstance.value.desiredEndPercentage,
  (newValue) => {
    if (newValue > 100) {
      newChargingInstance.value.desiredEndPercentage = 100
    } else if (newValue < 0) {
      newChargingInstance.value.desiredEndPercentage = 0
    }
  },
)

watch(
  () => newChargingInstance.value.deviceId,
  async (newValue) => {
    if (newValue) {
      selectedDevice.value = await deviceStore.getDeviceById(newValue)
    }
  },
)

onMounted(async () => {
  await deviceStore.fetchNonChargingDevices()
})
</script>

<template>
  <main class="container d-flex justify-content-center align-items-center">
    <div class="col-12 col-md-6">
      <div class="card2 p-5">
        <div class="card-header">
          <h3 class="fw-bold text-center">New Charging Instance</h3>
        </div>
        <div class="card-body">
          <div class="mb-3">
            <label class="form-label fw-bold fs-4">Device</label>
            <select v-model="newChargingInstance.deviceId" class="form-select" required>
              <option v-for="device in deviceStore.devices" :value="device.id">
                {{ device.name }}
              </option>
            </select>
          </div>

          <div class="mb-3">
            <label class="form-label fw-bold fs-4">Starting Battery Percentage</label>
            <input
              type="number"
              class="form-control"
              v-model="newChargingInstance.startingPercentage"
              min="0"
              max="100"
            />
          </div>

          <div class="mb-3">
            <label class="form-label fw-bold fs-4">Desired Percentage</label>
            <input
              type="number"
              class="form-control"
              v-model="newChargingInstance.desiredEndPercentage"
              min="0"
              max="100"
            />
          </div>

          <hr class="bg-white" />
          <div class="mb-3 text-danger fw-bold">
            <div class="bg-white p-2 rounded border border-danger" v-if="selectedDevice.deviceType == 0 && selectedDevice.detachableBattery == true">
              If there are a lot of ports occupied, please detach your battery and charge that, in
              order to make sure that other devices will have space.
            </div>
            <div class="bg-white p-2 rounded border border-danger"  v-if="selectedDevice.deviceType == 1 && selectedDevice.isFoldable == true">
              If there are a lot of ports occupied, please fold your scooter, in order to make sure
              that other devices will have space.
            </div>
            <div class="bg-white p-2 rounded border border-danger"  v-if="selectedDevice.deviceType == 2 && selectedDevice.canBeLocked == false">
              We strongly encourage you to stay near your skateboard while charging since we cannot
              guarantee that it will not be stolen.
            </div>
          </div>
        </div>
        <div class="card-footer">
          <button class="btn btn-success w-100" @click="startChargingInstance">Start</button>
        </div>
      </div>
    </div>
  </main>
  <div></div>
</template>
