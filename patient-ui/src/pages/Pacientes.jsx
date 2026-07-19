import { useState, useEffect } from 'react';
import * as pacienteService from '../api/pacienteService';
import PacienteForm from '../components/PacienteForm';

export default function Pacientes() {
  const [items, setItems] = useState([]);
  const [showForm, setShowForm] = useState(false);
  const [editingItem, setEditingItem] = useState(null);
  const [error, setError] = useState('');

  const fetchData = () => {
    pacienteService.getAll()
      .then((res) => setItems(res.data))
      .catch((err) => setError(err.message));
  };

  useEffect(() => { fetchData(); }, []);

  const handleCreate = (data) => {
    pacienteService.create(data)
      .then(() => { setShowForm(false); fetchData(); })
      .catch((err) => setError(err.message));
  };

  const handleUpdate = (data) => {
    pacienteService.update(editingItem.id, data)
      .then(() => { setEditingItem(null); setShowForm(false); fetchData(); })
      .catch((err) => setError(err.message));
  };

  const handleDelete = (id) => {
    if (!confirm('Eliminar este visitante?')) return;
    pacienteService.remove(id)
      .then(() => fetchData())
      .catch((err) => setError(err.message));
  };

  return (
    <div>
      <div className="page-header">
        <h1>Visitantes</h1>
        <button className="btn btn-primary" onClick={() => { setEditingItem(null); setShowForm(!showForm); }}>
          {showForm ? 'Cerrar' : '+ Nuevo'}
        </button>
      </div>
      {error && <div className="error">{error}</div>}
      {showForm && (
        <PacienteForm
          onSubmit={editingItem ? handleUpdate : handleCreate}
          editingItem={editingItem}
          onCancel={() => { setShowForm(false); setEditingItem(null); }}
        />
      )}
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Nombre</th>
            <th>Apellido</th>
            <th>Telefono</th>
            <th>Email</th>
            <th>Acciones</th>
          </tr>
        </thead>
        <tbody>
          {items.map((item) => (
            <tr key={item.id}>
              <td>{item.id}</td>
              <td>{item.nombre}</td>
              <td>{item.apellido}</td>
              <td>{item.telefono}</td>
              <td>{item.email}</td>
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
            <tr><td colSpan={6} className="empty">No hay visitantes</td></tr>
          )}
        </tbody>
      </table>
    </div>
  );
}
