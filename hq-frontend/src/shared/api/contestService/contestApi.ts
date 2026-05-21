import axios from "axios";

export const contestApi = axios.create({
    baseURL: import.meta.env.VITE_CONTEST_API_URL,
    headers: {
        'Content-Type': 'application/json',
    }}
)