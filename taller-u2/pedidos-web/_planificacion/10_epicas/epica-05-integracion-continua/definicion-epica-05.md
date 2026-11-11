# Épica 05 · Integración continua

- **Estado:** Pendiente
- **Objetivo:** que cada push compile la solución y ejecute los tests de forma automática.

## Alcance

1. Pipeline de CI que ejecuta `dotnet build` y `dotnet test` en cada push (AUD-01-005).
2. Plataforma de CI: `PENDIENTE_DESARROLLADOR`.

## Iteraciones

Se definen al cerrar la Épica 04.

| Iteración | Nombre |
| :-------- | :----- |
| 05-01 | Pipeline de compilación y tests |

## Criterio de término

- Un push con un test roto deja el pipeline en rojo, y uno con los tests en verde lo deja en verde.

## Revisión al cierre

Se completa al cerrar la épica, en pocas líneas: si se cumplió el criterio de término, qué se aprendió y qué cambia en el backlog o en las épicas siguientes. Después se proponen las iteraciones de la épica siguiente (sección 4.4 de `_planificacion/README.md`).
