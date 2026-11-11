# Plan de migración · Pedidos Legado → Pedidos Web

> Base: el código de `legado/src/`, el análisis de `analista-legado` y [defectos-corregidos.md](defectos-corregidos.md). Las plantillas de diagnóstico de la U4 (`docs/diagnostico/`) aún no están completas en este repo. Cuando lo estén, este plan debe contrastarse con la matriz de decisión.

## 1. Etapas

Se migra un módulo a la vez (strangler fig). Cada etapa sigue el mismo ciclo: congelar con un golden master, migrar igual al legado, y corregir defectos en commits aparte.

1. **Cálculo de totales** (`src/PedidosWeb.Core/Totales/`): hecho en la U5. Tiene 60 casos en verde y R05 corregido.
2. **Pantalla de pedidos: grabar y totalizar** (`UFrmPedidos.pas`, `btnGrabarClick`). Hoy graba un total calculado con reglas propias (`UFrmPedidos.pas:296-320`). Antes de migrar hay que resolver H1 (umbrales y Navidad) y lograr que el portal grabe el total de `CalculoTotales`.
   - **Golden master:** pedidos grabados, comparando el `TOTAL` de `PEDIDOS` con el cálculo C#.
3. **Stock** (`UStock.pas`): descuento de stock por pedido y movimientos, con trato distinto para VIP.
   - **Golden master:** stock antes y después de una secuencia de pedidos.
4. **Clientes y productos** (`UFrmClientes.pas`, `UFrmProductos.pas`): mantenedores con poca lógica, que se reemplazan por la API y el portal.
   - **Golden master:** validaciones de alta y edición.
5. **Informes** (`UFrmInformes.pas`): consulta por fechas, cliente y estado, y exportación CSV. Se migra al final, cuando los datos ya viven en la base nueva.
   - **Golden master:** el CSV exportado para un rango de fechas fijo.

## 2. Convivencia

- **Fuente de verdad:** el legado sigue siendo la fuente oficial de cada módulo hasta su corte. El portal empieza en «modo sombra»: recalcula con el C# cada pedido que graba el legado y anota la diferencia.
- **Diferencias esperadas:** se descuentan las que vienen de R05 (±1 o ±2 pesos) y las de H1. Cualquier otra diferencia es una alerta que se investiga antes de avanzar.
- **Golden master vivo:** cada diferencia nueva que se explique se agrega como caso, de modo que los 60 casos actuales crecen con la operación real.
- **Base de datos:** mientras dure la convivencia, el portal lee la base DBISAM o una réplica y solo escribe en sus propias tablas. Así no hay dos sistemas escribiendo `PEDIDOS`.

## 3. Riesgos

| Riesgo | Mitigación |
|---|---|
| El total grabado no sale de `CalculoTotales`, sino del formulario con otras reglas (`UFrmPedidos.pas:320`). Comparar solo contra el módulo oculta diferencias reales. | En el modo sombra, comparar contra el `TOTAL` grabado en `PEDIDOS`, no solo contra el golden master. Resolver H1 antes de la etapa 2. |
| Umbrales ambiguos (H1): el comentario, el código y la pantalla dicen cosas distintas, y hay 13 pedidos justo en el umbral. | Decisión escrita de Comercial y Contabilidad. Hasta entonces, replicar el `>` del módulo y registrar las diferencias. |
| Contabilidad no acepta las diferencias de ±1 o ±2 pesos de R05 al conciliar. | Informar R05 antes del corte, con la tabla de D1, e incluir en la conciliación una tolerancia documentada solo para esos casos. |
| `Double` contra `decimal`: puede haber valores junto al límite de redondeo que no aparecen en los 60 casos. | Ampliar el golden master con pedidos reales del modo sombra y con casos límite (x,50, umbrales exactos). |
| Lógica oculta en globales: Navidad (`UGlobales.pas:33`), tasa de IVA leída de configuración (`UFrmPrincipal.pas:64`). | Inventariar los parámetros globales y llevarlos a la configuración del portal antes de migrar la pantalla de pedidos. |

## 4. Criterio de corte

- **Cuándo se apaga cada módulo del legado:** se cumplen las tres condiciones siguientes.
  - El golden master del módulo está 100 % en verde, con cada defecto corregido registrado.
  - Hay al menos un ciclo de cierre mensual en modo sombra sin diferencias inexplicadas.
  - Contabilidad firma la conciliación, con las tolerancias de R05 aceptadas.
- **Cómo se apaga:** el módulo del legado pasa a solo lectura y el portal es el único que escribe. El legado no se borra hasta cerrar la última etapa.
- **Vuelta atrás:** volver al módulo del legado activando un feature flag por módulo, durante un período de gracia de un cierre mensual. Mientras dure, el portal exporta lo que grabó en el formato del legado, para poder reinyectarlo si hay que volver.
