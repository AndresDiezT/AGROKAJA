import { useMutation, useQueryClient } from "@tanstack/react-query"
import {
    registerUser,
    loginUser,
    refreshTokenUser,
    logoutUser,
    confirmEmailUser,
    registerEmployeeUser,
} from "../../api/auth.api"
import { useNavigate } from "react-router-dom"

export function useRegisterUser() {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: registerUser,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['users', 'customers'] }),
                queryClient.invalidateQueries({ queryKey: ['users-filter'] })
        }
    })
}

export function useConfirmEmailUser(token) {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: confirmEmailUser(token),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['users'] }),
                queryClient.invalidateQueries({ queryKey: ['users-filter'] })
        }
    })
}

export function useRegisterEmployee() {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: registerEmployeeUser,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['users', 'employees'] }),
                queryClient.invalidateQueries({ queryKey: ['users-filter'] })
        }
    })
}

export function useLoginUser() {

    return useMutation({
        mutationFn: loginUser,
        onSuccess: (data) => {
            localStorage.setItem("token", data.data.accessToken)
            window.location.reload()
        }
    })
}

export function useRefreshTokenUser() {
    const navigate = useNavigate()

    return useMutation({
        mutationFn: refreshTokenUser,
        onSuccess: (data) => {
            localStorage.setItem("token", data.data.accessToken)
        },
        onError: (error) => {
            console.error("❌ Error al refrescar token", error)
            localStorage.removeItem("token")
            window.location.reload()
            navigate("/login")
        }
    })
}

export function useLogoutUser() {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: logoutUser,
        onSuccess: () => {
            queryClient.clear()
            window.location.reload()
        },
        onError: (error) => {
            console.error("❌ Error al refrescar token", error)
            window.location.reload()
        }
    })
}