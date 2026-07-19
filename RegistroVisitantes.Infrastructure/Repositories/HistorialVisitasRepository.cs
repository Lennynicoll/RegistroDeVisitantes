using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Core;
using RegistroVisitantes.Infrastructure.Context;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Infrastructure.Repositories
{
    public class HistorialVisitasRepository : BaseRepository<HistorialVisitas>, IHistorialVisitasRepository
    {
        public HistorialVisitasRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
