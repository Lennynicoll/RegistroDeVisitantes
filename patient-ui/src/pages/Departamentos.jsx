import { useState, useEffect } from 'react';
import * as departamentoService from '../api/departamentoService';

export default function Departamentos() {
  const [items, setItems] = useState([]);
  const [form, setForm] = useState({ nombre: '', descripcion: '' });
  const [editingItem, setEditingItem] = useState(null);
  const [showForm, setShowForm] = useState(false);
  const [error, setError] = useState('');

  const fetchData = () => {
    departamentoService.getAll()
      .then((res) => setItems(res.data))
      .catch((err) => setError(err.message));
  };

  useEffect(() => { fetchData(); }, []);

  const handleSubmit = (e) => {
    e.preventDefault();
    const action = editingItem
      ? departamentoService.update(editingItem.id, form)
      : departamentoService.create(form);
    action
      .then(() => { setForm({ nombre: '', descripcion: '' }); setEditingItem(null); setShowForm(false); fetchData(); })
      .catch((err) => setError(err.message));
  };

  const handleDelete = (id) => {
    if (!confirm('Eliminar este departamento?')) return;
    departamentoService.remove(id)
      .then(() => fetchData())
      .catch((err) => setError(err.message));
  };

  return (
    <div>
      <div className="page-header">
        <h1>Departamentos</h1>
        <button className="btn btn-primary" onClick={() => { setForm({ nombre: '', descripcion: '' }); setEditingItem(null); setShowForm(!showForm); }}>
          {showForm ? 'Cerrar' : '+ Nuevo'}
        </button>
      </div>
      {error && <div className="error">{error}</div>}
      {showForm && (
        <form className="form-row" onSubmit={handleSubmit}>
          <label>
            Nombre
            <input value={form.nombre} onChange={(e) => setForm({ ...form, nombre: e.target.value })} required />
          </label>
          <label>
            Descripcion
            <input value={form.descripcion} onChange={(e) => setForm({ ...form, descripcion: e.target.value })} />
          </label>
          <button type="submit" className="btn btn-primary">
            {editingItem ? 'Actualizar' : 'Guardar'}
          </button>
          <button type="button" className="btn btn-secondary" onClick={() => { setShowForm(false); setEditingItem(null); }}>
            Cancelar
          </button>
        </form>
      )}
      <table>
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
              <td>
                <button className="btn btn-edit" onClick={() => { setEditingItem(item); setForm({ nombre: item.nombre, descripcion: item.descripcion || '' }); setShowForm(true); }}>
                  Editar
                </button>
                <button className="btn btn-delete" onClick={() => handleDelete(item.id)}>
                  Eliminar
                </button>
              </td>
            </tr>
          ))}
          {items.length === 0 && (
            <tr><td colSpan={4} className="empty">No hay departamentos</td></tr>
          )}
        </tbody>
      </table>
    </div>
  );
}
