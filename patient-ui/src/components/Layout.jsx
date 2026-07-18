import { NavLink, Outlet } from 'react-router-dom';
import './Layout.css';

export default function Layout() {
  return (
    <div>
      <header className="header">
        <h1>Sistema de Salud</h1>
        <nav>
          <NavLink to="/">Dashboard</NavLink>
          <NavLink to="/pacientes">Pacientes</NavLink>
          <NavLink to="/medicos">Medicos</NavLink>
          <NavLink to="/citas">Citas</NavLink>
          <NavLink to="/medicamentos">Medicamentos</NavLink>
          <NavLink to="/especialidades">Especialidades</NavLink>
          <NavLink to="/departamentos">Departamentos</NavLink>
        </nav>
      </header>
      <main className="main">
        <Outlet />
      </main>
    </div>
  );
}
