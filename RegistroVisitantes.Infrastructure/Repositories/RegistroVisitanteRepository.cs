using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Core;
using RegistroVisitantes.Infrastructure.Context;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Infrastructure.Repositories
{
    public class RegistroVisitanteRepository : BaseRepository<RegistroVisitante>, IRegistroVisitanteRepository
    {
        public RegistroVisitanteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
