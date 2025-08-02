import { useMemo } from "react"
import {
    useReactTable,
    getCoreRowModel,
    getPaginationRowModel,
    flexRender
} from '@tanstack/react-table'
import { Link } from "react-router-dom"

export default function ConfigDataTable({
    title,
    columns,
    data,
    total,
    page,
    pageSize,
    onPageChange,
    onPageSizeChange,
    editUrl,
    detailUrl,
    onToggleActive
}) {
    const pageCount = Math.ceil(total / pageSize)

    const tableColumns = useMemo(() => {
        const mappedColumns = columns.map(col => ({
            accessorKey: col.key,
            header: col.label,
            cell: info => {
                const value = info.getValue()

                if (col.type === "boolean") {
                    return (
                        <span
                            className={`font-bold ${value ? "text-green-400" : "text-red-400"
                                }`}
                        >
                            {value ? "✔️" : "✖️"}
                        </span>
                    )
                }

                if (col.type === "date" && value) {
                    return new Date(value).toLocaleDateString()
                }

                return value ?? "-"
            }
        }))

        mappedColumns.push({
            id: "actions",
            header: "Acciones",
            cell: ({ row }) => {
                const rowData = row.original
                return (
                    <div className="flex gap-2">
                        <Link to={detailUrl(rowData)}
                            className="bg-yellow-600 text-white px-2 py-1 rounded hover:bg-blue-700"
                        >
                            Ver
                        </Link>
                        <Link to={editUrl(rowData)}
                            className="px-2 py-1 rounded edit-button transition cursor-pointer"
                        >
                            Editar
                        </Link>
                        <button
                            onClick={() => onToggleActive(rowData)}
                            className={`${rowData.isActive
                                ? "bg-red-600 hover:bg-red-700"
                                : "bg-green-600 hover:bg-green-700"
                                } text-white px-2 py-1 rounded`}
                        >
                            {rowData.isActive ? "Desactivar" : "Activar"}
                        </button>
                    </div>
                )
            }
        })

        return mappedColumns
    }, [columns, editUrl, onToggleActive])

    const table = useReactTable({
        data,
        columns: tableColumns,
        pageCount: Math.ceil(total / pageSize),
        state: {
            pagination: {
                pageIndex: page - 1,
                pageSize
            }
        },
        getCoreRowModel: getCoreRowModel(),
        getPaginationRowModel: getPaginationRowModel(),
        manualPagination: true,
    })

    const handlePageClick = (newPage) => {
        if (newPage >= 1 && newPage <= pageCount) {
            onPageChange(newPage)
        }
    }

    return (
        <div className="custom-table rounded-lg shadow">
            <h1 className="text-lg font-semibold py-2 p-3">{title}</h1>
            <div className="w-full overflow-x-auto">
                <table className="w-full text-sm hidden md:table">
                    <thead className="sticky top-0">
                        {table.getHeaderGroups().map((headerGroup) => (
                            <tr key={headerGroup.id}>
                                {headerGroup.headers.map((header) => (
                                    <th key={header.id} className="p-2 text-left">
                                        {header.isPlaceholder ? null : flexRender(
                                            header.column.columnDef.header,
                                            header.getContext()
                                        )}
                                    </th>
                                ))}
                            </tr>
                        ))}
                    </thead>
                    <tbody>
                        {table.getRowModel().rows.map((row) => (
                            <tr key={row.id} className="border-b border-base-300 hover:bg-base-300">
                                {row.getVisibleCells().map((cell) => (
                                    <td key={cell.id} className="p-2">
                                        {flexRender(
                                            cell.column.columnDef.cell,
                                            cell.getContext()
                                        )}
                                    </td>
                                ))}
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>

            <div className="flex flex-col gap-2 md:hidden">
                {table.getRowModel().rows.map((row) => (
                    <div
                        key={row.id}
                        className="rounded-lg p-4 shadow border border-base-300"
                    >
                        {row.getVisibleCells().map((cell) => (
                            <div
                                key={cell.id}
                                className="flex justify-between py-1"
                            >
                                <span className="font-semibold">
                                    {flexRender(
                                        cell.column.columnDef.header,
                                        cell.getContext()
                                    )}
                                </span>
                                <span>
                                    {flexRender(
                                        cell.column.columnDef.cell,
                                        cell.getContext()
                                    )}
                                </span>
                            </div>
                        ))}
                    </div>
                ))}
            </div>

            <div className="flex justify-between items-center p-2 flex-wrap gap-2">
                <div>
                    Página {page} de {table.getPageCount()}
                </div>
                <div className="flex gap-2 flex-wrap text-primary">
                    <button
                        onClick={() => handlePageClick(page - 1)}
                        disabled={page <= 1}
                        className={`px-2 py-1 rounded ${page <= 1 ? "bg-gray-600 cursor-not-allowed" : "custom-button cursor-pointer"}`}
                    >
                        Anterior
                    </button>
                    {(() => {
                        const windowSize = 5
                        const half = Math.floor(windowSize / 2)

                        let start = Math.max(page - half, 1)
                        let end = Math.min(start + windowSize - 1, pageCount)

                        // Ajustar si esta cerca del final
                        if (end - start + 1 < windowSize) {
                            start = Math.max(end - windowSize + 1, 1)
                        }

                        const pages = []
                        for (let i = start; i <= end; i++) {
                            pages.push(i)
                        }

                        return pages.map((pageNum) => (
                            <button
                                key={pageNum}
                                onClick={() => handlePageClick(pageNum)}
                                className={`px-2 py-1 rounded ${page === pageNum ? "bg-accent text-white" : "bg-gray-700 hover:bg-gray-600 cursor-pointer"}`}
                            >
                                {pageNum}
                            </button>
                        ))
                    })()}

                    <button
                        onClick={() => handlePageClick(page + 1)}
                        disabled={page >= pageCount}
                        className={`px-2 py-1 rounded ${page >= pageCount ? "bg-gray-600 cursor-not-allowed" : "custom-button cursor-pointer"}`}
                    >
                        Siguiente
                    </button>
                    <select
                        className="custom-button rounded px-2 py-1 cursor-pointer"
                        value={pageSize}
                        onChange={(e) => onPageSizeChange(Number(e.target.value))}
                    >
                        {[5, 10, 20, 50].map((size) => (
                            <option key={size} value={size}>
                                {size} por página
                            </option>
                        ))}
                    </select>
                </div>
            </div>
        </div>
    )
}