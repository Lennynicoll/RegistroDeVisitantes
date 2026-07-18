using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Core;
using RegistroVisitantes.Infrastructure.Context;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Infrastructure.Repositories
{
    public class RecetaMedicaRepository : BaseRepository<RecetaMedica>, IRecetaMedicaRepository
    {
        public RecetaMedicaRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
