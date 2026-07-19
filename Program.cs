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
builder.Services.AddScoped<IRegistroVisitanteRepository, RegistroVisitanteRepository>();
builder.Services.AddScoped<IAnfitrionRepository, AnfitrionRepository>();
builder.Services.AddScoped<IMotivoVisitaRepository, MotivoVisitaRepository>();
builder.Services.AddScoped<IDepartamentoRepository, DepartamentoRepository>();
builder.Services.AddScoped<IRegistroVisitaRepository, RegistroVisitaRepository>();
builder.Services.AddScoped<IHistorialVisitasRepository, HistorialVisitasRepository>();
builder.Services.AddScoped<INotaVisitaRepository, NotaVisitaRepository>();
builder.Services.AddScoped<IOficinaRepository, OficinaRepository>();
builder.Services.AddScoped<IPermisoVisitaRepository, PermisoVisitaRepository>();
builder.Services.AddScoped<IDetallePermisoRepository, DetallePermisoRepository>();
builder.Services.AddScoped<ISeguridadEdificioRepository, SeguridadEdificioRepository>();
builder.Services.AddScoped<IVisitanteSeguroRepository, VisitanteSeguroRepository>();

builder.Services.AddScoped<IVisitanteService, VisitanteService>();
builder.Services.AddScoped<IVisitaService, VisitaService>();
builder.Services.AddScoped<IRegistroVisitanteService, RegistroVisitanteService>();
builder.Services.AddScoped<IAnfitrionService, AnfitrionService>();
builder.Services.AddScoped<IMotivoVisitaService, MotivoVisitaService>();
builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();
builder.Services.AddScoped<IRegistroVisitaService, RegistroVisitaService>();
builder.Services.AddScoped<IHistorialVisitasService, HistorialVisitasService>();
builder.Services.AddScoped<INotaVisitaService, NotaVisitaService>();
builder.Services.AddScoped<IOficinaService, OficinaService>();
builder.Services.AddScoped<IPermisoVisitaService, PermisoVisitaService>();
builder.Services.AddScoped<IDetallePermisoService, DetallePermisoService>();
builder.Services.AddScoped<ISeguridadEdificioService, SeguridadEdificioService>();
builder.Services.AddScoped<IVisitanteSeguroService, VisitanteSeguroService>();

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
