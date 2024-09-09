<template>
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
      <h2 class="text-3xl font-bold text-gray-900 mb-8">Dashboard</h2>
      
      <!-- Key Metrics -->
      <div class="grid grid-cols-1 gap-5 sm:grid-cols-2 lg:grid-cols-4 mb-8">
        <div v-for="(metric, index) in keyMetrics" :key="index" class="bg-white overflow-hidden shadow rounded-lg">
          <div class="px-4 py-5 sm:p-6">
            <div class="flex items-center">
              <div class="flex-shrink-0 bg-blue-500 rounded-md p-3">
                <component :is="metric.icon" class="h-6 w-6 text-white" />
              </div>
              <div class="ml-5 w-0 flex-1">
                <dl>
                  <dt class="text-sm font-medium text-gray-500 truncate">{{ metric.name }}</dt>
                  <dd class="text-2xl font-semibold text-gray-900">{{ metric.value }}</dd>
                </dl>
              </div>
            </div>
          </div>
          <div class="bg-gray-50 px-4 py-4 sm:px-6">
            <div class="text-sm">
              <a href="#" class="font-medium text-blue-600 hover:text-blue-500">View all<span class="sr-only"> {{ metric.name }}</span></a>
            </div>
          </div>
        </div>
      </div>
  
      <!-- KPI Performance -->
      <div class="bg-white shadow rounded-lg mb-8">
        <div class="px-4 py-5 sm:px-6 border-b border-gray-200">
          <h3 class="text-lg leading-6 font-medium text-gray-900">Current KPI Performance</h3>
        </div>
        <div class="px-4 py-5 sm:p-6">
          <div class="space-y-6">
            <div v-for="(kpi, index) in kpis" :key="index" class="flex items-center">
              <div class="w-64 mr-4">
                <div class="text-sm font-medium text-gray-500 mb-1">{{ kpi.name }}</div>
                <div class="flex items-center">
                  <div class="w-full bg-gray-200 rounded-full h-2.5">
                    <div class="bg-blue-600 h-2.5 rounded-full" :style="{ width: `${kpi.value}%` }"></div>
                  </div>
                  <span class="ml-3 text-sm font-medium text-gray-900">{{ kpi.value }}%</span>
                </div>
              </div>
              <div class="flex-grow text-right">
                <span :class="[
                  'inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium',
                  kpi.status === 'on-track' ? 'bg-green-100 text-green-800' : 
                  kpi.status === 'at-risk' ? 'bg-yellow-100 text-yellow-800' : 
                  'bg-red-100 text-red-800'
                ]">
                  {{ kpi.status }}
                </span>
              </div>
            </div>
          </div>
        </div>
      </div>
  
      <!-- Recent Activities and Announcements -->
      <div class="grid grid-cols-1 lg:grid-cols-2 gap-8">
        <!-- Recent Activities -->
        <div class="bg-white shadow rounded-lg">
          <div class="px-4 py-5 sm:px-6 border-b border-gray-200">
            <h3 class="text-lg leading-6 font-medium text-gray-900">Recent Activities</h3>
          </div>
          <div class="px-4 py-5 sm:p-6">
            <ul class="divide-y divide-gray-200">
              <li v-for="activity in recentActivities" :key="activity.id" class="py-4">
                <div class="flex space-x-3">
                  <div :class="[
                    'flex-shrink-0 h-10 w-10 rounded-full flex items-center justify-center',
                    activity.type === 'employee' ? 'bg-blue-100' :
                    activity.type === 'announcement' ? 'bg-green-100' :
                    activity.type === 'meeting' ? 'bg-yellow-100' :
                    'bg-purple-100'
                  ]">
                    <component :is="activity.icon" :class="[
                      'h-6 w-6',
                      activity.type === 'employee' ? 'text-blue-600' :
                      activity.type === 'announcement' ? 'text-green-600' :
                      activity.type === 'meeting' ? 'text-yellow-600' :
                      'text-purple-600'
                    ]" />
                  </div>
                  <div class="flex-1 space-y-1">
                    <div class="flex items-center justify-between">
                      <h3 class="text-sm font-medium">{{ activity.title }}</h3>
                      <p class="text-sm text-gray-500">{{ activity.time }}</p>
                    </div>
                    <p class="text-sm text-gray-500">{{ activity.description }}</p>
                  </div>
                </div>
              </li>
            </ul>
          </div>
        </div>
  
        <!-- Announcements -->
        <div class="bg-white shadow rounded-lg">
          <div class="px-4 py-5 sm:px-6 border-b border-gray-200">
            <h3 class="text-lg leading-6 font-medium text-gray-900">Recent Announcements</h3>
          </div>
          <div class="px-4 py-5 sm:p-6">
            <ul class="divide-y divide-gray-200">
              <li v-for="announcement in announcements" :key="announcement.id" class="py-4">
                <div class="flex space-x-3">
                  <div class="flex-shrink-0">
                    <Megaphone class="h-6 w-6 text-blue-600" />
                  </div>
                  <div class="flex-1 space-y-1">
                    <div class="flex items-center justify-between">
                      <h3 class="text-sm font-medium">{{ announcement.title }}</h3>
                      <p class="text-sm text-gray-500">{{ announcement.date }}</p>
                    </div>
                    <p class="text-sm text-gray-500">{{ announcement.content }}</p>
                  </div>
                </div>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <script setup>
  import { ref, computed } from 'vue'
  import { useAppStore } from '@/stores/counter'
  import { Users, BarChart2, Calendar, Clipboard, UserPlus, Megaphone, Video } from 'lucide-vue-next'
  
  const store = useAppStore()
  
  const keyMetrics = computed(() => [
    { name: 'Total Employees', value: store.totalEmployees, icon: Users },
    { name: 'Active Announcements', value: store.activeAnnouncements, icon: Megaphone },
    { name: 'Upcoming Meetings', value: store.upcomingMeetings, icon: Calendar },
    { name: 'Pending Requests', value: store.pendingRequests, icon: Clipboard },
  ])
  
  const kpis = ref([
    { name: 'Customer Satisfaction', value: 85, status: 'on-track' },
    { name: 'Loan Processing Time', value: 70, status: 'at-risk' },
    { name: 'New Account Acquisitions', value: 60, status: 'off-track' },
    { name: 'Employee Productivity', value: 90, status: 'on-track' },
  ])
  
  const recentActivities = ref([
    { id: 1, type: 'employee', icon: UserPlus, title: 'New Employee', description: 'John Doe joined the IT Department', time: '2 hours ago' },
    { id: 2, type: 'announcement', icon: Megaphone, title: 'New Announcement', description: 'Quarterly Town Hall Meeting scheduled', time: '4 hours ago' },
    { id: 3, type: 'meeting', icon: Video, title: 'Team Meeting', description: 'Project kickoff meeting for Mobile App', time: 'Yesterday' },
    { id: 4, type: 'request', icon: Clipboard, title: 'Leave Request', description: 'Jane Smith\'s leave request approved', time: '2 days ago' },
  ])
  
  const announcements = ref([
    { id: 1, title: 'Quarterly Town Hall Meeting', date: 'Jun 15, 2023', content: 'Join us for our quarterly town hall meeting on Friday at 2 PM in the main auditorium.' },
    { id: 2, title: 'New Benefits Package', date: 'Jun 10, 2023', content: 'We are excited to announce our new employee benefits package. Please review the details in your email.' },
    { id: 3, title: 'IT System Maintenance', date: 'Jun 5, 2023', content: 'The IT systems will undergo maintenance this weekend. Please save your work and log out by Friday 6 PM.' },
  ])
  </script>