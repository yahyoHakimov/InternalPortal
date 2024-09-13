import { defineStore } from 'pinia';
import axios from 'axios';

const API_URL = 'https://localhost:7126/api';

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
            users: [], 
            departments: [],
            roles: []
        },
    }),
    actions: {
        // Generalized fetch function for any entity type
        async fetchItems(entityType, page = 1, limit = 10) {
            if (!this.items.hasOwnProperty(entityType)) {
                console.error(`Invalid entityType: ${entityType}`);
                return;
            }
            try {
                const response = await axios.get(`${API_URL}/${entityType}?page=${page}&limit=${limit}`);
                this.setItems(entityType, response.data);
            } catch (error) {
                console.error(`Error fetching ${entityType}:`, error);
            }
        },


        // Generalized add function for any entity type
        async addItem(entityType, item) {
            if (!this.items.hasOwnProperty(entityType)) {
                console.error(`Invalid entityType: ${entityType}`);
                return;
            }
            try {
                const response = await axios.post(`${API_URL}/${entityType}`, item);
                this.items[entityType].push(response.data);
            } catch (error) {
                console.error(`Error adding to ${entityType}:`, error);
            }
        },

        // Generalized delete function for any entity type
        async deleteItem(entityType, id) {
            if (!this.items.hasOwnProperty(entityType)) {
                console.error(`Invalid entityType: ${entityType}`);
                return;
            }
            try {
                await axios.delete(`${API_URL}/${entityType}/${id}`);
                this.items[entityType] = this.items[entityType].filter(item => item.id !== id);
            } catch (error) {
                console.error(`Error deleting from ${entityType}:`, error);
            }
        },

        // Set items in the state for any entity type
        setItems(entityType, items) {
            if (this.items.hasOwnProperty(entityType)) {
                this.items[entityType] = items;
            } else {
                console.error(`Invalid entityType: ${entityType}`);
            }
        },
    },
    getters: {
        // Get total count for any entity type
        totalItems: (state) => (entityType) => state.items[entityType]?.length || 0,

        // Custom getters for each entity type
        totalEmployees: (state) => state.items.employees.length,
        activeAnnouncements: (state) => state.items.announcements.filter(a => a.active).length,
        upcomingMeetings: (state) => state.items.meetings.filter(m => new Date(m.date) > new Date()).length,
        pendingRequests: (state) => state.items.requests.filter(r => r.status === 'pending').length,
    },
});
