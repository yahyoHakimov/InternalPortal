<template>
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        <h2 class="text-2xl font-semibold text-gray-900 mb-6">Announcements</h2>

        <!-- Announcements List -->
        <div class="bg-white shadow overflow-hidden sm:rounded-md mb-6">
            <ul class="divide-y divide-gray-200">
                <li v-for="announcement in announcements" :key="announcement.id" class="px-6 py-4">
                    <div class="flex items-center justify-between">
                        <div>
                            <h3 class="text-lg font-medium text-gray-900">{{ announcement.title }}</h3>
                            <p class="mt-1 text-sm text-gray-500">{{ announcement.date }}</p>
                        </div>
                        <span :class="[
              'px-2 inline-flex text-xs leading-5 font-semibold rounded-full',
              announcement.status === 'active' ? 'bg-green-100 text-green-800' : 'bg-red-100 text-red-800'
            ]">
                            {{ announcement.status }}
                        </span>
                    </div>
                    <p class="mt-2 text-sm text-gray-600">{{ announcement.content }}</p>
                </li>
            </ul>
        </div>

        <!-- Add Announcement Button -->
        <button @click="showAddAnnouncementModal = true" class="bg-blue-600 text-white px-4 py-2 rounded-md hover:bg-blue-700 transition-colors duration-300">
            Add Announcement
        </button>

        <!-- Add Announcement Modal -->
        <div v-if="showAddAnnouncementModal" class="fixed inset-0 bg-gray-600 bg-opacity-50 overflow-y-auto h-full w-full flex items-center justify-center">
            <div class="bg-white p-8 rounded-md shadow-xl w-full max-w-md">
                <h3 class="text-lg font-semibold mb-4">Add New Announcement</h3>
                <form @submit.prevent="addAnnouncement">
                    <div class="mb-4">
                        <label for="title" class="block text-sm font-medium text-gray-700">Title</label>
                        <input type="text" id="title" v-model="newAnnouncement.title" required class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-300 focus:ring focus:ring-blue-200 focus:ring-opacity-50">
                    </div>
                    <div class="mb-4">
                        <label for="content" class="block text-sm font-medium text-gray-700">Content</label>
                        <textarea id="content" v-model="newAnnouncement.content" rows="3" required class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-300 focus:ring focus:ring-blue-200 focus:ring-opacity-50"></textarea>
                    </div>
                    <div class="flex justify-end space-x-2">
                        <button type="button" @click="showAddAnnouncementModal = false" class="px-4 py-2 border border-gray-300 rounded-md text-sm font-medium text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500">
                            Cancel
                        </button>
                        <button type="submit" class="px-4 py-2 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500">
                            Add Announcement
                        </button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref } from 'vue'
import { useAppStore } from '@/stores/counter'

const store = useAppStore()

const announcements = ref([
  { id: 1, title: 'Quarterly Town Hall Meeting', date: '2023-06-15', status: 'active', content: 'Join us for our quarterly town hall meeting on Friday at 2 PM in the main auditorium.' },
  { id: 2, title: 'New Benefits Package', date: '2023-06-10', status: 'active', content: 'We are excited to announce our new employee benefits package. Please review the details in your email.' },
  { id: 3, title: 'Office Closure', date: '2023-05-28', status: 'expired', content: 'The office will be closed on Monday, May 29th, for the Memorial Day holiday.' },
])

const showAddAnnouncementModal = ref(false)
const newAnnouncement = ref({
  title: '',
  content: '',
})

const addAnnouncement = () => {
  const announcement = {
    id: announcements.value.length + 1,
    ...newAnnouncement.value,
    date: new Date().toISOString().split('T')[0],
    status: 'active',
  }
  announcements.value.unshift(announcement)
  store.addAnnouncement(announcement)
  showAddAnnouncementModal.value = false
  newAnnouncement.value = { title: '', content: '' }
}
</script>