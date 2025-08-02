import { MapPin, Pencil, Trash2, Plus } from "lucide-react"
import { Link, useNavigate } from "react-router-dom"
import { useProfile } from "../../context/ProfileContext"
import { useDeactivateAddress } from "../../hooks/mutations/useAddressMutation"
import { useState } from "react"

export default function Addresses() {
    const { profile, refetch } = useProfile()
    const [errorMessage, setErrorMessage] = useState("")
    const addresses = profile?.addresses

    const navigate = useNavigate()

    const { mutate: deactivateAddress, isLoading } = useDeactivateAddress()

    const handleDeactivate = (id) => {
        setErrorMessage("")
        deactivateAddress(id,
            {
                onSuccess: () => {
                    refetch()
                },
                onError: (err) => {
                    const msg = err?.response?.data?.message || "Ocurrió un error al eliminar la dirección, hazlo nuevamente o intentalo mas tarde"
                    setErrorMessage(msg)
                    console.log(err)
                }
            })
    }
    
    const handleEditNavigate = (id) => {
        navigate("edit/"+id)
    }

    return (
        <div className="space-y-6">
            <div className="flex items-center justify-between">
                <h1 className="text-3xl font-bold text-gray-800 border-b border-gray-300 pb-2">Mis Direcciones</h1>
                <Link to="add" className="btn btn-primary gap-2">
                    <Plus size={18} />
                    Agregar nueva dirección
                </Link>
            </div>
            {errorMessage && (
                <div className="alert alert-error shadow-lg">
                    <div>
                        <span>{errorMessage}</span>
                    </div>
                </div>
            )}

            {addresses.length === 0 ? (
                <div className="text-center text-gray-500 mt-10">
                    No tienes direcciones guardadas aún.
                </div>
            ) : (
                <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                    {addresses.map((addr, index) => (
                        <div
                            key={addr.idAddress}
                            className="card bg-base-100 shadow-md border border-gray-200 hover:shadow-lg transition"
                        >
                            <div className="card-body space-y-2">
                                <div className="flex items-center justify-between">
                                    <h2 className="card-title text-lg flex items-center gap-2">
                                        <MapPin size={18} />
                                        Dirección #{index + 1}
                                        {addr.isDefaultAddress && (
                                            <span className="badge badge-primary text-xs ml-2">
                                                Principal
                                            </span>
                                        )}
                                    </h2>
                                    <div className="flex gap-2">
                                        <button className="btn btn-sm btn-outline btn-warning"
                                            onClick={() => handleEditNavigate(addr.idAddress)}
                                        >
                                            <Pencil size={16} />
                                        </button>
                                        <button className="btn btn-sm btn-outline btn-error"
                                            onClick={() => handleDeactivate(addr.idAddress)}
                                        >
                                            <Trash2 size={16} />
                                        </button>
                                    </div>
                                </div>
                                <p className="text-sm text-gray-700 font-medium">
                                    {addr.streetAddress}
                                </p>
                                <p className="text-sm text-gray-600">
                                    {addr.nameCity}, {addr.nameDepartment}
                                </p>
                                <p className="text-sm text-gray-600">
                                    Código Postal: {addr.postalCodeAddress}
                                </p>
                                {addr.addressReference && (
                                    <p className="text-sm text-gray-500">
                                        Referencia: {addr.addressReference}
                                    </p>
                                )}
                                <p className="text-sm text-gray-500">
                                    Teléfono: {addr.phoneNumber}
                                </p>
                            </div>
                        </div>
                    ))}
                </div>
            )}
        </div>
    )
}