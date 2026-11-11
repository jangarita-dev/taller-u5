# DESIGN.md · Sistema de diseño de Pedidos Web

Fuente única de verdad del diseño de la interfaz Blazor del portal de pedidos. No hay marca, maqueta ni referencias visuales: cada valor de este documento lo decide el desarrollador. Lo que no está decidido lleva `PENDIENTE_DESARROLLADOR` y no se implementa hasta que se defina aquí.

---

## 1. Concepto visual

`PENDIENTE_DESARROLLADOR`: concepto en una línea y tres principios con su razón.

Condiciones de uso que el concepto debe respetar: la usan vendedores de la distribuidora que buscan pedidos rápido, filtrando por cliente, estado y fechas. La interfaz prioriza la lectura de tablas y filtros por sobre lo decorativo.

## 2. Color

`PENDIENTE_DESARROLLADOR`: tokens de superficie, acento y texto con su hex y uso.

Todo par de texto y fondo cumple WCAG 2.2 AA: 4,5:1 para texto normal y 3:1 para texto grande y para componentes de interfaz. Cada combinación se mide y se registra en una tabla de contraste en esta sección antes de usarla.

El estado de un pedido no se comunica solo con color: siempre lleva su texto (`Pendiente`, `Despachado`, etc.), porque una parte de las personas no distingue ciertos colores.

## 3. Tipografía

`PENDIENTE_DESARROLLADOR`: familias, pesos, fallbacks, origen de las fuentes y escala tipográfica.

## 4. Espaciado, retícula y forma

`PENDIENTE_DESARROLLADOR`: escala de espaciado, contenedor, retícula, radios y sombras.

## 5. Componentes

`PENDIENTE_DESARROLLADOR`: componentes con sus reglas (tabla de pedidos, filtros, detalle de pedido, mensajes de error y de lista vacía).

### 5.1 Botones

`PENDIENTE_DESARROLLADOR`: variantes, colores y hover.

Todos los botones tienen foco visible y un objetivo táctil de al menos 44 × 44 px.

## 6. Iconografía

`PENDIENTE_DESARROLLADOR`: librería y formato de los íconos.

Íconos decorativos con `aria-hidden="true"`; los que actúan solos como botón llevan `aria-label` en el elemento interactivo.

## 7. Imágenes

`PENDIENTE_DESARROLLADOR`: origen y formato de las imágenes, si las hay.

Toda imagen lleva texto alternativo descriptivo en español y dimensiones explícitas.

## 8. Movimiento

`PENDIENTE_DESARROLLADOR`: movimiento permitido y prohibido.

Con `prefers-reduced-motion: reduce`, las transiciones se desactivan.

## 9. Implementación de los tokens

`PENDIENTE_DESARROLLADOR`: dónde viven los tokens en el proyecto Blazor (por ejemplo, variables CSS en un archivo único) y cómo se usan desde los componentes. Ningún componente usa valores de color, tamaño o espaciado sueltos fuera de los tokens.

## 10. Voz y textos

- Español de Chile, trato de tú, frases cortas. Primero la acción, después la explicación.
- Montos en pesos chilenos con separador de miles y sin decimales (`$75.450`). Fechas visibles como `31-10-2026`.
- Los mensajes de error dicen qué pasó y qué hacer: «Elige una fecha de inicio» en vez de «Error de validación».
