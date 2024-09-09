<template>
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        <h2 class="text-2xl font-semibold text-gray-900 mb-6">Feedback Management</h2>

        <!-- Feedback Form -->
        <div class="bg-white shadow sm:rounded-lg mb-8">
            <div class="px-4 py-5 sm:p-6">
                <h3 class="text-lg leading-6 font-medium text-gray-900 mb-4">Submit Feedback</h3>
                <form @submit.prevent="submitFeedback">
                    <div class="grid grid-cols-1 gap-y-6 gap-x-4 sm:grid-cols-6">
                        <div class="sm:col-span-4">
                            <label for="feedback-type" class="block text-sm font-medium text-gray-700">Feedback Type</label>
                            <select id="feedback-type" v-model="newFeedback.type" required class="mt-1 block w-full pl-3 pr-10 py-2 text-base border-gray-300 focus:outline-none focus:ring-blue-500 focus:border-blue-500 sm:text-sm rounded-md">
                                <option value="bug">Bug Report</option>
                                <option value="feature">Feature Request</option>
                                <option value="improvement">Improvement Suggestion</option>
                                <option value="other">Other</option>
                            </select>
                        </div>
                        <div class="sm:col-span-6">
                            <label for="feedback-details" class="block text-sm font-medium text-gray-700">Feedback Details</label>
                            <textarea id="feedback-details" v-model="newFeedback.details" rows="3" required class="mt-1 block w-full shadow-sm sm:text-sm focus:ring-blue-500 focus:border-blue-500 border-gray-300 rounded-md"></textarea>
                        </div>
                    </div>
                    <div class="mt-5">
                        <button type="submit" class="inline-flex items-center px-4 py-2 border border-transparent text-sm font-medium rounded-md shadow-sm text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500">
                            Submit Feedback
                        </button>
                    </div>
                </form>
            </div>
        </div>

        <!-- Feedback List -->
        <div class="bg-white shadow overflow-hidden sm:rounded-lg">
            <div class="px-4 py-5 sm:px-6">
                <h3 class="text-lg leading-6 font-medium text-gray-900">Recent Feedback</h3>
            </div>
            <div class="border-t border-gray-200">
                <ul class="divide-y divide-gray-200">
                    <li v-for="feedback in feedbackList" :key="feedback.id" class="px-4 py-4 sm:px-6">
                        <div class="flex items-center justify-between">
                            <p class="text-sm font-medium text-blue-600 truncate">
                                {{ feedback.type.charAt(0).toUpperCase() + feedback.type.slice(1) }}
                            </p>
                            <div class="ml-2 flex-shrink-0 flex">
                                <p :class="[
                  'px-2 inline-flex text-xs leading-5 font-semibold rounded-full',
                  feedback.status === 'resolved' ? 'bg-green-100 text-green-800' : 'bg-yellow-100 text-yellow-800'
                ]">
                                    {{ feedback.status.charAt(0).toUpperCase() + feedback.status.slice(1) }}
                                </p>
                            </div>
                        </div>
                        <div class="mt-2 sm:flex sm:justify-between">
                            <div class="sm:flex">
                                <p class="flex items-center text-sm text-gray-500">
                                    {{ feedback.details }}
                                </p>
                            </div>
                            <div class="mt-2 flex items-center text-sm text-gray-500 sm:mt-0">
                                <p>
                                    Submitted on {{ feedback.date }}
                                </p>
                            </div>
                        </div>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref } from 'vue'

const newFeedback = ref({ type: '', details: '' })

const feedbackList = ref([
  { id: 1, type: 'bug', details: 'Login page is not responsive on mobile devices', status: 'pending', date: '2023-06-01' },
  { id: 2, type: 'feature', details: 'Add dark mode to the application', status: 'resolved', date: '2023-05-28' },
  { id: 3, type: 'improvement', details: 'Make the dashboard load faster', status: 'pending', date: '2023-05-25' },
])

const submitFeedback = () => {
  // In a real application, you would send this feedback to the server
  feedbackList.value.unshift({
    id: feedbackList.value.length + 1,
    ...newFeedback.value,
    status: 'pending',
    date: new Date().toISOString().split('T')[0]
  })
  newFeedback.value = { type: '', details: '' }
}
</script>