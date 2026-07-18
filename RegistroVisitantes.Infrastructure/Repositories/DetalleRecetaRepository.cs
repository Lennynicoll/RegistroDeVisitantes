using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Core;
using RegistroVisitantes.Infrastructure.Context;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Infrastructure.Repositories
{
    public class DetalleRecetaRepository : BaseRepository<DetalleReceta>, IDetalleRecetaRepository
    {
        public DetalleRecetaRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
