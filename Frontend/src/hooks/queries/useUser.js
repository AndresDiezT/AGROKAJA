import { useQuery } from "@tanstack/react-query"
import { fetchCustomersAdmin, fetchEmployeesAdmin } from "../../api/users.api"

export function useEmployeesUser(filters) {
    return useQuery({
        queryKey: ['employees-filters', filters],
        queryFn: () => fetchEmployeesAdmin(filters),
        staleTime: 1000 * 60 * 5,
        refetchOnWindowFocus: false
    })
}

export function useCustomersUser(filters) {
    return useQuery({
        queryKey: ['customers-filters', filters],
        queryFn: () => fetchCustomersAdmin(filters),
        staleTime: 1000 * 60 * 5,
        refetchOnWindowFocus: false
    })
}