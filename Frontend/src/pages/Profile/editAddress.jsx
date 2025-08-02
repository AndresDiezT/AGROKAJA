import { useEffect, useMemo, useState } from "react"
import { useForm } from "react-hook-form"
import { yupResolver } from "@hookform/resolvers/yup"
import * as yup from "yup"
import { useParams, useNavigate } from "react-router-dom"

import { useGetAddressById } from "../../hooks/queries/useAddress"
import { useUpdateAddress } from "../../hooks/mutations/useAddressMutation"
import { useCities } from "../../hooks/queries/useCity"
import { useDepartments } from "../../hooks/queries/useDepartment"
import LoaderOverlay from "../../components/Loader/LoaderOverlay"
import { useProfile } from "../../context/ProfileContext"

// Esquema de validación
const schema = yup.object().shape({
    streetAddress: yup
        .string()
        .required('La dirección es obligatoria')
        .min(5, 'Debe tener al menos 5 caracteres')
        .max(255, 'Máximo 255 caracteres'),
    postalCodeAddress: yup.string().max(20, 'Máximo 20 caracteres'),
    isDefaultAddress: yup.boolean(),
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

export default function EditAddress() {
    const { id } = useParams()
    const idAddress = id
    const navigate = useNavigate()
    const { refetch } = useProfile()

    const { data: addressData, isLoading: addressLoading, isError } = useGetAddressById(idAddress)
    const { mutate: updateAddress, isLoading: isSaving } = useUpdateAddress()
    const { data: cities = [] } = useCities()
    const { data: departments = [] } = useDepartments()

    const [selectedDept, setSelectedDept] = useState(null)

    const {
        register,
        handleSubmit,
        formState: { errors },
        reset,
        setError,
        watch,
    } = useForm({
        resolver: yupResolver(schema),
    })

    useEffect(() => {
        if (addressData && cities.length > 0) {
            const { idCity, city } = addressData

            // Establecer el departamento correspondiente
            setSelectedDept(city?.idDepartment || null)

            reset({
                ...addressData,
                idCity: Number(idCity),
            })
        }
    }, [addressData, cities, reset])

    const filteredCities = useMemo(() => {
        return cities.filter(c => c.idDepartment === selectedDept)
    }, [cities, selectedDept])

    const onSubmit = data => {
        console.log(data)
        updateAddress(
            { id: idAddress, data },
            {
                onSuccess: () => {
                    refetch()
                    navigate("/profile/addresses")
                },
                onError: err => {
                    console.log(err.response)
                    const backendErrors = err.response?.data?.errors || {}
                    for (const key in backendErrors) {
                        const field = key.charAt(0).toLowerCase() + key.slice(1)
                        const message = backendErrors[key][0]
                        setError(field, { type: "server", message })
                    }
                },
            }
        )
    }

    if (addressLoading) return <LoaderOverlay />

    if (isError || !addressData) {
        return (
            <div className="text-center text-red-500 py-10">
                No se pudo cargar la dirección. Verifica que exista o que tengas acceso.
            </div>
        )
    }

    return (
        <form onSubmit={handleSubmit(onSubmit)} className="space-y-4 max-w-xl mx-auto p-4 bg-white shadow rounded">
            <h2 className="text-xl font-bold">Editar Dirección</h2>

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

            <div className="form-control">
                <label className="cursor-pointer label">
                    <span className="label-text">¿Dirección principal?</span>
                    <input type="checkbox" className="checkbox" {...register("isDefaultAddress")} />
                </label>
            </div>

            <div className="flex flex-col">
                <label>Teléfono</label>
                <label className="input w-full">
                    <span>+57</span>
                    <input {...register("phoneNumber")} className="grow outline-none border-none focus:ring-0" />
                </label>
                {errors.phoneNumber && <p className="text-red-500">{errors.phoneNumber.message}</p>}
            </div>

            <div>
                <label>Referencia</label>
                <textarea
                    {...register("addressReference")}
                    className="textarea textarea-bordered w-full"
                    rows={3}
                    maxLength={255}
                />
                {errors.addressReference && <p className="text-red-500">{errors.addressReference.message}</p>}
            </div>

            <div>
                <label>Departamento</label>
                <select
                    className="input input-bordered w-full"
                    value={selectedDept ?? ""}
                    onChange={e => {
                        const deptId = Number(e.target.value)
                        setSelectedDept(deptId || null)
                        reset({ ...watch(), idCity: 0 }) // limpia ciudad al cambiar dept
                    }}
                >
                    <option value={0} disabled>Selecciona un departamento</option>
                    {departments.map(dept => (
                        <option key={dept.idDepartment} value={dept.idDepartment}>
                            {dept.nameDepartment}
                        </option>
                    ))}
                </select>
            </div>

            <div>
                <label>Ciudad</label>
                <select {...register("idCity")} disabled={!selectedDept} className="input input-bordered w-full">
                    <option value={0} disabled>Selecciona una ciudad</option>
                    {filteredCities.map(city => (
                        <option key={city.idCity} value={city.idCity}>
                            {city.nameCity}
                        </option>
                    ))}
                </select>
                {errors.idCity && <p className="text-red-500">{errors.idCity.message}</p>}
            </div>

            <button type="submit" disabled={isSaving} className="btn btn-primary w-full">
                {isSaving ? "Guardando cambios..." : "Guardar Cambios"}
            </button>
        </form>
    )
}
