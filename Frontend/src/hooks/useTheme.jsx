import { useEffect, useState } from "react"

export default function useTheme() {
    const [theme, setTheme] = useState(() => {
        return localStorage.getItem("theme") || "cupcake"
    })

    useEffect(() => {
        localStorage.setItem("theme", theme)
    }, [theme])

    useEffect(() => {
        const handleStorage = (e) => {
            if (e.key === "theme" && e.newValue) {
                setTheme(e.newValue)
            }
        }
        window.addEventListener("storage", handleStorage)
        return () => window.removeEventListener("storage", handleStorage)
    }, [])

    return [theme, setTheme]
}
