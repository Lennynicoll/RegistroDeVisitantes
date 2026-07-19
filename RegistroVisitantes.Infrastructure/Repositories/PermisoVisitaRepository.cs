using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Core;
using RegistroVisitantes.Infrastructure.Context;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Infrastructure.Repositories
{
    public class PermisoVisitaRepository : BaseRepository<PermisoVisita>, IPermisoVisitaRepository
    {
        public PermisoVisitaRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
