import { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import {
  Menu,
  Search,
  ShoppingCart,
  X,
  ChevronRight,
  ChevronLeft,
  User,
  LogOut,
  MapPin,
  Package,
  BadgeDollarSign
} from 'lucide-react'
import { useAuth } from '../../auth/AuthProvider'

const categories = [
  {
    name: 'Granos y Cereales',
    subcategories: ['Maíz', 'Trigo', 'Arroz', 'Cebada'],
  },
  {
    name: 'Hortalizas',
    subcategories: ['Tomate', 'Lechuga', 'Zanahoria', 'Cebolla'],
  },
  {
    name: 'Frutas',
    subcategories: ['Plátano', 'Mango', 'Papaya', 'Fresa'],
  },
  {
    name: 'Leguminosas',
    subcategories: ['Frijol', 'Soya', 'Lentejas'],
  },
]

export default function CommonNavbar() {
  const { isAuthenticated, user, logout, profile } = useAuth()

  const [menuOpen, setMenuOpen] = useState(false)
  const [activeCategory, setActiveCategory] = useState(null)

  const navigation = useNavigate()

  const handleLogout = () => {
    logout()
    navigate("/login")
  }

  return (
    <>
      <nav className="navbar bg-[#65C56F] shadow-md fixed w-full z-50 px-4">
        <div className="flex w-full items-center justify-between gap-4 text-white">

          {/* Menú + Logo */}
          <div className="flex items-center gap-4 min-w-max">
            <button onClick={() => setMenuOpen(true)} className="cursor-pointer">
              <Menu />
            </button>
            <Link to="/" className="font-bold text-xl text-white hidden sm:inline">
              Agro KAJA
            </Link>
          </div>

          {/* Barra de búsqueda */}
          <label className="input flex flex-grow items-center bg-white rounded-full px-3 py-2 gap-2">
            <Search size={20} className="text-gray-500" />
            <input
              type="text"
              placeholder="Buscar Productos..."
              className="grow w-full text-black outline-none bg-transparent"
            />
          </label>

          {/* Grupo 1: Links ocultos en móviles */}
          <div className="hidden md:flex items-center gap-4 min-w-max">
            <Link to="explore" className="text-white text-xl hover:underline">Explorar</Link>
            <Link to="sell" className="text-white text-xl hover:underline">Vender</Link>
            {!isAuthenticated && (
              <>
                <button className="text-white text-xl hover:underline cursor-pointer" onClick={() => navigation('/login')}>
                  Iniciar sesión
                </button>
                <button className="text-white text-xl hover:underline cursor-pointer" onClick={() => navigation('/register')}>
                  Registrarse
                </button>
              </>
            )}
          </div>

          {/* Grupo 2: Siempre visible - Carrito + Perfil */}
          <div className="flex items-center gap-4 min-w-max">
            <Link to="cart" className="indicator">
              <button className="btn btn-circle btn-ghost">
                <ShoppingCart className="w-6 h-6 text-white" />
              </button>
              <span className="indicator-item badge badge-sm bg-red-500 text-white border-none">
                3
              </span>
            </Link>

            {isAuthenticated && (
              <div className="dropdown dropdown-end">
                <label tabIndex={0} className="btn btn-ghost btn-circle avatar">
                  <div className="w-10 rounded-full">
                    <img
                      src={profile?.profileImage}
                      alt="avatar"
                    />
                  </div>
                </label>
                <ul
                  tabIndex={0}
                  className="mt-3 z-[1] p-2 shadow menu menu-sm dropdown-content bg-white text-black rounded-box w-60"
                >
                  <li>
                    <button
                      onClick={() => navigation('/profile/my-profile')}
                      className="flex items-center gap-2 text-lg"
                    >
                      <User size={18} /> Mi perfil
                    </button>
                  </li>
                  <li>
                    <button
                      onClick={() => navigation('/profile/addresses')}
                      className="flex items-center gap-2 text-lg"
                    >
                      <MapPin size={18} /> Mis Direcciones
                    </button>
                  </li>
                  <li>
                    <button
                      onClick={() => navigation('/my-products')}
                      className="flex items-center gap-2 text-lg"
                    >
                      <Package size={18} /> Mis Productos
                    </button>
                  </li>
                  <li>
                    <button
                      onClick={() => navigation('/sales')}
                      className="flex items-center gap-2 text-lg"
                    >
                      <BadgeDollarSign size={18} /> Mis Ventas
                    </button>
                  </li>
                  <li>
                    <button
                      onClick={handleLogout}
                      className="flex items-center gap-2 text-lg text-red-500"
                    >
                      <LogOut size={18} /> Cerrar sesión
                    </button>
                  </li>
                </ul>
              </div>
            )}
          </div>
        </div>
      </nav>

      {menuOpen && (
        <div
          className='fixed inset-0 bg-black/15 z-30'
          onClick={() => setMenuOpen(false)}
        ></div>
      )
      }

      {/* barra lateral */}
      <aside
        className={`fixed top-0 left-0 h-full w-64 bg-white text-gray-900 shadow-lg transform transition-transform duration-300 z-50 ${menuOpen ? 'translate-x-0' : '-translate-x-full'
          }`}
        aria-label='Sidebar menu'
      >
        <div className='flex items-center justify-between px-6 py-4 border-b border-gray-300'>
          <span className='text-xl font-semibold text-green-700'>Agro KAJA</span>
          <button
            onClick={() => setMenuOpen(false)}
            className='text-2xl p-1 rounded hover:bg-gray-200 focus:outline-nonec cursor-pointer'
            aria-label='Cerrar menú'
          >
            <X />
          </button>
        </div>

        <nav className='flex flex-col h-[calc(100%-64px)] px-6 py-2'>

          <div className='flex-1 overflow-y-auto space-y-4'>

            <section>
              <h2 className='text-lg font-semibold mb-3 text-green-700'>Cuenta</h2>
              <ul className='space-y-2'>
                {isAuthenticated ? (
                  <>
                    <button className='w-full flex justify-between items-center text-left font-medium hover:underline cursor-pointer' onClick={() => navigation('/addresses')}>
                      Direcciones
                      <ChevronRight className='text-gray-500' />
                    </button>
                    {user.roles.includes("Admin") && (
                      <button className='w-full flex justify-between items-center text-left font-medium hover:underline cursor-pointer' onClick={() => navigation('/staff/dashboard')}>
                        Dashboard
                        <ChevronRight className='text-gray-500' />
                      </button>
                    )}
                  </>
                ) : (
                  <>
                    <li>
                      <button className='w-full flex justify-between items-center text-left font-medium hover:underline cursor-pointer' onClick={() => navigation('/login')}>
                        Iniciar sesión
                        <ChevronRight className='text-gray-500' />
                      </button>
                    </li>
                    <li>
                      <button className='w-full flex justify-between items-center text-left font-medium hover:underline cursor-pointer' onClick={() => navigation('/register')}>
                        Registrars
                        <ChevronRight className='text-gray-500' />
                      </button>
                    </li>
                  </>
                )}
              </ul>
            </section>
            <section>
              <h2 className='text-lg font-semibold mb-3 text-green-700 '>Categorías</h2>
              <div className='relative w-full h-auto'>
                <div
                  className={`absolute top-0 left-0 w-full transition-transform duration-300 ${activeCategory ? '-translate-x-full' : 'translate-x-0'
                    }`}
                >
                  <ul className='space-y-2 pb-4'>
                    <li>
                      <button className='w-full text-left font-medium hover:underline cursor-pointer' onClick={() => setMenuOpen(false)}>
                        Ver todas las categorías
                      </button>
                    </li>
                    {categories.map((cat) => (
                      <li key={cat.name}>
                        <button
                          className='w-full flex justify-between items-center text-left font-medium hover:underline cursor-pointer'
                          onClick={() => setActiveCategory(cat)}
                        >
                          {cat.name}
                          <ChevronRight className='text-gray-500' />
                        </button>
                      </li>
                    ))}
                  </ul>
                </div>

                <div
                  className={`absolute top-0 left-0 w-full transition-transform duration-300 ${activeCategory ? 'translate-x-0' : 'translate-x-full'
                    }`}
                >
                  {activeCategory && (
                    <div>
                      <div className='flex justify-between items-center mb-2'>
                        <h3 className='text-md font-semibold'>{activeCategory.name}</h3>
                        <button
                          onClick={() => setActiveCategory(null)}
                          className='flex items-center text-sm text-gray-600 hover:underline cursor-pointer'
                        >
                          <ChevronLeft className='mr-1' />
                          Volver
                        </button>
                      </div>
                      <ul className='space-y-1'>
                        {activeCategory.subcategories.map((sub, idx) => (
                          <li key={idx}>
                            <button className='w-full text-left hover:underline cursor-pointer' onClick={() => setMenuOpen(false)}>
                              - {sub}
                            </button>
                          </li>
                        ))}
                      </ul>
                    </div>
                  )}
                </div>
              </div>
            </section>
          </div>

          <section className='pt-4 border-t border-gray-200'>
            <h2 className='text-lg font-semibold mb-3 text-green-700'>
              Ayuda & Configuración
            </h2>
            <ul className='space-y-2'>
              <li>
                <button className='w-full text-left font-medium hover:underline cursor-pointer'>Atención al cliente</button>
              </li>
              <li>
                <button className='w-full text-left font-medium hover:underline cursor-pointer'>Preguntas frecuentes</button>
              </li>
              {isAuthenticated ? (
                <>
                  <li><button onClick={handleLogout} className="w-full text-left font-medium hover:underline cursor-pointer text-red-500">Cerrar Sesión</button></li>
                </>
              ) : (
                <></>
              )}
            </ul>
          </section>
        </nav>
      </aside>

    </>
  )
}
