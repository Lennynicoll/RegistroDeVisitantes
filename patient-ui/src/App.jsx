import { Routes, Route } from 'react-router-dom';
import Layout from './components/Layout';
import Dashboard from './pages/Dashboard';
import Pacientes from './pages/Pacientes';
import Medicos from './pages/Medicos';
import Citas from './pages/Citas';
import Medicamentos from './pages/Medicamentos';
import Especialidades from './pages/Especialidades';
import Departamentos from './pages/Departamentos';

export default function App() {
  return (
    <Routes>
      <Route element={<Layout />}>
        <Route path="/" element={<Dashboard />} />
        <Route path="/pacientes" element={<Pacientes />} />
        <Route path="/medicos" element={<Medicos />} />
        <Route path="/citas" element={<Citas />} />
        <Route path="/medicamentos" element={<Medicamentos />} />
        <Route path="/especialidades" element={<Especialidades />} />
        <Route path="/departamentos" element={<Departamentos />} />
      </Route>
    </Routes>
  );
}
