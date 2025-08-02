import client from './axiosInstance'

export const fetchDepartments = async () => {
  const { data } = await client.get('/departments')
  return data
}

export const fetchDepartmentsAdmin = async (filters) => {
  const { data } = await client.get('/departments/filter', {
    params: filters,
    paramsSerializer: {
      indexes: null
    }
  })
  return data
}

export const createDepartment = async (country) => {
  await client.post('/departments', country)
}

export const updateDepartment = async (id, country) => {
  await client.put(`/departments/${id}`, country)
}

export const deactivateDepartment = async (id) => {
  await client.put(`/departments/${id}/deactivate`)
}

export const activateDepartment = async (id) => {
  await client.put(`/departments/${id}/activate`)
}