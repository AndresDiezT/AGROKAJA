import { useForm, FormProvider } from "react-hook-form"
import { yupResolver } from "@hookform/resolvers/yup"
import * as yup from "yup"
import { useState, useMemo } from "react"
import { ArrowRight, ArrowLeft, CheckCircle } from "lucide-react"
import { toast } from "react-toastify"
import { useNavigate } from "react-router-dom"

import { useRoles } from "../../../hooks/queries/useRole"
import { useCities } from "../../../hooks/queries/useCity"
import { useDepartments } from "../../../hooks/queries/useDepartment"
import { useCountries } from "../../../hooks/queries/useCountry"
import { useTypesDocument } from "../../../hooks/queries/useTypeDocument"
import { useRegisterEmployee } from "../../../hooks/mutations/useAuthMutation"
import dayjs from "dayjs"

const steps = ["Información Personal", "Datos Laborales", "Confirmación"]
const salarios = [1000000, 2000000, 3000000, 4000000]

const personalSchema = yup.object().shape({
    idTypeDocument: yup.number().moreThan(0, "Tipo de documento requerido"),
    document: yup.string().required("Documento requerido"),
    firstName: yup.string().required("Nombres requeridos"),
    lastName: yup.string().required("Apellidos requeridos"),
    email: yup.string().email("Correo inválido").required("Correo requerido"),
    phoneNumber: yup.string().required("Teléfono requerido"),
    birthDate: yup.date()
        .max(dayjs().subtract(18, "year").toDate(), "Debes tener al menos 16 años")
        .required("La fecha de nacimiento es requerida"),
    idCountry: yup.number().moreThan(0, "País requerido"),
    idDepartment: yup.number().moreThan(0, "Departamento requerido"),
    idCity: yup.number().moreThan(0, "Ciudad requerida"),
})

const laborSchema = yup.object().shape({
    idRoles: yup.array().of(
        yup.number().moreThan(0, "Rol inválido")
    ).min(1, "Selecciona al menos un rol"),
    salary: yup.number().min(1, "Salario requerido"),
    hireDate: yup.date().required("Fecha de ingreso requerida"),
})

export default function AddEmployee() {
    const navigate = useNavigate()
    const [step, setStep] = useState(0)
    const emptySchema = yup.object().shape({})

    const methods = useForm({
        resolver: yupResolver(step === 0 ? personalSchema : step === 1 ? laborSchema : emptySchema),
        defaultValues: {
            document: "",
            email: "",
            firstName: "",
            lastName: "",
            countryCode: "+57",
            phoneNumber: "",
            birthDate: "",
            idCity: 0,
            idRole: 0,
            idTypeDocument: 0,
            salary: 0,
            hireDate: "",
            idCountry: 0,
            idDepartment: 0,
        }
    })

    const { register, handleSubmit, watch, setValue, getValues, formState: { errors } } = methods
    const { mutate: addEmployee, isPending } = useRegisterEmployee()

    // Watchers para selects dependientes
    const idCountry = watch("idCountry")
    const idDepartment = watch("idDepartment")

    const { data: countries = [] } = useCountries()
    const { data: departments = [] } = useDepartments()
    const { data: cities = [] } = useCities()
    const { data: roles = [] } = useRoles()
    const { data: typesDocument = [] } = useTypesDocument()

    const filteredDepartments = useMemo(() => departments.filter(dep => dep.idCountry === Number(idCountry)), [idCountry, departments])
    const filteredCities = useMemo(() => cities.filter(city => city.idDepartment === Number(idDepartment)), [idDepartment, cities])

    const nextStep = async () => {
        const valid = await methods.trigger()
        if (valid) setStep(prev => prev + 1)
    }

    const prevStep = () => setStep(prev => prev - 1)

    const onSubmit = (data) => {
        if (step !== steps.length - 1) return

        addEmployee(data, {
            onSuccess: (msg) => {
                toast.success(msg)
                navigate("/staff/employees/list")
            },
            onError: (err) => {
                console.log("Error del backend:", err)
                const data = err.response?.data
                const backendErrors = data?.errors || {}

                for (const key in backendErrors) {
                    const field = key.charAt(0).toLowerCase() + key.slice(1)
                    const message = backendErrors[key][0]

                    setError(field, { type: "server", message })
                }
            }
        })
    }

    // Fecha máxima permitida: hoy - 16 años
    const maxDate = dayjs().subtract(18, 'year').format('YYYY-MM-DD')

    return (
        <FormProvider {...methods}>
            <form onSubmit={handleSubmit(onSubmit)} className="max-w-4xl mx-auto p-8 rounded-xl shadow-xl space-y-8 flex flex-col items-center"
                onKeyDown={(e) => {
                    if (e.key === "Enter" && step !== steps.length - 1) {
                        e.preventDefault()
                    }
                }}
            >
                {/* Steps */}
                <ul className="steps w-full steps-vertical sm:steps-horizontal">
                    {steps.map((label, i) => (
                        <li key={i} className={`step ${i <= step ? "step-primary" : ""}`}>{label}</li>
                    ))}
                </ul>
                {errors.general && (
                    <div className="alert alert-error">
                        <span>{errors.general.message}</span>
                    </div>
                )}
                {/* Step Content */}
                {step === 0 && (
                    <div className="grid grid-cols-1 md:grid-cols-2 gap-4 w-full">
                        <div className="flex flex-col">
                            <label className="">Tipo de Documento</label>
                            <select {...register("idTypeDocument")} className="select w-full">{typesDocument.map(td => <option key={td.idTypeDocument} value={td.idTypeDocument}>{td.nameTypeDocument}</option>)}</select>
                            {errors.idTypeDocument && <p className="text-red-500 text-sm">{errors.idTypeDocument.message}</p>}
                        </div>
                        <div className="flex flex-col">
                            <label className="">Numero de Documento</label>
                            <input {...register("document")} className="input w-full" placeholder="sin puntos ni comas" />
                            {errors.document && <p className="text-red-500 text-sm">{errors.document.message}</p>}
                        </div>
                        <div className="flex flex-col">
                            <label className="">Nombres</label>
                            <input {...register("firstName")} className="input w-full" />
                            {errors.firstName && <p className="text-red-500 text-sm">{errors.firstName.message}</p>}
                        </div>
                        <div className="flex flex-col">
                            <label className="">Apellidos</label>
                            <input {...register("lastName")} className="input w-full" />
                            {errors.lastName && <p className="text-red-500 text-sm">{errors.lastName.message}</p>}
                        </div>
                        <div className="flex flex-col">
                            <label className="">Correo Electronico</label>
                            <input type="email" {...register("email")} className="input w-full" />
                            {errors.email && <p className="text-red-500 text-sm">{errors.email.message}</p>}
                        </div>
                        <div className="flex flex-col">
                            <label className="">Telefono</label>
                            <input {...register("phoneNumber")} placeholder="+57" className="input w-full" />
                            {errors.phoneNumber && <p className="text-red-500 text-sm">{errors.phoneNumber.message}</p>}
                        </div>
                        <div className="flex flex-col">
                            <label className="">Fecha de Nacimiento</label>
                            <input type="date" {...register("birthDate")} className="input w-full" max={maxDate} />
                            {errors.birthDate && <p className="text-red-500 text-sm">{errors.birthDate.message}</p>}
                        </div>

                        <div className="flex flex-col">
                            <label className="">País</label>
                            <select {...register("idCountry")} onChange={(e) => {
                                setValue("idCountry", e.target.value)
                                setValue("idDepartment", 0)
                                setValue("idCity", 0)
                            }} className="select w-full">
                                <option value={0}>Seleccionar país</option>
                                {countries.map(c => <option key={c.idCountry} value={c.idCountry}>{c.nameCountry}</option>)}
                            </select>
                            {errors.idCountry && <p className="text-red-500 text-sm">{errors.idCountry.message}</p>}
                        </div>

                        <div className="flex flex-col">
                            <label className="">Departamento</label>
                            <select {...register("idDepartment")} disabled={!idCountry} onChange={(e) => {
                                setValue("idDepartment", e.target.value)
                                setValue("idCity", 0)
                            }} className="select w-full">
                                <option value={0}>Seleccionar departamento</option>
                                {filteredDepartments.map(d => <option key={d.idDepartment} value={d.idDepartment}>{d.nameDepartment}</option>)}
                            </select>
                            {errors.idDepartment && <p className="text-red-500 text-sm">{errors.idDepartment.message}</p>}
                        </div>

                        <div className="flex flex-col">
                            <label className="">Ciudad</label>
                            <select {...register("idCity")} disabled={!idDepartment} className="select w-full">
                                <option value={0}>Seleccionar ciudad</option>
                                {filteredCities.map(c => <option key={c.idCity} value={c.idCity}>{c.nameCity}</option>)}
                            </select>
                            {errors.idCity && <p className="text-red-500 text-sm">{errors.idCity.message}</p>}
                        </div>
                    </div>
                )}

                {step === 1 && (
                    <div className="grid grid-cols-1 md:grid-cols-2 gap-6 w-full">
                        <div className="flex flex-col">
                            <label className="">Salario</label>
                            <select {...register("salary")} className="select w-full">
                                <option value="">Seleccionar salario</option>
                                {salarios.map((s, i) => <option key={i} value={s}>{`$${s.toLocaleString("es-CO")}`}</option>)}
                            </select>
                            {errors.salary && <p className="text-red-500 text-sm">{errors.salary.message}</p>}
                        </div>

                        <div className="flex flex-col">
                            <label className="">Fecha de Ingreso</label>
                            <input type="date" {...register("hireDate")} className="input w-full" />
                            {errors.hireDate && <p className="text-red-500 text-sm">{errors.hireDate.message}</p>}
                        </div>
                        <div className="mb-6 md:col-span-2">
                            <label className="block text-sm font-medium mb-2">
                                Selecciona Roles
                            </label>
                            {errors.idRoles && <p className="text-red-500 text-sm">{errors.idRoles.message}</p>}
                            <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-4">
                                {roles
                                    .filter((role) => role.nameRole !== "Campesino" && role.nameRole !== "Comprador")
                                    .map((role) => {
                                        const selected = methods.watch("idRoles") || []

                                        const handleChange = (checked) => {
                                            const updated = checked
                                                ? [...selected, role.idRole]
                                                : selected.filter((id) => id !== role.idRole)
                                            methods.setValue("idRoles", updated)
                                        }

                                        return (
                                            <label
                                                key={role.idRole}
                                                className="flex items-center space-x-2 cursor-pointer p-3 rounded-md border"
                                            >
                                                <input
                                                    type="checkbox"
                                                    checked={selected.includes(role.idRole)}
                                                    onChange={(e) => handleChange(e.target.checked)}
                                                    className="form-checkbox text-teal-600 h-5 w-5"
                                                />
                                                <span className="text-sm">{role.nameRole}</span>
                                            </label>
                                        )
                                    })}
                            </div>
                        </div>
                    </div>
                )}

                {step === 2 && (
                    <div className="space-y-6">
                        <h3 className="text-lg font-semibold flex items-center gap-2 text-primary">
                            <CheckCircle size={20} /> Confirmar Datos del Empleado
                        </h3>

                        <div className="grid grid-cols-1 md:grid-cols-2 gap-6 text-sm p-6 rounded-lg">
                            <div>
                                <h4 className="font-semibold mb-2">Información Personal</h4>
                                <p><strong>Tipo de Documento:</strong> {typesDocument.find(td => td.idTypeDocument === Number(getValues("idTypeDocument")))?.nameTypeDocument}</p>
                                <p><strong>Documento:</strong> {getValues("document")}</p>
                                <p><strong>Nombres:</strong> {getValues("firstName")}</p>
                                <p><strong>Apellidos:</strong> {getValues("lastName")}</p>
                                <p><strong>Email:</strong> {getValues("email")}</p>
                                <p><strong>Teléfono:</strong> {getValues("phoneNumber")}</p>
                                <p><strong>Fecha de Nacimiento:</strong> {getValues("birthDate")}</p>
                                <p><strong>País:</strong> {countries.find(c => c.idCountry === Number(getValues("idCountry")))?.nameCountry}</p>
                                <p><strong>Departamento:</strong> {departments.find(d => d.idDepartment === Number(getValues("idDepartment")))?.nameDepartment}</p>
                                <p><strong>Ciudad:</strong> {cities.find(c => c.idCity === Number(getValues("idCity")))?.nameCity}</p>
                            </div>

                            <div>
                                <h4 className="font-semibold mb-2">Información Laboral</h4>
                                <p><strong>Cargos:</strong> {
                                    (getValues("idRoles") || [])
                                        .map(roleId => roles.find(r => r.idRole === roleId)?.nameRole)
                                        .filter(Boolean)
                                        .join(", ")
                                }</p>
                                <p><strong>Salario:</strong> ${Number(getValues("salary")).toLocaleString("es-CO")}</p>
                                <p><strong>Fecha de Ingreso:</strong> {getValues("hireDate")}</p>
                            </div>
                        </div>
                    </div>
                )}

                {/* Navegación */}
                <div className="flex justify-between pt-4 gap-2">
                    {step > 0 && (
                        <button type="button" onClick={prevStep} className="btn">← Anterior</button>
                    )}
                    {step < steps.length - 1 ? (
                        <button type="button" onClick={nextStep} className="btn ml-auto">Siguiente</button>
                    ) : (
                        <button type="submit" className="btn ml-auto" onKeyDown={(e) => {
                            if (e.key === "Enter" && step !== steps.length - 1) {
                                e.preventDefault()
                            }
                        }}>Confirmar y Guardar</button>
                    )}
                </div>
            </form>
        </FormProvider>
    )
}