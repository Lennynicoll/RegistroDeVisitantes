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
    Promise.all([
      citaService.getAll(),
      pacienteService.getAll(),
      medicoService.getAll(),
    ])
      .then(([citas, pacs, meds]) => {
        setItems(citas.data);
        setPacientes(pacs.data);
        setMedicos(meds.data);
      })
      .catch((err) => setError(err.message));
  };

  useEffect(() => {
    fetchData();
  }, []);

  const handleCreate = (data) => {
    citaService
      .create(data)
      .then(() => {
        setShowForm(false);
        fetchData();
      })
      .catch((err) => setError(err.message));
  };

  const handleUpdate = (data) => {
    citaService
      .update(editingItem.id, data)
      .then(() => {
        setEditingItem(null);
        setShowForm(false);
        fetchData();
      })
      .catch((err) => setError(err.message));
  };

  const handleDelete = (id) => {
    if (!confirm('¿Eliminar esta cita?')) return;
    citaService
      .remove(id)
      .then(() => fetchData())
      .catch((err) => setError(err.message));
  };

  const openEdit = (item) => {
    setEditingItem(item);
    setShowForm(true);
  };

  const openCreate = () => {
    setEditingItem(null);
    setShowForm(true);
  };

  const getPacienteNombre = (id) => {
    const p = pacientes.find((x) => x.id === id);
    return p ? `${p.nombre} ${p.apellido}` : id;
  };

  const getMedicoNombre = (id) => {
    const m = medicos.find((x) => x.id === id);
    return m ? `${m.nombre} ${m.apellido}` : id;
  };

  const formatDate = (dateStr) => {
    if (!dateStr) return '';
    return new Date(dateStr).toLocaleString();
  };

  return (
    <div className="page">
      <div className="page-header">
        <h1>Citas</h1>
        <button className="btn btn-primary" onClick={openCreate}>
          + Nueva Cita
        </button>
      </div>
      {error && <div className="error">{error}</div>}
      <table className="table">
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
              <td>{getPacienteNombre(item.pacienteId)}</td>
              <td>{getMedicoNombre(item.medicoId)}</td>
              <td>{formatDate(item.fechaHora)}</td>
              <td>{item.motivo}</td>
              <td>
                <span className={`status status-${item.estado?.toLowerCase()}`}>
                  {item.estado}
                </span>
              </td>
              <td className="actions">
                <button className="btn btn-sm btn-edit" onClick={() => openEdit(item)}>
                  Editar
                </button>
                <button className="btn btn-sm btn-delete" onClick={() => handleDelete(item.id)}>
                  Eliminar
                </button>
              </td>
            </tr>
          ))}
          {items.length === 0 && (
            <tr>
              <td colSpan={7} className="empty">
                No hay citas registradas
              </td>
            </tr>
          )}
        </tbody>
      </table>
      {showForm && (
        <CitaForm
          onSubmit={editingItem ? handleUpdate : handleCreate}
          editingItem={editingItem}
          onCancel={() => {
            setShowForm(false);
            setEditingItem(null);
          }}
          pacientes={pacientes}
          medicos={medicos}
        />
      )}
    </div>
  );
}
