import { NavLink, Outlet } from 'react-router-dom';
import './Layout.css';

export default function Layout() {
  return (
    <div>
      <header className="header">
        <h1>Sistema de Registro de Visitantes</h1>
        <nav>
          <NavLink to="/">Dashboard</NavLink>
          <NavLink to="/pacientes">Visitantes</NavLink>
          <NavLink to="/medicos">Anfitriones</NavLink>
          <NavLink to="/citas">Registro de Visitas</NavLink>
          <NavLink to="/medicamentos">Oficinas</NavLink>
          <NavLink to="/especialidades">Motivos de Visita</NavLink>
          <NavLink to="/departamentos">Departamentos</NavLink>
        </nav>
      </header>
      <main className="main">
        <Outlet />
      </main>
    </div>
  );
}
