using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Core;
using RegistroVisitantes.Infrastructure.Context;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Infrastructure.Repositories
{
    public class MotivoVisitaRepository : BaseRepository<MotivoVisita>, IMotivoVisitaRepository
    {
        public MotivoVisitaRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
