import { useEffect, useState } from "react"
import { useDepartmentsAdmin } from "../../../hooks/queries/useDepartment"
import { useCountries } from "../../../hooks/queries/useCountry"
import {
    useActivateDepartment,
    useDeactivateDepartment,
} from "../../../hooks/mutations/useDepartmentMutation"
import ConfigDataTable from "../../../components/DataTables/ConfigDataTable"
import AdvancedFiltersModal from "../../../components/Modals/AdvancedFiltersModal"
import { Funnel, Search } from "lucide-react"
import ColumnSelectorModal from "../../../components/Modals/ColumnSelectorModal"
import LoaderStaff from "../../../components/Loader/LoaderStaff"

function cleanFilters(filters) {
    const cleaned = { ...filters }

    if (!cleaned.nameCountry?.trim()) delete cleaned.nameCountry
    if (!cleaned.idCountry || cleaned.idCountry === 0) delete cleaned.idCountry
    if (!cleaned.createdAt) delete cleaned.createdAt
    if (!cleaned.updatedAt) delete cleaned.updatedAt
    if (cleaned.isActive === null || cleaned.isActive === undefined) delete cleaned.isActive
    if (!cleaned.deactivatedAt) delete cleaned.deactivatedAt

    return cleaned
}

const availableFields = [
    { key: "createdAt", label: "Creado" },
    { key: "updatedAt", label: "Actualizado" },
    { key: "deactivatedAt", label: "Desactivado" },
]

export default function DepartmentList() {
    const [filters, setFilters] = useState({
        nameDepartment: "",
        idCountry: 0,
        createdAt: "",
        updatedAt: "",
        isActive: null,
        deactivatedAt: "",
        page: 1,
        pageSize: 10,
        sortBy: "CreatedAt",
        sortDesc: true,
        selectFields: []
    })
    const [searchDraft, setSearchDraft] = useState("")

    const [showAdvanced, setShowAdvanced] = useState(false)

    const [showColumnSelector, setShowColumnSelector] = useState(false)

    const cleanedFilters = cleanFilters(filters)

    const { data: departments = [], isLoading, isError, error, refetch } = useDepartmentsAdmin(cleanedFilters)
    console.log(departments.columns)
    useEffect(() => {
        refetch()
    }, [cleanedFilters, refetch])

    const { data: countries = [] } = useCountries()

    const { mutate: deactivateDepartment } = useDeactivateDepartment()
    const { mutate: activateDepartment } = useActivateDepartment()

    const handleToggleActive = (department) => {
        if (department.isActive) {
            deactivateDepartment(department.idDepartment, {
                onError: (err) => {
                    console.error("Error al desactivar:", err)
                },
            })
        } else {
            activateDepartment(department.idDepartment, {
                onError: (err) => {
                    department.error("Error al activar:", err)
                },
            })
        }
    }

    const handleApplySearch = () => {
        setFilters((prev) => ({
            ...prev,
            nameDepartment: searchDraft,
            page: 1
        }))
    }

    const toggleColumn = (key) => {
        setFilters((prev) => ({
            ...prev,
            selectFields: prev.selectFields.includes(key)
                ? prev.selectFields.filter(k => k !== key)
                : [...prev.selectFields, key]
        }))
    }

    const handleClearFilters = () => {
        setSearchDraft("")
        setFilters({
            nameDepartment: "",
            idCountry: 0,
            createdAt: "",
            updatedAt: "",
            isActive: null,
            deactivatedAt: "",
            page: 1,
            pageSize: 10,
            sortBy: "CreatedAt",
            sortDesc: true,
            selectFields: []
        })
    }

    function handlePageChange(newPage) {
        setFilters((prev) => ({
            ...prev,
            page: newPage
        }))
    }

    function handlePageSizeChange(newPageSize) {
        setFilters((prev) => ({
            ...prev,
            pageSize: newPageSize,
            page: 1
        }))
    }

    if (isLoading) {
        return <LoaderStaff />
    }

    return (
        <>
            <div className="mb-4 flex flex-wrap gap-3 items-center">
                <label className="input">
                    <Search size={20} />
                    <input
                        type="text"
                        placeholder="Buscar Departamento"
                        value={searchDraft}
                        onChange={(e) =>
                            setSearchDraft(e.target.value)
                        }
                        onKeyDown={(e) => e.key === 'Enter' && handleApplySearch()}
                        className="grow px-3 py-2 rounded w-full sm:w-60"
                    />
                </label>

                <button
                    onClick={handleApplySearch}
                    className="bg-teal-600 px-3 py-2 rounded text-white hover:bg-teal-500 cursor-pointer"
                >
                    Buscar
                </button>

                <select
                    value={filters.idCountry}
                    onChange={(e) =>
                        setFilters({ ...filters, idCountry: parseInt(e.target.value) })
                    }
                    className="select bg-gray-700 text-white px-3 py-2 rounded w-full sm:w-50"
                >
                    <option value={0}>Todos los Países</option>
                    {countries.map((country) => (
                        <option key={country.idCountry} value={country.idCountry}>
                            {country.nameCountry}
                        </option>
                    ))}
                </select>

                <select
                    value={filters.sortBy}
                    onChange={(e) =>
                        setFilters({ ...filters, sortBy: e.target.value })
                    }
                    className="select bg-gray-700 text-white px-3 py-2 rounded w-full sm:w-40"
                >
                    <option value="CreatedAt">Fecha Creación</option>
                    <option value="NameCountry">Nombre</option>
                </select>

                <button
                    onClick={() =>
                        setFilters((prev) => ({
                            ...prev,
                            sortDesc: !prev.sortDesc,
                        }))
                    }
                    className="bg-gray-600 px-3 py-2 rounded text-white hover:bg-gray-500"
                >
                    {filters.sortDesc ? "Desc ↓" : "Asc ↑"}
                </button>

                <div className="flex flex-wrap gap-2 w-full sm:w-auto">
                    <button
                        className={`px-3 py-1 rounded-full cursor-pointer text-white ${filters.isActive === undefined ? 'bg-blue-500' : 'bg-gray-700'}`}
                        onClick={() => setFilters({ ...filters, isActive: undefined })}
                    >
                        Todos
                    </button>
                    <button
                        className={`px-3 py-1 rounded-full cursor-pointer text-white ${filters.isActive === true ? 'bg-green-500' : 'bg-gray-700'}`}
                        onClick={() => setFilters({ ...filters, isActive: true })}
                    >
                        Activos
                    </button>
                    <button
                        className={`px-3 py-1 rounded-full cursor-pointer text-white ${filters.isActive === false ? 'bg-red-500' : 'bg-gray-700'}`}
                        onClick={() => setFilters({ ...filters, isActive: false })}
                    >
                        Inactivos
                    </button>
                </div>

                <button
                    onClick={handleClearFilters}
                    className="bg-gray-600 px-3 py-2 rounded text-white hover:bg-gray-500"
                >
                    Limpiar
                </button>

                <button
                    onClick={() => setShowAdvanced(true)}
                    className="flex items-center gap-1 bg-teal-600 px-3 py-2 rounded text-white hover:bg-teal-500 cursor-pointer"
                >
                    <Funnel size={16} /> Avanzados
                </button>
                <button
                    onClick={() => setShowColumnSelector(true)}
                    className="flex items-center gap-1 bg-teal-600 px-3 py-2 rounded text-white hover:bg-teal-500 cursor-pointer"
                >
                    Seleccionar campos
                </button>
            </div>
            {isError && (
                <div className="bg-red-800 text-white p-4 rounded shadow-md text-center">
                    <p>⚠️ {error?.message || "Intentalo de nuevo mas tarde"}</p>
                </div>
            )}

            <ConfigDataTable
                title="Departamentos | Estados"
                columns={departments?.columns || []}
                data={departments?.data || []}
                total={departments?.total || 0}
                page={filters.page}
                pageSize={filters.pageSize}
                onPageChange={handlePageChange}
                onPageSizeChange={handlePageSizeChange}
                editUrl={(rowData) => `/staff/locations/departments/edit/${rowData.idDepartment}`}
                detailUrl={(rowData) => `/staff/locations/departments/${rowData.idDepartment}`}
                onToggleActive={handleToggleActive}
            />

            {showAdvanced && (
                <AdvancedFiltersModal
                    onClose={() => setShowAdvanced(false)}
                    onApply={(advancedFilters) => {
                        setFilters((prev) => ({
                            ...prev,
                            ...advancedFilters,
                            page: 1
                        }))
                    }}
                    initialFilters={{
                        createdAt: filters.createdAt,
                        updatedAt: filters.updatedAt,
                        deactivatedAt: filters.deactivatedAt
                    }}
                />
            )}

            {showColumnSelector && (
                <ColumnSelectorModal
                    availableFields={availableFields}
                    filters={filters}
                    toggleColumn={toggleColumn}
                    setShowColumnSelector={setShowColumnSelector}
                />
            )}
        </>
    )
}