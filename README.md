# Registro de Visitantes - Tarea 3

## Qué hice

Reorganicé el proyecto creando las capas de Dominio e Infraestructura como pide la tarea. Todo estaba en la raíz y ahora está separado correctamente.

## Estructura

```
RegistroVisitantes.Domain/
├── Core/BaseEntity.cs
└── Entities/Visitante.cs, Visita.cs

RegistroVisitantes.Infrastructure/
├── Context/ApplicationDbContext.cs
├── Controllers/VisitantesController.cs, VisitasController.cs
├── Core/BaseRepository.cs
├── Exceptions/
├── Interfaces/IVisitanteRepository.cs, IVisitaRepository.cs
├── Models/VisitanteDTO.cs, VisitaDTO.cs
└── Repositories/VisitanteRepository.cs, VisitaRepository.cs
```

## Cómo correr

```
dotnet run
```

El API arranca en http://localhost:5297

## Endpoints

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | /api/Visitantes | Lista visitantes |
| POST | /api/Visitantes | Crear visitante |
| PUT | /api/Visitantes/{id} | Actualizar visitante |
| DELETE | /api/Visitantes/{id} | Eliminar visitante |
| GET | /api/Visitas | Lista visitas |
| POST | /api/Visitas | Crear visita |
| PUT | /api/Visitas/{id} | Actualizar visita |
| DELETE | /api/Visitas/{id} | Eliminar visita |

## Tecnologías

- ASP.NET Core 10
- Entity Framework Core (InMemory)
- Patrón Repositorio
