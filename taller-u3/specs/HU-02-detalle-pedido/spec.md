# HU-02 · Detalle de un pedido

> Estado: **aprobada** (2026-11-10). Decisiones 🔶 aceptadas tal como están.

**Historia:** Como vendedor, quiero ver el detalle de un pedido con su cliente, sus líneas y su total, para responder consultas del cliente sin abrir el sistema legado.

## Qué devuelve

La ruta `GET /api/pedidos/{id}/detalle` devuelve:

- Del pedido: `Id`, `Folio`, `Fecha`, `Estado` y `Sucursal`.
- Del cliente: `ClienteId` y `ClienteNombre`, tomados del propio `Pedido`.
  - 🔶 Decisión: no se usa `ClientesService` (es el módulo heredado que se ordena en la etapa 2 y hoy no expone un método que devuelva un `Cliente`).
- De cada línea: `Numero`, `Producto`, `Cantidad`, `PrecioUnitario` y `Subtotal` (`Cantidad × PrecioUnitario`), ordenadas por `Numero`.
- Total: suma de los subtotales de las líneas, en CLP (`decimal` sin decimales, sin IVA).

## Incrementos

1. **Calcular el total:** a partir de una lista de `LineaPedido`, calcular subtotales y total (`src/PedidosWeb.Core/Pedidos/`).
2. **Obtener el detalle:** armar el detalle de un pedido por id usando `IPedidoRepositorio` e `ILineaPedidoRepositorio` (`src/PedidosWeb.Core/Pedidos/`).
3. **Publicarlo en la API:** ruta `GET /api/pedidos/{id}/detalle` en `Program.cs` y registro de `ILineaPedidoRepositorio` en la inyección de dependencias.

## Criterios de aceptación

Con los datos de `PedidoRepositorioEnMemoria` y `LineaPedidoRepositorioEnMemoria`.

- **CA-01** · Dadas las líneas del pedido 8, cuando se calcula el total, entonces es **120.100** (3 × 21.500 + 12 × 2.950 + 10 × 2.020).
- **CA-02** · Dada una línea de 12 × 2.950, cuando se calcula su subtotal, entonces es **35.400**.
- **CA-03** · Dada una lista de líneas vacía, cuando se calcula el total, entonces es **0**.
- **CA-04** · Dado el pedido 8, cuando se obtiene su detalle, entonces trae folio `PED-STGO-000008`, cliente `C-001` · `Ferretería El Clavo`, estado `Entregado` y **3 líneas** en orden 1, 2, 3.
- **CA-05** · Dado cualquiera de los 45 pedidos, cuando se obtiene su detalle, entonces el total calculado es igual a `Pedido.Total`. (Verificado: hoy coinciden los 45.)
- **CA-06** · Dado un id que no existe (por ejemplo 999), cuando se obtiene el detalle, entonces no hay detalle (`null`).
- **CA-07** · Dado el pedido 8, cuando se llama `GET /api/pedidos/8/detalle`, entonces responde **200** con `total = 120100`.
- **CA-08** · Dado el id 999, cuando se llama `GET /api/pedidos/999/detalle`, entonces responde **404**.

## Si el pedido no existe

La capa Core devuelve `null` y la API responde **404 Not Found** sin cuerpo, igual que `GET /api/pedidos/{id}`.

🔶 Decisión: si el pedido existe pero no tiene líneas, se devuelve el detalle con lista vacía y total 0 (no es error). En los datos de ejemplo no ocurre.

## Fuera de alcance

- IVA, descuentos y despacho: el total es neto, igual que `Pedido.Total`.
- Datos extendidos del cliente (RUT, región, zona, activo) y uso de `ClientesService`.
- Editar el pedido o sus líneas.
- Tests de integración de la API: hoy no hay proyecto de tests para `PedidosWeb.Api` (TODO de `AGENTS.md`). 🔶 CA-07 y CA-08 se verifican a mano en el navegador o se decide dónde viven esos tests.
