# Mapa de uso de IA en el SDLC · Pedidos Web

> Contexto: equipo que heredó Pedidos Legado (Delphi + DBISAM) y lo evoluciona hacia Pedidos Web (.NET + PostgreSQL).
> Regla general: Claude propone, una persona decide. Nada entra al repo sin que alguien haya leído el diff.

**Autor:** jangarita-dev
**Fecha:** 2026-11-09

| Etapa | Se delega a Claude | Artefacto versionado | Revisa (persona) | Riesgo | Evento Scrum |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Requisitos** | Extraer reglas de negocio y casos de borde desde las reglas del README (el código Delphi no está en el repo). | `docs/specs/requisitos-folio.md` | **Felipe (Tech Lead)** | Asumir lógica desactualizada o reglas de negocio extintas. | Refinement |
| **Diseño** | Generar borrador del esquema PostgreSQL y contratos API DTO para .NET. | `docs/architecture/db-schema.sql` | **Carlos (Backend Senior)** | Incompatibilidad de tipos de datos entre DBISAM y Postgres. | Sprint Planning |
| **Código** | Estructurar la lógica principal de `FormatearFolio()` e interfaces de servicio. | `src/PedidosWeb.Core/Folios/FolioFormatter.cs` | **María (Developer)** | Generación de código redundante o parches ineficientes. | Daily Scrum |
| **Pruebas** | Proponer casos de prueba unitarios para los escenarios límite de folios. | `tests/PedidosWeb.Core.Tests/Folios/FolioFormatterTests.cs` | **Ana (QA Automation)** | Falsos positivos por tests mal planteados que no prueban fallos reales. | Durante el sprint |
| **Revisión y despliegue** | Redactar el resumen de cambios (Release Notes) a partir del historial de commits. | `CHANGELOG.md` | **Felipe (Tech Lead)** | Incluir detalles técnicos confidenciales o specs no desplegadas. | Sprint Review |
| **Mejora continua** | Identificar código duplicado y proponer refactorizaciones en la capa de persistencia. | `docs/debt/refactor-plan.md` | **Carlos (Backend Senior)** | Cambios destructivos en módulos críticos sin cobertura de tests. | Retrospective |

---

## Evidencia del paso 3

| Ronda | Sin IA | Con Claude Code |
| :--- | :--- | :--- |
| **Tiempo (min)** | 14 min | 4 min |
| **Errores encontrados** | 2 errores de sintaxis y manejo incorrecto de ceros a la izquierda. | 1 error en la validación de nulos al recibir folios vacíos. |
| **Qué tuve que corregir** | Ajusté manualmente el formato de concatenación y agregué la condición `string.IsNullOrEmpty`. | Le pedí a Claude ajustar el diff para incluir la validación de folios negativos antes de aplicar los cambios. |

**Conclusión:** Con Claude Code tardé 4 minutos frente a 14 sin IA, pero en ambas rondas apareció un error de validación que solo encontré revisando: la IA acelera la implementación, no reemplaza leer el diff ni los tests.
