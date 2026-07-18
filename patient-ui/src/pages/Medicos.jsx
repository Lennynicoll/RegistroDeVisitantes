import { useState, useEffect } from 'react';
import * as medicoService from '../api/medicoService';
import MedicoForm from '../components/MedicoForm';

export default function Medicos() {
  const [items, setItems] = useState([]);
  const [showForm, setShowForm] = useState(false);
  const [editingItem, setEditingItem] = useState(null);
  const [error, setError] = useState('');

  const fetchData = () => {
    medicoService
      .getAll()
      .then((res) => setItems(res.data))
      .catch((err) => setError(err.message));
  };

  useEffect(() => {
    fetchData();
  }, []);

  const handleCreate = (data) => {
    medicoService
      .create(data)
      .then(() => {
        setShowForm(false);
        fetchData();
      })
      .catch((err) => setError(err.message));
  };

  const handleUpdate = (data) => {
    medicoService
      .update(editingItem.id, data)
      .then(() => {
        setEditingItem(null);
        setShowForm(false);
        fetchData();
      })
      .catch((err) => setError(err.message));
  };

  const handleDelete = (id) => {
    if (!confirm('¿Eliminar este medico?')) return;
    medicoService
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

  return (
    <div className="page">
      <div className="page-header">
        <h1>Medicos</h1>
        <button className="btn btn-primary" onClick={openCreate}>
          + Nuevo Medico
        </button>
      </div>
      {error && <div className="error">{error}</div>}
      <table className="table">
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
                No hay medicos registrados
              </td>
            </tr>
          )}
        </tbody>
      </table>
      {showForm && (
        <MedicoForm
          onSubmit={editingItem ? handleUpdate : handleCreate}
          editingItem={editingItem}
          onCancel={() => {
            setShowForm(false);
            setEditingItem(null);
          }}
        />
      )}
    </div>
  );
}
