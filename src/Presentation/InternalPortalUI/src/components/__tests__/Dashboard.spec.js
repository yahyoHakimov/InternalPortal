import { mount } from '@vue/test-utils'
import { createTestingPinia } from '@pinia/testing'
import Dashboard from '@/views/Dashboard.vue'
import { useAppStore } from '@/stores/appStore'

describe('Dashboard.vue', () => {
    let wrapper
    let store

    beforeEach(() => {
        wrapper = mount(Dashboard, {
            global: {
                plugins: [createTestingPinia()],
            },
        })
        store = useAppStore()
    })

    it('renders the dashboard title', () => {
        expect(wrapper.find('h2').text()).toBe('Dashboard')
    })

    it('displays the correct number of key metrics', () => {
        const metrics = wrapper.findAll('.bg-white.overflow-hidden.shadow.rounded-lg')
        expect(metrics).toHaveLength(4)
    })

    it('shows the correct total employees from the store', async () => {
        await store.$patch({ totalEmployees: 100 })
        await wrapper.vm.$nextTick()
        const totalEmployees = wrapper.findAll('.text-2xl.font-bold')[0]
        expect(totalEmployees.text()).toBe('100')
    })

    it('displays the KPI performance section', () => {
        const kpiSection = wrapper.find('.space-y-6')
        expect(kpiSection.exists()).toBe(true)
    })

    it('shows the correct number of KPIs', () => {
        const kpis = wrapper.findAll('.flex.items-center')
        expect(kpis).toHaveLength(4) // Assuming there are 4 KPIs as in the original component
    })

    it('displays the recent activities section', () => {
        const recentActivities = wrapper.find('h3:nth-of-type(2)')
        expect(recentActivities.text()).toBe('Recent Activities')
    })

    it('shows the correct number of recent activities', () => {
        const activities = wrapper.findAll('.py-4')
        expect(activities).toHaveLength(4) // Assuming there are 4 recent activities as in the original component
    })

    it('displays the announcements section', () => {
        const announcements = wrapper.find('h3:nth-of-type(3)')
        expect(announcements.text()).toBe('Recent Announcements')
    })

    it('shows the correct number of announcements', () => {
        const announcementItems = wrapper.findAll('.flex.space-x-3')
        expect(announcementItems).toHaveLength(3) // Assuming there are 3 announcements as in the original component
    })
})