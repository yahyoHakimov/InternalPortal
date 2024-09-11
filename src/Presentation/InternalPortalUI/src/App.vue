<template>
    <div class="flex h-screen bg-gray-100">
        <!-- Sidebar and Main Content when Authenticated -->
        <template v-if="isAuthenticated">
            <Sidebar />
            <!-- Main Content -->
            <main class="flex-1 overflow-y-auto">
                <!-- Top Navigation -->
                <TopNav @logout="handleLogout" />
                <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
                    <router-view></router-view>
                </div>
            </main>
        </template>

        <!-- Public Routes without Sidebar when Not Authenticated -->
        <template v-else>
            <router-view></router-view>
        </template>
    </div>
</template>

<script setup>
    import { useAuthStore } from './stores/Auth/auth.js'
    import { computed, onMounted  } from 'vue'
    import { useRouter } from 'vue-router' // Import Vue Router for navigation

    import Sidebar from '@/components/Sidebar/Sidebar.vue'
    import TopNav from '@/components/Nav/TopNav.vue'

    // Access the auth store
    const authStore = useAuthStore()

    // Computed property for reactivity to check if the user is authenticated
    const isAuthenticated = computed(() => authStore.isAuthenticated)

    // Use Vue Router's useRouter function to navigate
    const router = useRouter()

    // Logout function to clear auth state and redirect
    const handleLogout = () => {
        authStore.logout(router)
    }

    /*onMounted(() => {
    authStore.checkAuth()} // Ensure this checks the token on page load
    })*/
</script>
