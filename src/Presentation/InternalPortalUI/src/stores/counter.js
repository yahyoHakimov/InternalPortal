import { defineStore } from 'pinia'
import axios from 'axios'

export const useAppStore = defineStore('app', {
    state: () => ({
        user: null,
        notifications: [],
        items: {
            employees: [],
            announcements: [],
            meetings: [],
            requests: [],
            attendance: [],
            documents: [],
            feedback: [],
            kpi: [],
            profile: [],
        },
    }),
    actions: {
        setUser(user) {
            this.user = user
        },
        addNotification(notification) {
            this.notifications.push(notification)
        },

        // Generalized fetch function for any entity type
        async fetchItems(entityType) {
            try {
                const response = await axios.get(`http://localhost:3000/api/${entityType}`);
                this.setItems(entityType, response.data);
            } catch (error) {
                console.error(`Error fetching ${entityType}:`, error);
            }
        },

        // Generalized add function for any entity type
        async addItem(entityType, item) {
            try {
                const response = await axios.post(`http://localhost:3000/api/${entityType}`, item);
                this.items[entityType].push(response.data);
            } catch (error) {
                console.error(`Error adding to ${entityType}:`, error);
            }
        },

        // Generalized delete function for any entity type
        async deleteItem(entityType, id) {
            try {
                await axios.delete(`http://localhost:3000/api/${entityType}/${id}`);
                this.items[entityType] = this.items[entityType].filter(item => item.id !== id);
            } catch (error) {
                console.error(`Error deleting from ${entityType}:`, error);
            }
        },

        // Set items in the state for any entity type
        setItems(entityType, items) {
            this.items[entityType] = items;
        },
    },
    getters: {
        // Get total count for any entity type
        totalItems: (state) => (entityType) => state.items[entityType]?.length || 0,

        // Custom getters for each entity type (you can extend these as needed)
        totalEmployees: (state) => state.items.employees.length,
        activeAnnouncements: (state) => state.items.announcements.filter(a => a.active).length,
        upcomingMeetings: (state) => state.items.meetings.filter(m => new Date(m.date) > new Date()).length,
        pendingRequests: (state) => state.items.requests.filter(r => r.status === 'pending').length,

        //auth
        isAuthenticated: (state) => !!state.token,
    },
})
