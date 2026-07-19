using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Core;
using RegistroVisitantes.Infrastructure.Context;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Infrastructure.Repositories
{
    public class RegistroVisitaRepository : BaseRepository<RegistroVisita>, IRegistroVisitaRepository
    {
        public RegistroVisitaRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
