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
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<HistorialClinico> HistorialesClinicos { get; set; }
        public DbSet<Diagnostico> Diagnosticos { get; set; }
        public DbSet<Medicamento> Medicamentos { get; set; }
        public DbSet<RecetaMedica> RecetasMedicas { get; set; }
        public DbSet<DetalleReceta> DetallesReceta { get; set; }
        public DbSet<SeguroMedico> SegurosMedicos { get; set; }
        public DbSet<PacienteSeguro> PacienteSeguros { get; set; }
    }
}
