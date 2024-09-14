import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useAuthStore = defineStore('auth', () => {
    const isAuthenticated = ref(!!localStorage.getItem('authToken'))

    function checkAuth() {
        isAuthenticated.value = !!localStorage.getItem('authToken')
    }

    function login(token) {
        localStorage.setItem('authToken', token)
        isAuthenticated.value = true
    }

    function logout(router) {
        localStorage.removeItem('authToken')
        isAuthenticated.value = false
        router.push('/login')
    }

    return {
        isAuthenticated,
        checkAuth,
        login,
        logout,
    }
})
