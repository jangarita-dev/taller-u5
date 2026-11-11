# Auditoría técnica

Registro acumulativo de auditorías. Las auditorías se agregan al final y nunca se borran. Antes de agregar una, revisa el último número usado en este archivo y suma uno. Un hallazgo se marca como resuelto indicando la iteración o la RDA que lo cerró (por ejemplo, `Resuelto en 02-01` o `Resuelto en RDA-004`).

**Severidad:** Alta (bloquea la publicación o afecta al usuario o al negocio), Media (afecta calidad o rendimiento), Baja (mejora).

---

## Auditoría 01 · Revisión inicial del código

- **Fecha:** 2026-11-10
- **Auditor:** Claude Code (Claude Opus 5.5), con la skill planificador-proyectos-web
- **Alcance:** repositorio `pedidos-web` en el commit `92c05a4`: `src/`, `tests/`, `PedidosWeb.sln`, `AGENTS.md`, `README.md`, `specs/` y `docs/`. Se ejecutaron `dotnet build` (0 errores) y `dotnet test` (7 de 7 tests superados). No se leyeron archivos `.env` (no hay ninguno versionado).

### Resumen

El dominio está bien separado de la API y tiene tests. No hay secretos en el código ni en archivos versionados, y `.gitignore` excluye `.env` y `*.pem`. Los riesgos son de cobertura y de diseño para lo que viene: la API no tiene tests, no hay un formato de error común y el repositorio entrega todos los pedidos sin filtrar, lo que no escala cuando llegue PostgreSQL.

### Hallazgos

| ID | Severidad | Área | Hallazgo | Acción recomendada | Estado |
| :- | :-------- | :--- | :------- | :----------------- | :----- |
| AUD-01-001 | Media | Pruebas | Los endpoints de `src/PedidosWeb.Api/Program.cs` (`/salud` y `/api/pedidos/{id}`) no tienen tests: solo existe `PedidosWeb.Core.Tests`. Un cambio en la API puede romperla sin que `dotnet test` lo detecte. | Crear un proyecto de tests de la API con `WebApplicationFactory<Program>` (ya preparado con `public partial class Program`). | Abierto |
| AUD-01-002 | Media | API | No hay un formato de error común: `GET /api/pedidos/{id}` responde `404` sin cuerpo, y la spec HU-01 deja el formato de error como TODO. Cada endpoint nuevo podría responder errores distintos. | Decidir el formato (por ejemplo `ProblemDetails`) en la spec HU-01, agregarlo al contrato y aplicarlo a todos los endpoints. | Abierto |
| AUD-01-003 | Baja | Arquitectura | `IPedidoRepositorio` solo ofrece `ObtenerTodos()` y `ObtenerPorId()`. Filtrar en memoria sobre `ObtenerTodos()` funciona con 45 pedidos, pero con PostgreSQL traería todo el historial en cada consulta. | Al implementar HU-01, definir la consulta filtrada en la interfaz del repositorio para que PostgreSQL la resuelva en la base de datos. | Abierto |
| AUD-01-004 | Baja | Dominio | `Pedido.Estado` es un `string` libre (`Pedido.cs`): nada impide un estado mal escrito ni define cuáles son válidos. | Decidir la lista de estados válidos en la spec HU-01 (RN-04) y evaluar representarla en el dominio. | Abierto |
| AUD-01-005 | Baja | Calidad | No hay pipeline de CI: la regla «tests en verde antes de cada commit» depende de que cada persona ejecute `dotnet test`. | Pipeline que compile y ejecute los tests en cada push (Épica 05). | Abierto |
