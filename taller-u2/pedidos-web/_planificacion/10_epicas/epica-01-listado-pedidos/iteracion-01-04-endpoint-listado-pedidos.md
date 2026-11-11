# Iteración 01-04 · Endpoint de listado de pedidos

- **Épica:** 01 · Listado de pedidos
- **Estado:** Pendiente
- **Rama sugerida:** iteracion/01-04-endpoint-listado-pedidos
- **Depende de:** 01-02, 01-03
- **Especificación:** `specs/HU-01-listar-pedidos/spec.md` (CA-09, CA-10, CA-11 y los errores esperados)
- **RDA relacionadas:** RDA-002
- **Hallazgos que cierra:** AUD-01-002

## Objetivo

Que `GET /api/pedidos` responda lo que dice el contrato `listarPedidos`, con los filtros de HU-01 y los errores en el formato común.

## Alcance y entregables

- Endpoint `GET /api/pedidos` en `src/PedidosWeb.Api/Program.cs`, que llama a la consulta filtrada de 01-03.
- Tests de integración en `tests/PedidosWeb.Api.Tests/` para el caso exitoso y los errores de la spec.
- Spec HU-01 en `Vigente` y la iteración cerrada con la revisión de la épica.

## Plan técnico

`Program.cs` solo mapea parámetros, llama al repositorio y traduce el resultado a HTTP: ninguna regla de negocio se escribe en el endpoint (RDA-002). Los parámetros, los códigos HTTP y el cuerpo de error salen de `docs/api/openapi.yaml`, que se actualizó en 01-01; si la implementación necesita algo distinto, primero se cambia el contrato. Las fechas se reciben en `AAAA-MM-DD` y se convierten a `DateOnly`.

## Tareas

1. Mostrar el plan del endpoint y esperar aprobación para editar `Program.cs`.
2. Mapear `GET /api/pedidos` con sus parámetros y la llamada a la consulta filtrada.
3. Responder los errores de la tabla «Errores esperados» de la spec con el formato común.
4. Test de integración del caso exitoso (CA-07 de punta a punta).
5. Tests de integración de CA-09, CA-10 y CA-11, y de una fecha con formato inválido.
6. Comprobar con `curl` a `http://localhost:5080/api/pedidos` el caso exitoso y un error, y registrar la salida en la bitácora.
7. Pasar la spec HU-01 a `Vigente` y marcar los criterios cumplidos.
8. Cerrar la épica: revisión al cierre y propuesta de las iteraciones de la Épica 02.

## Criterios de aceptación

- [ ] CA-09, CA-10 y CA-11 de la spec HU-01, cada uno con su test en verde.
- [ ] `dotnet test` está en verde con los tests de dominio y de API.
- [ ] La respuesta real de `GET /api/pedidos` coincide con `listarPedidos` en `docs/api/openapi.yaml`.
- [ ] `src/PedidosWeb.Api/Program.cs` ya no tiene el comentario «HU-01 … PENDIENTE».

## Fuera de alcance

- Crear, editar o anular pedidos.
- Autenticación y permisos por vendedor.

## Datos requeridos del cliente

- Ninguno.
