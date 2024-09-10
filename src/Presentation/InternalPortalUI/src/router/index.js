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
import LoginPage from '@/views/Auth/Login.vue'
import RegisterPage from '@/views/Auth/Register.vue'
// Define your routes
const routes = [
    { path: '/', component: Dashboard },
    { path: '/login', component: LoginPage, meta: { requiresAuth: false } },
    { path: '/register', component: RegisterPage, meta: { requiresAuth: false } },
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

router.beforeEach((to, from, next) => {
    const loggedIn = localStorage.getItem('authToken');
    const userRoles = JSON.parse(localStorage.getItem('userRoles')) || [];
    if (to.meta.requiresAuth && !loggedIn) {
        return next('/login');
    }

    if (to.meta.roles && !to.meta.roles.some(role => userRoles.includes(role))) {
        // If user does not have the required roles for the route, redirect them
        return next('/unauthorized'); // Redirect to unauthorized page or dashboard
    }

    next();
});


export default router
