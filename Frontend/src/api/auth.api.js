import client from './axiosInstance'

export const registerUser = async (data) => {
    return await client.post("auth/register", data)
}

export const confirmEmailUser = async (token) => {
    return await client.get("auth/confirm-email", {
        params: { token }
    })
}

export const registerEmployeeUser = async (data) => {
    return await client.post("auth/register/employee", data)
}

export const loginUser = async (data) => {
    return await client.post("auth/login", data)
}

export const refreshTokenUser = async () => {
    return await client.post("auth/refresh-token")
}

export const logoutUser = async () => {
    return await client.post("auth/logout")
}

export const fetchMe = async () => {
    const { data } = await client.get("auth/me")
    return data
}