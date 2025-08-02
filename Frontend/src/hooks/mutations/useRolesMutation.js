import { useMutation, useQueryClient } from "@tanstack/react-query"
import {
    activateRole,
    createRole,
    deactivateRole,
    updateRole
} from "../../api/roles.api"

export function useCreateRole() {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: createRole,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['roles'] })
        }
    })
}

export function useUpdateRole() {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: ({ id, data }) => updateRole(id, data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['roles'] })
        }
    })
}

export function useDeactivateRole() {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: (id) => deactivateRole(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['roles'] })
        }
    })
}

export function useActivateRole() {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: (id) => activateRole(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['roles'] })
        }
    })
}