<template>
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        <h2 class="text-2xl font-semibold text-gray-900 mb-6">Organization Structure</h2>

        <!-- Two columns: One for List, One for GoJS Chart -->
       
        <!-- Add Department Button -->
        <div class="flex justify-end mt-4">
            <button @click="showAddDepartmentModal = true" class="bg-blue-600 text-white px-4 py-2 rounded-md hover:bg-blue-700 transition-colors duration-300">
                Add Department
            </button>
        </div>

        <!-- Add Department Modal -->
        <div v-if="showAddDepartmentModal" class="fixed inset-0 bg-gray-600 bg-opacity-50 overflow-y-auto h-full w-full flex items-center justify-center">
            <div class="bg-white p-8 rounded-md shadow-xl w-full max-w-md">
                <h3 class="text-lg font-semibold mb-4">Add New Department</h3>
                <form @submit.prevent="addDepartment">
                    <!-- Form fields for adding department -->
                    <div class="mb-4">
                        <label for="departmentName" class="block text-sm font-medium text-gray-700">Department Name</label>
                        <input type="text" id="departmentName" v-model="newDepartment.departmentName" required class="mt-1 block w-full rounded-md border-gray-300 shadow-sm">
                    </div>
                    <div class="mb-4">
                        <label for="director" class="block text-sm font-medium text-gray-700">Director</label>
                        <select id="director" v-model="newDepartment.directorId" required class="mt-1 block w-full rounded-md border-gray-300 shadow-sm">
                            <option v-for="director in staticDirectors" :key="director.employeeId" :value="director.employeeId">
                                {{ director.firstName }} {{ director.lastName }}
                            </option>
                        </select>
                    </div>
                    <div class="flex justify-end space-x-2">
                        <button type="button" @click="showAddDepartmentModal = false" class="px-4 py-2 border border-gray-300 rounded-md text-sm font-medium text-gray-700 hover:bg-gray-50">
                            Cancel
                        </button>
                        <button type="submit" class="px-4 py-2 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-blue-700">
                            Add Department
                        </button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>

<script setup>
    import { ref, onMounted } from 'vue';
    import { useAppStore } from '@/stores/counter';
    import go from 'gojs';

    const store = useAppStore();
    const departments = ref([]);
    const staticDirectors = ref([ // List of static directors for now
        { employeeId: 1, firstName: 'John', lastName: 'Doe' },
        { employeeId: 2, firstName: 'Jane', lastName: 'Smith' },
        { employeeId: 3, firstName: 'Alice', lastName: 'Johnson' },
    ]);

    const employees = ref([]); // List of employees to select as directors
    const showAddDepartmentModal = ref(false);
    const newDepartment = ref({
        departmentName: '',
        directorId: null, // We'll map this to the selected director
    });

    // Fetch departments from the store
    onMounted(async () => {
        await store.fetchItems('departments');
        departments.value = store.items.departments;
    });

    // Add department functionality
    const addDepartment = async () => {
        const department = { ...newDepartment.value };
        await store.addItem('departments', department);
        departments.value = store.items.departments;
        showAddDepartmentModal.value = false;
        newDepartment.value = {
            departmentName: '',
            directorId: null,
        };
    };

</script>
