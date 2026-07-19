using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.RegistroVisitante;
using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Application.Service
{
    public class RegistroVisitanteService : BaseService<RegistroVisitanteDTO>, IRegistroVisitanteService
    {
        private readonly IRegistroVisitanteRepository _RegistroVisitanteRepository;

        public RegistroVisitanteService(IRegistroVisitanteRepository RegistroVisitanteRepository)
        {
            _RegistroVisitanteRepository = RegistroVisitanteRepository;
        }

        public async Task<ServiceResult> CreateWithValidationAsync(RegistroVisitanteDTO dto)
        {
            var errors = ValidatePaciente(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var RegistroVisitante = new RegistroVisitante
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

            var created = await _RegistroVisitanteRepository.CreateAsync(RegistroVisitante);

            return ServiceResult.Ok(MapToDTO(created), "RegistroVisitante creado exitosamente");
        }

        public async Task<ServiceResult> UpdateWithValidationAsync(int id, RegistroVisitanteDTO dto)
        {
            var existing = await _RegistroVisitanteRepository.GetByIdAsync(id);
            if (existing == null)
                return ServiceResult.Fail($"RegistroVisitante con ID {id} no encontrado");

            var errors = ValidatePaciente(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var RegistroVisitante = new RegistroVisitante
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

            var updated = await _RegistroVisitanteRepository.UpdateAsync(id, RegistroVisitante);

            return ServiceResult.Ok(MapToDTO(updated!), "RegistroVisitante actualizado exitosamente");
        }

        public async Task<IEnumerable<RegistroVisitanteDTO>> SearchByCedulaAsync(string cedula)
        {
            var RegistroVisitantes = await _RegistroVisitanteRepository.GetAllAsync();
            return RegistroVisitantes
                .Where(p => p.Cedula.Contains(cedula))
                .Select(MapToDTO);
        }

        public override async Task<IEnumerable<RegistroVisitanteDTO>> GetAllAsync()
        {
            var RegistroVisitantes = await _RegistroVisitanteRepository.GetAllAsync();
            return RegistroVisitantes.Select(MapToDTO);
        }

        public override async Task<RegistroVisitanteDTO?> GetByIdAsync(int id)
        {
            var RegistroVisitante = await _RegistroVisitanteRepository.GetByIdAsync(id);
            if (RegistroVisitante == null) return null;

            return MapToDTO(RegistroVisitante);
        }

        private static RegistroVisitanteDTO MapToDTO(RegistroVisitante entity)
        {
            return new RegistroVisitanteDTO
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

        private List<string> ValidatePaciente(RegistroVisitanteDTO dto)
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
