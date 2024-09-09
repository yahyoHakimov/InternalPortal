<template>
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        <h2 class="text-2xl font-semibold text-gray-900 mb-6">Document Management</h2>

        <!-- Document Repository -->
        <div class="bg-white shadow overflow-hidden sm:rounded-lg mb-8">
            <div class="px-4 py-5 sm:px-6 flex justify-between items-center">
                <h3 class="text-lg leading-6 font-medium text-gray-900">Document Repository</h3>
                <button @click="showUploadModal = true" class="bg-blue-600 text-white px-4 py-2 rounded-md hover:bg-blue-700 transition-colors duration-300">
                    Upload Document
                </button>
            </div>
            <div class="border-t border-gray-200">
                <ul class="divide-y divide-gray-200">
                    <li v-for="doc in documents" :key="doc.id" class="px-4 py-4 sm:px-6 flex items-center justify-between">
                        <div class="flex items-center">
                            <svg class="h-6 w-6 text-gray-400 mr-3" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M7 21h10a2 2 0 002-2V9.414a1 1 0 00-.293-.707l-5.414-5.414A1 1 0 0012.586 3H7a2 2 0 00-2 2v14a2 2 0 002 2z" />
                            </svg>
                            <div>
                                <p class="text-sm font-medium text-blue-600">{{ doc.name }}</p>
                                <p class="text-xs text-gray-500">Uploaded on {{ doc.uploadDate }} by {{ doc.uploader }}</p>
                            </div>
                        </div>
                        <div class="flex space-x-2">
                            <button @click="downloadDocument(doc)" class="text-blue-600 hover:text-blue-800">Download</button>
                            <button @click="deleteDocument(doc.id)" class="text-red-600 hover:text-red-800">Delete</button>
                        </div>
                    </li>
                </ul>
            </div>
        </div>

        <!-- Document Request -->
        <div class="bg-white shadow sm:rounded-lg">
            <div class="px-4 py-5 sm:p-6">
                <h3 class="text-lg leading-6 font-medium text-gray-900 mb-4">Request Document</h3>
                <form @submit.prevent="submitDocumentRequest">
                    <div class="grid grid-cols-1 gap-y-6 gap-x-4 sm:grid-cols-6">
                        <div class="sm:col-span-4">
                            <label for="document-type" class="block text-sm font-medium text-gray-700">Document Type</label>
                            <select id="document-type" v-model="documentRequest.type" class="mt-1 block w-full pl-3 pr-10 py-2 text-base border-gray-300 focus:outline-none focus:ring-blue-500 focus:border-blue-500 sm:text-sm rounded-md">
                                <option value="contract">Contract</option>
                                <option value="policy">Policy</option>
                                <option value="report">Report</option>
                                <option value="other">Other</option>
                            </select>
                        </div>
                        <div class="sm:col-span-6">
                            <label for="request-details" class="block text-sm font-medium text-gray-700">Request Details</label>
                            <textarea id="request-details" v-model="documentRequest.details" rows="3" class="mt-1 block w-full shadow-sm sm:text-sm focus:ring-blue-500 focus:border-blue-500 border-gray-300 rounded-md"></textarea>
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

        <!-- Upload Document Modal -->
        <div v-if="showUploadModal" class="fixed inset-0 bg-gray-600 bg-opacity-50 overflow-y-auto h-full w-full flex items-center justify-center">
            <div class="bg-white p-8 rounded-md shadow-xl w-full max-w-md">
                <h3 class="text-lg font-semibold mb-4">Upload Document</h3>
                <form @submit.prevent="uploadDocument">
                    <div class="mb-4">
                        <label for="document-name" class="block text-sm font-medium text-gray-700">Document Name</label>
                        <input type="text" id="document-name" v-model="newDocument.name" required class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-300 focus:ring focus:ring-blue-200 focus:ring-opacity-50">
                    </div>
                    <div class="mb-4">
                        <label for="document-file" class="block text-sm font-medium text-gray-700">File</label>
                        <input type="file" id="document-file" @change="handleFileUpload" required class="mt-1 block w-full">
                    </div>
                    <div class="flex justify-end space-x-2">
                        <button type="button" @click="showUploadModal = false" class="px-4 py-2 border border-gray-300 rounded-md text-sm font-medium text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500">
                            Cancel
                        </button>
                        <button type="submit" class="px-4 py-2 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500">
                            Upload
                        </button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref } from 'vue'

const documents = ref([
  { id: 1, name: 'Employee Handbook.pdf', uploadDate: '2023-06-01', uploader: 'HR Department' },
  { id: 2, name: 'Q2 Financial Report.xlsx', uploadDate: '2023-05-15', uploader: 'Finance Team' },
  { id: 3, name: 'IT Security Policy.docx', uploadDate: '2023-04-30', uploader: 'IT Department' },
])

const showUploadModal = ref(false)
const newDocument = ref({ name: '', file: null })

const documentRequest = ref({ type: '', details: '' })

const downloadDocument = (doc) => {
  // In a real application, this would trigger a download
  console.log(`Downloading document: ${doc.name}`)
}

const deleteDocument = (id) => {
  documents.value = documents.value.filter(doc => doc.id !== id)
}

const handleFileUpload = (event) => {
  newDocument.value.file = event.target.files[0]
}

const uploadDocument = () => {
  // In a real application, you would send the file to the server here
  documents.value.push({
    id: documents.value.length + 1,
    name: newDocument.value.name,
    uploadDate: new Date().toISOString().split('T')[0],
    uploader: 'Current User'
  })
  showUploadModal.value = false
  newDocument.value = { name: '', file: null }
}

const submitDocumentRequest = () => {
  // In a real application, you would send this request to the server
  console.log('Document request submitted:', documentRequest.value)
  documentRequest.value = { type: '', details: '' }
}
</script>