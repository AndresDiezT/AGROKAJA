import { Outlet } from 'react-router-dom'
import EmployeeNavbar from '../components/EmployeeNavbar'

function EmployeeLayout() {
    return (
        <div>
            <EmployeeNavbar />
            <main>
                <Outlet />
            </main>
        </div>
    )
}

export default EmployeeLayout