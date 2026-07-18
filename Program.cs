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

builder.Services.AddScoped<IVisitanteService, VisitanteService>();
builder.Services.AddScoped<IVisitaService, VisitaService>();

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

app.MapControllers();

app.Run();
