import { useMutation, useQueryClient } from "@tanstack/react-query"
import {
    activateDepartment,
    createDepartment,
    deactivateDepartment,
    updateDepartment
} from "../../api/departments.api"

export function useCreateDepartment() {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: createDepartment,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['departments'] })
            queryClient.invalidateQueries({ queryKey: ['departments-filter'] })
        }
    })
}

export function useUpdateDepartment() {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: ({ id, data }) => updateDepartment(id, data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['departments'] })
            queryClient.invalidateQueries({ queryKey: ['departments-filter'] })
        }
    })
}

export function useDeactivateDepartment() {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: (id) => deactivateDepartment(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['departments'] })
            queryClient.invalidateQueries({ queryKey: ['departments-filter'] })
        }
    })
}

export function useActivateDepartment() {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: (id) => activateDepartment(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['departments'] })
            queryClient.invalidateQueries({ queryKey: ['departments-filter'] })
        }
    })
}