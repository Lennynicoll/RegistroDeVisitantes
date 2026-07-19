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
        public DbSet<RegistroVisitante> RegistroVisitantes { get; set; }
        public DbSet<Anfitrion> Anfitriones { get; set; }
        public DbSet<MotivoVisita> MotivosVisita { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<RegistroVisita> RegistroVisitas { get; set; }
        public DbSet<HistorialVisitas> HistorialesVisitas { get; set; }
        public DbSet<NotaVisita> NotasVisita { get; set; }
        public DbSet<Oficina> Oficinas { get; set; }
        public DbSet<PermisoVisita> PermisosVisita { get; set; }
        public DbSet<DetallePermiso> DetallesPermiso { get; set; }
        public DbSet<SeguridadEdificio> SeguridadEdificios { get; set; }
        public DbSet<VisitanteSeguro> VisitanteSeguros { get; set; }
    }
}
