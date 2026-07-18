using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Core;
using RegistroVisitantes.Infrastructure.Context;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Infrastructure.Repositories
{
    public class PacienteSeguroRepository : BaseRepository<PacienteSeguro>, IPacienteSeguroRepository
    {
        public PacienteSeguroRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
