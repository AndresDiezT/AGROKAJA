import { Link } from 'react-router-dom'

export default function AuthNavbar() {
    return (
        <nav className='w-full py-4 px-6 shadow-md bg-emerald-700'>
            <div className='max-w-container mx-auto flex justify-center'>
                <Link to='/' className='text-2xl font-bold text-white hover:opacity-80 transition'>
                    AgroKAJA
                </Link>
            </div>
        </nav>
    )
}
