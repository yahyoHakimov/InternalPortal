<template>
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        <h2 class="text-2xl font-semibold text-gray-900 mb-6">Employee Management</h2>

        <!-- Employee List -->
        <div class="bg-white shadow overflow-hidden sm:rounded-md mb-6">
            <ul class="divide-y divide-gray-200">
                <li v-for="employee in employees" :key="employee.id" class="px-6 py-4 flex items-center justify-between">
                    <div class="flex items-center">
                        <img class="h-10 w-10 rounded-full mr-4" :src="employee.photo" :alt="employee.name" />
                        <div>
                            <p class="text-sm font-medium text-gray-900">{{ employee.name }}</p>
                            <p class="text-sm text-gray-500">{{ employee.department }} - {{ employee.role }}</p>
                        </div>
                    </div>
                    <div class="flex space-x-2">
                        <button @click="viewProfile(employee.id)" class="text-blue-600 hover:text-blue-800">View Profile</button>
                        <button @click="editEmployee(employee.id)" class="text-green-600 hover:text-green-800">Edit</button>
                        <button @click="deactivateEmployee(employee.id)" class="text-red-600 hover:text-red-800">Deactivate</button>
                    </div>
                </li>
            </ul>
        </div>

        <!-- Add Employee Button -->
        <button @click="showAddEmployeeModal = true" class="bg-blue-600 text-white px-4 py-2 rounded-md hover:bg-blue-700 transition-colors duration-300">
            Add Employee
        </button>

        <!-- Add Employee Modal -->
        <div v-if="showAddEmployeeModal" class="fixed inset-0 bg-gray-600 bg-opacity-50 overflow-y-auto h-full w-full flex items-center justify-center">
            <div class="bg-white p-8 rounded-md shadow-xl w-full max-w-md">
                <h3 class="text-lg font-semibold mb-4">Add New Employee</h3>
                <form @submit.prevent="addEmployee">
                    <div class="mb-4">
                        <label for="name" class="block text-sm font-medium text-gray-700">Name</label>
                        <input type="text" id="name" v-model="newEmployee.name" required class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-300 focus:ring focus:ring-blue-200 focus:ring-opacity-50">
                    </div>
                    <div class="mb-4">
                        <label for="department" class="block text-sm font-medium text-gray-700">Department</label>
                        <input type="text" id="department" v-model="newEmployee.department" required class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-300 focus:ring focus:ring-blue-200 focus:ring-opacity-50">
                    </div>
                    <div class="mb-4">
                        <label for="role" class="block text-sm font-medium text-gray-700">Role</label>
                        <input type="text" id="role" v-model="newEmployee.role" required class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-300 focus:ring focus:ring-blue-200 focus:ring-opacity-50">
                    </div>
                    <div class="flex justify-end space-x-2">
                        <button type="button" @click="showAddEmployeeModal = false" class="px-4 py-2 border border-gray-300 rounded-md text-sm font-medium text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500">
                            Cancel
                        </button>
                        <button type="submit" class="px-4 py-2 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500">
                            Add Employee
                        </button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useAppStore } from '@/stores/counter'
import { useRouter } from 'vue-router'

const store = useAppStore()
const router = useRouter()

const employees = ref([])
const showAddEmployeeModal = ref(false)
const newEmployee = ref({
  name: '',
  department: '',
  role: '',
})

onMounted(() => {
  // In a real application, you would fetch this data from an API
  employees.value = [
    { id: 1, name: 'John Doe', department: 'IT', role: 'Developer', photo: '/placeholder.svg?height=40&width=40' },
    { id: 2, name: 'Jane Smith', department: 'HR', role: 'Manager', photo: '/placeholder.svg?height=40&width=40' },
    { id: 3, name: 'Bob Johnson', department: 'Finance', role: 'Accountant', photo: '/placeholder.svg?height=40&width=40' },
  ]
  store.setEmployees(employees.value)
})

const viewProfile = (id) => {
  router.push({ name: 'Profile', params: { id } })
}

const editEmployee = (id) => {
  // Implement edit functionality
  console.log('Edit employee', id)
}

const deactivateEmployee = (id) => {
  // Implement deactivate functionality
  console.log('Deactivate employee', id)
}

const addEmployee = () => {
  const newId = employees.value.length + 1
  const employee = {
    id: newId,
    ...newEmployee.value,
    photo: '/placeholder.svg?height=40&width=40'
  }
  employees.value.push(employee)
  store.setEmployees(employees.value)
  showAddEmployeeModal.value = false
  newEmployee.value = { name: '', department: '', role: '' }
}
</script>