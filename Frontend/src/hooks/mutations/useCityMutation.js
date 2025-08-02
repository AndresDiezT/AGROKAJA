import { useMutation, useQueryClient } from "@tanstack/react-query"
import {
    activateCity,
    createCity,
    deactivateCity,
    updateCity
} from "../../api/cities.api"

export function useCreateCity() {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: createCity,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['cities'] }),
            queryClient.invalidateQueries({ queryKey: ['cities-filter'] })
        }
    })
}

export function useUpdateCity() {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: ({ id, data }) => updateCity(id, data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['cities'] }),
            queryClient.invalidateQueries({ queryKey: ['cities-filter'] })
        }
    })
}

export function useDeactivateCity() {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: (id) => deactivateCity(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['cities'] }),
            queryClient.invalidateQueries({ queryKey: ['cities-filter'] })
        }
    })
}

export function useActivateCity() {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: (id) => activateCity(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['cities'] }),
            queryClient.invalidateQueries({ queryKey: ['cities-filter'] })
        }
    })
}