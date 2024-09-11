import { defineStore } from 'pinia'
import axios from 'axios'

const API_URL = 'https://localhost:7126/api'

export const useAuthStore = defineStore('auth', {
    state: () => ({
        user: null,
        token: localStorage.getItem('authToken') || null,  // Initialize from localStorage
    }),
    getters: {
        isAuthenticated: (state) => !!state.token,
    },
    actions: {
        async login(email, password, router) {
            try {
                const response = await axios.post(`${API_URL}/auth/login`, { email, password });
                const { token, userRoles } = response.data;

                this.token = token;
                localStorage.setItem('authToken', token);  // Store token in localStorage
                localStorage.setItem('userRoles', JSON.stringify(userRoles));

                // Set the default Authorization header for all future requests
                axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;

                // Redirect to home or dashboard
                router.push('/dashboard');

                return true;
            } catch (error) {
                console.error('Login failed:', error);
                return false;
            }
        },
        async register(email, password) {
            try {
                await axios.post(`${API_URL}/auth/register`, { email, password });
            } catch (error) {
                console.error('Registration failed:', error);
                throw error;
            }
        },
        logout(router) {  // Accept router as a parameter
            this.user = null;
            this.token = null;
            localStorage.removeItem('authToken');
            localStorage.removeItem('userRoles');
            delete axios.defaults.headers.common['Authorization'];

            // Redirect to login page
            if (router) {
                router.push('/login');
            }
        },
        async checkAuth() {
            const token = localStorage.getItem('authToken');
            if (token) {
                try {
                    const response = await axios.get(`${API_URL}/account/user`, {
                        headers: { Authorization: `Bearer ${token}` }
                    });
                    this.user = response.data;
                    this.token = token;
                    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
                } catch (error) {
                    console.error('Token validation failed:', error);
                    this.logout();  // Token invalid, force logout
                }
            }
        }
    }
});
