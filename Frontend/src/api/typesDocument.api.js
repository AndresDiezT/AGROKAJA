import client from './axiosInstance'

export const fetchTypeDocuments = async () => {
  const { data } = await client.get('/typeDocuments')
  return data
}

export const fetchTypeDocumentsAdmin = async () => {
  const { data } = await client.get('/typeDocuments')
  return data
}

export const createTypeDocument = async (country) => {
  await client.post('/typeDocuments', country)
}

export const updateTypeDocument = async (id, country) => {
  await client.put(`/typeDocuments/${id}`, country)
}

export const deactivateTypeDocument = async (id) => {
    await client.put(`/typeDocuments/${id}/deactivate`)
}

export const activateTypeDocument = async (id) => {
    await client.put(`/typeDocuments/${id}/activate`)
}