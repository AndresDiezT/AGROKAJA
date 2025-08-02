import client from './axiosInstance'

export const fetchCities = async () => {
  const { data } = await client.get('/cities')
  return data
}

export const fetchCitiesAdmin = async (filters) => {
  const { data } = await client.get('/cities/filter', {
    params: filters,
    paramsSerializer: {
      indexes: null
    }
  })
  return data
}

export const createCity = async (country) => {
  await client.post('/cities', country)
}

export const updateCity = async (id, country) => {
  await client.put(`/cities/${id}`, country)
}

export const deactivateCity = async (id) => {
    await client.put(`/cities/${id}/deactivate`)
}

export const activateCity = async (id) => {
    await client.put(`/cities/${id}/activate`)
}