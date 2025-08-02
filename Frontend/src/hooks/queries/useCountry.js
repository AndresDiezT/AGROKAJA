import { useQuery } from "@tanstack/react-query"
import { fetchCountries, fetchCountriesAdmin } from "../../api/countries.api"

export function useCountries() {
    return useQuery({
        queryKey: ['countries'],
        queryFn: fetchCountries,
        staleTime: 1000 * 60 * 5,
        refetchOnWindowFocus: false
    })
}

export function useCountriesAdmin(filters) {
    return useQuery({
        queryKey: ['countries-filter', filters],
        queryFn: () => fetchCountriesAdmin(filters),
        staleTime: 1000 * 60 * 5,
        refetchOnWindowFocus: false
    })
}