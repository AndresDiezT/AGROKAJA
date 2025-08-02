import { useMutation, useQueryClient } from "@tanstack/react-query"
import {
    activateCountry,
    createCountry,
    deactivateCountry,
    updateCountry
} from "../../api/countries.api"

export function useCreateCountry() {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: createCountry,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['countries'] }),
            queryClient.invalidateQueries({ queryKey: ['countries-filter'] })
        }
    })
}

export function useUpdateCountry() {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: ({ id, data }) => updateCountry(id, data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['countries'] }),
            queryClient.invalidateQueries({ queryKey: ['countries-filter'] })
        }
    })
}

export function useDeactivateCountry() {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: (id) => deactivateCountry(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['countries'] }),
            queryClient.invalidateQueries({ queryKey: ['countries-filter'] })
        }
    })
}

export function useActivateCountry() {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: (id) => activateCountry(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['countries'] }),
            queryClient.invalidateQueries({ queryKey: ['countries-filter'] })
        }
    })
}