import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'

// LAYOUTS IMPORTS
import CommonLayout from './layouts/CommonLayout'
import AuthLayout from './layouts/AuthLayout'
import EmployeeLayout from './layouts/EmployeeLayout'

// AUTH IMPORTS
import Login from './pages/auth/Login'
import Register from './pages/auth/Register'

// COMMON IMPORTS
import Home from './pages/Home'
import Profile from './pages/Profile'
import NotFound from './pages/NotFound'

// EMPLOYEES IMPORTS
import Dashboard from './pages/Employees/Dashboard'


function App() {

  return (
    <>
      <Router>
        <Routes>
          <Route element={<CommonLayout />}>
            <Route index element={<Home />} />
            <Route path="/profile" element={<Profile />} />
          </Route>

          <Route element={<AuthLayout />}>
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
          </Route>

          <Route path="/employee" element={<EmployeeLayout />}>
            <Route index element={<Dashboard />} />
            {/* <Route path="profile" element={<Profile />} /> */}
          </Route>

          {/* Route: /404 */}
          <Route path="*" element={<NotFound />} />
        </Routes>
      </Router>
    </>
  )
}

export default App