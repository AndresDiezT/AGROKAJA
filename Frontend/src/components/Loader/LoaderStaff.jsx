import { motion } from "framer-motion"
import { Leaf, Tractor, Wheat } from "lucide-react"

const loadingMessages = [
    "Organizando el mercado agrícola...",
    "Cosechando registros de usuarios...",
    "Verificando datos de productos...",
    "Actualizando rutas de distribución...",
    "Preparando informes para los cultivos...",
    "Sincronizando transacciones del agro...",
    "Conectando con productores rurales...",
    "Cargando panel de administración...",
    "Verificando estado de las órdenes...",
    "Analizando movimientos del mercado...",
]

const randomMessage = loadingMessages[Math.floor(Math.random() * loadingMessages.length)]

export default function LoaderStaff() {
    return (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-green-900/80 backdrop-blur-sm">
            <motion.div
                initial={{ scale: 0.8, opacity: 0 }}
                animate={{ scale: 1, opacity: 1 }}
                transition={{ duration: 0.4 }}
                className="bg-base-100 p-8 rounded-xl shadow-lg flex flex-col items-center gap-4"
            >
                {/* Icono agrícola animado */}
                <motion.div
                    animate={{ rotate: 360 }}
                    transition={{ repeat: Infinity, duration: 2, ease: "linear" }}
                    className="text-green-500"
                >
                    <Wheat size={48} strokeWidth={2.5} />
                </motion.div>

                {/* Mensaje temático */}
                <motion.p
                    initial={{ opacity: 0, y: 10 }}
                    animate={{ opacity: 1, y: 0 }}
                    transition={{ delay: 0.3 }}
                    className="text-lg font-semibold text-center text-green-800"
                >
                    {randomMessage}
                </motion.p>

                {/* Progreso */}
                <progress className="progress progress-success w-56" />
            </motion.div>
        </div>
    )
}