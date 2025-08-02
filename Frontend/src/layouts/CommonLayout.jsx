import { Outlet } from 'react-router-dom'
import CommonNavbar from '../components/Menus/CommonNavbar'
import CommonFooter from '../components/footer/CommonFooter'

export default function CommonLayout() {
    return (
        <div
            className='flex flex-col min-h-screen'
            data-theme="light"
        >
            <CommonNavbar />
            <main className='pt-10 flex-1 bg-white'>
                <Outlet />
            </main>
            <CommonFooter />
        </div>
    )
}