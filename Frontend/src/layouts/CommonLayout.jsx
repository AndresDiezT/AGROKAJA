import { Outlet } from 'react-router-dom'
import CommonNavbar from '../components/CommonNavbar'

function CommonLayout() {
    return (
        <div>
            <CommonNavbar />
            <main className='pt-10'>
                <Outlet />
            </main>
        </div>
    )
}

export default CommonLayout