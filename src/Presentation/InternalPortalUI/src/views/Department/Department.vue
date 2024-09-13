<template>
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        <h2 class="text-2xl font-semibold text-gray-900 mb-6">Organization Structure</h2>

        <!-- Organization Hierarchy -->
        <div class="bg-white shadow overflow-hidden sm:rounded-md mb-6">
            <ul class="divide-y divide-gray-200">
                <li v-for="department in departments" :key="department.departmentId" class="p-6">
                    <div class="flex items-center justify-between">
                        <div>
                            <h3 class="text-xl font-semibold text-gray-900">{{ department.departmentName }}</h3>
                            <p class="text-sm text-gray-500">Director: {{ department.director.firstName }} {{ department.director.lastName }} - {{ department.director.role }}</p>
                        </div>
                        <div>
                            <button @click="addSubDepartment(department)" class="text-green-600 hover:text-green-800">Add Sub-Department</button>
                            <button @click="editDepartment(department)" class="text-blue-600 hover:text-blue-800">Edit Department</button>
                            <button @click="deleteDepartment(department.departmentId)" class="text-red-600 hover:text-red-800">Delete Department</button>
                        </div>
                    </div>

                    <ul class="pl-6 mt-4 space-y-2">
                        <li v-for="employee in department.employees" :key="employee.employeeId" class="flex items-center justify-between">
                            <div>
                                <p class="text-sm font-medium text-gray-900">{{ employee.firstName }} {{ employee.lastName }} - {{ employee.role }}</p>
                                <p class="text-sm text-gray-500">{{ employee.jobTitle }}</p>
                            </div>
                            <div class="space-x-2">
                                <button @click="viewProfile(employee)" class="text-blue-600 hover:text-blue-800">View Profile</button>
                                <button @click="deactivateEmployee(employee.employeeId)" class="text-red-600 hover:text-red-800">Deactivate</button>
                            </div>
                        </li>
                    </ul>
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
                        <label for="firstName" class="block text-sm font-medium text-gray-700">First Name</label>
                        <input type="text" id="firstName" v-model="newEmployee.firstName" required class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-300 focus:ring focus:ring-blue-200 focus:ring-opacity-50">
                    </div>
                    <div class="mb-4">
                        <label for="lastName" class="block text-sm font-medium text-gray-700">Last Name</label>
                        <input type="text" id="lastName" v-model="newEmployee.lastName" required class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-300 focus:ring focus:ring-blue-200 focus:ring-opacity-50">
                    </div>
                    <div class="mb-4">
                        <label for="email" class="block text-sm font-medium text-gray-700">Email</label>
                        <input type="email" id="email" v-model="newEmployee.email" required class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-300 focus:ring focus:ring-blue-200 focus:ring-opacity-50">
                    </div>
                    <div class="mb-4">
                        <label for="department" class="block text-sm font-medium text-gray-700">Department</label>
                        <select id="department" v-model="newEmployee.departmentId" required class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-300 focus:ring focus:ring-blue-200 focus:ring-opacity-50">
                            <option v-for="department in departments" :key="department.departmentId" :value="department.departmentId">{{ department.departmentName }}</option>
                        </select>
                    </div>
                    <div class="mb-4">
                        <label for="role" class="block text-sm font-medium text-gray-700">Role</label>
                        <select id="role" v-model="newEmployee.role" required class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-300 focus:ring focus:ring-blue-200 focus:ring-opacity-50">
                            <option v-for="role in roles" :key="role">{{ role }}</option>
                        </select>
                    </div>
                    <div class="mb-4">
                        <label for="jobTitle" class="block text-sm font-medium text-gray-700">Job Title</label>
                        <input type="text" id="jobTitle" v-model="newEmployee.jobTitle" required class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-300 focus:ring focus:ring-blue-200 focus:ring-opacity-50">
                    </div>
                    <div class="mb-4">
                        <label for="hireDate" class="block text-sm font-medium text-gray-700">Hire Date</label>
                        <input type="date" id="hireDate" v-model="newEmployee.hireDate" required class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-300 focus:ring focus:ring-blue-200 focus:ring-opacity-50">
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

const store = useAppStore()

const employees = ref([])
const departments = ref([])
const roles = ref(['Manager', 'Employee', 'Intern', 'Director']) // Example roles

const showAddEmployeeModal = ref(false)
const newEmployee = ref({
  firstName: '',
  lastName: '',
  email: '',
  departmentId: '',
  role: '',
  jobTitle: '',
  hireDate: '',
})

const selectedEmployee = ref({})

// Fetch employees and departments from the store
onMounted(async () => {
  await store.fetchItems('employees')
  employees.value = store.items.employees

  await store.fetchItems('departments')
  departments.value = store.items.departments
})

// View profile functionality
const viewProfile = (employee) => {
  selectedEmployee.value = employee
  showProfileModal.value = true
}

// Add employee functionality
const addEmployee = async () => {
  const employee = { ...newEmployee.value }
  await store.addItem('employees', employee)
  employees.value = store.items.employees
  showAddEmployeeModal.value = false
  newEmployee.value = {
    firstName: '',
    lastName: '',
    email: '',
    departmentId: '',
    role: '',
    jobTitle: '',
    hireDate: '',
  }
}
</script>
