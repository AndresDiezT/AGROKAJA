import React from 'react'
import { Outlet } from 'react-router-dom'
import CommonNavbar from '../components/CommonNavbar'

function CommonLayout() {
    return (
        <div>
            <CommonNavbar />
            <main>
                <Outlet />
            </main>
        </div>
    )
}

export default CommonLayout