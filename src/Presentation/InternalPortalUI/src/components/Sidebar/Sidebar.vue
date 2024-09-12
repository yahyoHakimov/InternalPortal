<template>
    <aside class="bg-blue-800 text-white transition-all duration-300 ease-in-out"
           :class="{ 'w-16': !isSidebarExpanded, 'w-64': isSidebarExpanded }"
           @mouseenter="expandSidebar"
           @mouseleave="collapseSidebar">
        <div class="flex items-center justify-center h-16">
            <h1 class="text-xl font-bold truncate transition-opacity duration-300"
                :class="{ 'opacity-0': !isSidebarExpanded, 'opacity-100': isSidebarExpanded }">
                Internal Portal
            </h1>
        </div>
        <nav>
            <ul class="space-y-2">
                <li v-for="(item, index) in navItems" :key="index">
                    <router-link :to="item.route"
                                 class="flex items-center px-4 py-2 text-gray-300 hover:text-white hover:bg-blue-700 transition-all duration-300">
                        <component :is="item.icon" class="w-5 h-5 transition-all duration-300"
                                   :class="{ 'mr-0': !isSidebarExpanded, 'mr-3': isSidebarExpanded }" />
                        <span v-if="isSidebarExpanded" class="transition-opacity duration-300 opacity-100">
                            {{ item.label }}
                        </span>
                    </router-link>
                </li>
            </ul>
        </nav>
    </aside>
</template>

<script setup>
import { ref } from 'vue'
import { Home, Users, BarChart2, Megaphone, User, Calendar, FileText, MessageSquare, Clipboard } from 'lucide-vue-next'

const isSidebarExpanded = ref(false)

const expandSidebar = () => {
  isSidebarExpanded.value = true
}

const collapseSidebar = () => {
  isSidebarExpanded.value = false
}

const navItems = [
  { icon: Home, label: 'Dashboard', route: '/' },
  { icon: Users, label: 'Employee', route: '/employee' },
  { icon: BarChart2, label: 'KPI', route: '/kpi' },
  { icon: Megaphone, label: 'Announcements', route: '/announcements' },
  { icon: User, label: 'Profile', route: '/profile' },
  { icon: Calendar, label: 'Attendance', route: '/attendance' },
  { icon: FileText, label: 'Documents', route: '/documents' },
  { icon: MessageSquare, label: 'Feedback', route: '/feedback' },
  { icon: Calendar, label: 'Meetings', route: '/meetings' },
  { icon: Calendar, label: 'Meetings', route: '/meetings1' },
  { icon: Clipboard, label: 'Requests', route: '/requests' },
]
</script>
