import { useSearchParams } from 'react-router-dom'
import { useConfirmEmailUser } from '../../hooks/mutations/useAuthMutation'

export default function VerifyEmail() {
    const [searchParams] = useSearchParams()
    const token = searchParams.get('token')

    const { isPending, isError, error, isSuccess } = useConfirmEmailUser(token)

    return (
        <div className="flex items-center justify-center min-h-screen">
            <div className="bg-white p-6 rounded shadow-md text-center max-w-md">
                {isPending && (
                    <>
                        <h1 className="text-2xl font-bold mb-4">Validando...</h1>
                        <p className="text-gray-600">Por favor espera.</p>
                    </>
                )}

                <>
                    <h1 className="text-2xl font-bold text-green-600 mb-4">¡Correo confirmado!</h1>
                    <p className="text-gray-700 mb-4">Ahora puedes iniciar sesión.</p>
                    <a
                        href="/login"
                        className="inline-block bg-green-600 text-white px-4 py-2 rounded hover:bg-green-700"
                    >
                        Iniciar sesión
                    </a>
                </>

                {isError && (
                    <>
                        <h1 className="text-2xl font-bold text-red-600 mb-4">Error</h1>
                        <p className="text-gray-700 mb-4">
                            {error?.response?.data?.error ||
                                error?.message ||
                                'El link de confirmación no es válido o ya fue usado.'}
                        </p>
                        <a
                            href="/register"
                            className="inline-block bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
                        >
                            Envíar otro link de confirmación
                        </a>
                    </>
                )}
            </div>
        </div>
    )
}
