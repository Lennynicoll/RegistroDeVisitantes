import { useState, useEffect } from 'react';

const emptyForm = {
  nombre: '',
  descripcion: '',
  presentacion: '',
  concentracion: '',
};

export default function MedicamentoForm({ onSubmit, editingItem, onCancel }) {
  const [form, setForm] = useState(emptyForm);

  useEffect(() => {
    if (editingItem) {
      setForm({
        nombre: editingItem.nombre || '',
        descripcion: editingItem.descripcion || '',
        presentacion: editingItem.presentacion || '',
        concentracion: editingItem.concentracion || '',
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
        Descripcion
        <input name="descripcion" value={form.descripcion} onChange={handleChange} />
      </label>
      <label>
        Ubicacion
        <input name="presentacion" value={form.presentacion} onChange={handleChange} />
      </label>
      <label>
        Capacidad
        <input name="concentracion" value={form.concentracion} onChange={handleChange} />
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
