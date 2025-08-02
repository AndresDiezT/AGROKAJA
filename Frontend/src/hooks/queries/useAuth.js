import { useQuery } from "@tanstack/react-query"
import { fetchMe } from "../../api/auth.api"

export function useMe() {
    const token = localStorage.getItem("token")

    return useQuery({
        queryKey: ['users'],
        queryFn: fetchMe,
        enabled: !!token,
        staleTime: 1000 * 60 * 5,
        refetchOnWindowFocus: false
    })
}