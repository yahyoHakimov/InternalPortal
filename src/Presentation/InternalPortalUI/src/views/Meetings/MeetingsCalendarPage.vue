<template>
    <div class="max-w-4xl mx-auto p-6">
      <h1 class="text-3xl font-bold mb-6">Create a Google Meet</h1>
  
      <div class="flex items-center mb-6">
        <input type="text" v-model="searchQuery" placeholder="Search meetings..." class="flex-grow p-2 border rounded-l-md" />
        <button @click="openCreateForm" class="bg-blue-500 text-white px-4 py-2 rounded-r-md hover:bg-blue-600">
          Create Meet
        </button>
      </div>
  
      <div v-if="error" class="bg-red-100 border-l-4 border-red-500 text-red-700 p-4 mb-6" role="alert">
        <p>{{ error }}</p>
      </div>
  
      <div v-if="showForm" class="bg-white shadow-md rounded-lg p-6">
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
      </div>
    </div>
  </template>
  
  <script>
  import { ref, reactive, onMounted } from 'vue';
  import { loadGapiInsideDOM } from 'gapi-script';
  import { initClient, createGoogleMeet, saveMeetingToDatabase } from '@/stores/meeting/meetingService.js';
  
  export default {
    name: 'CreateMeet',
    setup() {
      const searchQuery = ref('');
      const showForm = ref(false);
      const currentStep = ref(1);
      const error = ref('');
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
  
      return {
        searchQuery,
        showForm,
        currentStep,
        meetingDetails,
        error,
        openCreateForm,
        nextStep,
        saveMeeting
      };
    }
  };
  </script>
  