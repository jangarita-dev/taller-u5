# Iteración 01-01 · Decisiones de la spec HU-01

- **Épica:** 01 · Listado de pedidos
- **Estado:** Pendiente
- **Rama sugerida:** iteracion/01-01-decisiones-spec-hu-01
- **Depende de:** —
- **Especificación:** `specs/HU-01-listar-pedidos/spec.md` (todos los TODO)
- **RDA relacionadas:** RDA-002, RDA-005
- **Hallazgos que cierra:** — (deja decidido lo que necesitan AUD-01-002 y AUD-01-004)

## Objetivo

Dejar la spec HU-01 sin TODO y lista para que el desarrollador la apruebe, con `GET /api/pedidos` y el formato de error común descritos en el contrato.

## Alcance y entregables

- `specs/HU-01-listar-pedidos/spec.md` sin TODO, con las reglas RN-03, RN-04, RN-07, RN-08 y RN-09 decididas y los criterios CA-08, CA-09, CA-10 y CA-11 con su resultado esperado concreto.
- `docs/api/openapi.yaml` con la operación `listarPedidos`, sus parámetros y el esquema de error común.

## Plan técnico

La iteración no toca código. El agente presenta cada TODO con dos o tres opciones y su consecuencia en la firma de la API y en los tests; el desarrollador elige. Luego el agente reescribe la spec con lo elegido y actualiza el contrato antes que cualquier código (regla de alineación). El formato de error que se decida aquí vale para todos los endpoints, incluido el `404` de `obtenerPedidoPorId`.

## Tareas

1. Presentar las opciones para el parámetro de cliente (uno o dos parámetros) y la búsqueda por nombre sin distinguir mayúsculas ni tildes (RN-03).
2. Presentar las opciones para los estados: lista válida, estado fuera de la lista, mayúsculas, varios estados a la vez, y si `Anulado` aparece por defecto (RN-04 y pregunta abierta 1).
3. Presentar las opciones para fechas invertidas, rango máximo y fecha con formato inválido (RN-07 y RN-09).
4. Presentar las opciones para la paginación: tamaño de página, máximo y forma del envoltorio de la respuesta (RN-08).
5. Presentar las opciones para el formato de error y los códigos HTTP, y confirmar que una lista vacía es una respuesta exitosa (CA-08).
6. Reescribir la spec con las decisiones del desarrollador, sin TODO, y concretar CA-08 a CA-11.
7. Agregar `listarPedidos` y el esquema de error común a `docs/api/openapi.yaml`.
8. Registrar en `registro-pendientes.md` las decisiones tomadas y dejar la spec lista para aprobación.

## Criterios de aceptación

- [ ] Buscar «TODO» en `specs/HU-01-listar-pedidos/spec.md` no devuelve resultados.
- [ ] CA-08, CA-09, CA-10 y CA-11 dicen el código HTTP y el cuerpo (o la lista de `Id`) que se espera.
- [ ] `docs/api/openapi.yaml` tiene la operación `listarPedidos` con `desde`, `hasta`, cliente, estado y paginación, y un esquema de error que usan todas las respuestas de error.
- [ ] La spec queda en estado `Borrador` lista para revisión; el desarrollador la cambia a `Aprobada`.

## Fuera de alcance

- Escribir código en `src/` o `tests/`.
- Filtros por sucursal, folio o total, y exportar el listado.

## Datos requeridos del cliente

- Ninguno: en el contexto del curso, las reglas de negocio de HU-01 las decide el desarrollador.
