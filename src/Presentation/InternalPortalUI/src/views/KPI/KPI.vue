<template>
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        <h2 class="text-2xl font-semibold text-gray-900 mb-6">KPI Management</h2>

        <!-- KPI Dashboard -->
        <div class="bg-white shadow overflow-hidden sm:rounded-lg mb-6">
            <div class="px-4 py-5 sm:px-6">
                <h3 class="text-lg leading-6 font-medium text-gray-900">Current KPI Performance</h3>
            </div>
            <div class="border-t border-gray-200">
                <dl>
                    <div v-for="(kpi, index) in kpis" :key="index" class="bg-gray-50 px-4 py-5 sm:grid sm:grid-cols-3 sm:gap-4 sm:px-6">
                        <dt class="text-sm font-medium text-gray-500">{{ kpi.name }}</dt>
                        <dd class="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">
                            <div class="flex items-center">
                                <div class="w-full bg-gray-200 rounded-full h-2.5 mr-2">
                                    <div class="bg-blue-600 h-2.5 rounded-full" :style="{ width: `${kpi.value}%` }"></div>
                                </div>
                                <span>{{ kpi.value }}%</span>
                            </div>
                        </dd>
                    </div>
                </dl>
            </div>
        </div>

        <!-- KPI Update Form -->
        <div class="bg-white shadow sm:rounded-lg">
            <div class="px-4 py-5 sm:p-6">
                <h3 class="text-lg leading-6 font-medium text-gray-900 mb-4">Update KPI</h3>
                <form @submit.prevent="updateKPI">
                    <div class="grid grid-cols-1 gap-y-6 gap-x-4 sm:grid-cols-6">
                        <div class="sm:col-span-3">
                            <label for="employee" class="block text-sm font-medium text-gray-700">Employee</label>
                            <select id="employee" v-model="selectedEmployee" class="mt-1 block w-full pl-3 pr-10 py-2 text-base border-gray-300 focus:outline-none focus:ring-blue-500 focus:border-blue-500 sm:text-sm rounded-md">
                                <option v-for="employee in employees" :key="employee.id" :value="employee.id">{{ employee.name }}</option>
                            </select>
                        </div>
                        <div class="sm:col-span-3">
                            <label for="kpi" class="block text-sm font-medium text-gray-700">KPI</label>
                            <select id="kpi" v-model="selectedKPI" class="mt-1 block w-full pl-3 pr-10 py-2 text-base border-gray-300 focus:outline-none focus:ring-blue-500 focus:border-blue-500 sm:text-sm rounded-md">
                                <option v-for="(kpi, index) in kpis" :key="index" :value="kpi.name">{{ kpi.name }}</option>
                            </select>
                        </div>
                        <div class="sm:col-span-4">
                            <label for="value" class="block text-sm font-medium text-gray-700">Value (%)</label>
                            <input type="number" name="value" id="value" v-model="kpiValue" min="0" max="100" class="mt-1 focus:ring-blue-500 focus:border-blue-500 block w-full shadow-sm sm:text-sm border-gray-300 rounded-md">
                        </div>
                    </div>
                    <div class="mt-5">
                        <button type="submit" class="inline-flex items-center px-4 py-2 border border-transparent text-sm font-medium rounded-md shadow-sm text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500">
                            Update KPI
                        </button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, computed } from 'vue'
import { useAppStore } from '@/stores/counter'

const store = useAppStore()

const kpis = ref([
  { name: 'Customer Satisfaction', value: 85 },
  { name: 'Loan Processing Time', value: 70 },
  { name: 'New Account Acquisitions', value: 60 },
])

const employees = computed(() => store.employees)
const selectedEmployee = ref(null)
const selectedKPI = ref(null)
const kpiValue = ref(null)

const updateKPI = () => {
  if (selectedEmployee.value && selectedKPI.value && kpiValue.value) {
    const kpiIndex = kpis.value.findIndex(k => k.name === selectedKPI.value)
    if (kpiIndex !== -1) {
      kpis.value[kpiIndex].value = parseInt(kpiValue.value)
    }
    // In a real application, you would send this update to the backend
    console.log(`Updated KPI for employee ${selectedEmployee.value}: ${selectedKPI.value} = ${kpiValue.value}%`)
    // Reset form
    selectedEmployee.value = null
    selectedKPI.value = null
    kpiValue.value = null
  }
}
</script>