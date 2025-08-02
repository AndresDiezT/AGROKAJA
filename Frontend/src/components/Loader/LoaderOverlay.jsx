import { Leaf } from 'lucide-react'
import { motion } from 'framer-motion'

export default function LoaderOverlay() {
    return (
        <div className="fixed inset-0 z-50 bg-gradient-to-br from-lime-100 via-white to-emerald-100 flex flex-col items-center justify-center">
            <motion.div
                initial={{ rotate: 0 }}
                animate={{ rotate: 360 }}
                transition={{ repeat: Infinity, duration: 2, ease: "linear" }}
                className="mb-6"
            >
                <Leaf className="w-20 h-20 text-emerald-600 drop-shadow-lg" strokeWidth={2.5} />
            </motion.div>

            <motion.p
                initial={{ opacity: 0, y: 10 }}
                animate={{ opacity: 1, y: 0 }}
                transition={{ duration: 0.6, repeat: Infinity, repeatType: "reverse" }}
                className="text-emerald-800 text-xl font-semibold"
            >
                Cosechando datos... 🌿
            </motion.p>

            <p className="text-gray-500 text-sm mt-3 animate-pulse">
                Espéranos mientras preparamos todo 🌾
            </p>
        </div>
    )
}