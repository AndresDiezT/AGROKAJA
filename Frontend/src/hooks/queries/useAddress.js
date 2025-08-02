import { useQuery } from "@tanstack/react-query"
import { fetchAddressById } from "../../api/address.api"
import { useProfile } from "../../context/ProfileContext"

export function useGetAddressById(id) {
    const { profile } = useProfile()
    const userDocument = profile?.document

    return useQuery({
        queryKey: ['addresses', id],
        queryFn: () => fetchAddressById(id, userDocument),
        enabled: !!id && !!userDocument,
        staleTime: 1000 * 60 * 5,
        refetchOnWindowFocus: false
    })
}