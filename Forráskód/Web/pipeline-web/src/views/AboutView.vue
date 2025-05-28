<template>
  <main class="container d-flex justify-content-center align-items-center">
    <div class="card2 p-4 my-4">
      <h1 class="text-center text-success mb-4">
        <i class="bi bi-lightning-fill text-success"></i> About PipeLine
      </h1>
      <p>
        In today's world, electric bikes, scooters, and skateboards are becoming increasingly popular.
        However, their battery capacity is often insufficient for long trips, making it difficult
        for users to plan their journeys without charging concerns.
      </p>

      <h2 class="text-success mt-4">
        <i class="bi bi-rocket-takeoff text-success"></i> Our Solution
      </h2>
      <p>
        PipeLine aims to <strong>revolutionize electric transportation</strong> by introducing a network of smart charging stations.
        These stations allow users to <strong>conveniently charge their devices</strong>, monitor their charging status, 
        and track their usage history—all through an integrated web and mobile platform.
      </p>

      <h2 class="mt-4 text-success ">
        <i class="bi bi-key-fill text-success"></i> Key Features
      </h2>
      <ul>
        <li><i class="bi bi-tools text-success"></i> <strong>Device Management</strong> – Users can add, edit, and remove their devices.</li>
        <li><i class="bi bi-map-fill text-success"></i> <strong>Charging Station Finder</strong> – Locate nearby charging stations with an interactive map.</li>
        <li><i class="bi bi-battery-charging text-success"></i> <strong>Real-Time Charging Status</strong> – Track your device's charging process.</li>
        <li><i class="bi bi-graph-up text-success"></i> <strong>Usage Statistics</strong> – Get insights on charging habits and station availability.</li>
        <li><i class="bi bi-shield-lock text-success"></i> <strong>Admin Features</strong> – Manage stations, devices, and users in a centralized system.</li>
      </ul>

      <h2 class="mt-4 text-success ">
        <i class="bi bi-code-slash text-success"></i> Technologies Used
      </h2>
      <ul>
        <li><i class="bi bi-display text-success"></i> <strong>Frontend:</strong> Vue.js & Bootstrap for an intuitive web experience.</li>
        <li><i class="bi bi-server text-success"></i> <strong>Backend:</strong> C# ASP.NET with MySQL database for fast and scalable data handling.</li>
        <li><i class="bi bi-phone text-success"></i> <strong>Mobile Compatibility:</strong> Designed to work across desktop and mobile platforms.</li>
        <li><i class="bi bi-lock-fill text-success"></i> <strong>Authentication:</strong> Secure JWT-based login system.</li>
      </ul>

      <h2 class="mt-4 text-success ">
        <i class="bi bi-eye-fill text-success"></i> Our Vision
      </h2>
      <p>
        PipeLine is designed to make <strong>electric mobility more efficient and accessible</strong>.
        By building a <strong>reliable and user-friendly charging infrastructure</strong>, we aim to support
        sustainable urban transportation and reduce range anxiety for electric vehicle users.
      </p>
    </div>
  </main>
  <div></div>
</template>

<style scoped>

ul {
  list-style-type: none;
  padding: 0;
}

ul li {
  padding: 8px 0;
  display: flex;
  align-items: center;
  font-size: 1.1rem;
  transition: all 0.2s ease-in-out;
}

i {
  margin-right: 10px;
}

ul li:hover {
  transform: translateX(5px);
}
</style>
