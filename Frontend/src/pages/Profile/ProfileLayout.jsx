import { Outlet, NavLink } from "react-router-dom"
import {
    DollarSign, MapPinHouse, Settings, ShoppingBag, User
} from "lucide-react"
import { ProfileContext } from '../../context/ProfileContext'
import { useAuth } from '../../auth/AuthProvider'

export default function ProfileLayout() {
    const { profile, loading, refetch } = useAuth()

    if (loading || !profile) {
        return (
            <div className="min-h-screen bg-white flex flex-col lg:flex-row">
                <aside className="w-full lg:w-64 bg-[#323643] text-white flex flex-col pt-5">
                    <div className="p-6 border-b border-gray-600">
                        <div className="flex flex-col items-center space-y-3">
                            <div className="skeleton w-20 h-20 rounded-full"></div>
                            <div className="skeleton h-4 w-3/4"></div>
                            <div className="skeleton h-4 w-2/4"></div>
                        </div>
                    </div>
                    <nav className="flex flex-col gap-2 p-4">
                        {[...Array(5)].map((_, i) => (
                            <div key={i} className="skeleton h-10 w-full rounded-lg"></div>
                        ))}
                    </nav>
                </aside>
                <main className="flex-1 p-5 pt-10">
                    <div className="skeleton h-6 w-1/3 mb-4"></div>
                    <div className="skeleton h-4 w-2/3 mb-2"></div>
                    <div className="skeleton h-4 w-full mb-2"></div>
                    <div className="skeleton h-4 w-5/6"></div>
                </main>
            </div>
        )
    }

    return (
        <ProfileContext.Provider value={{ profile, refetch, loading}}>
            <div className="min-h-screen bg-white flex flex-col lg:flex-row">
                <aside className="w-full lg:w-64 bg-[#323643] text-white flex flex-col pt-5">
                    {/* Avatar y Info */}
                    <div className="p-6 border-b border-gray-600">
                        <div className="flex flex-col items-center">
                            <div className="avatar">
                                <div className="w-20 rounded-full ring ring-[#65C56F] ring-offset-base-100 ring-offset-2">
                                    <img src={profile?.profileImage} alt="Avatar" />
                                </div>
                            </div>
                            <h2 className="text-xl font-bold mt-3 text-center">{profile.username}</h2>
                            <p className="text-gray-300 text-center text-sm">{profile.email}</p>
                        </div>
                    </div>

                    {/* Barra lateral */}
                    <nav className="flex flex-wrap lg:flex-col flex-1 p-2 lg:p-4 gap-2">
                        <NavLink to="my-profile" className={({ isActive }) => `flex items-center gap-2 p-2 rounded-lg transition text-white ${isActive ? 'bg-[#65C56F]' : 'hover:bg-gray-700'}`}>
                            <User size={18} /> <span>Mi Perfil</span>
                        </NavLink>
                        <NavLink to="addresses" className={({ isActive }) => `flex items-center gap-2 p-2 rounded-lg transition text-white ${isActive ? 'bg-[#65C56F]' : 'hover:bg-gray-700'}`}>
                            <MapPinHouse size={18} /> <span>Direcciones</span>
                        </NavLink>
                        <NavLink to="orders" className={({ isActive }) => `flex items-center gap-2 p-2 rounded-lg transition text-white ${isActive ? 'bg-[#65C56F]' : 'hover:bg-gray-700'}`}>
                            <ShoppingBag size={18} /> <span>Mis Compras</span>
                        </NavLink>
                        <NavLink to="sales" className={({ isActive }) => `flex items-center gap-2 p-2 rounded-lg transition text-white ${isActive ? 'bg-[#65C56F]' : 'hover:bg-gray-700'}`}>
                            <DollarSign size={18} /> <span>Mis Ventas</span>
                        </NavLink>
                        <NavLink to="settings" className={({ isActive }) => `flex items-center gap-2 p-2 rounded-lg transition text-white ${isActive ? 'bg-[#65C56F]' : 'hover:bg-gray-700'}`}>
                            <Settings size={18} /> <span>Configuración</span>
                        </NavLink>
                    </nav>
                </aside>

                <main className="flex-1 p-5 pt-10">
                    <Outlet />
                </main>
            </div>
        </ProfileContext.Provider>
    )
}