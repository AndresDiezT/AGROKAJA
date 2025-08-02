export default function CommonFooter() {
    return (
        <footer className="bg-gray-900 text-gray-300 py-16">
            <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
                <div className="grid grid-cols-1 md:grid-cols-4 gap-12">

                    <div>
                        <h3 className="text-2xl font-bold text-indigo-500 mb-4">AGROKAJA</h3>
                        <p className="text-gray-400">
                            Innovando el futuro, ayudanos a apoyar el campo.
                        </p>
                    </div>

                    <div>
                        <h4 className="text-xl font-semibold mb-4">Enlaces</h4>
                        <ul className="space-y-2">
                            <li><a href="/" className="hover:text-indigo-400 transition">Inicio</a></li>
                            <li><a href="/" className="hover:text-indigo-400 transition">Explorar</a></li>
                            <li><a href="/about" className="hover:text-indigo-400 transition">Acerca de Nosotros</a></li>
                            <li><a href="/services" className="hover:text-indigo-400 transition">Servicios</a></li>
                            <li><a href="/contact" className="hover:text-indigo-400 transition">Formulario de Contacto</a></li>
                        </ul>
                    </div>

                    <div>
                        <h4 className="text-xl font-semibold mb-4">Soporte</h4>
                        <ul className="space-y-2">
                            <li><a href="/faq" className="hover:text-indigo-400 transition">Preguntas frecuentes</a></li>
                            <li><a href="/help" className="hover:text-indigo-400 transition">Centro de ayuda</a></li>
                            <li><a href="/privacy" className="hover:text-indigo-400 transition">Política de privacidad</a></li>
                            <li><a href="/terms" className="hover:text-indigo-400 transition">Términos de servicio</a></li>
                        </ul>
                    </div>

                    <div>
                        <h4 className="text-xl font-semibold mb-4">Contacto</h4>
                        <p className="mb-4">contacto@agrokaja.com</p>
                        <p className="mb-4">+57 3204985357</p>
                        <div className="flex space-x-4">
                            <a href="#" aria-label="Facebook" className="hover:text-indigo-400 transition">
                                <svg className="w-6 h-6 fill-current" viewBox="0 0 24 24"><path d="M22 12a10 10 0 10-11.64 9.83v-6.95h-2.5v-2.88h2.5v-2.2c0-2.47 1.47-3.83 3.72-3.83 1.08 0 2.2.2 2.2.2v2.42h-1.24c-1.22 0-1.6.77-1.6 1.56v1.85h2.74l-.44 2.88h-2.3v6.95A10 10 0 0022 12z" /></svg>
                            </a>
                            <a href="#" aria-label="Twitter" className="hover:text-indigo-400 transition">
                                <svg className="w-6 h-6 fill-current" viewBox="0 0 24 24"><path d="M23 3a10.9 10.9 0 01-3.14.86 5.48 5.48 0 002.4-3.02 10.72 10.72 0 01-3.48 1.32A5.38 5.38 0 0016.29 2c-2.92 0-5.28 2.36-5.28 5.28 0 .41.05.81.14 1.19-4.39-.22-8.3-2.33-10.91-5.52a5.2 5.2 0 00-.72 2.65c0 1.83.93 3.45 2.33 4.4a5.27 5.27 0 01-2.39-.65v.07c0 2.55 1.82 4.68 4.24 5.17a5.27 5.27 0 01-2.38.09 5.28 5.28 0 004.93 3.67A10.78 10.78 0 012 19.54 15.3 15.3 0 008.29 21c9.44 0 14.6-7.83 14.6-14.6 0-.22 0-.43-.02-.64A10.4 10.4 0 0023 3z" /></svg>
                            </a>
                            <a href="#" aria-label="LinkedIn" className="hover:text-indigo-400 transition">
                                <svg className="w-6 h-6 fill-current" viewBox="0 0 24 24"><path d="M16 8a6 6 0 016 6v7h-4v-7a2 2 0 00-4 0v7h-4v-7a6 6 0 016-6zm-8 0h4v12H8V8zm-2 0a2 2 0 11.001 3.999A2 2 0 016 8z" /></svg>
                            </a>
                        </div>
                    </div>
                </div>

                <div className="mt-12 border-t border-gray-700 pt-6 text-sm text-gray-500 text-center">
                    &copy {new Date().getFullYear()} AGROKAJA. Todos los derechos reservados.
                </div>
            </div>
        </footer>
    )
}