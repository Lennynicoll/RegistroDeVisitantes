using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Core;
using RegistroVisitantes.Infrastructure.Context;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Infrastructure.Repositories
{
    public class HistorialClinicoRepository : BaseRepository<HistorialClinico>, IHistorialClinicoRepository
    {
        public HistorialClinicoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
