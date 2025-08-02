import { Outlet } from 'react-router-dom'
import AuthNavbar from '../components/Menus/AuthNavbar'

export default function AuthLayout() {
    return (
        <div className='h-screen flex flex-col overflow-hidden z-1000 bg-emerald-700'>
            <AuthNavbar />
            <div className='absolute top-16 left-0 w-full h-1/2 bg-cover bg-center z-0 '
                style={{ backgroundImage: "url('/background_auth.png')" }}> 
            </div>
            <main className='flex-1 overflow-auto z-1000'>
                <Outlet />
            </main>
        </div>
    )
}