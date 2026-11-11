# Épica 01 · Listado de pedidos

- **Estado:** Pendiente
- **Objetivo:** que el vendedor liste pedidos filtrando por rango de fechas, cliente y estado con `GET /api/pedidos`, según la spec HU-01 aprobada, con la API cubierta por tests.

## Alcance

1. Resolver los TODO de `specs/HU-01-listar-pedidos/spec.md` y dejarla `Aprobada`.
2. Proyecto de tests de la API, con tests de los endpoints que ya existen.
3. Consulta filtrada de pedidos en `PedidosWeb.Core`, con un test por criterio de aceptación.
4. Endpoint `GET /api/pedidos`, alineado con el contrato y con el formato de error común.

## Iteraciones

| Iteración | Nombre |
| :-------- | :----- |
| 01-01 | Decisiones de la spec HU-01 |
| 01-02 | Tests de la API |
| 01-03 | Consulta filtrada en el dominio |
| 01-04 | Endpoint de listado de pedidos |

## Criterio de término

- La spec HU-01 está `Vigente`: cada uno de sus criterios de aceptación tiene un test xUnit y `dotnet test` está en verde.
- `docs/api/openapi.yaml` describe `GET /api/pedidos` y el formato de error común, y la implementación responde lo que dice el contrato.
- Los hallazgos AUD-01-001 y AUD-01-002 están resueltos.

## Revisión al cierre

Se completa al cerrar la épica, en pocas líneas: si se cumplió el criterio de término, qué se aprendió y qué cambia en el backlog o en las épicas siguientes. Después se proponen las iteraciones de la épica siguiente (sección 4.4 de `_planificacion/README.md`).
