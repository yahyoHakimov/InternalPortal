import { mount } from '@vue/test-utils'
import { createTestingPinia } from '@pinia/testing'
import { createRouter, createWebHistory } from 'vue-router'
import Profile from '@/views/Profile.vue'
import { useAppStore } from '@/stores/appStore'

describe('Profile.vue', () => {
    let wrapper
    let store
    let router

    beforeEach(() => {
        router = createRouter({
            history: createWebHistory(),
            routes: [{ path: '/profile/:id', name: 'Profile', component: Profile }],
        })

        wrapper = mount(Profile, {
            global: {
                plugins: [createTestingPinia(), router],
            },
        })
        store = useAppStore()
        router.push('/profile/1')
    })

    it('renders the employee profile title', () => {
        expect(wrapper.find('h2').text()).toBe('Employee Profile')
    })

    it('displays the employee information', () => {
        const employeeInfo = wrapper.find('.px-4.py-5.sm\\:px-6')
        expect(employeeInfo.exists()).toBe(true)
    })

    it('shows the edit profile button', () => {
        const editButton = wrapper.find('button')
        expect(editButton.text()).toBe('Edit Profile')
    })

    it('displays the skills and achievements section', () => {
        const skillsSection = wrapper.find('h3:nth-of-type(2)')
        expect(skillsSection.text()).toBe('Skills and Achievements')
    })

    it('opens the edit profile modal when button is clicked', async () => {
        const editButton = wrapper.find('button')
        await editButton.trigger('click')
        const modal = wrapper.find('.fixed.inset-0')
        expect(modal.exists()).toBe(true)
    })

    it('updates profile when form is submitted', async () => {
        await wrapper.setData({
            showEditModal: true,
            editedEmployee: { name: 'Updated Name', email: 'updated@email.com' },
        })
        const form = wrapper.find('form')
        await form.trigger('submit.prevent')
        expect(wrapper.vm.employee.name).toBe('Updated Name')
    })

    it('fetches employee data based on route params', async () => {
        await store.$patch({
            employees: [{ id: 1, name: 'John Doe', role: 'Developer' }],
        })
        await router.isReady()
        await wrapper.vm.$nextTick()
        expect(wrapper.vm.employee.name).toBe('John Doe')
    })
})