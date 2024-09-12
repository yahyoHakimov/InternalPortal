import { mount } from '@vue/test-utils'
import Feedback from '@/views/Feedback.vue'

describe('Feedback.vue', () => {
    let wrapper

    beforeEach(() => {
        wrapper = mount(Feedback)
    })

    it('renders the feedback management title', () => {
        expect(wrapper.find('h2').text()).toBe('Feedback Management')
    })

    it('displays the feedback submission form', () => {
        const form = wrapper.find('form')
        expect(form.exists()).toBe(true)
    })

    it('shows the correct feedback type options', () => {
        const options = wrapper.findAll('select option')
        expect(options).toHaveLength(4) // bug, feature, improvement, other
    })

    it('submits new feedback', async () => {
        await wrapper.setData({
            newFeedback: { type: 'bug', details: 'Found a bug in the system' },
        })
        const form = wrapper.find('form')
        await form.trigger('submit.prevent')
        const feedbackList = wrapper.findAll('.px-4.py-4.sm\\:px-6')
        expect(feedbackList).toHaveLength(4) // Assuming 3 initial items + 1 new
    })

    it('displays the recent feedback list', () => {
        const feedbackList = wrapper.find('.divide-y.divide-gray-200')
        expect(feedbackList.exists()).toBe(true)
    })

    it('shows the correct number of feedback items', () => {
        const feedbackItems = wrapper.findAll('.px-4.py-4.sm\\:px-6')
        expect(feedbackItems).toHaveLength(3) // Assuming 3 initial items
    })

    it('displays the correct status for each feedback item', () => {
        const statuses = wrapper.findAll('.px-2.inline-flex.text-xs.leading-5.font-semibold.rounded-full')
        expect(statuses.at(0).text()).toBe('Pending')
        expect(statuses.at(1).text()).toBe('Resolved')
    })
})