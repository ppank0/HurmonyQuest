import axios, { AxiosRequestHeaders, InternalAxiosRequestConfig } from 'axios';

// Создаём базовый экземпляр axios с адресом API
export const $host = axios.create({
  baseURL: process.env.REACT_API_URL, // например, http://localhost:7057/api
});

// Интерсептор добавляет токен к каждому запросу
$host.interceptors.request.use((config: InternalAxiosRequestConfig) => {
  const token = localStorage.getItem('token'); // берём токен из localStorage
  if (token) {
    config.headers = {
      ...config.headers,
      Authorization: `Bearer ${token}`, // добавляем заголовок Authorization
    } as AxiosRequestHeaders;
  }
  return config;
}, (error) => {
  return Promise.reject(error);
});


