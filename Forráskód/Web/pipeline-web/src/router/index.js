import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import LoginView from '../views/LoginView.vue'
import RegisterView from '../views/RegisterView.vue'
import ProfileView from '../views/ProfileView.vue'
import DevicesView from '../views/DevicesView.vue'
import NewDeviceView from '../views/NewDeviceView.vue'
import DevicesDataView from '../views/DevicesDataView.vue'
import ChargingInstancesView from '../views/ChargingInstancesView.vue'
import NewChargingInstanceView from '../views/NewChargingInstanceView.vue'
import ChargingStationsView from '../views/ChargingStationsView.vue'
import ChargingStationsDataView from '../views/ChargingStationsDataView.vue'
import { useUserStore } from '@/stores/user'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/login',
      name: 'login',
      component: LoginView,
      meta: { requiresGuest: true },
    },
    {
      path: '/register',
      name: 'register',
      component: RegisterView,
      meta: { requiresGuest: true },
    },
    {
      path: '/profile',
      name: 'profile',
      component: ProfileView,
      meta: { requiresAuth: true },
    },
    {
      path: '/devices',
      name: 'devices',
      component: DevicesView,
      meta: { requiresAuth: true },
    },
    {
      path: '/devices/newdevice',
      name: 'new device',
      component: NewDeviceView,
      meta: { requiresAuth: true },
    },
    {
      path: '/devices/:deviceId',
      name: 'devices data',
      component: DevicesDataView,
      meta: { requiresAuth: true },
    },
    {
      path: '/charginginstances',
      name: 'charginginstances',
      component: ChargingInstancesView,
      meta: { requiresAuth: true },
    },
    {
      path: '/charginginstances/newcharginginstance/:chargingPortId',
      name: 'new charginginstance',
      component: NewChargingInstanceView,
      meta: { requiresAuth: true },
    },
    {
      path: '/chargingstations',
      name: 'chargingstations',
      component: ChargingStationsView,
    },
    {
      path: '/chargingstations/:chargingStationId',
      name: 'chargingstations ports',
      component: ChargingStationsDataView,
    },
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    {
      path: '/about',
      name: 'about',
      component: () => import('../views/AboutView.vue'),
    },
  ],
})

router.beforeEach(async (to, from, next) => {
  const loggedIn = await useUserStore().checkAuth()

  if (to.meta.requiresAuth && !loggedIn) {
    next('/login')
  } else if (to.meta.requiresGuest && loggedIn) {
    next('/profile')
  } else {
    next()
  }
})

export default router
