# Épica 04 · PostgreSQL

- **Estado:** Pendiente
- **Objetivo:** que los pedidos vivan en PostgreSQL detrás de `IPedidoRepositorio`, sin cambiar el comportamiento de la API.

## Alcance

1. RDA de acceso a datos (biblioteca, migraciones y dónde vive la cadena de conexión, siempre fuera del repositorio).
2. Repositorio de PostgreSQL que implementa `IPedidoRepositorio`, con la consulta filtrada resuelta en la base de datos (AUD-01-003).
3. Datos de ejemplo cargados por migración o script versionado.

## Iteraciones

Se definen al cerrar la Épica 03.

| Iteración | Nombre |
| :-------- | :----- |
| 04-01 | Decisión de acceso a datos |

## Criterio de término

- Todos los criterios de aceptación de HU-01 pasan contra PostgreSQL, y ningún secreto ni cadena de conexión queda en el repositorio.

## Revisión al cierre

Se completa al cerrar la épica, en pocas líneas: si se cumplió el criterio de término, qué se aprendió y qué cambia en el backlog o en las épicas siguientes. Después se proponen las iteraciones de la épica siguiente (sección 4.4 de `_planificacion/README.md`).
