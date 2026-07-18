using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.Medico;
using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Application.Service
{
    public class MedicoService : BaseService<MedicoDTO>, IMedicoService
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IEspecialidadRepository _especialidadRepository;
        private readonly IDepartamentoRepository _departamentoRepository;

        public MedicoService(IMedicoRepository medicoRepository, IEspecialidadRepository especialidadRepository, IDepartamentoRepository departamentoRepository)
        {
            _medicoRepository = medicoRepository;
            _especialidadRepository = especialidadRepository;
            _departamentoRepository = departamentoRepository;
        }

        public async Task<ServiceResult> CreateWithValidationAsync(MedicoDTO dto)
        {
            var errors = await ValidateMedico(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var medico = new Medico
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Cedula = dto.Cedula,
                Telefono = dto.Telefono,
                Correo = dto.Correo,
                HorarioAtencion = dto.HorarioAtencion,
                EspecialidadId = dto.EspecialidadId,
                DepartamentoId = dto.DepartamentoId
            };

            var created = await _medicoRepository.CreateAsync(medico);

            return ServiceResult.Ok(MapToDTO(created), "Médico creado exitosamente");
        }

        public async Task<ServiceResult> UpdateWithValidationAsync(int id, MedicoDTO dto)
        {
            var existing = await _medicoRepository.GetByIdAsync(id);
            if (existing == null)
                return ServiceResult.Fail($"Médico con ID {id} no encontrado");

            var errors = await ValidateMedico(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var medico = new Medico
            {
                Id = id,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Cedula = dto.Cedula,
                Telefono = dto.Telefono,
                Correo = dto.Correo,
                HorarioAtencion = dto.HorarioAtencion,
                EspecialidadId = dto.EspecialidadId,
                DepartamentoId = dto.DepartamentoId
            };

            var updated = await _medicoRepository.UpdateAsync(id, medico);

            return ServiceResult.Ok(MapToDTO(updated!), "Médico actualizado exitosamente");
        }

        public async Task<IEnumerable<MedicoDTO>> GetByEspecialidadIdAsync(int especialidadId)
        {
            var medicos = await _medicoRepository.GetAllAsync();
            return medicos
                .Where(m => m.EspecialidadId == especialidadId)
                .Select(MapToDTO);
        }

        public override async Task<IEnumerable<MedicoDTO>> GetAllAsync()
        {
            var medicos = await _medicoRepository.GetAllAsync();
            return medicos.Select(MapToDTO);
        }

        public override async Task<MedicoDTO?> GetByIdAsync(int id)
        {
            var medico = await _medicoRepository.GetByIdAsync(id);
            if (medico == null) return null;

            return MapToDTO(medico);
        }

        private static MedicoDTO MapToDTO(Medico entity)
        {
            return new MedicoDTO
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Apellido = entity.Apellido,
                Cedula = entity.Cedula,
                Telefono = entity.Telefono,
                Correo = entity.Correo,
                HorarioAtencion = entity.HorarioAtencion,
                EspecialidadId = entity.EspecialidadId,
                DepartamentoId = entity.DepartamentoId
            };
        }

        private async Task<List<string>> ValidateMedico(MedicoDTO dto)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                errors.Add("El nombre es requerido");
            else if (dto.Nombre.Length > 100)
                errors.Add("El nombre no puede exceder 100 caracteres");

            if (string.IsNullOrWhiteSpace(dto.Apellido))
                errors.Add("El apellido es requerido");
            else if (dto.Apellido.Length > 100)
                errors.Add("El apellido no puede exceder 100 caracteres");

            if (string.IsNullOrWhiteSpace(dto.Cedula))
                errors.Add("La cédula es requerida");
            else if (dto.Cedula.Length < 10 || dto.Cedula.Length > 13)
                errors.Add("La cédula debe tener entre 10 y 13 caracteres");

            if (string.IsNullOrWhiteSpace(dto.Telefono))
                errors.Add("El teléfono es requerido");
            else if (dto.Telefono.Length < 7)
                errors.Add("El teléfono debe tener al menos 7 caracteres");

            if (string.IsNullOrWhiteSpace(dto.Correo))
                errors.Add("El correo es requerido");
            else if (!dto.Correo.Contains("@") || !dto.Correo.Contains("."))
                errors.Add("El correo no tiene un formato válido");

            if (!string.IsNullOrWhiteSpace(dto.HorarioAtencion) && dto.HorarioAtencion.Length > 100)
                errors.Add("El horario de atención no puede exceder 100 caracteres");

            if (dto.EspecialidadId <= 0)
                errors.Add("El ID de especialidad es requerido");
            else
            {
                var especialidadExists = await _especialidadRepository.GetByIdAsync(dto.EspecialidadId);
                if (especialidadExists == null)
                    errors.Add($"La especialidad con ID {dto.EspecialidadId} no existe");
            }

            if (dto.DepartamentoId <= 0)
                errors.Add("El ID de departamento es requerido");
            else
            {
                var departamentoExists = await _departamentoRepository.GetByIdAsync(dto.DepartamentoId);
                if (departamentoExists == null)
                    errors.Add($"El departamento con ID {dto.DepartamentoId} no existe");
            }

            return errors;
        }
    }
}
