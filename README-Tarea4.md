# Registro de Visitantes - Tarea 4

## Qué hice

Agregué la capa de Servicios al proyecto. Ahora los Controllers no hablan directo con los repositorios, sino que pasan por un servicio que se encarga de validar los datos antes de guardarlos.

## Estructura

```
RegistroVisitantes.Application/
├── Contract/
│   ├── IVisitanteService.cs
│   └── IVisitaService.cs
├── Core/
│   ├── IBaseService.cs
│   ├── BaseService.cs
│   └── ServiceResult.cs
├── Dtos/
│   ├── DtoBase.cs
│   ├── Visitante/VisitanteDTO.cs
│   └── Visita/VisitaDTO.cs
└── Service/
    ├── VisitanteService.cs
    └── VisitaService.cs
```

## Qué validaciones hice

**Visitante:**
- Nombre y apellido son requeridos
- Cédula debe tener entre 10 y 13 caracteres
- Correo debe tener formato válido (contiene @ y .)
- Teléfono al menos 7 caracteres

**Visita:**
- Fecha y hora son requeridas
- Motivo debe tener al menos 3 caracteres
- El visitante debe existir en la base de datos

## Endpoints nuevos

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | /api/Visitantes/cedula/{cedula} | Buscar visitante por cédula |
| GET | /api/Visitas/visitante/{id} | Buscar visitas por visitante |

## Tecnologías

- ASP.NET Core 10
- Entity Framework Core (InMemory)
- Patrón Servicio
