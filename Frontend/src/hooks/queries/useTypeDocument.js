import { useQuery } from "@tanstack/react-query"
import { fetchTypeDocuments, fetchTypeDocumentsAdmin } from "../../api/typesDocument.api"

export function useTypesDocument() {
    return useQuery({
        queryKey: ['typeDocuments'],
        queryFn: fetchTypeDocuments,
        staleTime: 1000 * 60 * 5,
        refetchOnWindowFocus: false
    })
}

export function useTypesDocumentAdmin() {
    return useQuery({
        queryKey: ['typeDocuments-filter'],
        queryFn: fetchTypeDocumentsAdmin,
        staleTime: 1000 * 60 * 5,
        refetchOnWindowFocus: false
    })
}