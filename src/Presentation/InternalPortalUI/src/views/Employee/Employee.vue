<template>
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        <div class="flex justify-between items-center mb-6">
            <h2 class="text-2xl font-semibold text-gray-900">Employee Management</h2>
            <!-- Add Employee Button -->
            <button @click="showAddEmployeeModal = true"
                    class="bg-blue-600 text-white px-4 py-2 rounded-md hover:bg-blue-700 transition-colors duration-300">
                + Add Employee
            </button>
        </div>

        <!-- Employee List with Cards -->
        <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
            <div v-for="employee in employees" :key="employee.employeeId" class="bg-white shadow-md rounded-lg p-6 flex flex-col items-center space-y-4">
                <img class="h-24 w-24 rounded-full object-cover border border-gray-300"
                     :src="employee.photo || '/placeholder.svg'"
                     :alt="employee.firstName + ' ' + employee.lastName" />
                <div class="text-center">
                    <h3 class="text-lg font-semibold text-gray-900">{{ employee.firstName }} {{ employee.lastName }}</h3>
                    <p class="text-sm text-gray-500">{{ employee.department }} - {{ employee.role }}</p>
                    <p class="text-sm text-gray-500">{{ employee.jobTitle }}</p>
                    <p class="text-sm text-gray-500">Application User: {{ employee.applicationUser.email }}</p>
                </div>
                <div class="flex space-x-4 justify-between mt-4">
                    <button @click="viewProfile(employee)"
                            class="bg-blue-600 text-white px-3 py-1 rounded-md hover:bg-blue-700 transition-colors duration-300">
                        View Profile
                    </button>
                    <button @click="deactivateEmployee(employee.employeeId)"
                            class="bg-red-600 text-white px-3 py-1 rounded-md hover:bg-red-700 transition-colors duration-300">
                        Deactivate
                    </button>
                </div>
            </div>
        </div>

        <!-- Add Employee Modal -->
        <div v-if="showAddEmployeeModal"
             class="fixed inset-0 bg-gray-600 bg-opacity-50 overflow-y-auto h-full w-full flex items-center justify-center">
            <div class="bg-white p-8 rounded-md shadow-xl w-full max-w-md">
                <h3 class="text-lg font-semibold mb-4">Add New Employee</h3>
                <form @submit.prevent="addEmployee">
                    <div class="mb-4">
                        <label for="firstName" class="block text-sm font-medium text-gray-700">First Name</label>
                        <input type="text"
                               id="firstName"
                               v-model="newEmployee.firstName"
                               required
                               class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-300 focus:ring focus:ring-blue-200 focus:ring-opacity-50" />
                    </div>
                    <div class="mb-4">
                        <label for="lastName" class="block text-sm font-medium text-gray-700">Last Name</label>
                        <input type="text"
                               id="lastName"
                               v-model="newEmployee.lastName"
                               required
                               class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-300 focus:ring focus:ring-blue-200 focus:ring-opacity-50" />
                    </div>
                    <div class="mb-4">
                        <label for="email" class="block text-sm font-medium text-gray-700">Email</label>
                        <input type="email"
                               id="email"
                               v-model="newEmployee.email"
                               required
                               class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-300 focus:ring focus:ring-blue-200 focus:ring-opacity-50" />
                    </div>
                    <div class="mb-4">
                        <label for="department" class="block text-sm font-medium text-gray-700">Department</label>
                        <input type="text"
                               id="department"
                               v-model="newEmployee.department"
                               required
                               class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-300 focus:ring focus:ring-blue-200 focus:ring-opacity-50" />
                    </div>
                    <div class="mb-4">
                        <label for="role" class="block text-sm font-medium text-gray-700">Role</label>
                        <input type="text"
                               id="role"
                               v-model="newEmployee.role"
                               required
                               class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-300 focus:ring focus:ring-blue-200 focus:ring-opacity-50" />
                    </div>
                    <div class="mb-4">
                        <label for="jobTitle" class="block text-sm font-medium text-gray-700">Job Title</label>
                        <input type="text"
                               id="jobTitle"
                               v-model="newEmployee.jobTitle"
                               required
                               class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-300 focus:ring focus:ring-blue-200 focus:ring-opacity-50" />
                    </div>
                    <div class="mb-4">
                        <label for="hireDate" class="block text-sm font-medium text-gray-700">Hire Date</label>
                        <input type="date"
                               id="hireDate"
                               v-model="newEmployee.hireDate"
                               required
                               class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-300 focus:ring focus:ring-blue-200 focus:ring-opacity-50" />
                    </div>
                    <div class="mb-4">
                        <label for="applicationUserId" class="block text-sm font-medium text-gray-700">Application User</label>
                        <select v-model="newEmployee.applicationUserId" class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-300 focus:ring focus:ring-blue-200 focus:ring-opacity-50">
                            <option v-for="user in users" :key="user.id" :value="user.id">
                                {{ user.email }}
                            </option>
                        </select>
                    </div>
                    <div class="mb-4">
                        <label for="role" class="block text-sm font-medium text-gray-700">Role</label>
                        <select id="role" v-model="newEmployee.roleId" required class="mt-1 block w-full rounded-md border-gray-300 shadow-sm">
                            <option v-for="role in roles" :key="role.id" :value="role.id">{{ role.name }}</option>
                        </select>
                    </div>
                    <div class="flex justify-end space-x-2">
                        <button type="button"
                                @click="showAddEmployeeModal = false"
                                class="px-4 py-2 border border-gray-300 rounded-md text-sm font-medium text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500">
                            Cancel
                        </button>
                        <button type="submit"
                                class="px-4 py-2 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500">
                            Add Employee
                        </button>
                    </div>
                </form>
            </div>
        </div>

        <!-- Profile View Modal -->
        <div v-if="showProfileModal"
             class="fixed inset-0 bg-gray-600 bg-opacity-50 overflow-y-auto h-full w-full flex items-center justify-center">
            <div class="bg-white p-8 rounded-md shadow-xl w-full max-w-lg">
                <h3 class="text-lg font-semibold mb-4">Employee Profile</h3>
                <div class="flex space-x-6">
                    <img class="h-32 w-32 rounded-full object-cover"
                         :src="selectedEmployee.photo || '/placeholder.svg'"
                         :alt="selectedEmployee.firstName + ' ' + selectedEmployee.lastName" />
                    <div>
                        <h4 class="text-xl font-semibold text-gray-900">{{ selectedEmployee.firstName }} {{ selectedEmployee.lastName }}</h4>
                        <p class="text-sm text-gray-500">Email: {{ selectedEmployee.email }}</p>
                        <p class="text-sm text-gray-500">Department: {{ selectedEmployee.department }}</p>
                        <p class="text-sm text-gray-500">Role: {{ employee.firstName }} {{ employee.lastName }} - {{ employee.role.name }}</p>
                        <p class="text-sm text-gray-500">Job Title: {{ selectedEmployee.jobTitle }}</p>
                        <p class="text-sm text-gray-500">Hire Date: {{ selectedEmployee.hireDate }}</p>
                        <p class="text-sm text-gray-500">Application User: {{ selectedEmployee.applicationUser.email }}</p>
                    </div>
                </div>
                <div class="mt-6 flex justify-end">
                    <button type="button"
                            @click="closeProfileModal"
                            class="px-4 py-2 border border-gray-300 rounded-md text-sm font-medium text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500">
                        Close
                    </button>
                </div>
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
    const users = ref([]) // To hold Application Users for the dropdown
    const roles = ref([])
    const showAddEmployeeModal = ref(false)
    const showProfileModal = ref(false)
    const newEmployee = ref({
        firstName: '',
        lastName: '',
        email: '',
        department: '',
        role: '',
        jobTitle: '',
        hireDate: '',
        applicationUserId: '',
    })

    const selectedEmployee = ref({})

    // Fetch employees and users from the store and server
    onMounted(async () => {
        await store.fetchItems('employees')
        employees.value = store.items.employees

        // Fetch users for the dropdown
        await store.fetchItems('users')
        users.value = store.items.users

        await store.fetchItems("roles");
        roles.value = store.items.roles
    })

    // View profile functionality
    const viewProfile = (employee) => {
        selectedEmployee.value = employee
        showProfileModal.value = true
    }

    // Close the profile modal
    const closeProfileModal = () => {
        showProfileModal.value = false
    }

    // Deactivate employee functionality placeholder
    const deactivateEmployee = (id) => {
        console.log('Deactivate employee', id)
    }

    // Add a new employee to the store and close the modal
    const addEmployee = async () => {
        const employee = {
            ...newEmployee.value,
            photo: '', // Placeholder for the photo
        }
        await store.addItem('employees', employee)
        employees.value = store.items.employees // Update the local state
        showAddEmployeeModal.value = false
        newEmployee.value = { firstName: '', lastName: '', email: '', department: '', role: '', jobTitle: '', hireDate: '', applicationUserId: '' }
    }
</script>
