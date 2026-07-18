import { useState, useEffect } from 'react';
import * as medicamentoService from '../api/medicamentoService';
import MedicamentoForm from '../components/MedicamentoForm';

export default function Medicamentos() {
  const [items, setItems] = useState([]);
  const [showForm, setShowForm] = useState(false);
  const [editingItem, setEditingItem] = useState(null);
  const [error, setError] = useState('');

  const fetchData = () => {
    medicamentoService
      .getAll()
      .then((res) => setItems(res.data))
      .catch((err) => setError(err.message));
  };

  useEffect(() => {
    fetchData();
  }, []);

  const handleCreate = (data) => {
    medicamentoService
      .create(data)
      .then(() => {
        setShowForm(false);
        fetchData();
      })
      .catch((err) => setError(err.message));
  };

  const handleUpdate = (data) => {
    medicamentoService
      .update(editingItem.id, data)
      .then(() => {
        setEditingItem(null);
        setShowForm(false);
        fetchData();
      })
      .catch((err) => setError(err.message));
  };

  const handleDelete = (id) => {
    if (!confirm('¿Eliminar este medicamento?')) return;
    medicamentoService
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
        <h1>Medicamentos</h1>
        <button className="btn btn-primary" onClick={openCreate}>
          + Nuevo Medicamento
        </button>
      </div>
      {error && <div className="error">{error}</div>}
      <table className="table">
        <thead>
          <tr>
            <th>ID</th>
            <th>Nombre</th>
            <th>Descripcion</th>
            <th>Presentacion</th>
            <th>Concentracion</th>
            <th>Acciones</th>
          </tr>
        </thead>
        <tbody>
          {items.map((item) => (
            <tr key={item.id}>
              <td>{item.id}</td>
              <td>{item.nombre}</td>
              <td>{item.descripcion}</td>
              <td>{item.presentacion}</td>
              <td>{item.concentracion}</td>
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
              <td colSpan={6} className="empty">
                No hay medicamentos registrados
              </td>
            </tr>
          )}
        </tbody>
      </table>
      {showForm && (
        <MedicamentoForm
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
