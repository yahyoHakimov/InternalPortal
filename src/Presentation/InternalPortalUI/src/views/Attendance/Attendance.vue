<template>
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        <h2 class="text-2xl font-semibold text-gray-900 mb-6">Attendance Management</h2>

        <!-- Attendance Calendar -->
        <div class="bg-white shadow overflow-hidden sm:rounded-lg mb-8">
            <div class="px-4 py-5 sm:px-6">
                <h3 class="text-lg leading-6 font-medium text-gray-900">Monthly Attendance</h3>
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
                        <div :class="[
              'mt-1 w-3 h-3 mx-auto rounded-full',
              day.status === 'present' ? 'bg-green-500' :
              day.status === 'absent' ? 'bg-red-500' :
              day.status === 'late' ? 'bg-yellow-500' : 'bg-gray-200'
            ]"></div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Attendance Summary -->
        <div class="bg-white shadow overflow-hidden sm:rounded-lg">
            <div class="px-4 py-5 sm:px-6">
                <h3 class="text-lg leading-6 font-medium text-gray-900">Attendance Summary</h3>
            </div>
            <div class="border-t border-gray-200">
                <dl>
                    <div class="bg-gray-50 px-4 py-5 sm:grid sm:grid-cols-3 sm:gap-4 sm:px-6">
                        <dt class="text-sm font-medium text-gray-500">Present Days</dt>
                        <dd class="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">{{ attendanceSummary.present }}</dd>
                    </div>
                    <div class="bg-white px-4 py-5 sm:grid sm:grid-cols-3 sm:gap-4 sm:px-6">
                        <dt class="text-sm font-medium text-gray-500">Absent Days</dt>
                        <dd class="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">{{ attendanceSummary.absent }}</dd>
                    </div>
                    <div class="bg-gray-50 px-4 py-5 sm:grid sm:grid-cols-3 sm:gap-4 sm:px-6">
                        <dt class="text-sm font-medium text-gray-500">Late Days</dt>
                        <dd class="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">{{ attendanceSummary.late }}</dd>
                    </div>
                    <div class="bg-white px-4 py-5 sm:grid sm:grid-cols-3 sm:gap-4 sm:px-6">
                        <dt class="text-sm font-medium text-gray-500">Attendance Rate</dt>
                        <dd class="mt-1 text-sm text-gray-900 sm:mt-0 sm:col-span-2">{{ attendanceSummary.rate }}%</dd>
                    </div>
                </dl>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, computed } from 'vue'

const currentDate = new Date()
const currentMonth = currentDate.getMonth()
const currentYear = currentDate.getFullYear()

const daysInMonth = new Date(currentYear, currentMonth + 1, 0).getDate()
const firstDayOfMonth = new Date(currentYear, currentMonth, 1).getDay()

const calendarDays = ref([])

for (let i = 0; i < firstDayOfMonth; i++) {
  calendarDays.value.push({ date: '', status: '' })
}

for (let i = 1; i <= daysInMonth; i++) {
  const status = Math.random() < 0.7 ? 'present' : (Math.random() < 0.5 ? 'absent' : 'late')
  calendarDays.value.push({ date: i, status })
}

const attendanceSummary = computed(() => {
  const present = calendarDays.value.filter(day => day.status === 'present').length
  const absent = calendarDays.value.filter(day => day.status === 'absent').length
  const late = calendarDays.value.filter(day => day.status === 'late').length
  const total = present + absent + late
  const rate = total > 0 ? ((present + late) / total * 100).toFixed(2) : 0

  return { present, absent, late, rate }
})
</script>