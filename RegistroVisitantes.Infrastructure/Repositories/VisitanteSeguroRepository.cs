using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Core;
using RegistroVisitantes.Infrastructure.Context;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Infrastructure.Repositories
{
    public class VisitanteSeguroRepository : BaseRepository<VisitanteSeguro>, IVisitanteSeguroRepository
    {
        public VisitanteSeguroRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
