import { mount } from '@vue/test-utils'
import Documents from '@/views/Documents.vue'

describe('Documents.vue', () => {
    let wrapper

    beforeEach(() => {
        wrapper = mount(Documents)
    })

    it('renders the document management title', () => {
        expect(wrapper.find('h2').text()).toBe('Document Management')
    })

    it('displays the document repository', () => {
        const repository = wrapper.find('.bg-white.shadow.overflow-hidden.sm\\:rounded-lg')
        expect(repository.exists()).toBe(true)
    })

    it('shows the correct number of documents', () => {
        const documents = wrapper.findAll('.px-4.py-4.sm\\:px-6')
        expect(documents).toHaveLength(3)
    })

    it('displays the "Upload Document" button', () => {
        const uploadButton = wrapper.find('button')
        expect(uploadButton.text()).toBe('Upload Document')
    })

    it('opens the upload document modal when button is clicked', async () => {
        const uploadButton = wrapper.find('button')
        await uploadButton.trigger('click')
        const modal = wrapper.find('.fixed.inset-0')
        expect(modal.exists()).toBe(true)
    })

    it('adds a new document when form is submitted', async () => {
        await wrapper.setData({
            showUploadModal: true,
            newDocument: { name: 'New Document', file: new File([''], 'test.pdf') },
        })
        const form = wrapper.find('form')
        await form.trigger('submit.prevent')
        const documents = wrapper.findAll('.px-4.py-4.sm\\:px-6')
        expect(documents).toHaveLength(4)
    })

    it('displays the document request form', () => {
        const requestForm = wrapper.find('form:nth-of-type(2)')
        expect(requestForm.exists()).toBe(true)
    })

    it('submits a document request', async () => {
        await wrapper.setData({
            documentRequest: { type: 'contract', details: 'Need employment contract' },
        })
        const form = wrapper.find('form:nth-of-type(2)')
        await form.trigger('submit.prevent')
        // In a real scenario, we would expect some state change or API call
        // For this test, we'll just check if the form data is reset
        expect(wrapper.vm.documentRequest.type).toBe('')
        expect(wrapper.vm.documentRequest.details).toBe('')
    })
})