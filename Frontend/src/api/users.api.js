import client from './axiosInstance'

export const fetchEmployeesAdmin = async (filters) => {
    const { data } = await client.get("users/filter/employees", {
        params: filters,
        paramsSerializer: {
            indexes: null
        }
    })

    return data ?? { data: [], columns: [], total: 0 }
}

export const fetchCustomersAdmin = async (filters) => {
    const { data } = await client.get("/users/filter/customers", {
        params: filters,
        paramsSerializer: {
            indexes: null
        }
    })
    
    return data ?? { data: [], columns: [], total: 0 }
}

export const updateUser = async (document, data) => {
    await client.put(`/users/${document}`, data)
}

export const deactivateUser = async (document) => {
    console.log(document)
    return await client.put(`/users/${document}/deactivate`)
}

export const activateUser = async (document) => {
    return await client.put(`/users/${document}/activate`)
}