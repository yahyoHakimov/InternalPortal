<template>
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        <h2 class="text-2xl font-semibold text-gray-900 mb-6">Meeting Management</h2>

        <!-- Calendar View -->
        <div class="bg-white shadow overflow-hidden sm:rounded-lg mb-8">
            <div class="px-4 py-5 sm:px-6 flex justify-between items-center">
                <h3 class="text-lg leading-6 font-medium text-gray-900">Meeting Calendar</h3>
                <button @click="showScheduleModal = true" class="bg-blue-600 text-white px-4 py-2 rounded-md hover:bg-blue-700 transition-colors duration-300">
                    Schedule Meeting
                </button>
            </div>
            <div class="border-t border-gray-200 px-4 py-5 sm:p-0">
                <div class="grid grid-cols-7 gap-2 text-center font-semibold mb-2">
                    <div v-for="day in ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat']" :key="day" class="py-2">
                        {{ day }}
                    </div>
                </div>
                <div class="grid grid-cols-7 gap-2">
                    <div v-for="day in calendarDays" :key="day.date" class="border rounded-md p-2 text-center">
                        <div class="text-sm">{{ day.date }}</div>
                        <div v-if="day.meetings.length > 0" class="mt-1 text-xs text-blue-600">
                            {{ day.meetings.length }} meeting{{ day.meetings.length > 1 ? 's' : '' }}
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Upcoming Meetings -->
        <div class="bg-white shadow overflow-hidden sm:rounded-lg">
            <div class="px-4 py-5 sm:px-6">
                <h3 class="text-lg leading-6 font-medium text-gray-900">Upcoming Meetings</h3>
            </div>
            <div class="border-t border-gray-200">
                <ul class="divide-y divide-gray-200">
                    <li v-for="meeting in upcomingMeetings" :key="meeting.id" class="px-4 py-4 sm:px-6">
                        <div class="flex items-center justify-between">
                            <p class="text-sm font-medium text-blue-600 truncate">
                                {{ meeting.title }}
                            </p>
                            <div class="ml-2 flex-shrink-0 flex">
                                <p class="px-2 inline-flex text-xs leading-5 font-semibold rounded-full bg-green-100 text-green-800">
                                    {{ meeting.type }}
                                </p>
                            </div>
                        </div>
                        <div class="mt-2 sm:flex sm:justify-between">
                            <div class="sm:flex">
                                <p class="flex items-center text-sm text-gray-500">
                                    <svg class="flex-shrink-0 mr-1.5 h-5 w-5 text-gray-400" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
                                        <path fill-rule="evenodd" d="M6 2a1 1 0 00-1 1v1H4a2 2 0 00-2 2v10a2 2 0 002 2h12a2 2 0 002-2V6a2 2 0 00-2-2h-1V3a1 1 0 10-2 0v1H7V3a1 1 0 00-1-1zm0 5a1 1 0 000 2h8a1 1 0 100-2H6z" clip-rule="evenodd" />
                                    </svg>
                                    {{ meeting.date }} at {{ meeting.time }}
                                </p>
                            </div>
                            <div class="mt-2 flex items-center text-sm text-gray-500 sm:mt-0">
                                <svg class="flex-shrink-0 mr-1.5 h-5 w-5 text-gray-400" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
                                    <path fill-rule="evenodd" d="M10 9a3 3 0 100-6 3 3 0 000 6zm-7 9a7 7 0 1114 0H3z" clip-rule="evenodd" />
                                </svg>
                                {{ meeting.participants.join(', ') }}
                            </div>
                        </div>
                    </li>
                </ul>
            </div>
        </div>

        <!-- Schedule Meeting Modal -->
        <div v-if="showScheduleModal" class="fixed inset-0 bg-gray-600 bg-opacity-50 overflow-y-auto h-full w-full flex items-center justify-center">
            <div class="bg-white p-8 rounded-md shadow-xl w-full max-w-md">
                <h3 class="text-lg font-semibold mb-4">Schedule Meeting</h3>
                <form @submit.prevent="scheduleMeeting">
                    <div class="mb-4">
                        <label for="meeting-title" class="block text-sm font-medium text-gray-700">Meeting Title</label>
                        <input type="text" id="meeting-title" v-model="newMeeting.title" required class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-300 focus:ring focus:ring-blue-200 focus:ring-opacity-50">
                    </div>
                    <div class="mb-4">
                        <label for="meeting-date" class="block text-sm font-medium text-gray-700">Date</label>
                        <input type="date" id="meeting-date" v-model="newMeeting.date" required class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-300 focus:ring focus:ring-blue-200 focus:ring-opacity-50">
                    </div>
                    <div class="mb-4">
                        <label for="meeting-time" class="block text-sm font-medium text-gray-700">Time</label>
                        <input type="time" id="meeting-time" v-model="newMeeting.time" required class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-300 focus:ring focus:ring-blue-200 focus:ring-opacity-50">
                    </div>
                    <div class="mb-4">
                        <label for="meeting-type" class="block text-sm font-medium text-gray-700">Meeting Type</label>
                        <select id="meeting-type" v-model="newMeeting.type" required class="mt-1 block w-full pl-3 pr-10 py-2 text-base border-gray-300 focus:outline-none focus:ring-blue-500 focus:border-blue-500 sm:text-sm rounded-md">
                            <option value="In-person">In-person</option>
                            <option value="Virtual">Virtual</option>
                        </select>
                    </div>
                    <div class="mb-4">
                        <label for="meeting-participants" class="block text-sm font-medium text-gray-700">Participants (comma-separated)</label>
                        <input type="text" id="meeting-participants" v-model="newMeeting.participants" required class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-300 focus:ring focus:ring-blue-200 focus:ring-opacity-50">
                    </div>
                    <div class="flex justify-end space-x-2">
                        <button type="button" @click="showScheduleModal = false" class="px-4 py-2 border border-gray-300 rounded-md text-sm font-medium text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500">
                            Cancel
                        </button>
                        <button type="submit" class="px-4 py-2 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500">
                            Schedule
                        </button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, computed } from 'vue'

const showScheduleModal = ref(false)
const newMeeting = ref({
  title: '',
  date: '',
  time: '',
  type: '',
  participants: ''
})

const upcomingMeetings = ref([
  { id: 1, title: 'Team Standup', date: '2023-06-15', time: '09:00', type: 'Virtual', participants: ['John Doe', 'Jane Smith', 'Bob Johnson'] },
  { id: 2, title: 'Project Review', date: '2023-06-16', time: '14:00', type: 'In-person', participants: ['Alice Brown', 'Charlie Davis'] },
  { id: 3, title: 'Client Meeting', date: '2023-06-17', time: '11:00', type: 'Virtual', participants: ['Eve Wilson', 'Frank Thomas', 'Grace Lee'] },
])

const currentDate = new Date()
const currentMonth = currentDate.getMonth()
const currentYear = currentDate.getFullYear()

const daysInMonth = new Date(currentYear, currentMonth + 1, 0).getDate()
const firstDayOfMonth = new Date(currentYear, currentMonth, 1).getDay()

const calendarDays = computed(() => {
  const days = []
  for (let i = 0; i < firstDayOfMonth; i++) {
    days.push({ date: '', meetings: [] })
  }
  for (let i = 1; i <= daysInMonth; i++) {
    const date = `${currentYear}-${(currentMonth + 1).toString().padStart(2, '0')}-${i.toString().padStart(2, '0')}`
    const meetings = upcomingMeetings.value.filter(meeting => meeting.date === date)
    days.push({ date: i, meetings })
  }
  return days
})

const scheduleMeeting = () => {
  const meeting = {
    id: upcomingMeetings.value.length + 1,
    ...newMeeting.value,
    participants: newMeeting.value.participants.split(',').map(p => p.trim())
  }
  upcomingMeetings.value.push(meeting)
  showScheduleModal.value = false
  newMeeting.value = { title: '', date: '', time: '', type: '', participants: '' }
}
</script>