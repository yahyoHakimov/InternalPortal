<template>
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        <h2 class="text-2xl font-semibold text-gray-900 mb-6">Meeting Management</h2>

        <!-- Calendar View -->
        <div class="bg-white shadow overflow-hidden sm:rounded-lg mb-8">
            <div class="px-4 py-5 sm:px-6 flex justify-between items-center">
                <h3 class="text-lg leading-6 font-medium text-gray-900">Meeting Calendar</h3>
                <button @click="showScheduleModal = true" class="bg-blue-600 text-white px-4 py-2 rounded-md hover:bg-blue-700 transition-colors duration-300">
                    Schedule Meeting
                </button>
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
                        <div v-if="day.meetings.length > 0" class="mt-1 text-xs text-blue-600">
                            {{ day.meetings.length }} meeting{{ day.meetings.length > 1 ? 's' : '' }}
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Upcoming Meetings -->
        <div class="bg-white shadow overflow-hidden sm:rounded-lg">
            <div class="px-4 py-5 sm:px-6">
                <h3 class="text-lg leading-6 font-medium text-gray-900">Upcoming Meetings</h3>
            </div>
            <div class="border-t border-gray-200">
                <ul class="divide-y divide-gray-200">
                    <li v-for="meeting in upcomingMeetings" :key="meeting.id" class="px-4 py-4 sm:px-6">
                        <div class="flex items-center justify-between">
                            <p class="text-sm font-medium text-blue-600 truncate">
                                {{ meeting.title }}
                            </p>
                            <div class="ml-2 flex-shrink-0 flex">
                                <p class="px-2 inline-flex text-xs leading-5 font-semibold rounded-full bg-green-100 text-green-800">
                                    {{ meeting.type }}
                                </p>
                            </div>
                        </div>
                        <div class="mt-2 sm:flex sm:justify-between">
                            <div class="sm:flex">
                                <p class="flex items-center text-sm text-gray-500">
                                    <svg class="flex-shrink-0 mr-1.5 h-5 w-5 text-gray-400" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
                                        <path fill-rule="evenodd" d="M6 2a1 1 0 00-1 1v1H4a2 2 0 00-2 2v10a2 2 0 002 2h12a2 2 0 002-2V6a2 2 0 00-2-2h-1V3a1 1 0 10-2 0v1H7V3a1 1 0 00-1-1zm0 5a1 1 0 000 2h8a1 1 0 100-2H6z" clip-rule="evenodd" />
                                    </svg>
                                    {{ meeting.date }} at {{ meeting.time }}
                                </p>
                            </div>
                            <div class="mt-2 flex items-center text-sm text-gray-500 sm:mt-0">
                                <svg class="flex-shrink-0 mr-1.5 h-5 w-5 text-gray-400" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
                                    <path fill-rule="evenodd" d="M10 9a3 3 0 100-6 3 3 0 000 6zm-7 9a7 7 0 1114 0H3z" clip-rule="evenodd" />
                                </svg>
                                {{ meeting.participants.join(', ') }}
                            </div>
                        </div>
                    </li>
                </ul>
            </div>
        </div>

        <!-- Schedule Meeting Modal -->
        <div v-if="showScheduleModal" class="fixed inset-0 bg-gray-600 bg-opacity-50 overflow-y-auto h-full w-full flex items-center justify-center">
            <div class="bg-white p-8 rounded-md shadow-xl w-full max-w-md">
                <h3 class="text-lg font-semibold mb-4">Schedule Meeting</h3>
                <form @submit.prevent="scheduleMeeting">
                    <h2 class="text-2xl font-semibold mb-4">{{ currentStep === 1 ? 'Meeting Details' : 'Meeting Summary' }}</h2>

                    <div v-if="currentStep === 1">
                        <!-- Meeting Details Form -->
                        <div class="mb-4">
                            <label for="title" class="block text-sm font-medium text-gray-700">Title</label>
                            <input type="text" id="title" v-model="meetingDetails.title" class="block w-full rounded-md border-gray-300" />
                        </div>
                        <div class="mb-4">
                            <label for="date" class="block text-sm font-medium text-gray-700">Date</label>
                            <input type="date" id="date" v-model="meetingDetails.date" class="block w-full rounded-md border-gray-300" />
                        </div>
                        <div class="mb-4">
                            <label for="duration" class="block text-sm font-medium text-gray-700">Duration</label>
                            <select id="duration" v-model="meetingDetails.duration" class="block w-full rounded-md border-gray-300">
                                <option value="15">15 min</option>
                                <option value="30">30 min</option>
                                <option value="45">45 min</option>
                                <option value="60">60 min</option>
                            </select>
                        </div>
                        <button @click="nextStep" class="bg-blue-500 text-white px-4 py-2 rounded-md">Next</button>
                    </div>

                    <div v-else>
                        <!-- Meeting Summary -->
                        <div class="mb-4"><strong>Title:</strong> {{ meetingDetails.title }}</div>
                        <div class="mb-4"><strong>Date:</strong> {{ meetingDetails.date }}</div>
                        <div class="mb-4"><strong>Duration:</strong> {{ meetingDetails.duration }} minutes</div>
                        <div class="mb-4"><strong>Google Meet URL:</strong> {{ meetingDetails.meetUrl }}</div>
                        <button @click="saveMeeting" class="bg-green-500 text-white px-4 py-2 rounded-md">Save</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>

<script setup>
    import { ref, reactive, computed, onMounted } from 'vue'
    import { loadGapiInsideDOM } from 'gapi-script'
    import { initClient, createGoogleMeet, saveMeetingToDatabase } from '@/stores/meeting/meetingService'

    const showScheduleModal = ref(false)
    const currentStep = ref(1)
    const error = ref('');
    const showForm = ref(false);

    const meetingDetails = reactive({
        title: '',
        date: '',
        duration: '15',
        meetUrl: ''
    });

    const openCreateForm = () => {
        showForm.value = true;
        currentStep.value = 1;
    };

    const nextStep = async () => {
        if (currentStep.value === 1) {
            try {
                meetingDetails.meetUrl = await createGoogleMeet(meetingDetails);
                currentStep.value = 2;
            } catch (err) {
                error.value = 'Failed to generate Google Meet URL. Please try again.';
            }
        }
    };

    const saveMeeting = async () => {
        try {
            await saveMeetingToDatabase(meetingDetails);
            showForm.value = false;
            resetMeetingDetails();
            alert('Meeting saved successfully!');
        } catch (err) {
            error.value = 'Failed to save the meeting. Please try again.';
        }
    };

    const resetMeetingDetails = () => {
        Object.assign(meetingDetails, {
            title: '',
            date: '',
            duration: '15',
            meetUrl: ''
        });
    };

    onMounted(async () => {
        try {
            await loadGapiInsideDOM();
            gapi.load('client:auth2', initClient);
        } catch (err) {
            console.error('Failed to load GAPI:', err);
        }
    });

    const upcomingMeetings = ref([]);

    const currentDate = new Date();
    const currentMonth = currentDate.getMonth();
    const currentYear = currentDate.getFullYear();

    const daysInMonth = new Date(currentYear, currentMonth + 1, 0).getDate();
    const firstDayOfMonth = new Date(currentYear, currentMonth, 1).getDay();

    const calendarDays = computed(() => {
        const days = [];
        for (let i = 0; i < firstDayOfMonth; i++) {
            days.push({ date: '', meetings: [] });
        }
        for (let i = 1; i <= daysInMonth; i++) {
            const date = `${currentYear}-${(currentMonth + 1).toString().padStart(2, '0')}-${i.toString().padStart(2, '0')}`;
            const meetings = upcomingMeetings.value.filter(meeting => meeting.date === date);
            days.push({ date: i, meetings });
        }
        return days;
    });
</script>
