import { useState, useEffect } from 'react';
import * as especialidadService from '../api/especialidadService';

export default function Especialidades() {
  const [items, setItems] = useState([]);
  const [form, setForm] = useState({ nombre: '', descripcion: '' });
  const [editingItem, setEditingItem] = useState(null);
  const [showForm, setShowForm] = useState(false);
  const [error, setError] = useState('');

  const fetchData = () => {
    especialidadService
      .getAll()
      .then((res) => setItems(res.data))
      .catch((err) => setError(err.message));
  };

  useEffect(() => {
    fetchData();
  }, []);

  const resetForm = () => {
    setForm({ nombre: '', descripcion: '' });
    setEditingItem(null);
    setShowForm(false);
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    const action = editingItem
      ? especialidadService.update(editingItem.id, form)
      : especialidadService.create(form);
    action
      .then(() => {
        resetForm();
        fetchData();
      })
      .catch((err) => setError(err.message));
  };

  const handleDelete = (id) => {
    if (!confirm('¿Eliminar esta especialidad?')) return;
    especialidadService
      .remove(id)
      .then(() => fetchData())
      .catch((err) => setError(err.message));
  };

  const openEdit = (item) => {
    setEditingItem(item);
    setForm({ nombre: item.nombre, descripcion: item.descripcion || '' });
    setShowForm(true);
  };

  return (
    <div className="page">
      <div className="page-header">
        <h1>Especialidades</h1>
        <button className="btn btn-primary" onClick={() => { resetForm(); setShowForm(true); }}>
          + Nueva Especialidad
        </button>
      </div>
      {error && <div className="error">{error}</div>}
      {showForm && (
        <form className="inline-form" onSubmit={handleSubmit}>
          <input
            placeholder="Nombre"
            value={form.nombre}
            onChange={(e) => setForm({ ...form, nombre: e.target.value })}
            required
          />
          <input
            placeholder="Descripcion"
            value={form.descripcion}
            onChange={(e) => setForm({ ...form, descripcion: e.target.value })}
          />
          <button type="submit" className="btn btn-primary">
            {editingItem ? 'Actualizar' : 'Guardar'}
          </button>
          <button type="button" className="btn btn-secondary" onClick={resetForm}>
            Cancelar
          </button>
        </form>
      )}
      <table className="table">
        <thead>
          <tr>
            <th>ID</th>
            <th>Nombre</th>
            <th>Descripcion</th>
            <th>Acciones</th>
          </tr>
        </thead>
        <tbody>
          {items.map((item) => (
            <tr key={item.id}>
              <td>{item.id}</td>
              <td>{item.nombre}</td>
              <td>{item.descripcion}</td>
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
              <td colSpan={4} className="empty">
                No hay especialidades registradas
              </td>
            </tr>
          )}
        </tbody>
      </table>
    </div>
  );
}
