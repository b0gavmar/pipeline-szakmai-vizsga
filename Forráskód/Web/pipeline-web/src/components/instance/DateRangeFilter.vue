<template>
  <div class="row justify-content-center mb-3">
    <div class="col-6 col-md-3">
      <label class="text-light">Start Date:</label>
      <input type="date" :value="startDate" @input="$emit('update:startDate', $event.target.value)" class="form-control" />
    </div>
    <div class="col-6 col-md-3">
      <label class="text-light">End Date:</label>
      <input type="date" :value="endDate" @input="$emit('update:endDate', $event.target.value)" class="form-control" />
    </div>
  </div>
</template>

<script setup>
const props = defineProps({
  startDate: String,
  endDate: String,
})
</script>
