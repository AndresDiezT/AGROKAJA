import { useState } from "react"
import {
    Menu,
    User,
    Settings,
    LifeBuoy,
    Briefcase,
    Home,
    Bell,
    ChevronDown,
    Plus,
    ClipboardList,
    Database,
} from "lucide-react"
import { Outlet, useNavigate } from "react-router"
import { useAuth } from "../../auth/AuthProvider"

const menuItems = [
    { id: "dashboard", label: "Dashboard", icon: Home, route: "/staff/dashboard" },
    {
        id: "Productos",
        label: "Productos",
        icon: Briefcase,
        children: [
            {
                id: "manage-products",
                label: "Gestión Productos",
                icon: ClipboardList,
                route: "/staff/products/manage"
            },
            {
                id: "to-review",
                label: "Por Revisar",
                icon: ClipboardList,
                route: "/staff/products/manage"
            }
        ]
    },
    {
        id: "customers",
        label: "Clientes",
        icon: User,
        children: [
            {
                id: "list-customers",
                label: "Gestión clientes",
                icon: ClipboardList,
                route: "/staff/customers/list"
            }
        ]
    },
    {
        id: "employees",
        label: "Empleados",
        icon: User,
        children: [
            {
                id: "add-employee",
                label: "Agregar empleado",
                icon: Plus,
                route: "/staff/employees/add"
            },
            {
                id: "manage-employees",
                label: "Gestión empleados",
                icon: ClipboardList,
                route: "/staff/employees/list"
            }
        ]
    },
    {
        id: "settings",
        label: "Configuración",
        icon: Settings,
        children: [
            {
                id: "manage-roles",
                label: "Roles",
                icon: Database,
                route: "/staff/settings/roles",
            }
        ]
    },
    {
        id: "locations",
        label: "Localizaciones",
        icon: Settings,
        children: [
            {
                id: "list-countries",
                label: "Países",
                icon: ClipboardList,
                route: "/staff/locations/countries"
            },
            {
                id: "list-departments",
                label: "Departamentos",
                icon: ClipboardList,
                route: "/staff/locations/departments"
            },
            {
                id: "list-cities",
                label: "Ciudades",
                icon: ClipboardList,
                route: "/staff/locations/cities"
            },
        ]
    },
]

export default function EmployeeSidebar({ theme, setTheme }) {
    const [sidebarOpen, setSidebarOpen] = useState(false)
    const [activeMenu, setActiveMenu] = useState("dashboard")
    const [expandedMenus, setExpandedMenus] = useState([])
    const [userOpen, setUserOpen] = useState(false)
    const { logout } = useAuth()
    const navigate = useNavigate()

    const toggleSubmenu = (id) => {
        setExpandedMenus((prev) =>
            prev.includes(id) ? prev.filter((menu) => menu !== id) : [...prev, id]
        )
    }

    const handleLogout = () => {
        logout()
        navigate("/login")
    }

    const handleNavigation = (id, route) => {
        setActiveMenu(id)
        if (route) navigate(route)
        setSidebarOpen(false)
    }

    return (
        <div className="flex h-screen">
            <aside
                className={`fixed inset-y-0 left-0 bg-base-100 w-80 transform transition-transform duration-300 ease-in-out z-40
                ${sidebarOpen ? "translate-x-0" : "-translate-x-full"} md:flex-shrink-0`}
            >
                <div className="flex flex-col h-full">
                    <nav className="flex-1 overflow-y-auto px-4 py-6 space-y-2 pt-20">
                        {menuItems.map(({ id, label, icon: Icon, children, route }) => (
                            <div key={id}>
                                <button
                                    onClick={() => {
                                        if (children) {
                                            toggleSubmenu(id)
                                        } else {
                                            handleNavigation(id, route)
                                        }
                                    }}
                                    className={`flex items-center justify-between w-full px-4 py-3 rounded-lg transition cursor-pointer
                                        ${activeMenu === id
                                            ? "bg-base-300 font-semibold"
                                            : "hover:bg-base-300"
                                        }`}
                                >
                                    <div className="flex items-center">
                                        <Icon className="w-5 h-5 mr-3" />
                                        {label}
                                    </div>
                                    {children && (
                                        <ChevronDown
                                            className={`w-4 h-4 ml-auto transform transition-transform duration-200 ${expandedMenus.includes(id) ? "rotate-180" : ""
                                                }`}
                                        />
                                    )}
                                </button>
                                {children && expandedMenus.includes(id) && (
                                    <div className="ml-8 mt-1 space-y-1">
                                        {children.map(({ id: childId, label, icon: ChildIcon, route }) => (
                                            <button
                                                key={childId}
                                                onClick={() => handleNavigation(childId, route)}
                                                className="flex items-center w-full px-3 py-2 text-sm rounded-lg hover:bg-base-300 transition cursor-pointer"
                                            >
                                                <ChildIcon className="w-4 h-4 mr-2" />
                                                {label}
                                            </button>
                                        ))}
                                    </div>
                                )}
                            </div>
                        ))}
                    </nav>

                    <div className="px-6 py-4 border-t bg-base-300">
                        <button onClick={() => navigate("/")} className="flex items-center w-full px-4 py-3 rounded-lg hover:bg-base-100 transition cursor-pointer sm:hidden">
                            <Home className="w-5 h-5 mr-3" />
                            Inicio
                        </button>
                        <button className="flex items-center w-full px-4 py-3 rounded-lg hover:bg-base-100 transition cursor-pointer">
                            <LifeBuoy className="w-5 h-5 mr-3" />
                            Soporte
                        </button>
                        <select
                            value={theme}
                            onChange={(e) => setTheme(e.target.value)}
                            className="select w-full"
                        >
                            <option value="light">Claro</option>
                            <option value="dark">Oscuro</option>
                        </select>
                    </div>
                </div>
            </aside>

            {sidebarOpen && (
                <div
                    className="fixed inset-0 bg-black/10 z-30"
                    onClick={() => setSidebarOpen(false)}
                    aria-hidden="true"
                />
            )}

            <div className="flex flex-col flex-1">
                <header className="bg-base-200 fixed top-0 left-0 right-0 p-4 shadow-md z-40 flex items-center justify-between">
                    <div className="flex gap-4 items-center">
                        <button
                            className="focus:outline-none cursor-pointer"
                            onClick={() => setSidebarOpen(!sidebarOpen)}
                            aria-label="Abrir menú"
                        >
                            <Menu size={24} />
                        </button>
                        <h2 className="text-lg font-semibold sm:inline hidden">
                            Panel de Administración
                        </h2>
                        <button
                            onClick={() => navigate("/")}
                            className="items-center gap-1 focus:outline-none cursor-pointer sm:inline hidden"
                        >
                            <span>Volver al Inicio</span>
                        </button>
                    </div>
                    <div className="flex gap-4 items-center">
                        <button className="relative p-2 focus:outline-none cursor-pointer">
                            <Bell size={24} />
                            <span className="absolute top-0 right-0 bg-red-500 text-xs font-bold rounded-full px-2">
                                3
                            </span>
                        </button>
                        <div className="relative">
                            <button
                                className="flex items-center gap-1 focus:outline-none cursor-pointer"
                                onClick={() => setUserOpen(!userOpen)}
                            >
                                <span>Andres Diez</span>
                                <ChevronDown
                                    size={18}
                                    className={` transition ${userOpen ? "rotate-180" : ""}`}
                                />
                            </button>
                            {userOpen && (
                                <div className="absolute right-0 mt-2 w-48 bg-base-200 rounded-lg shadow-lg p-2 border">
                                    <a href="/profile" className="block px-4 py-2 hover:bg-base-300 rounded">
                                        Perfil
                                    </a>
                                    <a href="/settings" className="block px-4 py-2 hover:bg-base-300 rounded">
                                        Configuración
                                    </a>
                                    <button onClick={handleLogout} className="block w-full text-left px-4 py-2 hover:bg-base-300 rounded text-red-400">Cerrar Sesión</button>
                                </div>
                            )}
                        </div>
                    </div>
                </header>

                <main className="flex-1 p-6 bg-base-100 mt-16 overflow-auto">
                    <Outlet />
                </main>
            </div>
        </div>
    )
}