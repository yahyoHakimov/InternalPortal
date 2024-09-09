<template>
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        <h2 class="text-2xl font-semibold text-gray-900 mb-6">Request Management</h2>

        <!-- New Request Form -->
        <div class="bg-white shadow sm:rounded-lg mb-8">
            <div class="px-4 py-5 sm:p-6">
                <h3 class="text-lg leading-6 font-medium text-gray-900 mb-4">Submit New Request</h3>
                <form @submit.prevent="submitRequest">
                    <div class="grid grid-cols-1 gap-y-6 gap-x-4 sm:grid-cols-6">
                        <div class="sm:col-span-3">
                            <label for="request-type" class="block text-sm font-medium text-gray-700">Request Type</label>
                            <select id="request-type" v-model="newRequest.type" required class="mt-1 block w-full pl-3 pr-10 py-2 text-base border-gray-300 focus:outline-none focus:ring-blue-500 focus:border-blue-500 sm:text-sm rounded-md">
                                <option value="leave">Leave Application</option>
                                <option value="it-support">IT Support</option>
                                <option value="document-access">Document Access</option>
                                <option value="other">Other</option>
                            </select>
                        </div>
                        <div class="sm:col-span-3">
                            <label for="priority" class="block text-sm font-medium text-gray-700">Priority</label>
                            <select id="priority" v-model="newRequest.priority" required class="mt-1 block w-full pl-3 pr-10 py-2 text-base border-gray-300 focus:outline-none focus:ring-blue-500 focus:border-blue-500 sm:text-sm rounded-md">
                                <option value="low">Low</option>
                                <option value="medium">Medium</option>
                                <option value="high">High</option>
                            </select>
                        </div>
                        <div class="sm:col-span-6">
                            <label for="request-details" class="block text-sm font-medium text-gray-700">Request Details</label>
                            <textarea id="request-details" v-model="newRequest.details" rows="3" required class="mt-1 block w-full shadow-sm sm:text-sm focus:ring-blue-500 focus:border-blue-500 border-gray-300 rounded-md"></textarea>
                        </div>
                    </div>
                    <div class="mt-5">
                        <button type="submit" class="inline-flex items-center px-4 py-2 border border-transparent text-sm font-medium rounded-md shadow-sm text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500">
                            Submit Request
                        </button>
                    </div>
                </form>
            </div>
        </div>

        <!-- Request List -->
        <div class="bg-white shadow overflow-hidden sm:rounded-lg">
            <div class="px-4 py-5 sm:px-6">
                <h3 class="text-lg leading-6 font-medium text-gray-900">Recent Requests</h3>
            </div>
            <div class="border-t border-gray-200">
                <ul class="divide-y divide-gray-200">
                    <li v-for="request in requests" :key="request.id" class="px-4 py-4 sm:px-6">
                        <div class="flex items-center justify-between">
                            <p class="text-sm font-medium text-blue-600 truncate">
                                {{ request.type.charAt(0).toUpperCase() + request.type.slice(1).replace('-', ' ') }}
                            </p>
                            <div class="ml-2 flex-shrink-0 flex">
                                <p :class="[
                  'px-2 inline-flex text-xs leading-5 font-semibold rounded-full',
                  request.status === 'approved' ? 'bg-green-100 text-green-800' :
                  request.status === 'rejected' ? 'bg-red-100 text-red-800' :
                  'bg-yellow-100 text-yellow-800'
                ]">
                                    {{ request.status.charAt(0).toUpperCase() + request.status.slice(1) }}
                                </p>
                            </div>
                        </div>
                        <div class="mt-2 sm:flex sm:justify-between">
                            <div class="sm:flex">
                                <p class="flex items-center text-sm text-gray-500">
                                    {{ request.details }}
                                </p>
                            </div>
                            <div class="mt-2 flex items-center text-sm text-gray-500 sm:mt-0">
                                <p>
                                    Submitted on {{ request.date }}
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
import { useAppStore } from '@/stores/counter'

const store = useAppStore()

const newRequest = ref({
  type: '',
  priority: '',
  details: ''
})

const requests = ref([
  { id: 1, type: 'leave', details: 'Annual leave request for next week', status: 'pending', date: '2023-06-01' },
  { id: 2, type: 'it-support', details: 'Need help setting up new laptop', status: 'approved', date: '2023-05-28' },
  { id: 3, type: 'document-access', details: 'Request access to Q1 financial reports', status: 'rejected', date: '2023-05-25' },
])

const submitRequest = () => {
  const request = {
    id: requests.value.length + 1,
    ...newRequest.value,
    status: 'pending',
    date: new Date().toISOString().split('T')[0]
  }
  requests.value.unshift(request)
  store.addRequest(request)
  newRequest.value = { type: '', priority: '', details: '' }
}
</script>