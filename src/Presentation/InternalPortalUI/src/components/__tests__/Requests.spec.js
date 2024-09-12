import { mount } from '@vue/test-utils'
import { createTestingPinia } from '@pinia/testing'
import Requests from '@/views/Requests.vue'
import { useAppStore } from '@/stores/appStore'

describe('Requests.vue', () => {
    let wrapper
    let store

    beforeEach(() => {
        wrapper = mount(Requests, {
            global: {
                plugins: [createTestingPinia()],
            },
        })
        store = useAppStore()
    })

    it('renders the request management title', () => {
        expect(wrapper.find('h2').text()).toBe('Request Management')
    })

    it('displays the new request form', () => {
        const form = wrapper.find('form')
        expect(form.exists()).toBe(true)
    })

    it('shows the correct request type options', () => {
        const options = wrapper.findAll('select#request-type option')
        expect(options).toHaveLength(4) // leave, it-support, document-access, other
    })

    it('submits a new request', async () => {
        await wrapper.setData({
            newRequest: {
                type: 'leave',
                priority: 'high',
                details: 'Vacation request for next week',
            },
        })
        const form = wrapper.find('form')
        await form.trigger('submit.prevent')
        expect(store.addRequest).toHaveBeenCalled()
    })

    it('displays the recent requests list', () => {
        const requestsList = wrapper.find('.divide-y.divide-gray-200')
        expect(requestsList.exists()).toBe(true)
    })

    it('shows the correct number of recent requests', () => {
        const requests = wrapper.findAll('.px-4.py-4.sm\\:px-6')
        expect(requests).toHaveLength(3) // Assuming 3 initial requests
    })

    it('displays the correct status for each request', () => {
        const statuses = wrapper.findAll('.px-2.inline-flex.text-xs.leading-5.font-semibold.rounded-full')
        expect(statuses.at(0).text()).toBe('Pending')
        expect(statuses.at(1).text()).toBe('Approved')
        expect(statuses.at(2).text()).toBe('Rejected')
    })
})