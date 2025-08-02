import { useState } from "react"

export default function AdvancedFiltersModal({ onClose, onApply, initialFilters }) {
    const [advancedFilters, setAdvancedFilters] = useState({
        createdAt: initialFilters.createdAt || "",
        updatedAt: initialFilters.updatedAt || "",
        deactivatedAt: initialFilters.deactivatedAt || ""
    })

    const handleApply = () => {
        onApply(advancedFilters)
        onClose()
    }

    return (
        <div className="fixed inset-0 bg-black/50 z-50 flex items-center justify-center bg-opacity-50">
            <div className="modal-custom p-6 rounded-lg shadow-lg w-full max-w-md relative">
                <h2 className="text-xl mb-4 font-semibold">Filtros Avanzados</h2>

                <div className="mb-4">
                    <label className="block text-sm mb-1">Fecha de Creación</label>
                    <input
                        type="date"
                        value={advancedFilters.createdAt}
                        onChange={(e) =>
                            setAdvancedFilters({ ...advancedFilters, createdAt: e.target.value })
                        }
                        className="w-full px-3 py-2 rounded border focus:outline-none focus:ring-2 focus:ring-teal-500"
                    />
                </div>

                <div className="mb-4">
                    <label className="block text-sm mb-1">Fecha de Actualización</label>
                    <input
                        type="date"
                        value={advancedFilters.updatedAt}
                        onChange={(e) =>
                            setAdvancedFilters({ ...advancedFilters, updatedAt: e.target.value })
                        }
                        className="w-full px-3 py-2 rounded border focus:outline-none focus:ring-2 focus:ring-teal-500"
                    />
                </div>

                <div className="mb-4">
                    <label className="block text-sm mb-1">Fecha de Desactivación</label>
                    <input
                        type="date"
                        value={advancedFilters.deactivatedAt}
                        onChange={(e) =>
                            setAdvancedFilters({ ...advancedFilters, deactivatedAt: e.target.value })
                        }
                        className="w-full px-3 py-2 rounded border focus:outline-none focus:ring-2 focus:ring-teal-500"
                    />
                </div>

                <div className="flex justify-end space-x-2">
                    <button
                        onClick={onClose}
                        className="custom-button px-3 py-2 rounded cursor-pointer"
                    >
                        Cancelar
                    </button>
                    <button
                        onClick={handleApply}
                        className="custom-button px-3 py-2 rounded cursor-pointer"
                    >
                        Aplicar
                    </button>
                </div>

                <button
                    onClick={onClose}
                    className="absolute top-2 right-6 text-gray-400 cursor-pointer"
                >
                    ✕
                </button>
            </div>
        </div>
    )
}
