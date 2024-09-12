import { mount } from '@vue/test-utils'
import { createTestingPinia } from '@pinia/testing'
import Announcements from '@/views/Announcements.vue'
import { useAppStore } from '@/stores/appStore'

describe('Announcements.vue', () => {
    let wrapper
    let store

    beforeEach(() => {
        wrapper = mount(Announcements, {
            global: {
                plugins: [createTestingPinia()],
            },
        })
        store = useAppStore()
    })

    it('renders the announcements title', () => {
        expect(wrapper.find('h2').text()).toBe('Announcements')
    })

    it('displays the announcements list', () => {
        const announcementsList = wrapper.find('.divide-y.divide-gray-200')
        expect(announcementsList.exists()).toBe(true)
    })

    it('shows the correct number of announcements', () => {
        const announcements = wrapper.findAll('.px-6.py-4')
        expect(announcements).toHaveLength(3) // Assuming there are 3 announcements as in the original component
    })

    it('displays the "Add Announcement" button', () => {
        const addButton = wrapper.find('button')
        expect(addButton.text()).toBe('Add Announcement')
    })

    it('opens the add announcement modal when button is clicked', async () => {
        const addButton = wrapper.find('button')
        await addButton.trigger('click')
        const modal = wrapper.find('.fixed.inset-0')
        expect(modal.exists()).toBe(true)
    })

    it('adds a new announcement when form is submitted', async () => {
        await wrapper.setData({
            showAddAnnouncementModal: true,
            newAnnouncement: { title: 'New Announcement', content: 'Test content' },
        })
        const form = wrapper.find('form')
        await form.trigger('submit.prevent')
        expect(store.addAnnouncement).toHaveBeenCalled()
    })
})