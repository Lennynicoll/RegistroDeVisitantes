using Microsoft.EntityFrameworkCore;
using RegistroVisitantes.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("RegistroVisitantesDb"));

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
