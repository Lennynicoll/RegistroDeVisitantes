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
    <div className="modal-overlay">
      <div className="modal">
        <h3>{editingItem ? 'Editar Medicamento' : 'Nuevo Medicamento'}</h3>
        <form onSubmit={handleSubmit}>
          <label>
            Nombre
            <input name="nombre" value={form.nombre} onChange={handleChange} required />
          </label>
          <label>
            Descripcion
            <textarea name="descripcion" value={form.descripcion} onChange={handleChange} rows={3} />
          </label>
          <label>
            Presentacion
            <input name="presentacion" value={form.presentacion} onChange={handleChange} />
          </label>
          <label>
            Concentracion
            <input name="concentracion" value={form.concentracion} onChange={handleChange} />
          </label>
          <div className="modal-actions">
            <button type="submit" className="btn btn-primary">
              {editingItem ? 'Actualizar' : 'Guardar'}
            </button>
            <button type="button" className="btn btn-secondary" onClick={onCancel}>
              Cancelar
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
