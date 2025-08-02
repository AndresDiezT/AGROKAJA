import { useSearchParams } from 'react-router-dom'

export default function EmailSent() {
  const [searchParams] = useSearchParams()
  const email = searchParams.get('email')

  return (
    <div className="flex items-center justify-center min-h-screen">
      <div className="bg-white p-6 rounded shadow-md text-center max-w-md">
        <h1 className="text-2xl font-bold mb-4">¡Registro exitoso!</h1>
        <p className="mb-2">Hemos enviado un enlace de confirmación a:</p>
        <p className="font-semibold">{email}</p>
        <p className="mt-4 text-gray-600">
          Por favor revisa tu bandeja de entrada (y la carpeta de spam).
        </p>
      </div>
    </div>
  )
}
