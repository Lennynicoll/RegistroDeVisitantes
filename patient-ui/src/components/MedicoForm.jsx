import { useState, useEffect } from 'react';

const emptyForm = {
  nombre: '',
  apellido: '',
  especialidad: '',
  telefono: '',
  email: '',
};

export default function MedicoForm({ onSubmit, editingItem, onCancel }) {
  const [form, setForm] = useState(emptyForm);

  useEffect(() => {
    if (editingItem) {
      setForm({
        nombre: editingItem.nombre || '',
        apellido: editingItem.apellido || '',
        especialidad: editingItem.especialidad || '',
        telefono: editingItem.telefono || '',
        email: editingItem.email || '',
      });
    } else {
      setForm(emptyForm);
    }
  }, [editingItem]);

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    onSubmit(form);
    setForm(emptyForm);
  };

  return (
    <form className="form-row" onSubmit={handleSubmit}>
      <label>
        Nombre
        <input name="nombre" value={form.nombre} onChange={handleChange} required />
      </label>
      <label>
        Apellido
        <input name="apellido" value={form.apellido} onChange={handleChange} required />
      </label>
      <label>
        Especialidad
        <input name="especialidad" value={form.especialidad} onChange={handleChange} />
      </label>
      <label>
        Telefono
        <input name="telefono" value={form.telefono} onChange={handleChange} />
      </label>
      <label>
        Email
        <input name="email" type="email" value={form.email} onChange={handleChange} />
      </label>
      <button type="submit" className="btn btn-primary">
        {editingItem ? 'Actualizar' : 'Guardar'}
      </button>
      <button type="button" className="btn btn-secondary" onClick={onCancel}>
        Cancelar
      </button>
    </form>
  );
}
