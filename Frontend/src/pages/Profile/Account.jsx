import { useEffect, useState } from "react"
import { LockKeyhole, Newspaper, ReceiptText, Scale } from "lucide-react"
import { useForm } from "react-hook-form"
import { yupResolver } from "@hookform/resolvers/yup"
import * as yup from "yup"
import { useUpdateUser } from "../../hooks/mutations/useUserMutation"
import { useProfile } from "../../context/ProfileContext"
import dayjs from "dayjs"

const schema = yup.object({
  username: yup.string().required("El nombre de usuario es requerido"),
  firstName: yup.string().required("El nombre es requerido"),
  lastName: yup.string().required("El apellido es requerido"),
  birthDate: yup.date()
    .max(dayjs().subtract(16, "year").toDate(), "Debes tener al menos 16 años")
    .required("La fecha de nacimiento es requerida"),
  phoneNumber: yup.string().required("El teléfono es requerido"),
})

export default function Account() {
  const { profile, refetch } = useProfile()
  const document = profile?.document

  const [editingPersonal, setEditingPersonal] = useState(false)
  const [editingUsername, setEditingUsername] = useState(false)
  const [editingPhone, setEditingPhone] = useState(false)

  const {
    register,
    handleSubmit,
    setValue,
    formState: { errors }
  } = useForm({
    resolver: yupResolver(schema),
    values: {
      username: profile?.username || "",
      firstName: profile?.firstName || "",
      lastName: profile?.lastName || "",
      birthDate: profile?.birthDate || "",
      phoneNumber: profile?.phoneNumber || "",
    }
  })

  useEffect(() => {
    if (profile) {
      setValue("username", profile.username || "")
      setValue("firstName", profile.firstName || "")
      setValue("lastName", profile.lastName || "")
      setValue("birthDate", profile.birthDate || "")
      setValue("phoneNumber", profile.phoneNumber || "")
    }
  }, [profile, setValue])

  const { mutate: updateUser, isPending } = useUpdateUser()

  const onSubmit = (data) => {
    if (!editingPersonal && !editingUsername && !editingPhone) return;

    updateUser({ document, data }, {
      onSuccess: () => {
        refetch()
        setEditingPersonal(false)
        setEditingPhone(false)
        setEditingUsername(false)
      },
      onError: (err) => {
        console.error("Error al actualizar", err)
      }
    })
  }

  // Fecha máxima permitida: hoy - 16 años
  const maxDate = dayjs().subtract(16, 'year').format('YYYY-MM-DD')

  return (
    <div className="w-full space-y-4">
      <h1 className="text-3xl font-bold text-gray-800 border-b border-gray-300 pb-2">Mi Perfil</h1>

      {/* Información Personal */}
      <div className="collapse collapse-arrow bg-[#65C56F] rounded-box">
        <input type="checkbox" />
        <div className="collapse-title text-xl font-medium">Información Personal</div>
        <div className="collapse-content space-y-4">
          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <div>
              <label className="label text-white font-bold">Tipo de Documento</label>
              <input className="input input-bordered w-full" value={profile?.nameTypeDocument || ""} disabled />
            </div>
            <div>
              <label className="label text-white font-bold">Número de Documento</label>
              <input className="input input-bordered w-full" value={profile?.document || ""} disabled />
            </div>

            <div>
              <label className="label text-white font-bold">Nombres</label>
              <input
                {...register("firstName")}
                className="input input-bordered w-full"
                disabled={!editingPersonal}
              />
              {errors.firstName && <p className="text-red-500 text-sm">{errors.firstName.message}</p>}
            </div>

            <div>
              <label className="label text-white font-bold">Apellidos</label>
              <input
                {...register("lastName")}
                className="input input-bordered w-full"
                disabled={!editingPersonal}
              />
              {errors.lastName && <p className="text-red-500 text-sm">{errors.lastName.message}</p>}
            </div>

            <div className="md:col-span-2">
              <label className="label text-white font-bold">Fecha de Nacimiento</label>
              <input
                {...register("birthDate")}
                type="date"
                className="input input-bordered w-full"
                disabled={!editingPersonal}
                max={maxDate}
              />
              {errors.birthDate && <p className="text-red-500 text-sm">{errors.birthDate.message}</p>}
            </div>
          </div>
          <div className="text-left">
            <button
              className="btn btn-lg btn-primary"
              onClick={editingPersonal ? handleSubmit(onSubmit) : () => setEditingPersonal(true)}
              disabled={isPending}
            >
              {editingPersonal ? "Guardar" : "Modificar"}
            </button>
          </div>
        </div>
      </div>

      {/* Datos de la Cuenta */}
      <div className="collapse collapse-arrow bg-[#65C56F] rounded-box">
        <input type="checkbox" />
        <div className="collapse-title text-xl font-medium">Datos de la Cuenta</div>
        <div className="collapse-content space-y-4">
          <div className="grid grid-cols-1 gap-4">

            {/* Usuario */}
            <div className="card bg-white shadow-md p-4">
              <div className="flex flex-col md:flex-row justify-between items-start md:items-center gap-4">
                <div className="w-full md:w-2/3">
                  <label className="label font-bold text-gray-700">Nombre de Usuario</label>
                  <input
                    {...register("username")}
                    className="input input-bordered w-full"
                    disabled={!editingUsername}
                  />
                  {errors.username && <p className="text-red-500 text-sm">{errors.username.message}</p>}
                </div>
                <div>
                  <button
                    className="btn btn-outline btn-accent"
                    onClick={editingUsername ? handleSubmit(onSubmit) : () => setEditingUsername(true)}
                    disabled={isPending}
                  >
                    {editingUsername ? "Guardar" : "Modificar"}
                  </button>
                </div>
              </div>
            </div>

            {/* Email */}
            <div className="card bg-white shadow-md p-4">
              <div className="flex flex-col md:flex-row justify-between items-start md:items-center gap-4">
                <div className="w-full md:w-2/3">
                  <label className="label font-bold text-gray-700">Correo Electrónico</label>
                  <input className="input input-bordered w-full" value={profile?.email || ""} disabled />
                  {profile?.emailIsVerified ? (
                    <p className="text-sm mt-1 text-green-600">✅ Verificado</p>
                  ) : (
                    <p className="text-sm mt-1 text-red-600">❌ No verificado</p>
                  )}
                </div>
              </div>
            </div>

            {/* Teléfono */}
            <div className="card bg-white shadow-md p-4">
              <div className="flex flex-col md:flex-row justify-between items-start md:items-center gap-4">
                <div className="w-full md:w-2/3">
                  <label className="label font-bold text-gray-700">Teléfono</label>
                  <input
                    {...register("phoneNumber")}
                    className="input input-bordered w-full"
                    disabled={!editingPhone}
                  />
                  {errors.phoneNumber && <p className="text-red-500 text-sm">{errors.phoneNumber.message}</p>}
                  {profile?.phoneIsVerified ? (
                    <p className="text-sm mt-1 text-green-600">✅ Verificado</p>
                  ) : (
                    <p className="text-sm mt-1 text-red-600">❌ No verificado</p>
                  )}
                </div>
                <div>
                  {profile?.phoneIsVerified ? (
                    <button
                      className="btn btn-outline btn-accent"
                      onClick={editingPhone ? handleSubmit(onSubmit) : () => setEditingPhone(true)}
                      disabled={isPending}
                    >
                      {editingPhone ? "Guardar" : "Modificar"}
                    </button>
                  ) : (
                    <div className="flex gap-2">
                      <button className="btn btn-outline btn-accent">Verificar</button>
                      <button
                        className="btn btn-outline btn-accent"
                        onClick={editingPhone ? handleSubmit(onSubmit) : () => setEditingPhone(true)}
                        disabled={isPending}
                      >
                        {editingPhone ? "Guardar" : "Modificar"}
                      </button>
                    </div>
                  )}

                </div>
              </div>
            </div>

          </div>
        </div>
      </div>

      {/* Seguridad */}
      <div className="collapse collapse-arrow bg-[#65C56F] rounded-box">
        <input type="checkbox" />
        <div className="collapse-title text-xl font-medium">Seguridad</div>
        <div className="collapse-content space-y-4">
          <h2 className="card-title text-lg">Contraseña</h2>
          <p className="text-gray-600">Tu contraseña protege tu cuenta del acceso no autorizado.</p>
          <button className="btn btn-lg btn-primary mt-5">Modificar Contraseña</button>
        </div>
      </div>

      {/* Políticas */}
      <div className="collapse collapse-arrow bg-[#65C56F] rounded-box">
        <input type="checkbox" />
        <div className="collapse-title text-xl font-medium">Información Legal y Políticas</div>
        <div className="collapse-content p-4 rounded-b-box space-y-4">
          <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
            {[
              {
                title: "Política de Privacidad",
                desc: "Cómo se maneja tu información personal.",
                icon: <Newspaper className="text-gray-800" />,
              },
              {
                title: "Protección de Datos",
                desc: "Tus datos están protegidos bajo la ley.",
                icon: <LockKeyhole className="text-gray-800" />,
              },
              {
                title: "Términos y Condiciones",
                desc: "Conoce las reglas para el uso de nuestra plataforma.",
                icon: <ReceiptText className="text-gray-800" />,
              },
              {
                title: "Derechos del Consumidor",
                desc: "Información sobre tus derechos como cliente.",
                icon: <Scale className="text-gray-800" />,
              },
            ].map((item, i) => (
              <div key={i} className="card bg-white shadow-md border hover:shadow-lg cursor-pointer transition">
                <div className="card-body flex-row items-center gap-4">
                  <div>{item.icon}</div>
                  <div>
                    <h2 className="card-title text-gray-800">{item.title}</h2>
                    <p className="text-sm text-gray-800">{item.desc}</p>
                  </div>
                </div>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  )
}
