import { defineStore } from 'pinia'
import axios from 'axios'

const API_URL = 'https://localhost:7126/api'

export const useAuthStore = defineStore('auth', {
    state: () => ({
        user: null,
        token: null,
    }),
    getters: {
        isAuthenticated: (state) => !!state.token,
    },
    actions: {
        async login(email, password, rememberMe) {
            try {
                const response = await axios.post(`${API_URL}/auth/login`, {
                    email,
                    password,
                    rememberMe
                })
                this.token = response.data.token
                localStorage.setItem('token', this.token)
                axios.defaults.headers.common['Authorization'] = `Bearer ${this.token}`
            } catch (error) {
                console.error('Login failed:', error)
                throw error
            }
        },
        async register(email, password) {
            try {
                await axios.post(`${API_URL}/auth/register`, {
                    email,
                    password
                })
            } catch (error) {
                console.error('Registration failed:', error)
                throw error
            }
        },
        logout() {
            this.user = null
            this.token = null
            localStorage.removeItem('token')
            delete axios.defaults.headers.common['Authorization']
        },
        async checkAuth() {
            const token = localStorage.getItem('token')
            if (token) {
                try {
                    const response = await axios.get(`${API_URL}/account/user`, {
                        headers: { Authorization: `Bearer ${token}` }
                    })
                    this.user = response.data
                    this.token = token
                } catch (error) {
                    console.error('Token validation failed:', error)
                    this.logout()
                }
            }
        }
    }
})
