import { useState } from "react"
import {
    useActivateUser,
    useDeactivateUser,
} from "../../../hooks/mutations/useUserMutation"
import { useCustomersUser } from '../../../hooks/queries/useUser'
import { useRoles } from '../../../hooks/queries/useRole'
import { useTypesDocument } from '../../../hooks/queries/useTypeDocument'
import UsersDataTable from "../../../components/DataTables/UsersDataTable"
import { Funnel, Search } from "lucide-react"
import AdvancedFiltersModal from "../../../components/Modals/AdvancedFiltersModal"
import ColumnSelectorModal from "../../../components/Modals/ColumnSelectorModal"
import LoaderStaff from "../../../components/Loader/LoaderStaff"

function cleanFilters(filters) {
    const cleaned = { ...filters }

    if (!cleaned.document?.trim()) delete cleaned.document
    if (!cleaned.username?.trim()) delete cleaned.username
    if (!cleaned.email?.trim()) delete cleaned.email
    if (!cleaned.firstName?.trim()) delete cleaned.firstName
    if (!cleaned.lastName?.trim()) delete cleaned.lastName
    if (!cleaned.phoneNumber?.trim()) delete cleaned.phoneNumber
    if (!cleaned.birthDate?.trim()) delete cleaned.phoneNumber
    if (!cleaned.idRole || cleaned.idRole === 0) delete cleaned.idRole
    if (!cleaned.idTypeDocument || cleaned.idTypeDocument === 0) delete cleaned.idTypeDocument
    if (cleaned.isActive === null || cleaned.isActive === undefined) delete cleaned.isActive
    if (!cleaned.createdAt) delete cleaned.createdAt
    if (!cleaned.updatedAt) delete cleaned.updatedAt
    if (!cleaned.deactivatedAt) delete cleaned.deactivatedAt

    return cleaned
}

const availableFields = [
    { key: "username", label: "Usuario" },
    { key: "email", label: "Correo" },
    { key: "firstName", label: "Nombre" },
    { key: "lastName", label: "Apellidos" },
    { key: "phoneNumber", label: "Teléfono" },
    { key: "roleName", label: "Rol" },
    { key: "typeDocumentName", label: "Tipo Documento" },
    { key: "birthDate", label: "Fecha Nacimiento" },
    { key: "createdAt", label: "Creado" },
    { key: "updatedAt", label: "Actualizado" },
    { key: "deactivatedAt", label: "Desactivado" },
]

export default function CustomerList() {
    const [filters, setFilters] = useState({
        document: "",
        username: "",
        email: "",
        phoneNumber: "",
        firstName: "",
        lastName: "",
        birthDate: "",
        idRole: 0,
        idTypeDocument: 0,
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

    const { data: customers = [], isLoading, isError, error, refetch } = useCustomersUser(cleanedFilters)
    const { data: roles = [] } = useRoles()
    const { data: typesDocument = [] } = useTypesDocument()

    const { mutate: deactivate } = useActivateUser()
    const { mutate: activate } = useDeactivateUser()

    const handleToggleActive = (customer) => {
        if (customer.isActive) {
            deactivate(customer.document, {
                onError: (err) => {
                    console.error("Error al desactivar:", err)
                },
            })
        } else {
            activate(customer.document, {
                onError: (err) => {
                    console.error("Error al activar:", err)
                },
            })
        }
    }

    const handleApplySearch = () => {
        setFilters((prev) => ({
            ...prev,
            document: searchDraft,
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
            document: "",
            username: "",
            email: "",
            phoneNumber: "",
            firstName: "",
            lastName: "",
            birthDate: "",
            idRole: 0,
            idTypeDocument: 0,
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
                        placeholder="Buscar por Documento..."
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
                    value={filters.idRole}
                    onChange={(e) =>
                        setFilters({ ...filters, idRole: parseInt(e.target.value) })
                    }
                    className="select bg-gray-700 text-white px-3 py-2 rounded w-full sm:w-50"
                >
                    <option value={0}>Todos los roles</option>
                    {roles.map((role) => (
                        <>
                            {role.idRole == 1 || role.idRole == 2 ?
                                <option key={role.idRole} value={role.idRole}>
                                    {role.nameRole}
                                </option> : <></>}
                        </>
                    ))}
                </select>

                <select
                    value={filters.idTypeDocument}
                    onChange={(e) =>
                        setFilters({ ...filters, idTypeDocument: parseInt(e.target.value) })
                    }
                    className="select bg-gray-700 text-white px-3 py-2 rounded w-full sm:w-50"
                >
                    <option value={0}>Todo tipo de documento</option>
                    {typesDocument.map((typeDocument) => (
                        <option key={typeDocument.idTypeDocument} value={typeDocument.idTypeDocument}>
                            {typeDocument.nameTypeDocument}
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
                    <option value="Document">Documento</option>
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

            <UsersDataTable
                title="Clientes"
                isEmployee={false}
                columns={customers?.columns || []}
                data={customers?.data || []}
                total={customers?.total || 0}
                page={filters.page}
                pageSize={filters.pageSize}
                onPageChange={handlePageChange}
                onPageSizeChange={handlePageSizeChange}
                editUrl={(rowData) => `/staff/customers/edit/${rowData.document}`}
                detailUrl={(rowData) => `/staff/customers/${rowData.document}`}
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