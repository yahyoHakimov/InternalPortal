import { createRouter, createWebHistory } from 'vue-router'
import Dashboard from '@/views/Dashboard/Dashboard.vue'
import Employee from '@/views/Employee/Employee.vue'
import Announcement from '@/views/Announcement/Announcement.vue'
import KPI from '@/views/KPI/KPI.vue'
import Profile from '@/views/Profile/Profile.vue'
import Attendance from '@/views/Attendance/Attendance.vue'
import Documents from '@/views/Document/Documents.vue'
import Feedback from '@/views/Feedback/Feedback.vue'
import Meetings from '@/views/Meetings/Meetings.vue'
import Request from '@/views/Requests/Request.vue'
// Define your routes
const routes = [
    { path: '/', component: Dashboard },
    { path: '/employee', component: Employee },
    { path: '/kpi', component: KPI },
    { path: '/announcements', component: Announcement },
    { path: '/profile', component: Profile },
    { path: '/attendance', component: Attendance },
    { path: '/documents', component: Documents },
    { path: '/feedback', component: Feedback },
    { path: '/meetings', component: Meetings },
    { path: '/requests', component: Request },
    { path: '/:pathMatch(.*)*', redirect: '/' }

]

// Create the router instance
const router = createRouter({
    history: createWebHistory(),
    routes,
})

export default router
