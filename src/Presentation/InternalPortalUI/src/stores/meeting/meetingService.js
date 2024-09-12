import { gapi } from 'gapi-script';
import axios from 'axios';

const CLIENT_ID = '351279214088-jru090bc0bkdg1lqq9lufke0tcnv0kpl.apps.googleusercontent.com';  // Replace with your Google Client ID
const API_KEY = 'AIzaSyDSaMhAU9YH3Xj2MNJRneVQNrL4mdow8ek';      // Replace with your Google API Key
const SCOPES = 'https://www.googleapis.com/auth/calendar https://www.googleapis.com/auth/calendar.events';
const API_URL = 'https://localhost:7126/meetings';


export const fetchMeetings = async () => {
    try {
        const response = await axios.get(`${API_URL}/all`);
        return { success: true, data: response.data };
    } catch (error) {
        console.error('Error fetching meetings:', error);
        return { success: false, message: 'Error fetching meetings' };
    }
};

export const initClient = async () => {
    try {
        await gapi.client.init({
            apiKey: API_KEY,  // Make sure this is valid and correct
            clientId: CLIENT_ID,  // Ensure this matches the Google Cloud client ID
            discoveryDocs: ['https://www.googleapis.com/discovery/v1/apis/calendar/v3/rest'],
            scope: SCOPES,  // Ensure the scopes match what you need (e.g., 'https://www.googleapis.com/auth/calendar')
            redirect_uri: 'http://localhost:5173/oauth2/callback'  // Ensure the redirect_uri is correctly specified
        });
        console.log('Google API client initialized');
        await silentSignIn();
    } catch (err) {
        console.error('Error initializing Google API client:', err);
        throw new Error('Error initializing Google API client');
    }
};


export const silentSignIn = async () => {
    const authInstance = gapi.auth2.getAuthInstance();
    try {
        console.log(authInstance)
        await authInstance.signIn({ prompt: 'none' });
        console.log('User signed in silently');
    } catch (error) {
        console.log('Silent sign-in failed, using popup');
        await authInstance.signIn();
    }
};

export const createGoogleMeet = async (meetingDetails) => {
    const authInstance = gapi.auth2.getAuthInstance();
    if (!authInstance.isSignedIn.get()) {
        await authInstance.signIn();
    }
    const accessToken = authInstance.currentUser.get().getAuthResponse().access_token;
    const startTime = new Date(`${meetingDetails.date}T09:00:00`).toISOString();
    const endTime = new Date(new Date(startTime).getTime() + meetingDetails.duration * 60000).toISOString();
    console.log(accessToken)
    console.log(startTime)
    console.log(endTime)
    const response = await axios.post(`${API_URL}/schedule`, { startTime, endTime, accessToken });
    console.log(response.data)
    return response.data.meetingUrl;
};

export const saveMeetingToDatabase = async (meetingDetails) => {
    const formattedDetails = {
        Duration: meetingDetails.duration,
        HostName: 'Some Host',  // Replace with actual host name
        MeetingTitle: meetingDetails.title,
        MeetingDateTime: `${meetingDetails.date}T09:00:00`,
        MeetUrl: meetingDetails.meetUrl
    };

    const response = await axios.post(`${API_URL}/create`, formattedDetails);
    return response.data;
};
