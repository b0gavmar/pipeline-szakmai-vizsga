import { defineStore } from 'pinia'
import { ref } from 'vue'
import axios from 'axios'
import { useToast } from 'vue-toastification'

export const useUserStore = defineStore('user', () => {
  const toast = useToast()
  const user = ref(null)
  const isAuthenticated = ref(false)

  const api = axios.create({
    withCredentials: true,
  })

  const checkAuth = async () => {
    try {
      await api.get('https://localhost:7020/api/applicationuser/me')
      return true
    } catch {
      return false
    }
  }

  const login = async (email, password, stayLoggedIn) => {
    try {
      if (!user.value) {
        await api.post('https://localhost:7020/api/authentication/login', {
          email,
          password,
          stayLoggedIn,
        })
      }
      toast.success('Logged in!')
      await fetchUser()
    } catch (error) {
      toast.error('The email and password do not match')
      console.error(error.response?.data?.message || 'Login error!')
    }
  }

  const register = async (email, password) => {
    try {
      if (!user.value) {
        await api.post('https://localhost:7020/api/authentication/register', {
          email,
          password,
        })
      }
      toast.success('Registration successful!')
      await fetchUser()
    } catch (error) {
      toast.error('Registration failed!')
      console.error(error.response?.data?.message || 'Registration error!')
    }
  }

  const changePassword = async (currentPw, newPw) => {
         if (!/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?":{}|<>]).{8,}$/.test(newPw)) {
          toast.error('The new password is in an invalid format!')
          return
        }
    try {
      await api.put('https://localhost:7020/api/authentication/changePassword', {
        email: user.value.email,
        currentPassword: currentPw,
        newPassword: newPw,
      })
      toast.success('Password changed!')
    } catch (error) {
      throw new Error(error.response?.data?.message || 'Password change error!')
    }
  }

  const fetchUser = async () => {
    try {
      const response = await api.get('https://localhost:7020/api/applicationuser/me')
      user.value = response.data
      isAuthenticated.value = true
    } catch (error) {
      console.error('Could not fetch user:', error)
      user.value = null
      isAuthenticated.value = false
    }
  }

  const logout = async () => {
    try {
      await api.post('https://localhost:7020/api/authentication/logout')
      user.value = null
      isAuthenticated.value = false
      toast.success('Logged out!')
    } catch (error) {
      console.error('Logout error:', error)
    }
  }

  const updateUser = async (userToUpdate) => {
    if (
      !userToUpdate.email ||
      userToUpdate.email.trim() === '' ||
      !userToUpdate.email.includes('@') ||
      !userToUpdate.email.includes('.')
    ) {
      toast.error('Email is empty or is in an invalid format!')
      return
    }

    try {
      await api.put('https://localhost:7020/api/applicationuser/', userToUpdate)
      toast.success('User updated successfully!')
      user.value = userToUpdate
    } catch {
      toast.error('Error when updating the user!')
    }
  }

  return {
    user,
    isAuthenticated,

    checkAuth,
    login,
    register,
    changePassword,
    fetchUser,
    logout,
    updateUser,
  }
})
