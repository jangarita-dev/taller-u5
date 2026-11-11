# Iteración 01-02 · Tests de la API

- **Épica:** 01 · Listado de pedidos
- **Estado:** Pendiente
- **Rama sugerida:** iteracion/01-02-tests-api
- **Depende de:** 01-01 (por el formato de error común)
- **Especificación:** —
- **RDA relacionadas:** RDA-002
- **Hallazgos que cierra:** AUD-01-001

## Objetivo

Que `dotnet test` también pruebe la API: un proyecto de tests de integración que cubre `/salud` y `/api/pedidos/{id}`.

## Alcance y entregables

- Proyecto `tests/PedidosWeb.Api.Tests/` en `PedidosWeb.sln`, con xUnit y `WebApplicationFactory<Program>`.
- Tests de `GET /salud` y `GET /api/pedidos/{id}` (pedido existente y pedido que no existe).
- Una RDA nueva que registra dónde viven los tests de la API.

## Plan técnico

Se crea un proyecto de tests hermano de `PedidosWeb.Core.Tests`, con la misma versión de xUnit, referencia a `PedidosWeb.Api` y el paquete NuGet `Microsoft.AspNetCore.Mvc.Testing` para levantar la API en memoria con `WebApplicationFactory<Program>` (`Program.cs` ya declara `public partial class Program`). Si el formato de error decidido en 01-01 cambia la respuesta `404`, se actualiza el endpoint para usarlo. Riesgo: agregar un paquete y cambiar el `.sln` requieren aprobación del desarrollador según `AGENTS.md`.

## Tareas

1. Mostrar el plan y pedir aprobación para crear el proyecto, agregar el paquete NuGet y cambiar el `.sln`.
2. Crear `tests/PedidosWeb.Api.Tests/` y agregarlo a `PedidosWeb.sln`.
3. Test: `GET /salud` responde `200` con `{"estado":"ok"}`.
4. Test: `GET /api/pedidos/21` responde `200` con el folio `PED-ANT-000021`.
5. Test: `GET /api/pedidos/999` responde `404` con el formato de error decidido en 01-01.
6. Ajustar el `404` de `obtenerPedidoPorId` al formato de error común, si 01-01 lo decidió así.
7. Registrar la RDA de ubicación de los tests de la API y entregar al desarrollador la propuesta para cerrar el TODO de la sección «Tests» de `AGENTS.md`.

## Criterios de aceptación

- [ ] `dotnet test` ejecuta los tests de `PedidosWeb.Core.Tests` y de `PedidosWeb.Api.Tests`, todos en verde.
- [ ] Los tres casos de las tareas 3, 4 y 5 tienen su test, con nombre `Metodo_Escenario_Resultado`.
- [ ] `docs/api/openapi.yaml` y la respuesta real de `obtenerPedidoPorId` coinciden.

## Fuera de alcance

- Tests del endpoint de listado (van en 01-04).
- Pipeline de CI (Épica 05).

## Datos requeridos del cliente

- Ninguno.
