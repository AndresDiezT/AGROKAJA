import { useState } from 'react'
import { Link } from 'react-router-dom'
import { useLoginUser } from '../../hooks/mutations/useAuthMutation'
import { useNavigate } from 'react-router-dom'
import { Leaf } from 'lucide-react'

export default function Login() {
  const [form, setForm] = useState({ email: '', password: '' })
  const [errors, setErrors] = useState({})

  const { mutate: login, isPending, isError } = useLoginUser()

  const navigate = useNavigate()

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value })
  }

  const handleSubmit = async (e) => {
    e.preventDefault()
    setErrors({})

    if (!form.email || !form.password) {
      const newErrors = {}
      if (!form.email) newErrors.email = "El correo es requerido"
      if (!form.password) newErrors.password = "La contraseña es requerida"
      setErrors(newErrors)
      return
    }

    login(
      form,
      {
        onError: (err) => {
          const data = err.response?.data
          const backendErrors = data?.errors || {}
          const normalizedErrors = {}

          // Normaliza los campos a minúsculas para que coincidan con los nombres de input
          for (const key in backendErrors) {
            normalizedErrors[key.toLowerCase()] = backendErrors[key][0]
          }

          setErrors(normalizedErrors)
        },
      }
    )
  }

  return (
    <div className='min-h-full flex items-center justify-center'>
      <form onSubmit={handleSubmit} className='bg-white p-8 rounded-xl shadow-md w-full max-w-md transition-all duration-500 hover:shadow-2xl'>
        <h2 className='text-2xl font-bold mb-6 text-emerald-700 text-center'>🌱 Iniciar sesión</h2>

        <div className='mb-4'>
          <label className='block font-semibold mb-2 text-emerald-800'>Correo</label>
          <input
            name='email'
            type='email'
            value={form.email}
            onChange={handleChange}
            className='w-full px-4 py-2 border border-lime-600 rounded-lg bg-lime-50 text-gray-800 focus:outline-none focus:ring-2 focus:ring-emerald-500 transition duration-300'
            required
          />
          {errors.email && (
            <p className="text-red-600 text-sm mt-1">{errors.email}</p>
          )}
        </div>

        <div className='mb-3'>
          <label className='block font-semibold mb-2 text-emerald-800'>Contraseña</label>
          <input
            name='password'
            type='password'
            value={form.password}
            onChange={handleChange}
            className='w-full px-4 py-2 border border-lime-600 rounded-lg bg-lime-50 text-gray-800 focus:outline-none focus:ring-2 focus:ring-emerald-500 transition duration-300'
            required
          />
          {errors.password && (
            <p className="text-red-600 text-sm mt-1">{errors.password}</p>
          )}
        </div>

        {errors.general && (
          <div className="text-red-700 p-3 rounded-lg bg-red-100 text-sm text-center mb-4 animate-pulse">
            {errors.general}
          </div>
        )}
        <Link to="/forgot-password" className="block text-sm text-teal-700 hover:underline mb-2 text-center">
          He olvidado mi contraseña
        </Link>

        <button
          type="submit"
          disabled={isPending}
          className="w-full bg-emerald-600 text-white font-semibold py-2 rounded-xl hover:bg-emerald-700 transition duration-300 cursor-pointer"
        >
          {isPending ? "Iniciando Sesión..." : "Iniciar Sesión"}
        </button>
        <Link to="/register" className="block mt-3 text-sm text-teal-700 hover:underline text-center">
          Quiero Crear Mi Cuenta
        </Link>
      </form>
    </div>
  )
}