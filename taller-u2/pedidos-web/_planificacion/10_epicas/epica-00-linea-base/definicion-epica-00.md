# Épica 00 · Línea base

- **Estado:** Terminada
- **Objetivo:** registrar lo que el proyecto ya tenía construido cuando se adoptó esta documentación, para planificar desde ahí.

## Origen

- **Proyecto analizado:** repositorio `pedidos-web` (taller de la Unidad 2)
- **Fecha del análisis:** 2026-11-10
- **Versión o commit:** `92c05a4` (rama `main`)
- **Cómo se analizó:** lectura del código y ejecución de `dotnet build` (0 errores, también del proyecto `PedidosWeb.Api`) y `dotnet test` (7 de 7 tests superados) con el SDK 10.0.403. La API no se levantó con `dotnet run`.

## Lo que ya existe

| Funcionalidad o módulo | Estado | Cómo se sabe | Dónde está |
| :--------------------- | :----- | :----------- | :--------- |
| `FormatearFolio`: folio `PED-{SUCURSAL}-{NUMERO}` con las reglas R1 a R5 del legado | Funciona | `dotnet test`: tests de `FolioFormatterTests` superados | `src/PedidosWeb.Core/Folios/FolioFormatter.cs` |
| Modelo `Pedido` (CLP en `decimal`, fecha en `DateOnly`) | Funciona | Observado en el código; lo usan los tests del repositorio | `src/PedidosWeb.Core/Pedidos/Pedido.cs` |
| Repositorio en memoria con 45 pedidos de ejemplo (septiembre y octubre de 2026) | Funciona | `dotnet test`: tests de `PedidoRepositorioEnMemoriaTests` superados | `src/PedidosWeb.Core/Pedidos/PedidoRepositorioEnMemoria.cs` e `IPedidoRepositorio.cs` |
| `GET /salud` responde `{"estado":"ok"}` | No verificada | Observado en el código; compila, pero no se levantó la API | `src/PedidosWeb.Api/Program.cs` |
| `GET /api/pedidos/{id}`: detalle de un pedido o `404` | No verificada | Observado en el código; compila, pero no se levantó la API ni tiene tests | `src/PedidosWeb.Api/Program.cs` |
| HU-01 · Listar pedidos con filtros | A medio hacer | Spec en borrador con TODO abiertos; endpoint marcado como PENDIENTE en el código | `specs/HU-01-listar-pedidos/spec.md` y `src/PedidosWeb.Api/Program.cs` |

## Stack detectado

| Pieza | Versión | Deducido de |
| :---- | :------ | :---------- |
| .NET SDK | 10.0.403 | `dotnet --version` |
| Framework destino | `net10.0` | los tres `.csproj` |
| ASP.NET Core (Minimal API) | el que trae el SDK | `src/PedidosWeb.Api/PedidosWeb.Api.csproj` (`Microsoft.NET.Sdk.Web`) |
| xUnit | 2.9.2 (runner 2.8.2) | `tests/PedidosWeb.Core.Tests/PedidosWeb.Core.Tests.csproj` |
| Microsoft.NET.Test.Sdk | 17.11.1 | `tests/PedidosWeb.Core.Tests/PedidosWeb.Core.Tests.csproj` |
| Puerto local de la API | `http://localhost:5080` | `src/PedidosWeb.Api/Properties/launchSettings.json` |

## Lo que no se pudo verificar

- Las respuestas reales de `/salud` y `/api/pedidos/{id}`: no se levantó la API y no hay tests de integración.
- Hosting y despliegue: no hay archivos de despliegue ni pipeline en el repositorio.

## Iteraciones

No tiene. Este trabajo se hizo antes de adoptar la documentación y no se reconstruye: no lleva iteraciones ni bitácoras.

## Revisión al cierre

- **Hallazgos de la revisión inicial:** Auditoría 01 en `auditoria-tecnica.md`.
- **Qué entra al backlog:** HU-01, que está a medio hacer, y los hallazgos AUD-01-001 y AUD-01-002 (Media), además de los Bajos que acompañan a HU-01.
- **Primera épica planificada:** Épica 01 · Listado de pedidos. Cierra HU-01, que ya tiene spec y criterios, y en el camino agrega los tests de la API y el formato de error común que la base necesita antes de construir la interfaz.
