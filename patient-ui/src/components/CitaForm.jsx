import { useState, useEffect } from 'react';

const emptyForm = {
  pacienteId: '',
  medicoId: '',
  fechaHora: '',
  motivo: '',
  estado: 'Programada',
};

export default function CitaForm({ onSubmit, editingItem, onCancel, pacientes, medicos }) {
  const [form, setForm] = useState(emptyForm);

  useEffect(() => {
    if (editingItem) {
      setForm({
        pacienteId: editingItem.pacienteId || '',
        medicoId: editingItem.medicoId || '',
        fechaHora: editingItem.fechaHora
          ? editingItem.fechaHora.slice(0, 16)
          : '',
        motivo: editingItem.motivo || '',
        estado: editingItem.estado || 'Programada',
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
        <h3>{editingItem ? 'Editar Cita' : 'Nueva Cita'}</h3>
        <form onSubmit={handleSubmit}>
          <label>
            Paciente
            <select name="pacienteId" value={form.pacienteId} onChange={handleChange} required>
              <option value="">Seleccione un paciente</option>
              {pacientes.map((p) => (
                <option key={p.id} value={p.id}>
                  {p.nombre} {p.apellido}
                </option>
              ))}
            </select>
          </label>
          <label>
            Medico
            <select name="medicoId" value={form.medicoId} onChange={handleChange} required>
              <option value="">Seleccione un medico</option>
              {medicos.map((m) => (
                <option key={m.id} value={m.id}>
                  {m.nombre} {m.apellido}
                </option>
              ))}
            </select>
          </label>
          <label>
            Fecha y Hora
            <input name="fechaHora" type="datetime-local" value={form.fechaHora} onChange={handleChange} required />
          </label>
          <label>
            Motivo
            <textarea name="motivo" value={form.motivo} onChange={handleChange} rows={3} />
          </label>
          <label>
            Estado
            <select name="estado" value={form.estado} onChange={handleChange}>
              <option value="Programada">Programada</option>
              <option value="Completada">Completada</option>
              <option value="Cancelada">Cancelada</option>
            </select>
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
