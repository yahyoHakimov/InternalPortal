<template>
    <div class="flex h-screen bg-gray-100">
      <!-- Sidebar Component -->
      <template v-if="isAuthenticated"> 
        <Sidebar />
  
        <!-- Main Content -->
        <main class="flex-1 overflow-y-auto">
          <!-- Top Navigation -->
          <TopNav @logout="logout" />
  
          <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
            <router-view></router-view>
          </div>
        </main>
      </template>
  
      <template v-else>
        <router-view></router-view>
      </template>
    </div>
  </template>
  
  <script setup>
  import { useAuthStore } from './stores/Auth/auth'
  import { computed } from 'vue'
  import Sidebar from '@/components/Sidebar/Sidebar.vue'
  import TopNav from '@/components/Nav/TopNav.vue'
  
  const authStore = useAuthStore()
  
  // Use computed for reactivity when the state changes
  const isAuthenticated = computed(() => authStore.isAuthenticated)
  
  const logout = () => {
    authStore.logout()
    router.push('/login')
  }
  </script>
  