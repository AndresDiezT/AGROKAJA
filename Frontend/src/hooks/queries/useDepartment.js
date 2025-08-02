import { useQuery } from "@tanstack/react-query"
import { fetchDepartments, fetchDepartmentsAdmin } from "../../api/departments.api"

export function useDepartments() {
    return useQuery({
        queryKey: ['departments'],
        queryFn: fetchDepartments,
        staleTime: 1000 * 60 * 5,
        refetchOnWindowFocus: false
    })
}

export function useDepartmentsAdmin(filters) {
    return useQuery({
        queryKey: ['departments-filter', filters],
        queryFn: () => fetchDepartmentsAdmin(filters),
        staleTime: 1000 * 60 * 5,
        refetchOnWindowFocus: false
    })
}