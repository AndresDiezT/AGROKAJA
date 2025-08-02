import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'

// LAYOUTS IMPORTS
import CommonLayout from './layouts/CommonLayout'
import AuthLayout from './layouts/AuthLayout'
import EmployeeLayout from './layouts/EmployeeLayout'

// AUTH IMPORTS
import Login from './pages/Auth/Login'
import Register from './pages/Auth/Register'
import EmailSent from './pages/Auth/EmailSent'
import VerifyEmail from './pages/Auth/VerifyEmail'
import RedirectIfAuth from './auth/RedirectIfAuth'
import { RequireAuth } from './auth/RequireAuth'

// COMMON IMPORTS
import Home from './pages/Home'
import ProfileLayout from './pages/Profile/ProfileLayout'
import Account from './pages/Profile/Account'
import Addresses from './pages/Profile/Addresses'
import NotFound from './pages/NotFound'

// EMPLOYEES IMPORTS
import Dashboard from './pages/Staff/Dashboard'
import AddEmployee from './pages/Staff/Employees/AddEmployee'
import CustomerList from './pages/Staff/Customers/CustomerList'
import EmployeeList from './pages/Staff/Employees/EmployeeList'
import AddAddress from './pages/Profile/AddAddress'
import EditAddress from './pages/Profile/editAddress'
import RolesList from './pages/Staff/Roles/RolesList'
import CityList from './pages/Staff/Location/CityList'
import CountryList from './pages/Staff/Location/CountryList'
import DepartmentList from './pages/Staff/Location/DepartmentList'

function App() {

  return (
    <>
      <Router>
        <Routes>
          <Route element={<CommonLayout />}>
            <Route index element={<Home />} />
            <Route element={<RequireAuth requiredPermission="common.profile.access" />}>
              <Route path='/profile' element={<ProfileLayout />}>
                <Route path='/profile/my-profile' element={<Account />} />

                <Route path='/profile/addresses' element={<Addresses />} />
                <Route path='/profile/addresses/add' element={<AddAddress />} />
                <Route path='/profile/addresses/edit/:id' element={<EditAddress />} />
              </Route>
            </Route>
          </Route>

          <Route element={<AuthLayout />}>
            <Route path='/login' element={
              <RedirectIfAuth>
                <Login />
              </RedirectIfAuth>
            } />
            <Route path='/register' element={
              <RedirectIfAuth>
                <Register />
              </RedirectIfAuth>
            } />
            <Route path='/email-sent' element={<EmailSent />} />
            <Route path='/confirm-email' element={<VerifyEmail />} />
          </Route>

          <Route element={<RequireAuth requiredPermission="admin.dashboard.access" />}>
            <Route path='/staff' element={<EmployeeLayout />}>
              <Route path='dashboard' element={<Dashboard />} />
              <Route path='employees/add' element={<AddEmployee />} />
              <Route path='employees/list' element={<EmployeeList />} />
              <Route path='customers/list' element={<CustomerList />} />

              <Route element={<RequireAuth requiredPermission="admin.roles.read" />}>
                <Route path='settings/roles' element={<RolesList />} />
              </Route>

              <Route element={<RequireAuth requiredPermission="admin.countries.read" />}>
                <Route path='locations/countries' element={<CountryList />} />
              </Route>

              <Route element={<RequireAuth requiredPermission="admin.departments.read" />}>
                <Route path='locations/departments' element={<DepartmentList />} />
              </Route>

              <Route element={<RequireAuth requiredPermission="admin.cities.read" />}>
                <Route path='locations/cities' element={<CityList />} />
              </Route>
            </Route>
          </Route>


          {/* Route: /404 */}
          <Route path='*' element={<NotFound />} />
        </Routes>
      </Router>
    </>
  )
}

export default App