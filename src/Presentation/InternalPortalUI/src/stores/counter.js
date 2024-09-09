import { defineStore } from 'pinia'

export const useAppStore = defineStore('app', {
    state: () => ({
        user: null,
        notifications: [],
        employees: [],
        announcements: [],
        meetings: [],
        requests: [],
    }),
    actions: {
        setUser(user) {
            this.user = user
        },
        addNotification(notification) {
            this.notifications.push(notification)
        },
        setEmployees(employees) {
            this.employees = employees
        },
        addAnnouncement(announcement) {
            this.announcements.push(announcement)
        },
        addMeeting(meeting) {
            this.meetings.push(meeting)
        },
        addRequest(request) {
            this.requests.push(request)
        },
    },
    getters: {
        totalEmployees: (state) => state.employees.length,
        activeAnnouncements: (state) => state.announcements.filter(a => a.active).length,
        upcomingMeetings: (state) => state.meetings.filter(m => new Date(m.date) > new Date()).length,
        pendingRequests: (state) => state.requests.filter(r => r.status === 'pending').length,
    },
})