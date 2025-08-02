import client from './axiosInstance'

export const fetchCountries = async () => {
  const { data } = await client.get('/countries')
  return data
}

export const fetchCountriesAdmin = async (filters) => {
  const { data } = await client.get('/countries/filter', {
    params: filters,
    paramsSerializer: {
      indexes: null
    }
  })
  return data
}

export const createCountry = async (country) => {
  await client.post('/countries', country)
}

export const updateCountry = async (id, country) => {
  await client.put(`/countries/${id}`, country)
}

export const deactivateCountry = async (id) => {
    await client.put(`/countries/${id}/deactivate`)
}

export const activateCountry = async (id) => {
    await client.put(`/countries/${id}/activate`)
}