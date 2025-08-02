const MOCK_USER_STATS = {
  total: 1523,
  active: 1120,
  inactive: 403,
  newThisWeek: 47,
  newUsersByDay: [
    { date: '2025-07-24', count: 4 },
    { date: '2025-07-25', count: 8 },
    { date: '2025-07-26', count: 15 },
    { date: '2025-07-27', count: 12 },
    { date: '2025-07-28', count: 6 },
  ]
}

const MOCK_RECENT_USERS = [
  { id: 1, name: 'Juan Pérez', email: 'juan@example.com', createdAt: '2025-07-27' },
  { id: 2, name: 'Ana López', email: 'ana@example.com', createdAt: '2025-07-28' },
  { id: 3, name: 'Carlos Ruiz', email: 'carlos@example.com', createdAt: '2025-07-28' },
]


function Dashboard() {
    return (
        <div>Dashboard
        </div>
    )
}

export default Dashboard