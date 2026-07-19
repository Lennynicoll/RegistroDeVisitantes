import { useState, useEffect } from 'react';
import * as pacienteService from '../api/pacienteService';
import * as medicoService from '../api/medicoService';
import * as citaService from '../api/citaService';
import * as medicamentoService from '../api/medicamentoService';

export default function Dashboard() {
  const [stats, setStats] = useState({ pacientes: 0, medicos: 0, citas: 0, medicamentos: 0 });

  useEffect(() => {
    Promise.all([
      pacienteService.getAll(),
      medicoService.getAll(),
      citaService.getAll(),
      medicamentoService.getAll(),
    ])
      .then(([pacientes, medicos, citas, medicamentos]) => {
        setStats({
          pacientes: pacientes.data.length,
          medicos: medicos.data.length,
          citas: citas.data.length,
          medicamentos: medicamentos.data.length,
        });
      })
      .catch(console.error);
  }, []);

  return (
    <div>
      <h1>Dashboard</h1>
      <div className="stats">
        <div className="stat-card">
          <h3>Visitantes</h3>
          <p>{stats.pacientes}</p>
        </div>
        <div className="stat-card">
          <h3>Anfitriones</h3>
          <p>{stats.medicos}</p>
        </div>
        <div className="stat-card">
          <h3>Visitas</h3>
          <p>{stats.citas}</p>
        </div>
        <div className="stat-card">
          <h3>Medicamentos</h3>
          <p>{stats.medicamentos}</p>
        </div>
      </div>
    </div>
  );
}
