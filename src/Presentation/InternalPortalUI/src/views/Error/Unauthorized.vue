<template>
    <div class="min-h-screen flex items-center justify-center bg-gray-100 py-12 px-4 sm:px-6 lg:px-8">
      <div class="max-w-md w-full space-y-8 text-center">
        <h1 class="text-6xl font-bold text-red-600">403</h1>
        <h2 class="text-2xl font-bold text-gray-900">
          Unauthorized Access
        </h2>
        <p class="mt-4 text-gray-600">
          You do not have permission to view this page.
        </p>
        <div class="mt-6">
          <router-link to="/" class="btn btn-primary">Go Back to Dashboard</router-link>
        </div>
      </div>
    </div>
  </template>
  
  <script setup>
  </script>
  
  <style scoped>
  .btn {
    background-color: #007bff;
    color: white;
    padding: 10px 20px;
    border-radius: 5px;
    text-decoration: none;
  }
  
  .btn:hover {
    background-color: #0056b3;
  }
  </style>
  