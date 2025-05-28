import { createRouter, createWebHistory } from "@ionic/vue-router";
import TabsPage from "../views/TabsPage.vue";
import { useUserStore } from "../stores/user";
import LoginPage from "@/views/LoginPage.vue";
import RegisterPage from "@/views/RegisterPage.vue";
import MapPage from "@/views/MapPage.vue";
import ProfilePage from "@/views/ProfilePage.vue";
import DevicesPage from "@/views/DevicesPage.vue";
import { nextTick } from "vue";

const routes = [
  {
    path: "/",
    redirect: "/login",
    meta: { requiresGuest: true },
  },
  {
    path: "/login",
    component: LoginPage,
    meta: { requiresGuest: true },
  },
  {
    path: "/register",
    component: RegisterPage,
    meta: { requiresGuest: true },
  },
  {
    path: "/tabs/",
    component: TabsPage,
    meta: { requiresAuth: true },
    children: [
      {
        path: "map",
        component: MapPage,
        meta: { requiresAuth: true },
      },
      {
        path: "profile",
        component: ProfilePage,
        meta: { requiresAuth: true },
      },
      {
        path: "devices",
        component: DevicesPage,
        meta: { requiresAuth: true },
      },
    ],
  },
];
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
});
router.beforeEach(async (to, from, next) => {
  const loggedIn = await useUserStore().checkAuth();
  console.log(loggedIn);
  
  if (to.meta.requiresAuth && !loggedIn) {
    await nextTick();
    next("/login");
  } else if (to.meta.requiresGuest && loggedIn) {
    next("/tabs/profile");
  } else {
    next();
  }
});
export default router;
