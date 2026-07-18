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

  const cards = [
    { title: 'Pacientes', value: stats.pacientes, color: '#4f46e5' },
    { title: 'Medicos', value: stats.medicos, color: '#0891b2' },
    { title: 'Citas', value: stats.citas, color: '#059669' },
    { title: 'Medicamentos', value: stats.medicamentos, color: '#d97706' },
  ];

  return (
    <div className="page">
      <h1>Dashboard</h1>
      <div className="stats-grid">
        {cards.map((card) => (
          <div key={card.title} className="stat-card" style={{ borderTopColor: card.color }}>
            <h3>{card.title}</h3>
            <p className="stat-value">{card.value}</p>
          </div>
        ))}
      </div>
    </div>
  );
}
