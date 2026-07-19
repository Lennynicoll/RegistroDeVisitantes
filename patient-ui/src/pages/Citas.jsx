import { useState, useEffect } from 'react';
import * as citaService from '../api/citaService';
import * as pacienteService from '../api/pacienteService';
import * as medicoService from '../api/medicoService';
import CitaForm from '../components/CitaForm';

export default function Citas() {
  const [items, setItems] = useState([]);
  const [pacientes, setPacientes] = useState([]);
  const [medicos, setMedicos] = useState([]);
  const [showForm, setShowForm] = useState(false);
  const [editingItem, setEditingItem] = useState(null);
  const [error, setError] = useState('');

  const fetchData = () => {
    Promise.all([citaService.getAll(), pacienteService.getAll(), medicoService.getAll()])
      .then(([citas, pacs, meds]) => {
        setItems(citas.data);
        setPacientes(pacs.data);
        setMedicos(meds.data);
      })
      .catch((err) => setError(err.message));
  };

  useEffect(() => { fetchData(); }, []);

  const handleCreate = (data) => {
    citaService.create(data)
      .then(() => { setShowForm(false); fetchData(); })
      .catch((err) => setError(err.message));
  };

  const handleUpdate = (data) => {
    citaService.update(editingItem.id, data)
      .then(() => { setEditingItem(null); setShowForm(false); fetchData(); })
      .catch((err) => setError(err.message));
  };

  const handleDelete = (id) => {
    if (!confirm('Eliminar esta visita?')) return;
    citaService.remove(id)
      .then(() => fetchData())
      .catch((err) => setError(err.message));
  };

  const getNombre = (lista, id) => {
    const item = lista.find((x) => x.id === id);
    return item ? `${item.nombre} ${item.apellido}` : id;
  };

  const formatDate = (dateStr) => {
    if (!dateStr) return '';
    return new Date(dateStr).toLocaleString();
  };

  return (
    <div>
      <div className="page-header">
        <h1>Registro de Visitas</h1>
        <button className="btn btn-primary" onClick={() => { setEditingItem(null); setShowForm(!showForm); }}>
          {showForm ? 'Cerrar' : '+ Nueva'}
        </button>
      </div>
      {error && <div className="error">{error}</div>}
      {showForm && (
        <CitaForm
          onSubmit={editingItem ? handleUpdate : handleCreate}
          editingItem={editingItem}
          onCancel={() => { setShowForm(false); setEditingItem(null); }}
          pacientes={pacientes}
          medicos={medicos}
        />
      )}
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Paciente</th>
            <th>Medico</th>
            <th>Fecha</th>
            <th>Motivo</th>
            <th>Estado</th>
            <th>Acciones</th>
          </tr>
        </thead>
        <tbody>
          {items.map((item) => (
            <tr key={item.id}>
              <td>{item.id}</td>
              <td>{getNombre(pacientes, item.pacienteId)}</td>
              <td>{getNombre(medicos, item.medicoId)}</td>
              <td>{formatDate(item.fechaHora)}</td>
              <td>{item.motivo}</td>
              <td>
                <span className={`status status-${item.estado?.toLowerCase()}`}>
                  {item.estado}
                </span>
              </td>
              <td>
                <button className="btn btn-edit" onClick={() => { setEditingItem(item); setShowForm(true); }}>
                  Editar
                </button>
                <button className="btn btn-delete" onClick={() => handleDelete(item.id)}>
                  Eliminar
                </button>
              </td>
            </tr>
          ))}
          {items.length === 0 && (
            <tr><td colSpan={7} className="empty">No hay visitas registradas</td></tr>
          )}
        </tbody>
      </table>
    </div>
  );
}
