<template>
    <div class="flex h-screen bg-gray-100">
        <!-- Sidebar Component -->
        <Sidebar v-if="isAuthenticated" />
        <!--<Sidebar />-->

        <!-- Main Content -->
        <main class="flex-1 overflow-y-auto">
            <!-- Top Navigation -->
            <TopNav v-if="isAuthenticated" @logout="logout" />
            <!--<TopNav @logout="logout" />-->

            <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
                <router-view></router-view>
            </div>
        </main>
    </div>
</template>

<script setup>
    import { useAuthStore } from './stores/Auth/auth'
    import { ref } from 'vue'
    import Sidebar from '@/components/Sidebar/Sidebar.vue'
    import TopNav from '@/components/Nav/TopNav.vue'

    const authStore = useAuthStore()
    const isAuthenticated = ref(authStore.isAuthenticated)

    const logout = () => {
        authStore.logout()
    }
</script>
