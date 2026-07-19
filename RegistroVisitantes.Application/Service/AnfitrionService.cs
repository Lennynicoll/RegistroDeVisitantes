using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.Anfitrion;
using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Application.Service
{
    public class AnfitrionService : BaseService<AnfitrionDTO>, IAnfitrionService
    {
        private readonly IAnfitrionRepository _AnfitrionRepository;
        private readonly IMotivoVisitaRepository _MotivoVisitaRepository;
        private readonly IDepartamentoRepository _departamentoRepository;

        public AnfitrionService(IAnfitrionRepository AnfitrionRepository, IMotivoVisitaRepository MotivoVisitaRepository, IDepartamentoRepository departamentoRepository)
        {
            _AnfitrionRepository = AnfitrionRepository;
            _MotivoVisitaRepository = MotivoVisitaRepository;
            _departamentoRepository = departamentoRepository;
        }

        public async Task<ServiceResult> CreateWithValidationAsync(AnfitrionDTO dto)
        {
            var errors = await ValidateMedico(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var Anfitrion = new Anfitrion
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Cedula = dto.Cedula,
                Telefono = dto.Telefono,
                Correo = dto.Correo,
                HorarioAtencion = dto.HorarioAtencion,
                MotivoVisitaId = dto.MotivoVisitaId,
                DepartamentoId = dto.DepartamentoId
            };

            var created = await _AnfitrionRepository.CreateAsync(Anfitrion);

            return ServiceResult.Ok(MapToDTO(created), "Médico creado exitosamente");
        }

        public async Task<ServiceResult> UpdateWithValidationAsync(int id, AnfitrionDTO dto)
        {
            var existing = await _AnfitrionRepository.GetByIdAsync(id);
            if (existing == null)
                return ServiceResult.Fail($"Médico con ID {id} no encontrado");

            var errors = await ValidateMedico(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var Anfitrion = new Anfitrion
            {
                Id = id,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Cedula = dto.Cedula,
                Telefono = dto.Telefono,
                Correo = dto.Correo,
                HorarioAtencion = dto.HorarioAtencion,
                MotivoVisitaId = dto.MotivoVisitaId,
                DepartamentoId = dto.DepartamentoId
            };

            var updated = await _AnfitrionRepository.UpdateAsync(id, Anfitrion);

            return ServiceResult.Ok(MapToDTO(updated!), "Médico actualizado exitosamente");
        }

        public async Task<IEnumerable<AnfitrionDTO>> GetByEspecialidadIdAsync(int MotivoVisitaId)
        {
            var Anfitriones = await _AnfitrionRepository.GetAllAsync();
            return Anfitriones
                .Where(m => m.MotivoVisitaId == MotivoVisitaId)
                .Select(MapToDTO);
        }

        public override async Task<IEnumerable<AnfitrionDTO>> GetAllAsync()
        {
            var Anfitriones = await _AnfitrionRepository.GetAllAsync();
            return Anfitriones.Select(MapToDTO);
        }

        public override async Task<AnfitrionDTO?> GetByIdAsync(int id)
        {
            var Anfitrion = await _AnfitrionRepository.GetByIdAsync(id);
            if (Anfitrion == null) return null;

            return MapToDTO(Anfitrion);
        }

        private static AnfitrionDTO MapToDTO(Anfitrion entity)
        {
            return new AnfitrionDTO
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Apellido = entity.Apellido,
                Cedula = entity.Cedula,
                Telefono = entity.Telefono,
                Correo = entity.Correo,
                HorarioAtencion = entity.HorarioAtencion,
                MotivoVisitaId = entity.MotivoVisitaId,
                DepartamentoId = entity.DepartamentoId
            };
        }

        private async Task<List<string>> ValidateMedico(AnfitrionDTO dto)
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

            if (dto.MotivoVisitaId <= 0)
                errors.Add("El ID de MotivoVisita es requerido");
            else
            {
                var especialidadExists = await _MotivoVisitaRepository.GetByIdAsync(dto.MotivoVisitaId);
                if (especialidadExists == null)
                    errors.Add($"La MotivoVisita con ID {dto.MotivoVisitaId} no existe");
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
