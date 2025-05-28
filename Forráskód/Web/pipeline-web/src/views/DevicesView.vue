<script setup>
import { useDeviceStore } from '@/stores/device'
import router from '@/router'
import { onMounted, ref, watch } from 'vue'
import darkScooter from '@/assets/escooter.png'
import greenScooter from '@/assets/escooter_green.png'
import darkBike from '@/assets/ebike.png'
import greenBike from '@/assets/ebike_green.png'
import darkSkateBoard from '@/assets/eskateboard.png'
import greenSkateBoard from '@/assets/eskateboard_green.png'
import { useThemeStore } from '@/stores/theme'

const theme = useThemeStore()

const deviceStore = useDeviceStore()
const searchQuery = ref('')
const numberOfDevices = ref(0)

const SeeDevicesData = (deviceId) => {
  router.push(`/devices/${deviceId}`)
}

const AddNewDevice = () => {
  router.push('/devices/newdevice')
}

const deviceTypeText = (type) => {
  if (type == 0) {
    return 'EBike'
  } else if (type == 1) {
    return 'EScooter'
  } else if (type == 2) {
    return 'ESkateBoard'
  }
}

watch(searchQuery, async (newValue) => {
  await deviceStore.fetchDevices(newValue)
})

onMounted(async () => {
  await deviceStore.fetchDevices('')
  numberOfDevices.value = await deviceStore.numberOfDevices()
})
</script>

<template>
  <div class="container-fluid py-3">
    <div class="row justify-content-center mb-3">
      <div class="col-12 col-md-6">
        <div class="d-flex justify-content-between align-items-center">
          <h3 class="text-light m-0">My number of devices: {{ numberOfDevices }}</h3>
          <button class="btn btn-success" @click="AddNewDevice()">
            <i class="bi bi-plus-circle"></i> Add Device
          </button>
        </div>
      </div>
    </div>

    <div class="row justify-content-center mb-3">
      <div class="col-12 col-md-6">
        <input
          v-model="searchQuery"
          type="text"
          class="form-control"
          placeholder="Search by Name, Manufacturer, or Type..."
        />
      </div>
    </div>

    <hr class="bg-white" />

    <div v-if="deviceStore.devices.length > 0" class="row justify-content-center gap-3">
      <div v-for="device in deviceStore.devices" :key="device.id" class="col-12 col-md-5 col-xl-3">
        <div class="card">
          <div class="card-title">
            <h4>{{ device.name || 'Unnamed Device' }}</h4>
          </div>
          <div class="card-body p-0">
            <div class="d-flex row align-items-center">
                <div class="col-12 col-md-8">
                <strong>Manufacturer:</strong> {{ device.manufacturer || 'N/A' }}<br />
                <strong>Model:</strong> {{ device.model || 'N/A' }}<br />
                <strong>Battery:</strong>
                {{ device.batteryCapacity ? device.batteryCapacity + ' mAh' : 'N/A' }} ;
                {{ device.batteryCapacity ? device.batteryVoltage + ' V' : 'N/A' }}<br />
                <strong>Charging Speed:</strong>
                {{ device.maxChargingSpeed ? device.maxChargingSpeed + ' kWh' : 'N/A' }}<br />
                <strong>Type:</strong> {{ deviceTypeText(device.deviceType) }}
                <div v-if="device.deviceType == 0">
                  <strong>Detachable battery:</strong> {{ device.detachableBattery }}
                </div>
                <div v-if="device.deviceType == 1">
                  <strong>Foldable:</strong> {{ device.isFoldable }}
                </div>
                <div v-if="device.deviceType == 2">
                  <strong>Can be locked:</strong> {{ device.canBeLocked }}
                </div>
              </div>

              <div class="col-12 col-md-4 text-center mt-3 mt-md-0">
                <img
                  :src="
                    device.deviceType == 0
                      ? theme.mode === 'dark'
                        ? greenBike
                        : darkBike
                      : device.deviceType == 1
                        ? theme.mode === 'dark'
                          ? greenScooter
                          : darkScooter
                        : theme.mode === 'dark'
                          ? greenSkateBoard
                          : darkSkateBoard
                  "
                  :alt="deviceTypeText(device.deviceType)"
                  class="device-img"
                />
              </div>
            </div>
          </div>
          <div class="card-footer">
            <button class="btn btn-success w-100" @click="SeeDevicesData(device.id)">
              <i class="bi bi-pencil-square"></i> Edit
            </button>
          </div>
        </div>
      </div>
    </div>

    <div v-else class="text-center text-light mt-4">
      <p>You have no registered devices yet.</p>
    </div>
  </div>
</template>

<style scoped>
img {
  max-width: 150px;
  width: 100%;
}
</style>
