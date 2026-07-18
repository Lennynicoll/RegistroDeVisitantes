using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RegistroVisitantes.Infrastructure.Context;
using RegistroVisitantes.Infrastructure.Interfaces;
using RegistroVisitantes.Infrastructure.Repositories;
using RegistroVisitantes.Application.Contract;
using RegistroVisitantes.Application.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("RegistroVisitantesDb"));

builder.Services.AddScoped<IVisitanteRepository, VisitanteRepository>();
builder.Services.AddScoped<IVisitaRepository, VisitaRepository>();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IMedicoRepository, MedicoRepository>();
builder.Services.AddScoped<IEspecialidadRepository, EspecialidadRepository>();
builder.Services.AddScoped<IDepartamentoRepository, DepartamentoRepository>();
builder.Services.AddScoped<ICitaRepository, CitaRepository>();
builder.Services.AddScoped<IHistorialClinicoRepository, HistorialClinicoRepository>();
builder.Services.AddScoped<IDiagnosticoRepository, DiagnosticoRepository>();
builder.Services.AddScoped<IMedicamentoRepository, MedicamentoRepository>();
builder.Services.AddScoped<IRecetaMedicaRepository, RecetaMedicaRepository>();
builder.Services.AddScoped<IDetalleRecetaRepository, DetalleRecetaRepository>();
builder.Services.AddScoped<ISeguroMedicoRepository, SeguroMedicoRepository>();
builder.Services.AddScoped<IPacienteSeguroRepository, PacienteSeguroRepository>();

builder.Services.AddScoped<IVisitanteService, VisitanteService>();
builder.Services.AddScoped<IVisitaService, VisitaService>();
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IMedicoService, MedicoService>();
builder.Services.AddScoped<IEspecialidadService, EspecialidadService>();
builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();
builder.Services.AddScoped<ICitaService, CitaService>();
builder.Services.AddScoped<IHistorialClinicoService, HistorialClinicoService>();
builder.Services.AddScoped<IDiagnosticoService, DiagnosticoService>();
builder.Services.AddScoped<IMedicamentoService, MedicamentoService>();
builder.Services.AddScoped<IRecetaMedicaService, RecetaMedicaService>();
builder.Services.AddScoped<IDetalleRecetaService, DetalleRecetaService>();
builder.Services.AddScoped<ISeguroMedicoService, SeguroMedicoService>();
builder.Services.AddScoped<IPacienteSeguroService, PacienteSeguroService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers()
    .AddApplicationPart(typeof(RegistroVisitantes.Infrastructure.Controllers.VisitantesController).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReact");

app.MapControllers();

app.Run();
