import { useState } from "react";
import {
  FiMenu,
  FiSearch,
  FiShoppingCart,
  FiUser,
  FiX,
  FiChevronRight,
  FiChevronUp
} from "react-icons/fi";

const categories = [
  {
    name: "Granos y Cereales",
    subcategories: ["Maíz", "Trigo", "Arroz", "Cebada"],
  },
  {
    name: "Hortalizas",
    subcategories: ["Tomate", "Lechuga", "Zanahoria", "Cebolla"],
  },
  {
    name: "Frutas",
    subcategories: ["Plátano", "Mango", "Papaya", "Fresa"],
  },
  {
    name: "Leguminosas",
    subcategories: ["Frijol", "Soya", "Lentejas"],
  },
];

const accountItems = [
  { label: "Iniciar sesión", onClick: () => setMenuOpen(false) },
  { label: "Registrarse", onClick: () => setMenuOpen(false) },
];

const helpItems = [
  { label: "Atención al cliente", onClick: () => setMenuOpen(false) },
  { label: "Preguntas frecuentes", onClick: () => setMenuOpen(false) },
  { label: "Configuración", onClick: () => setMenuOpen(false) },
];

function CommonNavbar() {
  const [menuOpen, setMenuOpen] = useState(false);
  const [activeCategory, setActiveCategory] = useState(null);

  return (
    <>
      <nav className="bg-primary text-light font-base shadow-md fixed w-full z-50 p-sm">
        <div className="max-w-container mx-auto m-auto flex items-center justify-between px-md py-sm gap-5">
          <div className="flex items-center gap-4">
            <button
              onClick={() => setMenuOpen(true)}
              className="text-xl cursor-pointer"
            >
              <FiMenu />
            </button>
            <span className="text-lg font-bold">Agro KAJA</span>
          </div>

          <div className="relative flex-grow">
            <input
              type="text"
              placeholder="Buscar Productos..."
              className="w-full pl-10 pr-4 py-3 rounded-2xl border border-gray-300 shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent transition"
            />
            <div className="absolute left-4 top-1/2 transform -translate-y-1/2 text-gray-400">
              <FiSearch size={20} />
            </div>
          </div>

          <div className="hidden md:flex items-center gap-4">
            <button className="hover:underline cursor-pointer">Explorar</button>
            <button className="hover:underline cursor-pointer">Vender</button>
            <button className="hover:underline cursor-pointer font-bold">Iniciar sesión</button>
            <button className="btn-primary">Registrarse</button>
          </div>
          <button className="text-xl">
            <FiShoppingCart />
          </button>
        </div>
      </nav>

      {menuOpen && (
        <div
          className="fixed inset-0 bg-black/15 z-30"
          onClick={() => setMenuOpen(false)}
        ></div>
      )}

      <aside
        className={`fixed top-0 left-0 h-full w-64 bg-white text-gray-900 shadow-lg transform transition-transform duration-300 z-50 ${menuOpen ? "translate-x-0" : "-translate-x-full"
          }`}
        aria-label="Sidebar menu"
      >
        <div className="flex items-center justify-between px-6 py-4 border-b border-gray-300">
          <span className="text-xl font-semibold text-green-700">Agro KAJA</span>
          <button
            onClick={() => setMenuOpen(false)}
            className="text-2xl p-1 rounded hover:bg-gray-200 focus:outline-nonec cursor-pointer"
            aria-label="Cerrar menú"
          >
            <FiX />
          </button>
        </div>

        <nav className="flex flex-col h-[calc(100%-64px)] px-6 py-2">

          <div className="flex-1 overflow-y-auto space-y-4">

            <section>
              <h2 className="text-md font-semibold mb-3 text-green-700">Cuenta</h2>
              <ul className="space-y-2">
                {accountItems.map((item, idx) => (
                  <li key={idx}>
                    <button
                      className="w-full text-left font-medium hover:underline cursor-pointer"
                      onClick={item.onClick}
                    >
                      {item.label}
                    </button>
                  </li>
                ))}
              </ul>
            </section>
            <section>
              <h2 className="text-md font-semibold mb-3 text-green-700 ">Categorías</h2>
              <div className="relative w-full h-auto">
                <div
                  className={`absolute top-0 left-0 w-full transition-transform duration-300 ${activeCategory ? "-translate-x-full" : "translate-x-0"
                    }`}
                >
                  <ul className="space-y-2 pb-4">
                    <li>
                      <button className="w-full text-left font-medium hover:underline cursor-pointer" onClick={() => setMenuOpen(false)}>
                        Ver todas las categorías
                      </button>
                    </li>
                    {categories.map((cat) => (
                      <li key={cat.name}>
                        <button
                          className="w-full flex justify-between items-center text-left font-medium hover:underline cursor-pointer"
                          onClick={() => setActiveCategory(cat)}
                        >
                          {cat.name}
                          <FiChevronRight className="text-gray-500" />
                        </button>
                      </li>
                    ))}
                  </ul>
                </div>

                <div
                  className={`absolute top-0 left-0 w-full transition-transform duration-300 ${activeCategory ? "translate-x-0" : "translate-x-full"
                    }`}
                >
                  {activeCategory && (
                    <div>
                      <div className="flex justify-between items-center mb-2">
                        <h3 className="text-md font-semibold">{activeCategory.name}</h3>
                        <button
                          onClick={() => setActiveCategory(null)}
                          className="flex items-center text-sm text-gray-600 hover:underline cursor-pointer"
                        >
                          <FiChevronUp className="mr-1 rotate-90" />
                          Volver
                        </button>
                      </div>
                      <ul className="space-y-1">
                        {activeCategory.subcategories.map((sub, idx) => (
                          <li key={idx}>
                            <button className="w-full text-left hover:underline cursor-pointer" onClick={() => setMenuOpen(false)}>
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

          <section className="pt-4 border-t border-gray-200">
            <h2 className="text-md font-semibold mb-3 text-green-700">
              Ayuda & Configuración
            </h2>
            <ul className="space-y-2">
              {helpItems.map((item, idx) => (
                <li key={idx}>
                  <button
                    className="w-full text-left font-medium hover:underline cursor-pointer"
                    onClick={item.onClick}
                  >
                    {item.label}
                  </button>
                </li>
              ))}
            </ul>
          </section>
        </nav>
      </aside>

    </>
  );
}

export default CommonNavbar;
