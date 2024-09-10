<template>
    <header class="bg-white shadow-sm">
        <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-4 flex items-center justify-between">
            <div class="relative w-64">
                <input type="text"
                       placeholder="Search..."
                       class="w-full pl-10 pr-4 py-2 rounded-lg border border-gray-300 focus:outline-none focus:ring-2 focus:ring-blue-500" />
                <Search class="absolute left-3 top-2.5 text-gray-400" />
            </div>
            <div class="flex items-center space-x-4">
                <button class="text-gray-500 hover:text-gray-700">
                    <Bell class="h-5 w-5" />
                </button>
                <!-- Use the logout event on button click -->
                <button class="text-gray-500 hover:text-gray-700" @click="handleLogout">
                    <LogOut class="h-5 w-5" />
                </button>
            </div>
        </div>
    </header>
</template>

<script setup>
    import { Bell, Search, LogOut } from 'lucide-vue-next'

    // Define emit to emit events to the parent component
    const emit = defineEmits(['logout'])

    // Function to handle logout and emit the event
    const handleLogout = () => {
        emit('logout')
    }
</script>
