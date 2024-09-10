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
        async login(email, password) {
            try {
                const response = await axios.post(`${API_URL}`, { email, password });
                const { token, userRoles } = response.data;
        
                this.token = token;
                localStorage.setItem('authToken', token);
                localStorage.setItem('userRoles', JSON.stringify(userRoles));
                axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        
                return true;
              } catch (error) {
                console.error('Login failed:', error);
                return false;
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
            this.user = null;
            this.token = null;
            localStorage.removeItem('authToken');
            localStorage.removeItem('userRoles');
            delete axios.defaults.headers.common['Authorization'];
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
