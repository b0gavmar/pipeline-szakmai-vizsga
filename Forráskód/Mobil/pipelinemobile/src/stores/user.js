import axios from "axios";
import { defineStore } from "pinia";
import { ref } from "vue";
import { useToast } from "vue-toastification";

export const api = axios.create({
  withCredentials: true,
});

export const useUserStore = defineStore("user", () => {
  const user = ref(null);
  const isAuthenticated = ref(false);
  const toast = useToast();

  const checkAuth = async () => {
    try {
      await api.get(`https://localhost:7020/api/ApplicationUser/me`);
      return true;
    } catch {
      return false;
    }
  };
  const login = async (email, password) => {
    try {
      if (!user.value) {
        await api.post(`https://localhost:7020/api/Authentication/login`, {
          email,
          password,
        });
      }
      toast.success("Logged in!");
      await fetchUser();
    } catch (error) {
      throw new Error(error.response?.data?.message || "error during login!");
    }
  };
  const fetchUser = async () => {
    try {
      const response = await api.get(
        `https://localhost:7020/api/ApplicationUser/me`
      );
      user.value = response.data;
      isAuthenticated.value = true;
    } catch (error) {
      console.error("Could not fetch user:", error);
      user.value = null;
      isAuthenticated.value = false;
    }
  };
  const logout = async () => {
    try {
      await api.post(`https://localhost:7020/api/Authentication/logout`, {});
      user.value = null;
      isAuthenticated.value = false;
      toast.success("Logged out!");
    } catch (error) {
      console.error("Logout error:", error);
    }
  };
  const register = async (email, password) => {
    try {
      if (useUserStore.user == null) {
        await api.post(`https://localhost:7020/api/Authentication/register`, {
          email,
          password,
        });
      }
      toast.success("User registration successful!");
      await fetchUser();
    } catch (error) {
      throw new Error(error.response?.data?.message || "error!");
    }
  };
  const updateUser = async (userToUpdate) => {
    try {
      await api.put(`https://localhost:7020/api/ApplicationUser`, userToUpdate);
      toast.success("User data was successfully updated!");
      user.value = userToUpdate;
    } catch {}
  };
  const changePassword = async (currentPw, newPw) => {
    try {
      await api.put(
        "https://localhost:7020/api/Authentication/changePassword",
        {
          email: user.value.email,
          currentPassword: currentPw,
          newPassword: newPw,
        }
      );
      toast.success("Password changed!");
    } catch (error) {
      throw new Error(
        error.response?.data?.message || "Password change error!"
      );
    }
  };

  return {
    user,
    isAuthenticated,
    checkAuth,
    login,
    fetchUser,
    logout,
    register,
    updateUser,
    changePassword,
  };
});
