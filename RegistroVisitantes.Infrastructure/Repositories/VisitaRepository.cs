using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Core;
using RegistroVisitantes.Infrastructure.Context;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Infrastructure.Repositories
{
    public class VisitaRepository : BaseRepository<Visita>, IVisitaRepository
    {
        public VisitaRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
