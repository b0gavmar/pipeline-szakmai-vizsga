<script setup>
import { useDeviceStore } from '@/stores/device'
import router from '@/router'
import { ref,computed } from 'vue'
import { useToast } from 'vue-toastification'

const toast = useToast()

const deviceStore = useDeviceStore()

const newDevice = ref({
  deviceType: 1,
  manufacturer: '',
  model: '',
  name: '',
  batteryCapacity: null,
  batteryVoltage: null,
  maxChargingSpeed: null,
  detachableBattery: null,
  isFoldable: false,
  canBeLocked: null,
})

const isValid = computed(
  () =>
    newDevice.value.name.trim() !== '' ||
    (newDevice.value.manufacturer.trim() !== '' && newDevice.value.model.trim() !== ''),
)

const addNewDevice = async () => {
  if (!isValid.value) {
    toast.error("Please fill out the name or the model and manufacturer")
  } else {
    if (newDevice.value.name.trim() === '') {
      newDevice.value.name = newDevice.value.manufacturer + ' ' + newDevice.value.model
    }
    await deviceStore.addDevice(newDevice.value)
    await router.push('/devices')
  }
}
</script>

<template>
  <main class="container my-3">
    <div class="row justify-content-center">
      <div class="col-12 col-md-8 col-lg-6 card2 p-4 shadow">
        <div class="card-header">
          <h2 class="text-center fw-bold">New Device</h2>
        </div>

        <div class="card-body">
          <div class="mb-3">
            <label class="form-label">Name:</label>
            <input
              type="text"
              v-model="newDevice.name"
              class="form-control"
              placeholder="Enter device name"
              required
            />
          </div>

          <div class="mb-3">
            <label class="form-label">Manufacturer:</label>
            <input
              type="text"
              v-model="newDevice.manufacturer"
              class="form-control"
              placeholder="Enter manufacturer"
              required
            />
          </div>

          <div class="mb-3">
            <label class="form-label">Model:</label>
            <input
              type="text"
              v-model="newDevice.model"
              class="form-control"
              placeholder="Enter model"
              required
            />
          </div>

          <div class="mb-3">
            <label class="form-label">Battery Capacity (mAh):</label>
            <input
              type="number"
              v-model="newDevice.batteryCapacity"
              class="form-control"
              placeholder="Enter battery capacity"
            />
          </div>

          <div class="mb-3">
            <label class="form-label">Battery Voltage (V):</label>
            <input
              type="number"
              v-model="newDevice.batteryVoltage"
              class="form-control"
              placeholder="Enter battery voltage"
            />
          </div>

          <div class="mb-3">
            <label class="form-label">Max Charging Speed (kWh):</label>
            <input
              type="number"
              v-model="newDevice.maxChargingSpeed"
              class="form-control"
              placeholder="Enter charging speed"
            />
          </div>

          <div class="mb-3">
            <label class="form-label">Type:</label>
            <select v-model.number="newDevice.deviceType" class="form-select">
              <option value="0">EBike</option>
              <option value="1">EScooter</option>
              <option value="2">ESkateBoard</option>
            </select>
          </div>

          <div class="mt-3">
            <div v-if="newDevice.deviceType == 0" class="form-check">
              <input
                type="checkbox"
                v-model="newDevice.detachableBattery"
                class="form-check-input"
                id="detachablebattery"
              />
              <label class="form-check-label" for="detachablebattery">Detachable battery</label>
            </div>
            <div v-if="newDevice.deviceType == 1" class="form-check">
              <input
                type="checkbox"
                v-model="newDevice.isFoldable"
                class="form-check-input"
                id="isfoldable"
              />
              <label class="form-check-label" for="isfoldable">Foldable</label>
            </div>
            <div v-if="newDevice.deviceType == 2" class="form-check">
              <input
                type="checkbox"
                v-model="newDevice.canBeLocked"
                class="form-check-input"
                id="canbelocked"
              />
              <label class="form-check-label" for="canbelocked">Lockable</label>
            </div>
          </div>
        </div>

        <div class="card-footer">
          <button class="btn btn-success w-100 mt-3" @click="addNewDevice">
            <i class="bi bi-save"></i> Save Device
          </button>
          <button class="btn btn-secondary w-100 mt-3" @click="router.push('/devices')">
            <i class="bi bi-arrow-left"></i> Back to Devices
          </button>
        </div>
      </div>
    </div>
  </main>
</template>
