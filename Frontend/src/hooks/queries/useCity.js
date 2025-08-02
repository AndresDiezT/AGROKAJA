import { useQuery } from "@tanstack/react-query"
import { fetchCities, fetchCitiesAdmin } from "../../api/cities.api"

export function useCities() {
    return useQuery({
        queryKey: ['cities'],
        queryFn: fetchCities,
        staleTime: 1000 * 60 * 5,
        refetchOnWindowFocus: false
    })
}

export function useCitiesAdmin(filters) {
    return useQuery({
        queryKey: ['cities-filter', filters],
        queryFn: () => fetchCitiesAdmin(filters),
        staleTime: 1000 * 60 * 5,
        refetchOnWindowFocus: false
    })
}