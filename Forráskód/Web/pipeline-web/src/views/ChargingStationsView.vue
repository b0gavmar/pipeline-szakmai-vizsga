<script setup>
import { useChargingStationStore } from '@/stores/chargingStation'
import router from '@/router'
import { onMounted, ref, watch, onUnmounted } from 'vue'
import { Map, Marker, Popup } from '@maptiler/sdk'

const chargingStationStore = useChargingStationStore()
const searchQuery = ref('')
const markers = ref([])
let map = null
const listContainer = ref(null)

const SeeStationsPorts = (chargingStationId) => {
  router.push(`/chargingstations/${chargingStationId}`)
}

watch(searchQuery, async (newValue) => {
  await chargingStationStore.fetchPagedChargingStations(newValue, true)
  updateMarkers()
})

onMounted(async () => {
  chargingStationStore.pageNumber = 1
  await chargingStationStore.fetchPagedChargingStations('', true)
  markers.value = chargingStationStore.chargingStations
  listContainer.value?.addEventListener('scroll', handleScroll)

  map = new Map({
    container: 'map',
    style: 'https://api.maptiler.com/maps/streets/style.json?key=3K6z7QIsAjfjT4juVPVR',
    center: [20.1498, 46.2530],
    zoom: 5,
  })

  updateMarkers()
  handleScroll()
})

onUnmounted(() => {
  listContainer.value?.removeEventListener('scroll', handleScroll)
})

const handleScroll = async () => {
  const el = listContainer.value
  if (!el) return

  const atBottom = Math.abs(el.scrollHeight - el.scrollTop - el.clientHeight) < 1

  if (atBottom && chargingStationStore.hasMore) {
    await chargingStationStore.fetchPagedChargingStations(searchQuery.value)
    updateMarkers()
  }
}

const updateMarkers = () => {
  markers.value.forEach((station) => {
    if (station.latitude && station.longitude) {
      const marker = new Marker()
        .setLngLat([station.longitude, station.latitude])
        .setPopup(new Popup().setText(station.name || 'Unnamed Station'))
        .addTo(map)
    }
  })
}

const focusOnMap = (station) => {
  if (station.latitude && station.longitude && map) {
    map.flyTo({
      center: [station.longitude, station.latitude],
      zoom: 12,
      essential: true,
    })
  }
}
</script>

<template>
  <main class="container-fluid mt-3 pt-3">
    <div class="row justify-content-center mb-3">
      <div class="col-12 col-md-8">
        <input
          type="text"
          v-model="searchQuery"
          class="form-control search-bar"
          placeholder="Search by name or address..."
        />
      </div>
    </div>

    <div class="row flex-grow-1 p-3">
      <div class="col-md-8 d-flex">
        <div id="map-container">
          <div id="map"></div>
        </div>
      </div>

      <div class="col-md-4 station-list p-3" ref="listContainer">
        <div v-if="chargingStationStore.chargingStations.length > 0">
          <div
            v-for="station in chargingStationStore.chargingStations"
            :key="station.id"
            class="card mb-2"
            @click="focusOnMap(station)"
          >
            <div class="card-title">
              <h5>{{ station.name || 'Unnamed Station' }}</h5>
            </div>
            <div class="card-body p-0">
              <p class="card-text">{{ station.address || 'Unknown Address' }}</p>
              <button class="btn btn-success w-100" @click.stop="SeeStationsPorts(station.id)">
                <i class="bi bi-plug"></i> View Ports
              </button>
            </div>
          </div>
        </div>
        <div v-else class="text-center">No charging stations found.</div>
      </div>
    </div>
  </main>
</template>

<style scoped>
.row.flex-grow-1 {
  flex-grow: 1;
  display: flex;
  height: 100%;
}

#map-container {
  flex-grow: 1;
  display: flex;
  height: 76vh;
  max-height: 100%;
}

#map {
  width: 100%;
  min-height: 500px;
}

.station-list {
  height: 100%;
  max-height: 76vh;
  overflow-y: auto;
}

.search-bar {
  padding: 12px;
  border-radius: 8px;
  border: 2px solid #444;
  transition:
    border-color 0.3s ease-in-out,
    box-shadow 0.2s;
}

.search-bar:focus {
  border-color: rgb(103, 230, 109);
  box-shadow: 0px 0px 10px rgba(103, 230, 109, 0.5);
  outline: none;
}
</style>
