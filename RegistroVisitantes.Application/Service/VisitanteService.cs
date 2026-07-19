using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Core;
using RegistroVisitantes.Application.Dtos.Visitante;
using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Application.Service
{
    public class VisitanteService : BaseService<VisitanteDTO>, IVisitanteService
    {
        private readonly IVisitanteRepository _visitanteRepository;

        public VisitanteService(IVisitanteRepository visitanteRepository)
        {
            _visitanteRepository = visitanteRepository;
        }

        public async Task<ServiceResult> CreateWithValidationAsync(VisitanteDTO dto)
        {
            var errors = ValidateVisitante(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var visitante = new Visitante
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Cedula = dto.Cedula,
                Correo = dto.Correo,
                Telefono = dto.Telefono
            };

            var created = await _visitanteRepository.CreateAsync(visitante);

            return ServiceResult.Ok(new VisitanteDTO
            {
                Id = created.Id,
                Nombre = created.Nombre,
                Apellido = created.Apellido,
                Cedula = created.Cedula,
                Correo = created.Correo,
                Telefono = created.Telefono
            }, "Visitante creado exitosamente");
        }

        public async Task<ServiceResult> UpdateWithValidationAsync(int id, VisitanteDTO dto)
        {
            var existing = await _visitanteRepository.GetByIdAsync(id);
            if (existing == null)
                return ServiceResult.Fail($"Visitante con ID {id} no encontrado");

            var errors = ValidateVisitante(dto);
            if (errors.Count > 0)
                return ServiceResult.Fail(errors);

            var visitante = new Visitante
            {
                Id = id,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Cedula = dto.Cedula,
                Correo = dto.Correo,
                Telefono = dto.Telefono
            };

            var updated = await _visitanteRepository.UpdateAsync(id, visitante);

            return ServiceResult.Ok(new VisitanteDTO
            {
                Id = updated!.Id,
                Nombre = updated.Nombre,
                Apellido = updated.Apellido,
                Cedula = updated.Cedula,
                Correo = updated.Correo,
                Telefono = updated.Telefono
            }, "Visitante actualizado exitosamente");
        }

        public async Task<IEnumerable<VisitanteDTO>> SearchByCedulaAsync(string cedula)
        {
            var visitantes = await _visitanteRepository.GetAllAsync();
            return visitantes
                .Where(v => v.Cedula.Contains(cedula))
                .Select(v => new VisitanteDTO
                {
                    Id = v.Id,
                    Nombre = v.Nombre,
                    Apellido = v.Apellido,
                    Cedula = v.Cedula,
                    Correo = v.Correo,
                    Telefono = v.Telefono
                });
        }

        public override async Task<IEnumerable<VisitanteDTO>> GetAllAsync()
        {
            var visitantes = await _visitanteRepository.GetAllAsync();
            return visitantes.Select(v => new VisitanteDTO
            {
                Id = v.Id,
                Nombre = v.Nombre,
                Apellido = v.Apellido,
                Cedula = v.Cedula,
                Correo = v.Correo,
                Telefono = v.Telefono
            });
        }

        public override async Task<VisitanteDTO?> GetByIdAsync(int id)
        {
            var visitante = await _visitanteRepository.GetByIdAsync(id);
            if (visitante == null) return null;

            return new VisitanteDTO
            {
                Id = visitante.Id,
                Nombre = visitante.Nombre,
                Apellido = visitante.Apellido,
                Cedula = visitante.Cedula,
                Correo = visitante.Correo,
                Telefono = visitante.Telefono
            };
        }

        private List<string> ValidateVisitante(VisitanteDTO dto)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                errors.Add("El nombre es requerido");

            if (string.IsNullOrWhiteSpace(dto.Apellido))
                errors.Add("El apellido es requerido");

            if (string.IsNullOrWhiteSpace(dto.Cedula))
                errors.Add("La cédula es requerida");
            else if (dto.Cedula.Length < 10 || dto.Cedula.Length > 13)
                errors.Add("La cédula debe tener entre 10 y 13 caracteres");

            if (string.IsNullOrWhiteSpace(dto.Correo))
                errors.Add("El correo es requerido");
            else if (!dto.Correo.Contains("@") || !dto.Correo.Contains("."))
                errors.Add("El correo no tiene un formato válido");

            if (!string.IsNullOrWhiteSpace(dto.Telefono) && dto.Telefono.Length < 7)
                errors.Add("El teléfono debe tener al menos 7 caracteres");

            return errors;
        }
    }
}
