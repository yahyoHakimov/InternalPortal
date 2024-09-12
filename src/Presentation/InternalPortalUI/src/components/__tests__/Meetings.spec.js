import { mount } from '@vue/test-utils'
import Meetings from '@/views/Meetings.vue'

describe('Meetings.vue', () => {
    let wrapper

    beforeEach(() => {
        wrapper = mount(Meetings)
    })

    it('renders the meeting management title', () => {
        expect(wrapper.find('h2').text()).toBe('Meeting Management')
    })

    it('displays the meeting calendar', () => {
        const calendar = wrapper.find('.grid.grid-cols-7.gap-2')
        expect(calendar.exists()).toBe(true)
    })

    it('shows the correct number of days in the calendar', () => {
        const days = wrapper.findAll('.border.rounded-md.p-2.text-center')
        expect(days.length).toBeGreaterThan(28) // Assuming at least 28 days in a month
    })

    it('displays the "Schedule Meeting" button', () => {
        const scheduleButton = wrapper.find('button')
        expect(scheduleButton.text()).toBe('Schedule Meeting')
    })

    it('opens the schedule meeting modal when button is clicked', async () => {
        const scheduleButton = wrapper.find('button')
        await scheduleButton.trigger('click')
        const modal = wrapper.find('.fixed.inset-0')
        expect(modal.exists()).toBe(true)
    })

    it('displays the upcoming meetings section', () => {
        const upcomingMeetings = wrapper.find('h3:nth-of-type(2)')
        expect(upcomingMeetings.text()).toBe('Upcoming Meetings')
    })

    it('shows the correct number of upcoming meetings', () => {
        const meetings = wrapper.findAll('.px-4.py-4.sm\\:px-6')
        expect(meetings).toHaveLength(3) // Assuming 3 initial meetings
    })

    it('schedules a new meeting', async () => {
        await wrapper.setData({
            showScheduleModal: true,
            newMeeting: {
                title: 'New Meeting',
                date: '2023-07-01',
                time: '10:00',
                type: 'Virtual',
                participants: 'John, Jane',
            },
        })
        const form = wrapper.find('form')
        await form.trigger('submit.prevent')
        const meetings = wrapper.findAll('.px-4.py-4.sm\\:px-6')
        expect(meetings).toHaveLength(4) // Assuming 3 initial meetings + 1 new
    })
})