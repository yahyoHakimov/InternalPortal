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
import Unauthorized from '@/views/Error/Unauthorized.vue' // Assuming you have an unauthorized page

// Define your routes
const routes = [
  { path: '/', component: Dashboard, meta: { requiresAuth: true } },
  { path: '/login', component: LoginPage, meta: { requiresAuth: false } },
  { path: '/register', component: RegisterPage, meta: { requiresAuth: false } },
  { path: '/employee', component: Employee, meta: { requiresAuth: true } },
  { path: '/kpi', component: KPI, meta: { requiresAuth: true } },
  { path: '/announcements', component: Announcement, meta: { requiresAuth: true } },
  { path: '/profile', component: Profile, meta: { requiresAuth: true } },
  { path: '/attendance', component: Attendance, meta: { requiresAuth: true } },
  { path: '/documents', component: Documents, meta: { requiresAuth: true } },
  { path: '/feedback', component: Feedback, meta: { requiresAuth: true } },
  { path: '/meetings', component: Meetings, meta: { requiresAuth: true } },
  { path: '/requests', component: Request, meta: { requiresAuth: true } },
  { path: '/unauthorized', component: Unauthorized }, // Add a page for unauthorized access
  { path: '/:pathMatch(.*)*', redirect: '/' }
]

// Create the router instance
const router = createRouter({
  history: createWebHistory(),
  routes,
})

// Navigation guard to handle route protection
router.beforeEach((to, from, next) => {
  const loggedIn = localStorage.getItem('authToken');
  const userRoles = JSON.parse(localStorage.getItem('userRoles')) || [];

  if (to.meta.requiresAuth && !loggedIn) {
    // Redirect to login if not authenticated
    return next('/login');
  }

  if (to.meta.roles && !to.meta.roles.some(role => userRoles.includes(role))) {
    // Redirect to unauthorized page if user doesn't have the required roles
    return next('/unauthorized');
  }

  next();
});

export default router;
