import { useEffect, useState } from "react"
import { useCitiesAdmin } from "../../../hooks/queries/useCity"
import { useDepartments } from "../../../hooks/queries/useDepartment"
import {
    useActivateCity,
    useDeactivateCity,
} from "../../../hooks/mutations/useCityMutation"
import ConfigDataTable from "../../../components/DataTables/ConfigDataTable"
import { Funnel, Search } from "lucide-react"
import { useCountries } from "../../../hooks/queries/useCountry"
import AdvancedFiltersModal from "../../../components/Modals/AdvancedFiltersModal"
import ColumnSelectorModal from "../../../components/Modals/ColumnSelectorModal"
import LoaderStaff from "../../../components/Loader/LoaderStaff"

function cleanFilters(filters) {
    const cleaned = { ...filters }

    if (!cleaned.nameCity?.trim()) delete cleaned.nameCity
    if (!cleaned.idCountry || cleaned.idCountry === 0) delete cleaned.idCountry
    if (!cleaned.idDepartment || cleaned.idDepartment === 0) delete cleaned.idDepartment
    if (!cleaned.createdAt) delete cleaned.createdAt
    if (!cleaned.updatedAt) delete cleaned.updatedAt
    if (cleaned.isActive === null || cleaned.isActive === undefined) delete cleaned.isActive
    if (!cleaned.deactivatedAt) delete cleaned.deactivatedAt

    return cleaned
}

const availableFields = [
    { key: "nameCountry", label: "País" },
    { key: "createdAt", label: "Creado" },
    { key: "updatedAt", label: "Actualizado" },
    { key: "deactivatedAt", label: "Desactivado" },
]

export default function CityList() {
    const [filters, setFilters] = useState({
        nameCity: "",
        idCountry: 0,
        idDepartment: 0,
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

    const { data: cities = [], isLoading, isError, error, refetch } = useCitiesAdmin(cleanedFilters)

    useEffect(() => {
        refetch()
    }, [cleanedFilters, refetch])

    const { data: countries = [] } = useCountries()
    const { data: departments = [] } = useDepartments()

    const { mutate: deactivateCity } = useDeactivateCity()
    const { mutate: activateCity } = useActivateCity()

    const handleToggleActive = (city) => {
        if (city.isActive) {
            deactivateCity(city.idCity, {
                onError: (err) => {
                    console.error("Error al desactivar:", err)
                },
            })
        } else {
            activateCity(city.idCity, {
                onError: (err) => {
                    console.error("Error al activar:", err)
                },
            })
        }
    }

    const handleApplySearch = () => {
        setFilters((prev) => ({
            ...prev,
            nameCity: searchDraft,
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
            nameCity: "",
            idCountry: 0,
            idDepartment: 0,
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
            page: 1,
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
                        placeholder="Buscar Ciudad"
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
                    value={filters.idDepartment}
                    onChange={(e) =>
                        setFilters({ ...filters, idDepartment: parseInt(e.target.value) })
                    }
                    className="select bg-gray-700 text-white px-3 py-2 rounded w-full sm:w-50"
                >
                    <option value={0}>Todos los departamentos</option>
                    {departments.map((department) => (
                        <option key={department.idDepartment} value={department.idDepartment}>
                            {department.nameDepartment}
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
                    <option value="NameCity">Nombre</option>
                </select>

                <button
                    onClick={() =>
                        setFilters((prev) => ({
                            ...prev,
                            sortDesc: !prev.sortDesc,
                        }))
                    }
                    className="bg-gray-600 px-3 py-2 rounded text-white hover:bg-gray-500 cursor-pointer"
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
                title="Ciudades"
                columns={cities?.columns || []}
                data={cities?.data || []}
                total={cities?.total || 0}
                page={filters.page}
                pageSize={filters.pageSize}
                onPageChange={handlePageChange}
                onPageSizeChange={handlePageSizeChange}
                editUrl={(rowData) => `/staff/locations/cities/edit/${rowData.idCity}`}
                detailUrl={(rowData) => `/staff/locations/cities/${rowData.idCity}`}
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