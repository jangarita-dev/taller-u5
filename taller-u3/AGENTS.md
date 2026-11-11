# AGENTS.md · Pedidos Web

## Proyecto
Portal web de pedidos de una distribuidora; moderniza Pedidos Legado (Delphi + DBISAM).
Hoy: biblioteca de dominio + API REST en ASP.NET Core (.NET 10) con 45 pedidos de ejemplo en memoria.

## Comandos
- Compilar: `dotnet build`
- Tests: `dotnet test`
- API: `dotnet run --project src/PedidosWeb.Api` (http://localhost:5080; `GET /salud` responde `{"estado":"ok"}`)

## Estilo de código
- Nombres del dominio, comentarios y documentación en español (`Pedido`, `ObtenerPorId`, `FormatearFolio`).
- Dinero en CLP: `decimal` sin decimales; nunca `double` ni `float`.
- Fechas sin hora: `DateOnly`.
- `src/PedidosWeb.Core/`: modelos, reglas y repositorios (`IPedidoRepositorio`), sin dependencias de infraestructura.
- `src/PedidosWeb.Api/`: solo endpoints Minimal API en `Program.cs` e inyección de dependencias.
- Nullable activado: no ignores sus advertencias.

## Tests
- xUnit 2.9 en `tests/PedidosWeb.Core.Tests/`, misma carpeta que la clase probada (`Pedidos/`, `Folios/`).
- Un archivo por clase: `<Clase>Tests.cs`. Métodos: `Metodo_Escenario_Resultado`.
- Se ejecutan con `dotnet test`; deben estar en verde antes de cada commit (regla 3).
- TODO: decidir dónde viven los tests de la API (hoy no hay proyecto de tests para `PedidosWeb.Api`).

## Seguridad
- Nunca escribas secretos, claves, tokens ni cadenas de conexión en el código ni en el repo.

## Reglas de trabajo
1. Toda funcionalidad parte de una spec aprobada en `specs/<historia>/spec.md`.
2. Un test xUnit por criterio de aceptación.
3. No hacer commit sin tests en verde.
4. Sin preguntar puedes: leer el repo, ejecutar `dotnet build`, `dotnet test` y `dotnet run`, y editar `specs/` y `docs/`.
5. Para cualquier otra cosa (editar `src/`, `tests/`, `.sln`, `.csproj`, agregar paquetes NuGet) muestra tu plan y espera aprobación.
6. Nunca hagas commit, push ni merge: los hace siempre una persona.
