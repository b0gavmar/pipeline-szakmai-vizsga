<template>
  <div class="col-12 col-md-4 col-lg-3">
    <div class="card">
      <div class="card-title">
        <h4>
          Station: {{ instance.chargingStationName }} <br />Port number: {{ instance.portNumber }}
        </h4>
      </div>
      <div class="card-body p-0">
        <p class="card-text">
          <strong>Device ID:</strong>
          {{ instance.deviceId || 'The device has been deleted' }}<br />
          <strong>Start:</strong> {{ formatDate(instance.start) }}<br />
          <strong>End:</strong> {{ formatDate(instance.end) }}<br />
          <strong>Starting Percentage:</strong> {{ instance.startingPercentage || '' }}%<br />
          <strong v-if="instance.end === null">Current Percentage:</strong>
          <strong v-else>End Percentage:</strong>
          {{ instance.endPercentage || '' }}% <br />
          <strong>Desired End Percentage:</strong>
          {{ instance.desiredEndPercentage === null ? "Not specified" : instance.desiredEndPercentage + "%" }}
        </p>
      </div>
      <div class="card-footer">
        <button
          class="btn w-100"
          :class="instance.end ? 'btn-secondary' : 'btn-success'"
          @click="() => onFinish(instance)"
          :disabled="instance.end"
        >
          <i class="bi bi-check-circle" v-if="!instance.end"></i>
          <i class="bi bi-check-circle-fill" v-else></i>
          {{ instance.end ? 'Finished' : 'Finish' }}
        </button>
      </div>
    </div>
  </div>
</template>

<script setup>
import dayjs from 'dayjs'
import utc from 'dayjs/plugin/utc'
import timezone from 'dayjs/plugin/timezone'

dayjs.extend(utc)
dayjs.extend(timezone)

const props = defineProps({
  instance: Object,
  onFinish: Function,
})

const formatDate = (dateString) => {
  if (!dateString) return 'N/A'
  return dayjs.utc(dateString).tz('Europe/Budapest').format('YYYY.MM.DD HH:mm')
}
</script>
