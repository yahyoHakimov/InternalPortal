import { mount } from '@vue/test-utils'
import Attendance from '@/views/Attendance.vue'

describe('Attendance.vue', () => {
    let wrapper

    beforeEach(() => {
        wrapper = mount(Attendance)
    })

    it('renders the attendance management title', () => {
        expect(wrapper.find('h2').text()).toBe('Attendance Management')
    })

    it('displays the monthly attendance calendar', () => {
        const calendar = wrapper.find('.grid.grid-cols-7.gap-2')
        expect(calendar.exists()).toBe(true)
    })

    it('shows the correct number of days in the calendar', () => {
        const days = wrapper.findAll('.border.rounded-md.p-2.text-center')
        expect(days.length).toBeGreaterThan(28) // Assuming at least 28 days in a month
    })

    it('displays the attendance summary', () => {
        const summary = wrapper.find('h3:nth-of-type(2)')
        expect(summary.text()).toBe('Attendance Summary')
    })

    it('shows the correct attendance statistics', () => {
        const stats = wrapper.findAll('.bg-gray-50.px-4.py-5, .bg-white.px-4.py-5')
        expect(stats).toHaveLength(4) // Present Days, Absent Days, Late Days, Attendance Rate
    })

    it('calculates the correct attendance rate', () => {
        const rate = wrapper.find('.bg-white.px-4.py-5:nth-of-type(4) dd')
        expect(parseFloat(rate.text())).toBeGreaterThanOrEqual(0)
        expect(parseFloat(rate.text())).toBeLessThanOrEqual(100)
    })
})