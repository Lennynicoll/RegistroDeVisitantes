using RegistroVisitantes.Domain.Entities;
using RegistroVisitantes.Infrastructure.Core;
using RegistroVisitantes.Infrastructure.Context;
using RegistroVisitantes.Infrastructure.Interfaces;

namespace RegistroVisitantes.Infrastructure.Repositories
{
    public class MedicamentoRepository : BaseRepository<Medicamento>, IMedicamentoRepository
    {
        public MedicamentoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
