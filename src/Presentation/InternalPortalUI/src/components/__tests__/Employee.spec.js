import { mount } from '@vue/test-utils'
import { createTestingPinia } from '@pinia/testing'
import Employee from '@/views/Employee.vue'
import { useAppStore } from '@/stores/appStore'

describe('Employee.vue', () => {
    let wrapper
    let store

    beforeEach(() => {
        wrapper = mount(Employee, {
            global: {
                plugins: [createTestingPinia()],
            },
        })
        store = useAppStore()
    })

    it('renders the employee management title', () => {
        expect(wrapper.find('h2').text()).toBe('Employee Management')
    })

    it('displays the employee list', () => {
        const employeeList = wrapper.find('.divide-y.divide-gray-200')
        expect(employeeList.exists()).toBe(true)
    })

    it('shows the correct number of employees', async () => {
        await store.$patch({
            employees: [
                { id: 1, name: 'John Doe', department: 'IT', role: 'Developer' },
                { id: 2, name: 'Jane Smith', department: 'HR', role: 'Manager' },
            ],
        })
        await wrapper.vm.$nextTick()
        const employees = wrapper.findAll('.px-6.py-4.flex.items-center.justify-between')
        expect(employees).toHaveLength(2)
    })

    it('displays the "Add Employee" button', () => {
        const addButton = wrapper.find('button')
        expect(addButton.text()).toBe('Add Employee')
    })

    it('opens the add employee modal when button is clicked', async () => {
        const addButton = wrapper.find('button')
        await addButton.trigger('click')
        const modal = wrapper.find('.fixed.inset-0')
        expect(modal.exists()).toBe(true)
    })

    it('adds a new employee when form is submitted', async () => {
        await wrapper.setData({
            showAddEmployeeModal: true,
            newEmployee: { name: 'New Employee', department: 'Finance', role: 'Analyst' },
        })
        const form = wrapper.find('form')
        await form.trigger('submit.prevent')
        expect(store.setEmployees).toHaveBeenCalled()
    })
})})