import axios from 'axios'

const axiosInstance = axios.create({
    baseURL: 'http://localhost:5236/api',
    withCredentials: true,
    headers: {
        'Content-Type': 'application/json'
    }
})

// Agregar el access token en cada request
axiosInstance.interceptors.request.use(
    (config) => {
        const token = localStorage.getItem('token')
        if (token) {
            config.headers.Authorization = `Bearer ${token}`
        }
        return config
    },
    (error) => {
        return Promise.reject(error)
    }
)

// Refrescar el token si expira
axiosInstance.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config

    if (error.response?.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true
      try {
        // Se pide un nuevo access token usando el refresh token (coockie HttpOnly)
        const { data } = await axiosInstance.post(
          'auth/refresh-token',
          {},
          { withCredentials: true }
        )
        localStorage.setItem('token', data.accessToken)

        window.dispatchEvent(new Event('tokenRefreshed'))

        originalRequest.headers.Authorization = `Bearer ${data.accessToken}`

        return axiosInstance(originalRequest)
      } catch (error) {
        localStorage.removeItem('token')
        window.location.href = '/login'
      }
    }

    return Promise.reject(error)
  }
)

export default axiosInstance