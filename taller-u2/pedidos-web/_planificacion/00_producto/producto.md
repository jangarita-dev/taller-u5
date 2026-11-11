# Producto · Pedidos Web

- **Cliente:** distribuidora ficticia del curso *Desarrollando con IA* (Vela LATAM). Todo el contenido y los datos son ficticios y tienen fines educativos.
- **Desarrollador:** JAngarita
- **Documento base:** `README.md` y `AGENTS.md` del repositorio, y la spec `specs/HU-01-listar-pedidos/spec.md`.
- **Última revisión:** 2026-11-10

---

## 1. Propósito

Que los vendedores de la distribuidora encuentren y consulten pedidos desde un portal web, en lugar de hacerlo en Pedidos Legado, el sistema de escritorio en Delphi con DBISAM que la empresa usa hace 18 años.

## 2. Qué es

1. Una API REST en ASP.NET Core (.NET 10) que expone los pedidos de la distribuidora.
2. Una biblioteca de dominio (`PedidosWeb.Core`) con las reglas del negocio heredadas del legado, como el formato de folio.
3. Una interfaz web en Blazor para que los vendedores consulten los pedidos.
4. El caso hilo del curso: crece unidad a unidad (módulo de totales migrado, PostgreSQL, pipeline de CI).

## 3. Qué no es

1. No reemplaza todavía todas las funciones de Pedidos Legado: solo las que el backlog compromete.
2. No crea, edita ni anula pedidos mientras no exista una historia aprobada que lo pida.
3. No maneja pagos ni facturación.
4. No es un sistema en producción: los datos son ficticios.

Toda funcionalidad parte de una spec aprobada en `specs/`. Lo que no esté en el backlog va a «Propuestas fuera de alcance» de `registro-pendientes.md`, no al código.

## 4. Público objetivo

| Perfil | Situación | Qué necesita del producto |
| :----- | :-------- | :------------------------ |
| Vendedor de la distribuidora | Busca los pedidos de un período, de un cliente o en un estado, y escribe rápido y sin tildes | Listar pedidos filtrando por cliente, estado y rango de fechas, y ver el detalle de un pedido |
| Otros perfiles (administración, bodega, jefatura) | `PENDIENTE_CLIENTE` | `PENDIENTE_CLIENTE` |

Condiciones de uso a considerar en todo diseño:

- Los vendedores escriben rápido y sin tildes: la búsqueda por nombre de cliente debe tolerarlo según lo que decida la spec HU-01.
- Los listados pueden tener miles de pedidos con datos reales: toda consulta va acotada por rango de fechas.
- Dispositivo y conectividad: `PENDIENTE_CLIENTE`.

## 5. Idioma y tono

- Todo el producto está en **español de Chile**: ortografía, vocabulario y formato de números de esa variante (montos en CLP con punto de miles y sin decimales, como `$75.450`).
- Trato de **tú**, frases cortas y directas, sin tecnicismos innecesarios.
- Los nombres del dominio en el código también van en español (`Pedido`, `ObtenerPorId`, `FormatearFolio`).

## 6. Indicadores de éxito

1. **Calidad de cada entrega:** cada criterio de aceptación de una spec tiene su test xUnit, y `dotnet test` está en verde antes de cada commit.
2. **Indicadores de negocio:** `PENDIENTE_CLIENTE` (por ejemplo, tiempo para encontrar un pedido frente a Pedidos Legado).

## 7. Hitos comprometidos

| Semana o fecha | Hito | Referencia |
| :------------- | :--- | :--------- |
| `PENDIENTE_DESARROLLADOR` | Listado de pedidos con filtros (HU-01) | Taller de la Unidad 2 |
| `PENDIENTE_DESARROLLADOR` | PostgreSQL en lugar del repositorio en memoria | Unidad 5 del curso |

Las fechas siguen el calendario del curso; se registran aquí cuando estén confirmadas.
