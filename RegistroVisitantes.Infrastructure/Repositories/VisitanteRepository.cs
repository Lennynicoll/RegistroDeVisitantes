using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Core;
using RegistroVisitantes.Infrastructure.Context;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Infrastructure.Repositories
{
    public class VisitanteRepository : BaseRepository<Visitante>, IVisitanteRepository
    {
        public VisitanteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
