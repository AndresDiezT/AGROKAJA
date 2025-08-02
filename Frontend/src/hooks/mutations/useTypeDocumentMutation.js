import { useMutation, useQueryClient } from "@tanstack/react-query"
import {
    activateTypeDocument,
    createTypeDocument,
    deactivateTypeDocument,
    updateTypeDocument
} from "../../api/typesDocument.api"

export function useCreateTypeDocument() {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: createTypeDocument,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['typesDocument'] })
        }
    })
}

export function useUpdateTypeDocument() {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: ({ id, data }) => updateTypeDocument(id, data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['typesDocument'] })
        }
    })
}

export function useDeactivateTypeDocument() {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: (id) => deactivateTypeDocument(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['typesDocument'] })
        }
    })
}

export function useActivateTypeDocument() {
    const queryClient = useQueryClient()

    return useMutation({
        mutationFn: (id) => activateTypeDocument(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['typesDocument'] })
        }
    })
}