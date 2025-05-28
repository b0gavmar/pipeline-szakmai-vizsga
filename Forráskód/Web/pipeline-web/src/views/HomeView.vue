<script setup>
import { useRouter } from 'vue-router'
import { useUserStore } from '@/stores/user'
import { onMounted, ref } from 'vue'
import ebikeIcon from '@/assets/ebike_green.png'
import escooterIcon from '@/assets/escooter_green.png'
import gmTablo from '@/assets/gm_tablo.png'
import gtTablo from '@/assets/gt_tablo.png'
import kmTablo from '@/assets/km_tablo.png'

const loggedIn = ref(false)

const router = useRouter()
onMounted( async() =>{
  if(useUserStore.user === null){
    
    loggedIn.value = false
  }
  else{
    loggedIn.value = false
  }
})
</script>

<template>
  <div class="mb-5">
    <section class="hero d-flex flex-column justify-content-center align-items-center text-center">
      <h1 class="fw-bold">Welcome to PipeLine</h1>
      <p>The best way to manage your electric devices efficiently.</p>
      <div class="mt-4">
        <button class="btn btn-success mx-2" @click="loggedIn === true ? router.push('/chargingstations') : router.push('/login')">
          Get Started
        </button>
        <button class="btn btn-outline-light mx-2" @click="router.push('/login')">Login</button>
      </div>

      <div class="animation-container">
        <img :src="ebikeIcon" alt="E-bike" class="bike" />
        <img :src="escooterIcon" alt="E-scooter" class="scooter" />
      </div>
    </section>

    <section class="container my-5">
      <h2 class="text-center text-success"><i class="bi bi-stars"></i> Why Choose PipeLine?</h2>
      <p class="text-center text-light">
        Managing your electric scooters, bikes, and skateboards should be easy and worry-free.
        PipeLine offers a seamless experience to track, charge, and maximize your battery life.
      </p>
      <div class="row justify-content-center">
        <div class="col-md-4 mt-2">
          <div class="card p-4 shadow-lg text-center">
            <h3 class="text-success"><i class="bi bi-tools"></i> Manage Devices</h3>
            <p>Add, edit, and track your electric devices in one place.</p>
          </div>
        </div>
        <div class="col-md-4 mt-2">
          <div class="card p-4 shadow-lg text-center">
            <h3 class="text-success"><i class="bi bi-battery-charging"></i> Charging History</h3>
            <p>Sort through previous charging instances and track your current ones.</p>
          </div>
        </div>
        <div class="col-md-4 mt-2">
          <div class="card p-4 shadow-lg text-center">
            <h3 class="text-success"><i class="bi bi-battery"></i> Battery Optimization</h3>
            <p>Maximize your battery with tools provided by our software.</p>
          </div>
        </div>
      </div>
    </section>

    <section class="container my-5 battery-tips">
      <h2 class="text-center text-success">
        <i class="bi bi-battery-full"></i> Battery Preservation Tips
      </h2>
      <p class="text-center text-light">
        Lithium-ion batteries degrade faster when improperly charged. Follow these tips for maximum
        longevity:
      </p>
      <div class="row justify-content-center">
        <div class="col-md-4 mt-2">
          <div class="card p-4 text-center">
            <h3 class="text-warning">
              <i class="bi bi-thermometer-half"></i> Avoid Extreme Temperatures
            </h3>
            <p>Do not charge in extreme heat or cold. Optimal range: 15°C - 25°C.</p>
          </div>
        </div>
        <div class="col-md-4 mt-2">
          <div class="card p-4 text-center">
            <h3 class="text-info">
              <i class="bi bi-battery-half"></i> Keep Charge Between 20% - 80%
            </h3>
            <p>Do not let the battery fully discharge or overcharge to 100% frequently.</p>
          </div>
        </div>
        <div class="col-md-4 mt-2">
          <div class="card p-4 text-center">
            <h3 class="text-danger">
              <i class="bi bi-exclamation-circle"></i> Avoid Full Draining
            </h3>
            <p>Frequently letting the battery drop to 0% damages longevity.</p>
          </div>
        </div>
      </div>
    </section>

    <section class="container pb-5">
      <h2 class="text-center text-success"><i class="bi bi-people"></i> Meet Our Team</h2>
      <div class="row justify-content-center">
        <div class="col-md-4 mt-2">
          <div class="card p-4 justify-content-center align-items-center team-member">
            <img :src="gmTablo" alt="Team Member 1" class="team-photo" />
            <h4 class="text-light mt-3">Martin Gavlik</h4>
            <p class="text-success">Lead Developer</p>
          </div>
        </div>
        <div class="col-md-4 mt-2">
          <div class="card p-4 justify-content-center align-items-center team-member">
            <img :src="gtTablo" alt="Team Member 2" class="team-photo" />
            <h4 class="text-light mt-3">Tamás Gavrán</h4>
          </div>
        </div>
        <div class="col-md-4 mt-2">
          <div class="card p-4 justify-content-center align-items-center team-member">
            <img :src="kmTablo" alt="Team Member 3" class="team-photo" />
            <h4 class="text-light mt-3">Milán Kónya</h4>
          </div>
        </div>
      </div>
    </section>
  </div>
</template>

<style scoped>
.container {
  min-height: unset !important;
}

.hero {
  height: 70vh;
  background: linear-gradient(135deg, #1e1e2f, #252545);
  color: white;
  padding: 50px 20px;
  position: relative;
  overflow: hidden;
}

.hero h1 {
  font-size: 3rem;
  animation: fadeIn 1s ease-in-out;
}

.hero p {
  font-size: 1.2rem;
  max-width: 600px;
  margin-top: 10px;
  animation: fadeIn 1.5s ease-in-out;
}

.hero .btn {
  font-size: 1.2rem;
  padding: 12px 24px;
}

.hero .btn-outline-light {
  border: 2px solid white;
  color: white;
}

.hero .btn-outline-light:hover {
  background-color: white;
  color: #1e1e2f;
}

.team-photo {
  width: 120px;
  height: 120px;
  object-fit: cover;
  border-radius: 50%;
  border: 3px solid rgb(103, 230, 109);
}

.bike,
.scooter {
  width: 100px;
  height: auto;
  position: absolute;
  bottom: 0;
}

.bike {
  left: -120px;
  animation: moveBike 5s linear infinite;
}

.scooter {
  right: -120px;
  animation: moveScooter 6s linear infinite;
}

@keyframes moveBike {
  from {
    left: -120px;
  }
  to {
    left: 100%;
  }
}

@keyframes moveScooter {
  from {
    right: -120px;
  }
  to {
    right: 100%;
  }
}

@keyframes fadeIn {
  from {
    opacity: 0;
    transform: translateY(-20px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}
</style>
