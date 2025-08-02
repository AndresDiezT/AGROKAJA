import EmployeeSidebar from '../components/Menus/EmployeeSidebar'
import useTheme from '../hooks/useTheme'

export default function EmployeeLayout() {
    const [theme, setTheme] = useTheme()

    return (
        <div
            className='min-h-screen overflow-x-hidden'
            data-theme={theme}
        >
            <EmployeeSidebar theme={theme} setTheme={setTheme} />

        </div>
    )
}