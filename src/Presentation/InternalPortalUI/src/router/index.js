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
import Unauthorized from '@/views/Error/Unauthorized.vue'
import MeetingsCalendarPage from '@/views/Meetings/MeetingsCalendarPage.vue'
import Department from '@/views/Department/Department.vue'

// Define your routes without role-based access control for now
const routes = [
    {
        path: '/', redirect: () => {
            const isAuthenticated = !!localStorage.getItem('authToken');
            return isAuthenticated ? '/dashboard' : '/login';  // Redirect based on authentication
        }
    },
    { path: '/login', component: LoginPage, meta: { requiresAuth: false } },
    { path: '/register', component: RegisterPage, meta: { requiresAuth: false } },
    { path: '/dashboard', component: Dashboard, meta: { requiresAuth: true } },
    { path: '/employee', component: Employee, meta: { requiresAuth: true } },
    { path: '/department', component: Department, meta: { requiresAuth: true } },
    { path: '/kpi', component: KPI, meta: { requiresAuth: true } },
    { path: '/announcements', component: Announcement, meta: { requiresAuth: true } },
    { path: '/profile', component: Profile, meta: { requiresAuth: true } },
    { path: '/attendance', component: Attendance, meta: { requiresAuth: true } },
    { path: '/documents', component: Documents, meta: { requiresAuth: true } },
    { path: '/feedback', component: Feedback, meta: { requiresAuth: true } },
    { path: '/meetings', component: Meetings, meta: { requiresAuth: true } },
    { path: '/meetings1', component: MeetingsCalendarPage, meta: { requiresAuth: true } },
    { path: '/requests', component: Request, meta: { requiresAuth: true } },
    { path: '/unauthorized', component: Unauthorized },
    { path: '/:pathMatch(.*)*', redirect: '/unauthorized' }  // Catch-all route to unauthorized
];

const router = createRouter({
    history: createWebHistory(),
    routes,
})

// Navigation guard to handle route protection
router.beforeEach((to, from, next) => {
    const isAuthenticated = !!localStorage.getItem('authToken');  // Check if user is logged in

    // If route requires authentication and user is not authenticated, redirect to login
    if (to.meta.requiresAuth && !isAuthenticated) {
        return next('/login');
    }

    next();  // Allow access if authenticated
});

export default router;
