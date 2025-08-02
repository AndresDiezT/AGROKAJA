import { useState } from "react"
import { useForm } from "react-hook-form"
import { yupResolver } from "@hookform/resolvers/yup"
import * as yup from "yup"
import { useCreateAddress } from '../../hooks/mutations/useAddressMutation'
import { useCities } from "../../hooks/queries/useCity"
import { useDepartments } from "../../hooks/queries/useDepartment"
import { useNavigate } from "react-router-dom"
import { useProfile } from "../../context/ProfileContext"

// Esquema de validación
const schema = yup.object().shape({
    streetAddress: yup
        .string()
        .required('La dirección es obligatoria')
        .min(5, 'Debe tener al menos 5 caracteres')
        .max(255, 'Máximo 255 caracteres'),
    postalCodeAddress: yup.string().max(20, 'Máximo 20 caracteres'),
    phoneNumber: yup
        .string()
        .required('El número de celular es obligatorio')
        .matches(/^\d{10}$/, 'Debe tener exactamente 10 dígitos numéricos'),
    addressReference: yup
        .string()
        .required('Añade una referencía')
        .max(255),
    idCity: yup.number("La ciudad es obligatoría").required('La ciudad es obligatoria'),
})
export default function AddAddress() {
    const {
        register,
        handleSubmit,
        formState: { errors, isSubmitting },
        reset,
        setError,
        setValue,
    } = useForm({
        resolver: yupResolver(schema),
    })

    const { profile, refetch } = useProfile()
    const navigate = useNavigate()
    const { mutate: createAddress, isLoading: isSaving } = useCreateAddress()
    const { data: cities = [], isLoading: citiesLoading } = useCities()
    const { data: departments = [], isLoading: departmentsLoading } = useDepartments()

    const [selectedDepartment, setSelectedDepartment] = useState("")

    const filteredCities = cities.filter(city => city.idDepartment === parseInt(selectedDepartment))

    const onSubmit = (formData) => {
        if (!profile?.document) {
            // Evita guardar si el perfil no está cargado correctamente
            setError("root", { type: "manual", message: "No se pudo obtener el documento del usuario" })
            return
        }
        const data = {
            ...formData,
            userDocument: profile?.document,
            countryCode: "+57"
        }

        createAddress(data, {
            onSuccess: () => {
                reset()
                refetch()
                navigate("/profile/addresses")
            },
            onError: (err) => {
                console.log("Error del backend:", err.response?.data)
                const data = err.response?.data
                const backendErrors = data?.errors || {}

                for (const key in backendErrors) {
                    const field = key.charAt(0).toLowerCase() + key.slice(1)
                    const message = backendErrors[key][0]

                    setError(field, { type: "server", message })
                }
            },
        })
    }

    return (
        <form onSubmit={handleSubmit(onSubmit)} className="space-y-4 max-w-xl mx-auto p-4 bg-white shadow rounded">
            <h2 className="text-xl font-bold">Agregar Dirección</h2>

            <div>
                <label>Dirección</label>
                <input {...register("streetAddress")} className="input input-bordered w-full" />
                {errors.streetAddress && <p className="text-red-500">{errors.streetAddress.message}</p>}
            </div>

            <div>
                <label>Código Postal</label>
                <input {...register("postalCodeAddress")} className="input input-bordered w-full" />
                {errors.postalCodeAddress && <p className="text-red-500">{errors.postalCodeAddress.message}</p>}
            </div>

            <div className="flex flex-col">
                <label>Teléfono</label>
                <label className="input w-full">
                    <span>+57</span>
                    <input {...register("phoneNumber")} className="input grow outline-none border-none focus:ring-0 focus:outline-none" />
                </label>
                {errors.phoneNumber && <p className="text-red-500">{errors.phoneNumber.message}</p>}
            </div>

            <div>
                <label>Referencia</label>
                <textarea
                    {...register("addressReference")}
                    className="textarea textarea-bordered w-full"
                    maxLength={255}
                    rows={3}
                />
                {errors.addressReference && <p className="text-red-500">{errors.addressReference.message}</p>}
            </div>

            <div>
                <label>Departamento</label>
                {departmentsLoading ? (
                    <p className="text-gray-500">Cargando departamentos...</p>
                ) : (
                    <select
                        className="input input-bordered w-full"
                        value={selectedDepartment}
                        onChange={(e) => {
                            setSelectedDepartment(e.target.value)
                            setValue("idCity", "") // Reset ciudad seleccionada
                        }}
                    >
                        <option value="">Selecciona un departamento</option>
                        {departments.map(dep => (
                            <option key={dep.idDepartment} value={dep.idDepartment}>
                                {dep.nameDepartment}
                            </option>
                        ))}
                    </select>
                )}
            </div>

            <div>
                <label>Ciudad</label>
                {citiesLoading ? (
                    <p className="text-gray-500">Cargando ciudades...</p>
                ) : (
                    <select
                        {...register("idCity")}
                        disabled={!selectedDepartment}
                        className="input input-bordered w-full"
                    >
                        <option value="">Selecciona una ciudad</option>
                        {filteredCities.map(city => (
                            <option key={city.idCity} value={city.idCity}>
                                {city.nameCity}
                            </option>
                        ))}
                    </select>
                )}
                {errors.idCity && <p className="text-red-500">{errors.idCity.message}</p>}
            </div>

            {errors.general && (
                <div className="alert alert-error">
                    <span>{errors.general.message}</span>
                </div>
            )}

            <button type="submit" disabled={isSubmitting || isSaving} className="btn btn-primary w-full">
                {isSaving ? "Guardando..." : "Guardar Dirección"}
            </button>
        </form>
    )
}