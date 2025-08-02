import { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { useTypesDocument } from '../../hooks/queries/useTypeDocument'
import { useRegisterUser } from '../../hooks/mutations/useAuthMutation'

export default function Register() {
  const { data: typesDocument = [] } = useTypesDocument()
  const { mutate: register, isPending } = useRegisterUser()

  const [step, setStep] = useState(1)
  const [form, setForm] = useState({
    document: '',
    username: '',
    email: '',
    password: '',
    confirmPassword: '',
    firstName: '',
    lastName: '',
    phoneNumber: '',
    birthDate: '',
    idRole: 0,
    idTypeDocument: 0
  })
  const [error, setError] = useState([])
  const navigate = useNavigate()

  const handleChange = (e) => {
    const { name, value } = e.target
    setForm({
      ...form,
      [name]: name === 'idTypeDocument' ? Number(value) : value
    })
  }

  const handleSelectRole = (idRole) => {
    setForm({ ...form, idRole })
    setStep(2)
  }

  const handleNext = async () => {
    setError('')
    if (step === 2) {
      if (!form.username || !form.email || !form.password || !form.confirmPassword) {
        setError('Completa todos los campos')
        return
      }
      if (form.password !== form.confirmPassword) {
        setError('Las contraseñas no coinciden')
        return
      }
      setStep(3)
    } else if (step === 3) {
      if (!form.firstName || !form.lastName || !form.birthDate || !form.idTypeDocument || !form.document) {
        setError('Completa todos los campos')
        return
      }
      setStep(4)
    } else if (step === 4) {
      if (!form.phoneNumber) {
        setError('Debes ingresar un número de teléfono')
        return
      }
      handleSubmit()
    }
  }

  const handleBack = () => {
    setError('')
    if (step > 1) setStep(step - 1)
  }

  const handleSubmit = () => {
    setError('')
    register(form, {
      onSuccess: () => {
        navigate(`/email-sent?email=${form.email}`)
      },
      onError: (err) => {
        console.error('Error al registrar usuario:', err)
        setError('Error al registrar usuario')
      },
    })
  }

  return (
    <div className="min-h-full flex items-center justify-center mt-5">
      <div className="bg-white text-emerald-800 p-8 rounded-xl shadow-md w-full max-w-md">
        <h2 className="text-2xl font-bold mb-6 text-center">Registro</h2>
        {step === 1 && (
          <div className="space-y-4">
            <p className="text-center mb-4">Cuentanos un poco de ti</p>
            <button
              onClick={() => handleSelectRole(1)}
              className="w-full px-4 py-2 bg-emerald-600 text-white rounded-xl hover:bg-emerald-700 shadow-md transition duration-300 cursor-pointer"
            >
              🌾 Soy campesino
            </button>
            <button
              onClick={() => handleSelectRole(2)}
              className="w-full px-4 py-2 bg-blue-500 text-white rounded-xl hover:bg-blue-600 shadow-md transition duration-300 cursor-pointer"
            >
              🛒 Soy comprador
            </button>
            <Link to="/login" className="block mt-3 text-sm text-teal-700 hover:underline text-center">
              Ya tengo cuenta
            </Link>
          </div>
        )}

        {step === 2 && (
          <>
            <div className="mb-4">
              <label className="block mb-1">Nombre de usuario</label>
              <input
                type="text"
                name="username"
                value={form.username}
                onChange={handleChange}
                className="w-full px-4 py-2 border border-lime-600 rounded-lg bg-lime-50 text-gray-800 focus:outline-none focus:ring-2 focus:ring-emerald-500 transition duration-300"
              />
            </div>
            <div className="mb-4">
              <label className="block mb-1">Correo electrónico</label>
              <input
                type="email"
                name="email"
                value={form.email}
                onChange={handleChange}
                className="w-full px-4 py-2 border border-lime-600 rounded-lg bg-lime-50 text-gray-800 focus:outline-none focus:ring-2 focus:ring-emerald-500 transition duration-300"
              />
            </div>
            <div className="mb-4">
              <label className="block mb-1">Contraseña</label>
              <input
                type="password"
                name="password"
                value={form.password}
                onChange={handleChange}
                className="w-full px-4 py-2 border border-lime-600 rounded-lg bg-lime-50 text-gray-800 focus:outline-none focus:ring-2 focus:ring-emerald-500 transition duration-300"
              />
            </div>
            <div className="mb-4">
              <label className="block mb-1">Confirmar contraseña</label>
              <input
                type="password"
                name="confirmPassword"
                value={form.confirmPassword}
                onChange={handleChange}
                className="w-full px-4 py-2 border border-lime-600 rounded-lg bg-lime-50 text-gray-800 focus:outline-none focus:ring-2 focus:ring-emerald-500 transition duration-300"
              />
            </div>
          </>
        )}

        {step === 3 && (
          <>
            <div className="mb-4">
              <label className="block mb-1">Tipo de documento</label>
              <select
                name="idTypeDocument"
                value={form.idTypeDocument}
                onChange={handleChange}
                className="w-full px-4 py-2 border border-lime-600 rounded-lg bg-lime-50 text-gray-800 focus:outline-none focus:ring-2 focus:ring-emerald-500 transition duration-300"
              >
                <option disabled value={0}>Seleccione un tipo</option>
                {typesDocument.map((typeDocument) => (
                  <option
                    key={typeDocument.idTypeDocument}
                    value={typeDocument.idTypeDocument}
                  >
                    {typeDocument.nameTypeDocument}
                  </option>
                ))}
              </select>
            </div>
            <div className="mb-4">
              <label className="block mb-1">Número de documento</label>
              <input
                type="text"
                name="document"
                value={form.document}
                onChange={handleChange}
                className="w-full px-4 py-2 border border-lime-600 rounded-lg bg-lime-50 text-gray-800 focus:outline-none focus:ring-2 focus:ring-emerald-500 transition duration-300"
              />
            </div>
            <div className="mb-4">
              <label className="block mb-1">Nombre</label>
              <input
                type="text"
                name="firstName"
                value={form.firstName}
                onChange={handleChange}
                className="w-full px-4 py-2 border border-lime-600 rounded-lg bg-lime-50 text-gray-800 focus:outline-none focus:ring-2 focus:ring-emerald-500 transition duration-300"
              />
            </div>
            <div className="mb-4">
              <label className="block mb-1">Apellido</label>
              <input
                type="text"
                name="lastName"
                value={form.lastName}
                onChange={handleChange}
                className="w-full px-4 py-2 border border-lime-600 rounded-lg bg-lime-50 text-gray-800 focus:outline-none focus:ring-2 focus:ring-emerald-500 transition duration-300"
              />
            </div>
            <div className="mb-4">
              <label className="block mb-1">Fecha de nacimiento</label>
              <input
                type="date"
                name="birthDate"
                value={form.birthDate}
                onChange={handleChange}
                max={new Date().toISOString().split("T")[0]}
                className="w-full px-4 py-2 border border-lime-600 rounded-lg bg-lime-50 text-gray-800 focus:outline-none focus:ring-2 focus:ring-emerald-500 transition duration-300"
              />
            </div>
          </>
        )}

        {step === 4 && (
          <div className="mb-4">
            <label className="block mb-1">Número de teléfono</label>
            <input
              type="text"
              name="phoneNumber"
              value={form.phoneNumber}
              onChange={handleChange}
              className="w-full px-4 py-2 border border-lime-600 rounded-lg bg-lime-50 text-gray-800 focus:outline-none focus:ring-2 focus:ring-emerald-500 transition duration-300"
            />
          </div>
        )}

        {error && <div className="text-red-600 text-sm mb-4">{error}</div>}

        <div className="flex justify-between mt-4">
          {step > 1 && (
            <button
              type="button"
              onClick={handleBack}
              className="px-4 py-2 bg-gray-300 rounded-lg hover:bg-gray-400 cursor-pointer"
            >
              Atrás
            </button>
          )}
          {step > 1 && (
            <button
              type="button"
              onClick={handleNext}
              className="px-4 py-2 bg-green-600 text-white rounded-lg hover:bg-green-700 cursor-pointer"
              disabled={isPending}
            >
              {isPending
                ? step === 4
                  ? 'Registrando...'
                  : 'Cargando...'
                : step === 4
                  ? 'Registrar'
                  : 'Siguiente'}
            </button>
          )}
        </div>
      </div>
    </div>
  )
}