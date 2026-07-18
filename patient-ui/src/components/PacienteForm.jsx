import { useState, useEffect } from 'react';

const emptyForm = {
  nombre: '',
  apellido: '',
  fechaNacimiento: '',
  telefono: '',
  email: '',
  direccion: '',
};

export default function PacienteForm({ onSubmit, editingItem, onCancel }) {
  const [form, setForm] = useState(emptyForm);

  useEffect(() => {
    if (editingItem) {
      setForm({
        nombre: editingItem.nombre || '',
        apellido: editingItem.apellido || '',
        fechaNacimiento: editingItem.fechaNacimiento
          ? editingItem.fechaNacimiento.split('T')[0]
          : '',
        telefono: editingItem.telefono || '',
        email: editingItem.email || '',
        direccion: editingItem.direccion || '',
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
        Fecha Nac.
        <input name="fechaNacimiento" type="date" value={form.fechaNacimiento} onChange={handleChange} required />
      </label>
      <label>
        Telefono
        <input name="telefono" value={form.telefono} onChange={handleChange} />
      </label>
      <label>
        Email
        <input name="email" type="email" value={form.email} onChange={handleChange} />
      </label>
      <label>
        Direccion
        <input name="direccion" value={form.direccion} onChange={handleChange} />
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
