export default function ColumnSelectorModal({
    availableFields,
    filters,
    toggleColumn,
    setShowColumnSelector
}) {
    return (
        <div className="fixed inset-0 bg-black/50 z-50 flex items-center justify-center bg-opacity-50">
            <div className="modal-custom p-6 rounded-lg shadow-lg w-full max-w-md relative" >
                <h2 className="text-lg font-semibold mb-2">Selecciona las columnas</h2>
                <div className="flex flex-col gap-1 max-h-60 overflow-y-auto">
                    {availableFields.map((col) => (
                        <label
                            key={col.key}
                            className="flex items-center justify-between p-2 rounded-lg border border-base-300 shadow-sm transition-colors cursor-pointer"
                        >
                            <span className="flex items-center gap-2">
                                <span className="font-medium">{col.label}</span>
                            </span>

                            <button
                                onClick={() => toggleColumn(col.key)}
                                className={`relative w-10 h-6 rounded-full transition-colors duration-300 cursor-pointer ${filters.selectFields.includes(col.key)
                                        ? "bg-teal-600"
                                        : "bg-gray-300"
                                    }`}
                            >
                                <span
                                    className={`absolute top-0.5 left-0.5 w-5 h-5 rounded-full bg-primary shadow transition-transform duration-300 ${filters.selectFields.includes(col.key)
                                            ? "translate-x-4"
                                            : "translate-x-0"
                                        }`}
                                />
                            </button>
                        </label>
                    ))}
                </div>
                <div className="flex justify-end gap-2 mt-4">
                    <button
                        onClick={() => setShowColumnSelector(false)}
                        className="custom-button px-3 py-2 rounded hover:bg-accent hover:text-accent-content cursor-pointer"
                    >
                        Cerrar
                    </button>
                </div>
            </div>
        </div>
    )
}