# Registro de pendientes

**Backlog y fuente única de verdad del trabajo pendiente.** Si discrepa con cualquier otro documento de planificación sobre el estado del trabajo, prevalece este archivo.

- **Última actualización:** 2026-11-10
- **Iteración activa:** ninguna (la siguiente es 01-01 · Decisiones de la spec HU-01)
- **Próximo hito:** HU-01 · Listar pedidos con filtros, en estado `Vigente`
- **Entrega comprometida:** `PENDIENTE_DESARROLLADOR` (calendario del curso)
- **Flujo de ramas vigente:** una rama `iteracion/XX-YY-descripcion-corta` por iteración, creada con aprobación del desarrollador, que se integra a `main`. Los merges los hace el desarrollador.

---

## 1. Estado de épicas e iteraciones

| Iteración | Nombre | Épica | Estado | Depende de | Bitácoras |
| :-------- | :----- | :---- | :----- | :--------- | :-------- |
| 00 | Línea base | 00 | Terminada | — | — |
| 01-01 | Decisiones de la spec HU-01 | 01 | Pendiente | — | — |
| 01-02 | Tests de la API | 01 | Pendiente | 01-01 | — |
| 01-03 | Consulta filtrada en el dominio | 01 | Pendiente | 01-01 | — |
| 01-04 | Endpoint de listado de pedidos | 01 | Pendiente | 01-02, 01-03 | — |
| 02 | Interfaz Blazor (iteraciones por detallar) | 02 | Pendiente | Épica 01 | — |
| 03 | Módulo de totales migrado (iteraciones por detallar) | 03 | Pendiente | Épica 02 | — |
| 04 | PostgreSQL (iteraciones por detallar) | 04 | Pendiente | Épica 03 | — |
| 05 | Integración continua (iteraciones por detallar) | 05 | Pendiente | Épica 04 | — |

## 2. Backlog

Lo comprometido que falta construir, ordenado por prioridad. Un elemento asignado a una iteración muestra su número; al cerrar la iteración se actualizan su fila y las de este backlog. Los terminados se quedan al final.

| Prioridad | Elemento | Tipo | Especificación | Iteración | Estado |
| :-------- | :------- | :--- | :------------- | :-------- | :----- |
| Alta | Resolver los TODO de HU-01 y aprobar la spec | Funcionalidad | `specs/HU-01-listar-pedidos/spec.md` | 01-01 | Pendiente |
| Alta | Formato de error común para todos los endpoints (AUD-01-002) | Técnico | `specs/HU-01-listar-pedidos/spec.md` | 01-01, 01-04 | Pendiente |
| Alta | Proyecto de tests de la API (AUD-01-001) | Técnico | — | 01-02 | Pendiente |
| Alta | Consulta filtrada de pedidos en el dominio (AUD-01-003) | Funcionalidad | `specs/HU-01-listar-pedidos/spec.md` | 01-03 | Pendiente |
| Alta | Lista de estados válidos en el dominio (AUD-01-004) | Técnico | `specs/HU-01-listar-pedidos/spec.md` | 01-03 | Pendiente |
| Alta | Endpoint `GET /api/pedidos` con filtros | Funcionalidad | `specs/HU-01-listar-pedidos/spec.md` | 01-04 | Pendiente |
| Media | Interfaz Blazor: prueba de concepto, diseño, listado y detalle de pedidos | Funcionalidad | — | Épica 02 | Pendiente |
| Media | Módulo de totales migrado desde Pedidos Legado | Funcionalidad | — | Épica 03 | Pendiente |
| Media | Repositorio de pedidos en PostgreSQL | Técnico | — | Épica 04 | Pendiente |
| Baja | Pipeline de CI que compila y ejecuta los tests (AUD-01-005) | Técnico | — | Épica 05 | Pendiente |

## 3. Datos pendientes del cliente

Mientras falten, se usa `PENDIENTE_CLIENTE` donde corresponda.

| Dato | Hallazgo | Necesario para | Estado |
| :--- | :------- | :------------- | :----- |
| Historias de usuario además de HU-01 (crear, editar o anular pedidos, clientes, otros perfiles) | — | Backlog después de la Épica 01 | Pendiente |
| Si el portal tendrá inicio de sesión y roles | — | Rediagnóstico del nivel (RDA-001) | Pendiente |
| Otros perfiles de usuario, dispositivo y conectividad de los vendedores (`producto.md`, sección 4) | — | Épica 02 | Pendiente |
| Indicadores de negocio (`producto.md`, sección 6) | — | Cierre de la Épica 02 | Pendiente |
| Reglas del cálculo de totales de Pedidos Legado | — | Épica 03 | Pendiente |

## 4. Decisiones por tomar

Decisiones del desarrollador que bloquean o condicionan trabajo. Mientras falten, se usa `PENDIENTE_DESARROLLADOR`.

| Tema | RDA o hallazgo | Responsable |
| :--- | :------------- | :---------- |
| TODO de la spec HU-01: parámetro de cliente, estados, fechas invertidas, paginación, rango máximo y errores | AUD-01-002, AUD-01-004 | Desarrollador (iteración 01-01) |
| Si el `404` de `obtenerPedidoPorId` adopta el formato de error común (`docs/api/openapi.yaml`) | AUD-01-002 | Desarrollador (iteración 01-01) |
| Modelo de hosting de Blazor (Server, WebAssembly o Web App interactiva) | RDA-004 | Desarrollador (Épica 02) |
| Diseño de la interfaz: concepto, color, tipografía, espaciado, componentes, botones, íconos, imágenes, movimiento y tokens (`DESIGN.md`) | — | Desarrollador (Épica 02) |
| Alternativas que se consideraron para .NET 10 y para los datos en memoria | RDA-002, RDA-003 | Desarrollador |
| Hosting y despliegue del portal | — | Desarrollador |
| Plataforma de CI | AUD-01-005 | Desarrollador (Épica 05) |
| Fechas de los hitos y entrega comprometida (`producto.md`, sección 7) | — | Desarrollador |

## 5. Propuestas fuera de alcance

Ideas detectadas durante el trabajo que no forman parte de lo comprometido. Toda propuesta necesita una spec aprobada antes de construirse. Si se aprueba una, pasa al backlog con una fila de historial.

| Propuesta | Origen | Fecha |
| :-------- | :----- | :---- |
| Filtrar el listado por sucursal, folio o total, y exportarlo | Spec HU-01, «Fuera de alcance» | 2026-11-10 |

## 6. Historial

Una fila por cambio, en una sola línea, con enlace a la bitácora que tiene el detalle. Nada se borra: si un cambio queda sin efecto, se agrega una fila que lo dice.

| Fecha | Cambio |
| :---- | :----- |
| 2026-11-10 | Creación de la documentación del proyecto: nivel mediano, línea base y 5 épicas, con 4 iteraciones detalladas en la Épica 01. |
