import { Link } from "react-router-dom"


function NotFound() {

    return (
        <div className='grid min-h-full place-items-center px-6 py-24 sm:py-32 lg:px-8 bg-white'>
            <div className='text-center'>
                <p className='text-base font-semibold text-primary'>Página no encontrada</p>
                <h1 className='mt-4 text-5xl font-semibold tracking-tight text-balance text-gray-900 sm:text-7xl'>404</h1>
                <p className='mt-6 text-lg font-medium text-pretty text-gray-500 sm:text-xl/8'>Lo sentimos, no encontramos la página que estas buscando</p>
                <div className='mt-10 flex items-center justify-center gap-x-6'>
                    <Link to='/' viewTransition className='rounded-md bg-primary px-3.5 py-2.5 text-sm font-semibold text-white shadow-xs hover:bg-indigo-500 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600'>Volver al inicio</Link>
                    <Link to='/contact' className='text-sm font-semibold text-gray-900'>Contactar a soporte</Link>
                </div>
            </div>
        </div>
    )
}

export default NotFound