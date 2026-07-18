using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.Paciente;
using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Application.Service
{
    public class PacienteService : BaseService<PacienteDTO>, IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteService(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<ServiceResult> CreateWithValidationAsync(PacienteDTO dto)
        {
            var errors = ValidatePaciente(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var paciente = new Paciente
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Cedula = dto.Cedula,
                FechaNacimiento = dto.FechaNacimiento,
                Genero = dto.Genero,
                Direccion = dto.Direccion,
                Telefono = dto.Telefono,
                Correo = dto.Correo,
                TipoSangre = dto.TipoSangre
            };

            var created = await _pacienteRepository.CreateAsync(paciente);

            return ServiceResult.Ok(MapToDTO(created), "Paciente creado exitosamente");
        }

        public async Task<ServiceResult> UpdateWithValidationAsync(int id, PacienteDTO dto)
        {
            var existing = await _pacienteRepository.GetByIdAsync(id);
            if (existing == null)
                return ServiceResult.Fail($"Paciente con ID {id} no encontrado");

            var errors = ValidatePaciente(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var paciente = new Paciente
            {
                Id = id,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Cedula = dto.Cedula,
                FechaNacimiento = dto.FechaNacimiento,
                Genero = dto.Genero,
                Direccion = dto.Direccion,
                Telefono = dto.Telefono,
                Correo = dto.Correo,
                TipoSangre = dto.TipoSangre
            };

            var updated = await _pacienteRepository.UpdateAsync(id, paciente);

            return ServiceResult.Ok(MapToDTO(updated!), "Paciente actualizado exitosamente");
        }

        public async Task<IEnumerable<PacienteDTO>> SearchByCedulaAsync(string cedula)
        {
            var pacientes = await _pacienteRepository.GetAllAsync();
            return pacientes
                .Where(p => p.Cedula.Contains(cedula))
                .Select(MapToDTO);
        }

        public override async Task<IEnumerable<PacienteDTO>> GetAllAsync()
        {
            var pacientes = await _pacienteRepository.GetAllAsync();
            return pacientes.Select(MapToDTO);
        }

        public override async Task<PacienteDTO?> GetByIdAsync(int id)
        {
            var paciente = await _pacienteRepository.GetByIdAsync(id);
            if (paciente == null) return null;

            return MapToDTO(paciente);
        }

        private static PacienteDTO MapToDTO(Paciente entity)
        {
            return new PacienteDTO
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Apellido = entity.Apellido,
                Cedula = entity.Cedula,
                FechaNacimiento = entity.FechaNacimiento,
                Genero = entity.Genero,
                Direccion = entity.Direccion,
                Telefono = entity.Telefono,
                Correo = entity.Correo,
                TipoSangre = entity.TipoSangre
            };
        }

        private List<string> ValidatePaciente(PacienteDTO dto)
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

            if (dto.FechaNacimiento == default)
                errors.Add("La fecha de nacimiento es requerida");
            else if (dto.FechaNacimiento > DateTime.Now)
                errors.Add("La fecha de nacimiento no puede ser futura");

            if (string.IsNullOrWhiteSpace(dto.Genero))
                errors.Add("El género es requerido");

            if (string.IsNullOrWhiteSpace(dto.Direccion))
                errors.Add("La dirección es requerida");
            else if (dto.Direccion.Length > 200)
                errors.Add("La dirección no puede exceder 200 caracteres");

            if (string.IsNullOrWhiteSpace(dto.Telefono))
                errors.Add("El teléfono es requerido");
            else if (dto.Telefono.Length < 7)
                errors.Add("El teléfono debe tener al menos 7 caracteres");

            if (string.IsNullOrWhiteSpace(dto.Correo))
                errors.Add("El correo es requerido");
            else if (!dto.Correo.Contains("@") || !dto.Correo.Contains("."))
                errors.Add("El correo no tiene un formato válido");

            if (!string.IsNullOrWhiteSpace(dto.TipoSangre) && dto.TipoSangre.Length > 10)
                errors.Add("El tipo de sangre no puede exceder 10 caracteres");

            return errors;
        }
    }
}
