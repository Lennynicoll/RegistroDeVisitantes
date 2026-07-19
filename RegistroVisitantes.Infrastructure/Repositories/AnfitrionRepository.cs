using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Core;
using RegistroVisitantes.Infrastructure.Context;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Infrastructure.Repositories
{
    public class AnfitrionRepository : BaseRepository<Anfitrion>, IAnfitrionRepository
    {
        public AnfitrionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
