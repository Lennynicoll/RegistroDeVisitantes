import { NavLink, Outlet } from 'react-router-dom';
import './Layout.css';

const navItems = [
  { path: '/', label: 'Dashboard', icon: '📊' },
  { path: '/pacientes', label: 'Pacientes', icon: '👤' },
  { path: '/medicos', label: 'Medicos', icon: '👨‍⚕️' },
  { path: '/citas', label: 'Citas', icon: '📅' },
  { path: '/medicamentos', label: 'Medicamentos', icon: '💊' },
  { path: '/especialidades', label: 'Especialidades', icon: '🏥' },
  { path: '/departamentos', label: 'Departamentos', icon: '🏢' },
];

export default function Layout() {
  return (
    <div className="layout">
      <aside className="sidebar">
        <div className="sidebar-header">
          <h2>Sistema de Salud</h2>
        </div>
        <nav className="sidebar-nav">
          {navItems.map((item) => (
            <NavLink
              key={item.path}
              to={item.path}
              end={item.path === '/'}
              className={({ isActive }) =>
                `nav-link${isActive ? ' active' : ''}`
              }
            >
              <span className="nav-icon">{item.icon}</span>
              <span className="nav-label">{item.label}</span>
            </NavLink>
          ))}
        </nav>
      </aside>
      <main className="main-content">
        <Outlet />
      </main>
    </div>
  );
}
