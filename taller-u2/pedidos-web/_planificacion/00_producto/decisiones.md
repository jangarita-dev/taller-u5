# Registro de Decisiones Arquitectónicas (RDA)

Consolidado de las decisiones técnicas del proyecto. Formato y reglas en `_planificacion/README.md` (secciones 5.2 y 6.4).

| RDA | Título | Estado |
| :-- | :----- | :----- |
| RDA-001 | Nivel de proceso | Aceptada |
| RDA-002 | .NET 10 con ASP.NET Core Minimal API y dominio sin infraestructura | Aceptada |
| RDA-003 | Pedidos en memoria hasta adoptar PostgreSQL | Aceptada |
| RDA-004 | Interfaz web con Blazor en la misma solución | Aceptada |
| RDA-005 | Especificaciones en `specs/` y documentación en `_planificacion/` | Aceptada |

---

## RDA-001 · Nivel de proceso

- **Fecha:** 2026-11-10
- **Estado:** Aceptada

**Contexto:** el proceso de trabajo (especificaciones, contrato de la API, auditorías, plan técnico) se ajusta a la complejidad del proyecto para no crear documentos que no aportan.
**Decisión:** nivel **mediano**. Factores: riesgo técnico alto (modernización de un sistema existente y Blazor nuevo para el desarrollador), mantenimiento alto (producto de largo plazo que crece por unidades y en el que trabajan agentes de IA), alcance, persistencia y arquitectura medios (varias funcionalidades con reglas de negocio, PostgreSQL previsto, frontend Blazor y API separados), integraciones y datos sensibles bajos (sin pagos ni datos personales sensibles). Autenticación sin definir. Componentes activos: backlog priorizado, RDA, auditoría técnica, una spec por funcionalidad con reglas de negocio, plan técnico en las iteraciones que tocan la API o los datos, contrato OpenAPI obligatorio, pruebas de cada endpoint (caso exitoso y al menos un error), `DESIGN.md` completo para la interfaz Blazor.
**Alternativas consideradas:** nivel complejo, descartado porque no hay datos sensibles, pagos ni varios roles; se revisa si aparece inicio de sesión con roles. Nivel pequeño, descartado porque la API tiene reglas de negocio con varios caminos y el proyecto se mantiene a largo plazo.
**Consecuencias:** quedan fuera a propósito el contract testing automático, el versionado de la API y las pruebas de carga; se proponen con una RDA si aparece un segundo consumidor de la API. El nivel se revisa si aparece o desaparece un factor que lo decidió (autenticación, integraciones, datos sensibles, consumidores de la API, personas en el proyecto).

---

## RDA-002 · .NET 10 con ASP.NET Core Minimal API y dominio sin infraestructura

- **Fecha:** 2026-11-10
- **Estado:** Aceptada

**Contexto:** decisión previa a la adopción de esta documentación, deducida de los `.csproj` (`net10.0`, `Microsoft.NET.Sdk.Web`), de `src/PedidosWeb.Api/Program.cs` y de `AGENTS.md`.
**Decisión:** .NET 10 con C#. La API usa Minimal API: solo endpoints e inyección de dependencias en `Program.cs`. Los modelos, reglas y repositorios viven en `PedidosWeb.Core`, sin dependencias de infraestructura. Tests con xUnit 2.9. Dinero en CLP como `decimal` sin decimales; fechas sin hora como `DateOnly`; nullable activado.
**Alternativas consideradas:** `PENDIENTE_DESARROLLADOR` (no quedaron escritas).
**Consecuencias:** el dominio se prueba sin levantar la API. Toda lógica de filtrado o cálculo va en `PedidosWeb.Core`, no en los endpoints.

---

## RDA-003 · Pedidos en memoria hasta adoptar PostgreSQL

- **Fecha:** 2026-11-10
- **Estado:** Aceptada

**Contexto:** decisión previa a la adopción de esta documentación, deducida de `PedidoRepositorioEnMemoria.cs` y del `README.md`, que anuncia PostgreSQL en la Unidad 5.
**Decisión:** los 45 pedidos de ejemplo (septiembre y octubre de 2026) viven en memoria detrás de `IPedidoRepositorio`. PostgreSQL reemplaza al repositorio en memoria en la Épica 04, sin cambiar la interfaz que usan los endpoints.
**Alternativas consideradas:** `PENDIENTE_DESARROLLADOR` (no quedaron escritas).
**Consecuencias:** los criterios de aceptación se escriben contra los 45 pedidos de ejemplo. Al llegar PostgreSQL, el filtrado debe hacerse en la consulta y no trayendo todos los pedidos a memoria (hallazgo AUD-01-003).

---

## RDA-004 · Interfaz web con Blazor en la misma solución

- **Fecha:** 2026-11-10
- **Estado:** Aceptada

**Contexto:** el portal necesita una interfaz para los vendedores. El equipo trabaja en C# con .NET 10, xUnit y Rider.
**Decisión:** la interfaz se construye con Blazor (.NET 10) como un proyecto más de `PedidosWeb.sln`, que consume la API REST. El modelo de hosting de Blazor (Server, WebAssembly o Web App con renderizado interactivo) es `PENDIENTE_DESARROLLADOR` y se decide en la prueba de concepto de la Épica 02.
**Alternativas consideradas:** Astro con Tailwind y PNPM, la recomendación por defecto para interfaces web: liviano y con los tokens de diseño en un solo lugar, pero suma un segundo ecosistema (Node y PNPM) y una carpeta fuera de la solución.
**Consecuencias:** todo queda en C# y con las mismas herramientas, y los modelos de `PedidosWeb.Core` se pueden compartir. Blazor es nuevo para el desarrollador: la Épica 02 empieza con una iteración corta de prueba de concepto. El ecosistema de componentes visuales es más chico que el de JavaScript.

---

## RDA-005 · Especificaciones en `specs/` y documentación en `_planificacion/`

- **Fecha:** 2026-11-10
- **Estado:** Aceptada

**Contexto:** el repositorio ya tenía `AGENTS.md` y specs en `specs/<historia>/spec.md` cuando se adoptó la documentación de `_planificacion/`, que por defecto guarda las especificaciones en `_planificacion/20_especificaciones/`.
**Decisión:** se conservan `AGENTS.md` y `specs/` tal como están. Las especificaciones siguen en `specs/HU-NN-nombre/spec.md`; `_planificacion/` no tiene carpeta de especificaciones. El plan técnico y las tareas viven en la iteración, no en un `plan.md` por historia. Los commits siguen Conventional Commits, como el historial existente.
**Alternativas consideradas:** mover las specs a `_planificacion/20_especificaciones/`, descartado porque rompe las rutas que ya usan `AGENTS.md`, el `README.md` y el taller.
**Consecuencias:** el validador de la documentación reporta la falta de `_planificacion/20_especificaciones/`; es un falso positivo esperado. `specs/README.md` menciona un `plan.md` por historia: conviene ajustarlo para que no compita con el plan técnico de la iteración.
