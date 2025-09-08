import axios, { AxiosRequestHeaders, InternalAxiosRequestConfig } from 'axios';

const $host = axios.create({
  baseURL: process.env.REACT_API_URL, 
});

const $authHost = axios.create({
  baseURL: process.env.REACT_APP_AUTH0_AUDIENCE, 
});

$authHost.interceptors.request.use((config: InternalAxiosRequestConfig) => {
  const token = localStorage.getItem('token'); 
  if (token) {
    config.headers = {
      ...config.headers,
      Authorization: `Bearer ${token}`, 
    } as AxiosRequestHeaders;
  }
  return config;
}, (error) => {
  return Promise.reject(error);
});


export{
  $host,
  $authHost
}
