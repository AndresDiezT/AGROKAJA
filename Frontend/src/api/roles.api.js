import client from './axiosInstance'

export const fetchRoles = async () => {
  const { data } = await client.get('/roles')
  return data
}

export const fetchRolesAdmin = async (filters) => {
  const { data } = await client.get('/roles/filter', {
    params: filters,
    paramsSerializer: {
      indexes: null
    }
  })
  return data
}

export const createRole = async (role) => {
  await client.post('/roles', role)
}

export const updateRole = async (id, role) => {
  await client.put(`/roles/${id}`, role)
}

export const deactivateRole = async (id) => {
  await client.put(`/roles/${id}/deactivate`)
}

export const activateRole = async (id) => {
  await client.put(`/roles/${id}/activate`)
}