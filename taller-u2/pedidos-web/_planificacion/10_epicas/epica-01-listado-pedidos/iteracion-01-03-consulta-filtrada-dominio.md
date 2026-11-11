# Iteración 01-03 · Consulta filtrada en el dominio

- **Épica:** 01 · Listado de pedidos
- **Estado:** Pendiente
- **Rama sugerida:** iteracion/01-03-consulta-filtrada-dominio
- **Depende de:** 01-01 (spec `Aprobada`)
- **Especificación:** `specs/HU-01-listar-pedidos/spec.md` (CA-01 a CA-08, y CA-10 si se resuelve en el dominio)
- **RDA relacionadas:** RDA-002, RDA-003
- **Hallazgos que cierra:** AUD-01-003, AUD-01-004 (si la spec decide representar los estados en el dominio)

## Objetivo

Que `PedidosWeb.Core` devuelva los pedidos de un rango de fechas filtrados por cliente y estado, en el orden de la spec, con un test por criterio de aceptación.

## Alcance y entregables

- Consulta filtrada en `IPedidoRepositorio` e implementación en `PedidoRepositorioEnMemoria`.
- Tests en `tests/PedidosWeb.Core.Tests/Pedidos/` para CA-01 a CA-08.

## Plan técnico

La regla de RDA-002 manda: el filtrado vive en `PedidosWeb.Core`, no en el endpoint. Se agrega a `IPedidoRepositorio` un método de consulta que recibe los filtros (rango obligatorio, cliente y estado opcionales, y paginación si la spec la define), para que PostgreSQL pueda resolverlo en la base de datos más adelante (AUD-01-003). El orden es `Fecha` descendente y, a igual fecha, `Id` descendente (RN-06). La comparación de nombres sin mayúsculas ni tildes sigue lo decidido para RN-03 y no depende de la cultura del equipo. La forma exacta de los tipos (un `record` de filtros, un resultado paginado) se propone al desarrollador antes de escribirla.

## Tareas

1. Mostrar al desarrollador la firma propuesta del método y de los tipos de filtro y resultado, y esperar aprobación.
2. Agregar el método a `IPedidoRepositorio` con su comentario XML en español.
3. Implementarlo en `PedidoRepositorioEnMemoria` según RN-01 a RN-09.
4. Escribir un test por cada criterio CA-01 a CA-08 en `PedidoRepositorioEnMemoriaTests.cs`.
5. Escribir el test de CA-10 si la regla de fechas invertidas se resuelve en el dominio.
6. Ejecutar `dotnet build` y `dotnet test` sin errores ni advertencias nuevas de nullable.

## Criterios de aceptación

- [ ] CA-01 a CA-08 de la spec HU-01, cada uno con su test en verde.
- [ ] `dotnet build` termina sin errores ni advertencias nuevas.
- [ ] Ningún endpoint de `Program.cs` cambia en esta iteración.

## Fuera de alcance

- El endpoint `GET /api/pedidos` (va en 01-04).
- PostgreSQL (Épica 04).

## Datos requeridos del cliente

- Ninguno.
