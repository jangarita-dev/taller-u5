# Defectos corregidos · migración del cálculo de totales

Regla: si cambia un valor esperado de `migracion/golden-master.csv`, ese caso aparece aquí.

## D1 · R05 · Redondeo al peso que no redondea (CORREGIDO)

- **Qué hace el legado:** `Calc3` calcula `Trunc(AValor + 0.49)` (`legado/src/UCalculoTotales.pas:50`). Su comentario dice «redondeo al peso» (`:47`), pero cuando la fracción está en [0,50; 0,51) redondea hacia abajo: 10,50 → 10. Se usa para el descuento (`:114`), el flete (`:98`, `:100`) y el IVA (`:105`). `RedondeaPesos` (`legado/src/UUtilidades.pas:28`) es idéntica y la usa la pantalla de pedidos.
- **Corrección:** en el C#, `CalculoTotales.Calc3` (`src/PedidosWeb.Core/Totales/CalculoTotales.cs`) redondea al peso más cercano y la mitad sube: `Math.Round(valor, 0, MidpointRounding.AwayFromZero)`. Todos los montos son `decimal`.
- **Por qué:** un medio peso se cobra o se descuenta según la regla aritmética, no por un artefacto del código. El caso más visible es el flete de las regiones 11 y 12: 14.990 × 1,15 = 17.238,5 y 16.490 × 1,15 = 18.963,5. El legado siempre lo dejaba un peso abajo.
- **Casos del golden master que cambian:** 13 en total, todos de ±1 o ±2 pesos. Valores en el orden descuento / flete / neto / IVA / total:

  | Caso | Folio | Antes | Después | Δ total | Causa |
  |---|---|---|---|---|---|
  | 2 | 1002 | 0 / 3500 / 191050 / 36964 / 231514 | 0 / 3500 / 191050 / 36965 / 231515 | +1 | IVA en ,50 |
  | 6 | 1006 | 25767 / 3500 / 872053 / 166355 / 1041908 | 25768 / 3500 / 872052 / 166355 / 1041907 | −1 | Descuento en ,50 |
  | 8 | 1008 | 0 / 3500 / 523450 / 100120 / 627070 | 0 / 3500 / 523450 / 100121 / 627071 | +1 | IVA en ,50 |
  | 18 | 1018 | 0 / 18963 / 132600 / 28797 / 180360 | 0 / 18964 / 132600 / 28797 / 180361 | +1 | Flete región 12 |
  | 21 | 1021 | 112358 / 17238 / 1292122 / 248778 / 1558138 | 112358 / 17239 / 1292122 / 248779 / 1558140 | +2 | Flete región 11, que también sube el IVA |
  | 23 | 1023 | 15000 / 18963 / 485000 / 95753 / 599716 | 15000 / 18964 / 485000 / 95753 / 599717 | +1 | Flete región 12 |
  | 25 | 1025 | 46425 / 6900 / 2153845 / 410542 / 2571287 | 46426 / 6900 / 2153844 / 410541 / 2571285 | −2 | Descuento en ,50, que también baja el IVA |
  | 27 | 1027 | 134670 / 17238 / 1252020 / 241159 / 1510417 | 134670 / 17239 / 1252020 / 241159 / 1510418 | +1 | Flete región 11 |
  | 36 | 1036 | 69701 / 18963 / 1521379 / 292665 / 1833007 | 69701 / 18964 / 1521379 / 292665 / 1833008 | +1 | Flete región 12 |
  | 37 | 1037 | 51264 / 18963 / 589536 / 115615 / 724114 | 51264 / 18964 / 589536 / 115615 / 724115 | +1 | Flete región 12 |
  | 42 | 1042 | 50000 / 18963 / 950000 / 184103 / 1153066 | 50000 / 18964 / 950000 / 184103 / 1153067 | +1 | Flete región 12 |
  | 46 | 1046 | 34882 / 17238 / 1418758 / 272839 / 1708835 | 34882 / 17239 / 1418758 / 272839 / 1708836 | +1 | Flete región 11 |
  | 54 | 1054 | 49973 / 17238 / 574687 / 112466 / 704391 | 49973 / 17239 / 574687 / 112466 / 704392 | +1 | Flete región 11 |

- **Suma de la columna total:** 58.202.135 → 58.202.144 (+9).
- **Commit:** `ebd2172` «R05: redondeo correcto al peso en Calc3». Va separado de `6358380` «Etapa 2: migrar CalculoTotales a C#, igual al legado».
- **Convivencia:** mientras convivan los dos sistemas, el portal y el legado pueden diferir en ±1 o ±2 pesos por pedido. Esto pasa siempre en el flete de las regiones 11 y 12, y a veces en descuentos o IVA terminados en ,50. Contabilidad debe aceptar esa diferencia como esperada en la conciliación. El legado solo avisa diferencias de más de 1 peso, y solo en modo prueba (`legado/src/UFrmPedidos.pas:327-331`), así que no las va a señalar por sí mismo.

## H1 · Umbrales del descuento por volumen (HALLAZGO, NO CORREGIDO)

- **Qué encontraste:** hay tres versiones de la misma regla.
  - El comentario de cabecera dice «descuento por volumen 5% sobre 400.000 (acta comercial 2011)» (`legado/src/UCalculoTotales.pas:4`).
  - El módulo de cálculo aplica un 5 % cuando la base supera estrictamente 500.000 y un 8 % cuando supera 1.000.000 (`legado/src/UCalculoTotales.pas:77`, `:79`).
  - La pantalla de pedidos, que es la que graba el total en `PEDIDOS`, usa umbrales inclusivos (`legado/src/UFrmPedidos.pas:296`, `:298`, `:320`).
- **Efecto:** en el golden master, 13 pedidos caen justo en el umbral, y en ellos la pantalla y el módulo dan resultados distintos:
  - **Base de exactamente 500.000** (casos 8, 11, 19, 23, 50 y 55): la pantalla aplica un 5 % más, es decir, 25.000 de descuento adicional.
  - **Base de exactamente 1.000.000** (casos 26, 32, 35, 39, 42, 48 y 60): la pantalla aplica un 8 % en vez de un 5 %, es decir, 30.000 más de descuento.
  - Si valiera el comentario («sobre 400.000»), también tendrían descuento los casos 17, 20 y 57, con bases entre 400.000 y 500.000.
  - Además, la pantalla aplica un descuento de Navidad de 2 % en diciembre que el módulo no tiene (`legado/src/UFrmPedidos.pas:304`). Hoy está apagado por defecto (`legado/src/UGlobales.pas:33`).
- **Quién decide:** Comercial, con el acta de 2011, junto con Contabilidad. El C# replica la versión de `UCalculoTotales.pas` (`>`), que es la que fija el golden master. No se cambia hasta que el negocio defina la regla vigente. Cuando la definan, se tratará como otro defecto (D2) con su propio commit y su propia actualización del CSV.
