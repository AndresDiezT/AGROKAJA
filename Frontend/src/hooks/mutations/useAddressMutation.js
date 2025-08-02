import { useMutation, useQueryClient } from "@tanstack/react-query"
import {
    activateAddress,
    createAddress,
    deactivateAddress,
    updateAddress
} from "../../api/address.api"

export function useCreateAddress() {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: createAddress,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['addresses'] }),
            queryClient.invalidateQueries({ queryKey: ['addresses-filter'] })
        }
    })
}

export function useUpdateAddress() {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: ({ id, data }) => updateAddress(id, data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['addresses'] }),
            queryClient.invalidateQueries({ queryKey: ['addresses-filter'] })
        }
    })
}

export function useDeactivateAddress() {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: (id) => deactivateAddress(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['addresses'] }),
            queryClient.invalidateQueries({ queryKey: ['addresses-filter'] })
        }
    })
}

export function useActivateAddress() {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: (id) => activateAddress(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['addresses'] }),
            queryClient.invalidateQueries({ queryKey: ['addresses-filter'] })
        }
    })
}