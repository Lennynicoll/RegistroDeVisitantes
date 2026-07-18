using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RegistroVisitantes.Infrastructure.Context;
using RegistroVisitantes.Infrastructure.Interfaces;
using RegistroVisitantes.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("RegistroVisitantesDb"));

builder.Services.AddScoped<IVisitanteRepository, VisitanteRepository>();
builder.Services.AddScoped<IVisitaRepository, VisitaRepository>();

builder.Services.AddControllers()
    .AddApplicationPart(typeof(RegistroVisitantes.Infrastructure.Controllers.VisitantesController).Assembly);

var app = builder.Build();

app.MapControllers();

app.Run();
