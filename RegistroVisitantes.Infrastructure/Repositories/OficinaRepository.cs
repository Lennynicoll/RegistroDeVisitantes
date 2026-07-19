using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Core;
using RegistroVisitantes.Infrastructure.Context;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Infrastructure.Repositories
{
    public class OficinaRepository : BaseRepository<Oficina>, IOficinaRepository
    {
        public OficinaRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
