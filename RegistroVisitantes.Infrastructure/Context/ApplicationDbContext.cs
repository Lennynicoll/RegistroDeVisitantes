using Microsoft.EntityFrameworkCore;
using RegistroVisitantes.Domain.Entities;

namespace RegistroVisitantes.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Visitante> Visitantes { get; set; }
        public DbSet<Visita> Visitas { get; set; }
    }
}
