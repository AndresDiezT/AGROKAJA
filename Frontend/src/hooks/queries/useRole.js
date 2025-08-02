import { useQuery } from "@tanstack/react-query"
import { fetchRoles, fetchRolesAdmin } from "../../api/roles.api"

export function useRoles() {
    return useQuery({
        queryKey: ['roles'],
        queryFn: fetchRoles,
        staleTime: 1000 * 60 * 5,
        refetchOnWindowFocus: false
    })
}

export function useRolesAdmin(filters) {
    return useQuery({
        queryKey: ['roles-filter', filters],
        queryFn: () => fetchRolesAdmin(filters),
        staleTime: 1000 * 60 * 5,
        refetchOnWindowFocus: false
    })
}