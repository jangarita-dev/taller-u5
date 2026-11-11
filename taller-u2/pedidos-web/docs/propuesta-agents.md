# Propuesta de cambios a AGENTS.md

`AGENTS.md` es un documento protegido: estos cambios los aplica el desarrollador si está de acuerdo. Mientras tanto, los agentes siguen el `AGENTS.md` actual.

---

## Cambio 1 · Nueva sección «Orden de lectura», después de «Proyecto»

**Texto actual:** no existe.

**Texto propuesto:**

```markdown
## Orden de lectura
1. `_planificacion/00_producto/registro-pendientes.md`: backlog e iteración activa. Es la fuente única de verdad del trabajo.
2. La iteración activa en `_planificacion/10_epicas/<epica>/`.
3. La spec que implementa, en `specs/<historia>/spec.md`, y `docs/api/openapi.yaml` si la tarea toca la API.
4. `_planificacion/00_producto/decisiones.md`: no contradigas una RDA aceptada.
5. `DESIGN.md` si la tarea toca la interfaz Blazor.
6. `_planificacion/README.md`: ciclos de trabajo, convenciones y plantillas.
Si algo está en conflicto, detente y pregunta.
```

**Por qué:** sin esto, un agente nuevo no sabe que existe `_planificacion/` ni cuál es la iteración activa, y puede empezar a trabajar en algo que no está priorizado.

---

## Cambio 2 · Nueva sección «Convenciones de la API», después de «Estilo de código»

**Texto actual:** no existe.

**Texto propuesto:**

```markdown
## Convenciones de la API
- REST con Minimal API; rutas en español, en plural y bajo `/api/` (`/api/pedidos`, `/api/pedidos/{id}`).
- Fechas en `AAAA-MM-DD`; montos en CLP como números enteros.
- Formato de errores y códigos HTTP: los que defina `docs/api/openapi.yaml`, iguales para todos los endpoints.
- El contrato `docs/api/openapi.yaml` cambia antes que el código. Si el contrato y el código no coinciden, detente y pregunta cuál está bien.
- Cada endpoint nuevo o modificado tiene tests de integración: el caso exitoso y al menos un error.
```

**Por qué:** el proyecto ya expone una API REST y va a tener un consumidor (la interfaz Blazor). Sin reglas comunes, cada endpoint puede responder errores con una forma distinta (hallazgo AUD-01-002).

---

## Cambio 3 · Nueva sección «Definición de preparada y de terminado», después de «Tests»

**Texto actual:** no existe (la regla 3 cubre solo los tests en verde).

**Texto propuesto:**

```markdown
## Definición de preparada y de terminado
**Preparada** (antes de empezar una iteración): objetivo en una o dos frases; criterios que se comprueban con un test o un comando; spec `Aprobada` si la tiene; iteraciones previas `Terminada`; y como máximo 8 tareas, revisables en unos 30 minutos. Si falla algo, no empieces: di qué falta.

**Terminado** (antes de dejarla `En revisión`):
1. `dotnet build` sin errores ni advertencias nuevas de nullable.
2. `dotnet test` en verde.
3. La API arranca con `dotnet run --project src/PedidosWeb.Api` y probaste lo que tocaste.
4. Cada criterio marcado tiene su evidencia en la bitácora de `_planificacion/99_bitacora/`.
5. `registro-pendientes.md` y los documentos afectados están al día.
```

**Por qué:** el validador de la documentación lo marca como faltante. Sin una definición de preparada, el agente puede empezar a implementar con una spec que todavía tiene TODO.

---

## Cambio 4 · Agregar «Commits» a la sección «Reglas de trabajo»

**Texto actual:** `6. Nunca hagas commit, push ni merge: los hace siempre una persona.`

**Texto propuesto:** dejarla igual y agregar:

```markdown
7. Al terminar, propón un mensaje de commit en Conventional Commits y en español (`feat: …`, `fix: …`, `test: …`, `docs: …`), en una línea de máximo 200 caracteres. Propón también la rama `iteracion/XX-YY-descripcion-corta` y espera aprobación antes de crearla.
```

**Por qué:** deja escrito el formato que ya usa el historial (`feat: FormatearFolio…`) y el nombre de las ramas que proponen las iteraciones.

---

## Cambio 5 · Cerrar el TODO de «Tests» (después de la iteración 01-02)

**Texto actual:** `- TODO: decidir dónde viven los tests de la API (hoy no hay proyecto de tests para PedidosWeb.Api).`

**Texto propuesto:** `- Los tests de la API viven en tests/PedidosWeb.Api.Tests/ y usan WebApplicationFactory<Program>.`

**Por qué:** la iteración 01-02 crea ese proyecto. Aplica este cambio solo cuando la iteración quede `Terminada`.

---

## Fuera de AGENTS.md

- **`specs/README.md`** dice que cada historia tiene «su `spec.md` y su `plan.md`». Con `_planificacion/`, el plan técnico vive en la iteración (RDA-005). Conviene cambiar esa línea a «una carpeta por historia de usuario, con su `spec.md`». Este archivo no es protegido: el agente puede hacerlo si lo apruebas.
- **`README.md`** (raíz) es el README del curso, con comandos y pasos del taller. La skill recomienda un README sin comandos, pero aquí lo dejamos como está porque sirve al taller.
