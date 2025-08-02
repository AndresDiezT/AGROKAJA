import {
    User, UploadCloud, Package, Truck, Search, ShoppingCart, HomeIcon, ThumbsUp, Shield, Tag, Box,
    Heart
} from 'lucide-react'

function Home() {
    
    return (
        <main className='text-gray-800'>
            <section className='bg-green-50 py-16 px-6 md:px-20 text-center'>
                <h1 className='text-3xl md:text-5xl font-bold text-green-700 mb-4'>
                    Del campo a tu mesa, sin intermediarios
                </h1>
                <p className='text-lg md:text-xl max-w-2xl mx-auto mb-6'>
                    AgroKAJA conecta directamente a productores rurales con compradores urbanos. Frescura, justicia y tecnología al servicio del campo.
                </p>
                <div className='flex flex-col md:flex-row justify-center gap-4'>
                    <button className='bg-green-700 text-white px-6 py-3 rounded-full hover:bg-green-800 transition'>Explorar productos</button>
                    <button className='border border-green-700 text-green-700 px-6 py-3 rounded-full hover:bg-green-100 transition'>Vender mi cosecha</button>
                </div>
            </section>

            <section className='py-16 px-6 md:px-20 bg-white'>
                <h2 className='text-2xl md:text-3xl font-bold text-center text-green-700 mb-12'>¿Cómo funciona AgroKAJA?</h2>
                <div className='grid md:grid-cols-2 gap-12'>

                    <div className='bg-green-50 p-6 rounded-xl shadow space-y-6'>
                        <h3 className='text-xl font-semibold text-green-700 text-center'>Para campesinos</h3>
                        <div className='space-y-4'>
                            <div className='flex items-start gap-4'>
                                <User className='text-green-700 text-2xl mt-1' />
                                <p><strong>1. Regístrate:</strong> Crea tu perfil de manera sencilla.</p>
                            </div>
                            <div className='flex items-start gap-4'>
                                <UploadCloud className='text-green-700 text-2xl mt-1' />
                                <p><strong>2. Publica tu cosecha:</strong> Sube fotos y describe tus productos.</p>
                            </div>
                            <div className='flex items-start gap-4'>
                                <Package className='text-green-700 text-2xl mt-1' />
                                <p><strong>3. Recibe pedidos:</strong> Concreta ventas sin salir de tu finca.</p>
                            </div>
                            <div className='flex items-start gap-4'>
                                <Truck className='text-green-700 text-2xl mt-1' />
                                <p><strong>4. Entrega o coordinamos:</strong> Nos adaptamos a tus posibilidades.</p>
                            </div>
                        </div>
                    </div>

                    <div className='bg-green-50 p-6 rounded-xl shadow space-y-6'>
                        <h3 className='text-xl font-semibold text-green-700 text-center'>Para compradores</h3>
                        <div className='space-y-4'>
                            <div className='flex items-start gap-4'>
                                <Search className='text-green-700 text-2xl mt-1' />
                                <p><strong>1. Explora productos:</strong> Encuentra frutas, verduras y más.</p>
                            </div>
                            <div className='flex items-start gap-4'>
                                <ShoppingCart className='text-green-700 text-2xl mt-1' />
                                <p><strong>2. Compra directo:</strong> Apoya a productores sin intermediarios.</p>
                            </div>
                            <div className='flex items-start gap-4'>
                                <HomeIcon className='text-green-700 text-2xl mt-1' />
                                <p><strong>3. Recibe en casa:</strong> Entregamos frescura directamente.</p>
                            </div>
                            <div className='flex items-start gap-4'>
                                <ThumbsUp className='text-green-700 text-2xl mt-1' />
                                <p><strong>4. Califica y recomienda:</strong> Ayuda a otros con tu opinión.</p>
                            </div>
                        </div>
                    </div>
                </div>
            </section>


            <section className='py-16 px-6 md:px-20 bg-green-50'>
                <h2 className='text-2xl md:text-3xl font-bold text-center text-green-700 mb-12'>
                    ¿Por qué confiar en AgroKAJA?
                </h2>

                <div className='grid md:grid-cols-2 lg:grid-cols-4 gap-6'>
                    <div className='bg-white rounded-xl shadow-md p-6 text-center hover:shadow-lg transition'>
                        <div className='text-green-600 mb-4 text-3xl mx-auto'>
                            <Shield />
                        </div>
                        <h4 className='font-semibold text-lg mb-1'>Pagos seguros</h4>
                        <p className='text-sm text-gray-600'>Tus transacciones están protegidas de inicio a fin.</p>
                    </div>

                    <div className='bg-white rounded-xl shadow-md p-6 text-center hover:shadow-lg transition'>
                        <div className='text-green-600 mb-4 text-3xl mx-auto'>
                            <Tag />
                        </div>
                        <h4 className='font-semibold text-lg mb-1'>Precios justos</h4>
                        <p className='text-sm text-gray-600'>Sin sobrecostos. Directo del productor.</p>
                    </div>

                    <div className='bg-white rounded-xl shadow-md p-6 text-center hover:shadow-lg transition'>
                        <div className='text-green-600 mb-4 text-3xl mx-auto'>
                            <Box />
                        </div>
                        <h4 className='font-semibold text-lg mb-1'>Productos frescos</h4>
                        <p className='text-sm text-gray-600'>Recién cosechados y seleccionados.</p>
                    </div>

                    <div className='bg-white rounded-xl shadow-md p-6 text-center hover:shadow-lg transition'>
                        <div className='text-green-600 mb-4 text-3xl mx-auto'>
                            <Heart />
                        </div>
                        <h4 className='font-semibold text-lg mb-1'>Apoyo al campo</h4>
                        <p className='text-sm text-gray-600'>Cada compra apoya a un campesino colombiano.</p>
                    </div>
                </div>
            </section>

            <section className='py-16 px-6 md:px-20 bg-white'>
                <h2 className='text-2xl md:text-3xl font-bold text-center text-green-700 mb-12'>Productos destacados</h2>
                <div className='grid grid-cols-2 md:grid-cols-4 gap-6'>
                    {[1, 2, 3, 4].map((i) => (
                        <div key={i} className='border rounded-xl overflow-hidden shadow-sm hover:shadow-md transition'>
                            <div className='bg-gray-200 h-32'></div>
                            <div className='p-4'>
                                <h4 className='font-semibold text-lg'>Producto {i}</h4>
                                <p className='text-sm text-gray-600'>$ Precio</p>
                            </div>
                        </div>
                    ))}
                </div>
            </section>

            <section className='py-16 px-6 md:px-20 bg-green-50'>
                <h2 className='text-2xl md:text-3xl font-bold text-center text-green-700 mb-12'>Testimonios</h2>
                <div className='grid md:grid-cols-2 gap-10'>
                    <div className='bg-white p-6 rounded-xl shadow'>
                        <p className='italic mb-4'>“Ahora puedo vender sin salir de mi finca. AgroKAJA me da tranquilidad.”</p>
                        <h4 className='font-semibold'>Don Luis, Boyacá</h4>
                    </div>
                    <div className='bg-white p-6 rounded-xl shadow'>
                        <p className='italic mb-4'>“Es increíble recibir productos tan frescos y saber que ayudo al campo.”</p>
                        <h4 className='font-semibold'>Laura, Bogotá</h4>
                    </div>
                </div>
            </section>

            <section className='py-16 px-6 md:px-20 bg-green-700 text-white text-center'>
                <h2 className='text-3xl font-bold mb-6'>¿Y tú, qué estás esperando?</h2>
                <div className='flex flex-col md:flex-row justify-center gap-4'>
                    <button className='bg-white text-green-700 px-6 py-3 rounded-full hover:bg-gray-100 transition'>Soy campesino – Quiero vender</button>
                    <button className='border border-white px-6 py-3 rounded-full hover:bg-green-600 transition'>Soy comprador – Quiero apoyar el campo</button>
                </div>
            </section>
        </main>
    )
}

export default Home