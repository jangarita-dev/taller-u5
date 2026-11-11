# HU-01 · Listar pedidos con filtros

> Estado: **borrador**. No se implementa hasta resolver los TODO y aprobar la spec.

## Historia de usuario

Como vendedor, quiero listar pedidos filtrando por cliente, estado y rango de fechas.

## Objetivo

Que el vendedor encuentre los pedidos de un período acotándolos por cliente y por estado, sin tener que abrirlos uno por uno con `GET /api/pedidos/{id}`. El rango de fechas siempre es obligatorio, para que ninguna consulta recorra todo el historial; cliente y estado son filtros opcionales que achican el resultado.

## Reglas de negocio

- **RN-01 · Rango obligatorio.** `desde` y `hasta` son obligatorios. Si falta alguno, es un error.
- **RN-02 · Rango inclusivo.** Un pedido entra si `desde <= Fecha <= hasta` (se incluyen ambos extremos).
- **RN-03 · Cliente opcional, por código o por nombre.** Si viene el filtro de cliente, un pedido entra si su `ClienteId` es igual al valor **o** si su `ClienteNombre` contiene el valor.
  - TODO: decidir si es un solo parámetro (`cliente`) o dos (`clienteId` y `clienteNombre`). Cambia la firma de la API y los tests.
  - TODO: decidir si la búsqueda por nombre distingue mayúsculas y tildes (`"dona"` vs `"Doña"`). El vendedor escribe rápido y sin tildes.
- **RN-04 · Estado opcional.** Si viene el filtro de estado, solo entran los pedidos con ese estado.
  - TODO: decidir la lista de estados válidos para filtrar. Los datos tienen 5 textos (`Pendiente`, `Confirmado`, `Despachado`, `Entregado`, `Anulado`), pero eso no los convierte en el contrato de la API.
  - TODO: decidir si un estado fuera de la lista es error o lista vacía, y si se distinguen mayúsculas.
  - TODO: decidir si se puede filtrar por más de un estado a la vez. Cambia el tipo del parámetro.
- **RN-05 · Filtros combinados con Y.** Si vienen varios filtros, el pedido debe cumplirlos todos.
- **RN-06 · Orden.** Por `Fecha` descendente (más recientes primero); a igual fecha, por `Id` descendente. (En los datos de ejemplo no hay dos pedidos con la misma fecha.)
- **RN-07 · Fechas invertidas.** TODO: decidir qué pasa si `desde > hasta` (error, intercambiarlas o lista vacía). Es un error común al escribir a mano y define un caso de test.
- **RN-08 · Paginación.** TODO: decidir el tamaño de página, el máximo y cómo se pide la página siguiente. Con datos reales el listado puede tener miles de pedidos.
- **RN-09 · Rango máximo.** TODO: decidir si hay un largo máximo de rango (por ejemplo, 3 meses). Hace que el rango obligatorio sirva de verdad.

## Criterios de aceptación

Con los 45 pedidos de `PedidoRepositorioEnMemoria`. Las listas son los `Id` en el orden esperado.

- [ ] **CA-01** `desde=2026-09-01`, `hasta=2026-10-31` sin otros filtros → 45 pedidos; el primero es el 21 (2026-10-31) y el último el 40 (2026-09-02).
- [ ] **CA-02** `desde=2026-09-01`, `hasta=2026-09-30` → 22 pedidos. `desde=2026-10-01`, `hasta=2026-10-31` → 23 pedidos.
- [ ] **CA-03** Los extremos se incluyen: `desde=hasta=2026-09-02` → `[40]`; `desde=hasta=2026-10-31` → `[21]`.
- [ ] **CA-04** Cliente por código: `cliente=C-001`, septiembre → `[24, 32, 40]`.
- [ ] **CA-05** Cliente por nombre: `cliente=El Clavo`, septiembre → `[24, 32, 40]`.
- [ ] **CA-06** Estado: `estado=Pendiente`, `2026-09-01..2026-10-31` → `[27, 18, 45, 9, 36]`.
- [ ] **CA-07** Filtros combinados: `cliente=C-002`, `estado=Despachado`, octubre → `[21, 37]`.
- [ ] **CA-08** Sin coincidencias: `cliente=C-007`, `estado=Pendiente`, `2026-09-01..2026-10-31` → lista vacía. TODO: confirmar que una lista vacía es respuesta exitosa y no un 404.
- [ ] **CA-09** Sin `desde` o sin `hasta` → error (formato según RN de errores).
- [ ] **CA-10** Fechas invertidas (`desde=2026-10-31`, `hasta=2026-09-01`) → según RN-07 (TODO).
- [ ] **CA-11** Paginación → según RN-08 (TODO).

## Ejemplos de entrada y salida

TODO: confirmar ruta y nombres de parámetros. Propuesta, siguiendo la ruta de detalle que ya existe: `GET /api/pedidos?desde=&hasta=&cliente=&estado=`, con fechas `AAAA-MM-DD`.

Entrada:

```
GET /api/pedidos?desde=2026-10-01&hasta=2026-10-31&cliente=C-002&estado=Despachado
```

Salida (cuerpo; la forma del envoltorio depende de RN-08):

```json
[
  { "id": 21, "folio": "PED-ANT-000021", "clienteId": "C-002", "clienteNombre": "Minimarket Doña Rosa",
    "fecha": "2026-10-31", "estado": "Despachado", "total": 75450, "sucursal": "ANT" },
  { "id": 37, "folio": "PED-ANT-000037", "clienteId": "C-002", "clienteNombre": "Minimarket Doña Rosa",
    "fecha": "2026-10-07", "estado": "Despachado", "total": 56650, "sucursal": "ANT" }
]
```

Entrada sin rango: `GET /api/pedidos?cliente=C-001` → error por RN-01.

## Errores esperados

TODO: decidir el formato de error (por ejemplo `ProblemDetails` de ASP.NET Core u otro cuerpo propio) y el código HTTP. Todos los casos de abajo dependen de esto.

| Caso | Regla | Respuesta |
|---|---|---|
| Falta `desde` o `hasta` | RN-01 | Error (TODO: código y cuerpo) |
| Fecha con formato inválido (`2026-13-40`, `ayer`) | — | TODO: decidir |
| `desde > hasta` | RN-07 | TODO: decidir |
| Estado fuera de la lista válida | RN-04 | TODO: decidir |
| Rango mayor al máximo | RN-09 | TODO: decidir |
| Cliente que no existe (`C-999`) | RN-03 | TODO: decidir si es lista vacía o error |

## Fuera de alcance

- Crear, editar o anular pedidos.
- Filtrar por sucursal, folio o total, y exportar el listado.
- Base de datos: los datos siguen en memoria (PostgreSQL llega en la Unidad 5).
- Autenticación y permisos por vendedor.

## Preguntas abiertas

1. ¿Los pedidos `Anulado` aparecen por defecto o solo si se filtran explícitamente? Afecta lo que ve el vendedor sin filtro de estado.
2. ¿Dónde viven los tests del endpoint? Hoy solo existe `PedidosWeb.Core.Tests`. Crear un proyecto de tests de la API cambia el `.sln`, y eso necesita aprobación.
3. Todos los TODO de arriba.
