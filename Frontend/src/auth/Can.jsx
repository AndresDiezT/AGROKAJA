import { useAuth } from "./useAuth"

// Hook para proteger componentes
export function Can({ permission, children }) {
  const { hasPermission } = useAuth()
  return hasPermission(permission) ? children : null
}
