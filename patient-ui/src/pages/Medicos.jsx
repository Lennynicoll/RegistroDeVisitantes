import { useState, useEffect } from 'react';
import * as medicoService from '../api/medicoService';
import MedicoForm from '../components/MedicoForm';

export default function Medicos() {
  const [items, setItems] = useState([]);
  const [showForm, setShowForm] = useState(false);
  const [editingItem, setEditingItem] = useState(null);
  const [error, setError] = useState('');

  const fetchData = () => {
    medicoService.getAll()
      .then((res) => setItems(res.data))
      .catch((err) => setError(err.message));
  };

  useEffect(() => { fetchData(); }, []);

  const handleCreate = (data) => {
    medicoService.create(data)
      .then(() => { setShowForm(false); fetchData(); })
      .catch((err) => setError(err.message));
  };

  const handleUpdate = (data) => {
    medicoService.update(editingItem.id, data)
      .then(() => { setEditingItem(null); setShowForm(false); fetchData(); })
      .catch((err) => setError(err.message));
  };

  const handleDelete = (id) => {
    if (!confirm('Eliminar este anfitrion?')) return;
    medicoService.remove(id)
      .then(() => fetchData())
      .catch((err) => setError(err.message));
  };

  return (
    <div>
      <div className="page-header">
        <h1>Anfitriones</h1>
        <button className="btn btn-primary" onClick={() => { setEditingItem(null); setShowForm(!showForm); }}>
          {showForm ? 'Cerrar' : '+ Nuevo'}
        </button>
      </div>
      {error && <div className="error">{error}</div>}
      {showForm && (
        <MedicoForm
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
            <th>Especialidad</th>
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
              <td>{item.especialidad}</td>
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
            <tr><td colSpan={7} className="empty">No hay anfitriones</td></tr>
          )}
        </tbody>
      </table>
    </div>
  );
}
