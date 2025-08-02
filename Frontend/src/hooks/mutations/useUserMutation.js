import { useMutation, useQueryClient } from "@tanstack/react-query"
import {
    updateUser,
    deactivateUser,
    activateUser
} from "../../api/users.api"

export function useUpdateUser() {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: ({ document, data }) => updateUser(document, data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['profile'] })
        }
    })
}

export function useDeactivateUser() {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: (document) => deactivateUser(document),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['users'] }),
                queryClient.invalidateQueries({ queryKey: ['users-filter'] })
        }
    })
}

export function useActivateUser() {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: (document) => activateUser(document),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['users'] }),
                queryClient.invalidateQueries({ queryKey: ['users-filter'] })
        }
    })
}