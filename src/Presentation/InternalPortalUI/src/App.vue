<template>
    <div class="flex h-screen bg-gray-100">
        <!-- Sidebar -->
        <aside class="w-64 bg-blue-800 text-white p-6">
            <div class="flex items-center mb-8">
                <h1 class="text-xl font-bold">Internal Portal</h1>
            </div>
            <nav>
                <ul class="space-y-4">
                    <li v-for="(item, index) in navItems" :key="index">
                        <router-link :to="item.route" class="flex items-center text-gray-300 hover:text-white transition-colors duration-200">
                            <component :is="item.icon" class="w-5 h-5 mr-3" />
                            {{ item.label }}
                        </router-link>
                    </li>
                </ul>
            </nav>
        </aside>

        <!-- Main Content -->
        <main class="flex-1 overflow-y-auto">
            <!-- Top Navigation -->
            <header class="bg-white shadow-sm">
                <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-4 flex items-center justify-between">
                    <div class="relative w-64">
                        <input type="text"
                               placeholder="Search..."
                               class="w-full pl-10 pr-4 py-2 rounded-lg border border-gray-300 focus:outline-none focus:ring-2 focus:ring-blue-500" />
                        <Search class="absolute left-3 top-2.5 text-gray-400" />
                    </div>
                    <div class="flex items-center space-x-4">
                        <button class="text-gray-500 hover:text-gray-700">
                            <Bell class="h-5 w-5" />
                        </button>
                        <div class="relative">
                           
                        </div>
                    </div>
                </div>
            </header>

            <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
                <router-view></router-view> <!-- This is crucial for rendering the routed components -->
            </div>
        </main>
    </div>
</template>

<script setup>
    import { ref } from 'vue'
    import { useRouter } from 'vue-router'
    import { Bell, Search, User, Users, BarChart2, Megaphone, Calendar, FileText, MessageSquare, Clipboard, ChevronRight, Home } from 'lucide-vue-next'

    const router = useRouter()

    const navItems = [
        { icon: Home, label: 'Dashboard', route: '/' }, // Added Dashboard with Home icon
        { icon: Users, label: 'Employee', route: '/employee' },
        { icon: BarChart2, label: 'KPI', route: '/kpi' },
        { icon: Megaphone, label: 'Announcements', route: '/announcements' },
        { icon: User, label: 'Profile', route: '/profile' },
        { icon: Calendar, label: 'Attendance', route: '/attendance' },
        { icon: FileText, label: 'Documents', route: '/documents' },
        { icon: MessageSquare, label: 'Feedback', route: '/feedback' },
        { icon: Calendar, label: 'Meetings', route: '/meetings' },
        { icon: Clipboard, label: 'Requests', route: '/requests' },
    ];


    const metrics = [
        { title: 'Total Employees', value: '1,234', icon: Users },
        { title: 'Active Announcements', value: '5', icon: Megaphone },
        { title: 'Upcoming Meetings', value: '3', icon: Calendar },
        { title: 'Pending Requests', value: '12', icon: Clipboard },
    ]

    const kpis = [
        { label: 'Customer Satisfaction', value: 85 },
        { label: 'Loan Processing Time', value: 70 },
        { label: 'New Account Acquisitions', value: 60 },
    ]

    const activities = [
        { icon: Users, text: 'New employee John Doe added to IT Department' },
        { icon: Megaphone, text: 'New announcement: Quarterly Town Hall Meeting' },
        { icon: Calendar, text: 'Team meeting scheduled for tomorrow at 10 AM' },
        { icon: Clipboard, text: 'Leave request approved for Jane Smith' },
    ]
</script>