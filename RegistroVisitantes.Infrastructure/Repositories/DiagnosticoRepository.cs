using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Core;
using RegistroVisitantes.Infrastructure.Context;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Infrastructure.Repositories
{
    public class DiagnosticoRepository : BaseRepository<Diagnostico>, IDiagnosticoRepository
    {
        public DiagnosticoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
