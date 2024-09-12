import { mount } from '@vue/test-utils'
import { createTestingPinia } from '@pinia/testing'
import KPI from '@/views/KPI.vue'
import { useAppStore } from '@/stores/appStore'

describe('KPI.vue', () => {
    let wrapper
    let store

    beforeEach(() => {
        wrapper = mount(KPI, {
            global: {
                plugins: [createTestingPinia()],
            },
        })
        store = useAppStore()
    })

    it('renders the KPI management title', () => {
        expect(wrapper.find('h2').text()).toBe('KPI Management')
    })

    it('displays the current KPI performance section', () => {
        const kpiPerformance = wrapper.find('h3')
        expect(kpiPerformance.text()).toBe('Current KPI Performance')
    })

    it('shows the correct number of KPIs', () => {
        const kpis = wrapper.findAll('.bg-gray-50.px-4.py-5')
        expect(kpis).toHaveLength(3) // Assuming there are 3 KPIs as in the original component
    })

    it('displays the KPI update form', () => {
        const updateForm = wrapper.find('form')
        expect(updateForm.exists()).toBe(true)
    })

    it('updates KPI when form is submitted', async () => {
        await wrapper.setData({
            selectedEmployee: 1,
            selectedKPI: 'Customer Satisfaction',
            kpiValue: 90,
        })
        const form = wrapper.find('form')
        await form.trigger('submit.prevent')
        const updatedKPI = wrapper.vm.kpis.find(k => k.name === 'Customer Satisfaction')
        expect(updatedKPI.value).toBe(90)
    })

    it('populates employee select with store data', async () => {
        await store.$patch({
            employees: [
                { id: 1, name: 'John Doe' },
                { id: 2, name: 'Jane Smith' },
            ],
        })
        await wrapper.vm.$nextTick()
        const options = wrapper.findAll('select#employee option')
        expect(options).toHaveLength(2)
    })
})