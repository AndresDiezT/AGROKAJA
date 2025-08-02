import { Navigate, Outlet } from "react-router-dom"
import { useAuth } from "../auth/AuthProvider"
import NotFound from "../pages/NotFound"
import LoaderOverlay from '../components/Loader/LoaderOverlay'

export function RequireAuth({ requiredPermission }) {
    // Hook para obtener el usuario y sus permisos
    const { isAuthenticated, hasPermission, loading } = useAuth()

    // Si aún se está cargando la información del usuario, muestra un mensaje de carga
    if (loading)
        return <LoaderOverlay />

    // Si no hay usuario autenticado, redirige al login
    if (!isAuthenticated)
        return <Navigate to="/login" replace />

    if (requiredPermission && !hasPermission(requiredPermission))
        return <NotFound />

    return <Outlet />
}