using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Core;
using RegistroVisitantes.Infrastructure.Context;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Infrastructure.Repositories
{
    public class SeguroMedicoRepository : BaseRepository<SeguroMedico>, ISeguroMedicoRepository
    {
        public SeguroMedicoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
