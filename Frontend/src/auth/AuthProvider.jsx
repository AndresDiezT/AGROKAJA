import { createContext, useContext, useState, useEffect, useCallback, useMemo } from "react"
import { jwtDecode } from "jwt-decode"
import { useMe } from "../hooks/queries/useAuth"
import { useLoginUser, useLogoutUser } from "../hooks/mutations/useAuthMutation"

const AuthContext = createContext()

export function AuthProvider({ children }) {
    // Estado para el usuario y token
    const [user, setUser] = useState(null)
    const [profile, setProfile] = useState(null)
    const [token, setToken] = useState(null)
    const [loading, setLoading] = useState(true)

    const { mutate: loginUser } = useLoginUser()
    const { mutate: logoutUser } = useLogoutUser()

    const hasToken = !!token

    const { data: me, isLoading: meLoading, error: meError, refetch } = useMe({ enabled: hasToken })

    useEffect(() => {
        if (me && Object.keys(me).length > 0) {
            setProfile(me)
        }
    }, [me])

    // Cargar el token del localStorage al iniciar
    useEffect(() => {
        const savedToken = localStorage.getItem("token")

        if (savedToken) {
            try {
                const decoded = jwtDecode(savedToken)
                setToken(savedToken)

                setUser({
                    email: decoded.email,
                    username: decoded.username,
                    roles: decoded.role || decoded.roles || [],
                    permissions: decoded.permission || decoded.permissions || [],
                })

            } catch (err) {
                console.warn("Token inválido", err)
                handleLogout()
            }
        }

        setLoading(false)
    }, [])

    useEffect(() => {
        const onTokenRefreshed = () => {
            const newToken = localStorage.getItem('token')
            if (!newToken) return

            try {
                const decoded = jwtDecode(newToken)
                setToken(newToken)
                setUser({
                    email: decoded.email,
                    username: decoded.username,
                    roles: decoded.roles || decoded.role || [],
                    permissions: decoded.permissions || decoded.permission || [],
                })
            } catch (err) {
                console.error('Error actualizando el contexto con el nuevo token', err)
            }
        }

        window.addEventListener('tokenRefreshed', onTokenRefreshed)
        return () => window.removeEventListener('tokenRefreshed', onTokenRefreshed)
    }, [])

    useEffect(() => {
        if (!meLoading && !loading && meError && token) {
            console.warn("⚠️ Sesión inválida o expirada")
            handleLogout()
        }
    }, [meError, meLoading, loading, token])

    // Función para iniciar sesión
    const handleLogin = useCallback((email, password) => {
        loginUser(
            { email, password },
            {
                onSuccess: (res) => {
                    const jwtToken = res?.data?.accessToken
                    if (!jwtToken) return

                    try {
                        const decoded = jwtDecode(jwtToken)

                        setToken(jwtToken)
                        setUser({
                            email: decoded.email,
                            username: decoded.username,
                            roles: decoded.roles || decoded.role || [],
                            permissions: decoded.permissions || decoded.permission || [],
                        })
                        localStorage.setItem("token", jwtToken)
                    } catch (error) {
                        console.error("Error al decodificar el token", err)
                    }
                },
                onError: (err) => {
                    console.error("Error de login:", err)
                },
            }
        )
    }, [])

    // Función para cerrar sesión
    const handleLogout = useCallback(() => {
        if (!token && !user) return

        logoutUser(undefined, {
            onSuccess: () => {
                setUser(null)
                setToken(null)
                localStorage.removeItem("token")
            },
            onError: () => {
                setUser(null)
                setToken(null)
                localStorage.removeItem("token")
            },
        })
    }, [token, user])

    // Funciones para verificar roles y permisos
    const hasRole = useCallback((role) => user?.roles?.includes(role), [user])
    const hasPermission = useCallback(
        (perm) => user?.permissions?.includes(perm),
        [user]
    )

    const value = useMemo(
        () => ({
            user,
            profile,
            refetch,
            token,
            login: handleLogin,
            logout: handleLogout,
            isAuthenticated: !!user,
            hasRole,
            hasPermission,
            loading: loading || meLoading,
        }),
        [user, profile, refetch, token, handleLogin, handleLogout, loading, meLoading, hasRole, hasPermission]
    )

    return (
        <AuthContext.Provider value={value}>
            {children}
        </AuthContext.Provider>
    )
}

export function useAuth() {
    const context = useContext(AuthContext)
    if (!context) {
        throw new Error("useAuth debe usarse dentro de <AuthProvider>")
    }
    return context
}
