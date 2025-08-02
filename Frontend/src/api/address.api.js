import client from './axiosInstance'

export const fetchAddressById = async (id, userDocument) => {
    const { data } = await client.get(`/addresses/${id}`, {
        params: { userDocument }
    })
    return data
}

export const fetchAddressByUser = async (document) => {
    const { data } = await client.get(`/addresses/by-user/${document}`)
    return data
}

export const createAddress = async (address) => {
    await client.post('/addresses', address)
}

export const updateAddress = async (id, address) => {
    await client.put(`/addresses/${id}`, address)
}

export const deactivateAddress = async (id) => {
    await client.put(`/addresses/${id}/deactivate`)
}

export const activateAddress = async (id) => {
    await client.put(`/addresses/${id}/activate`)
}