# .claude · Permisos de Claude Code

Esta carpeta configura qué puede hacer Claude Code en este repositorio. `settings.json` no lleva comentarios porque JSON no los admite; la explicación de cada regla está aquí. Las reglas traducen a permisos las «Reglas de trabajo» de `AGENTS.md`.

Los agentes de IA no editan esta carpeta: la mantiene el desarrollador.

## 1. Cómo se leen los permisos

`settings.json` tiene tres listas dentro de `permissions`:

| Lista | Qué significa |
| :---- | :------------ |
| `allow` | Claude Code lo hace sin pedir permiso. |
| `ask` | Claude Code pide aprobación cada vez. |
| `deny` | Claude Code no lo puede hacer, aunque se le pida. |

Si una acción calza con reglas de más de una lista, gana `deny`, luego `ask` y al final `allow`. Por eso `git branch *` puede estar en `ask` y `git branch -d *` en `deny`: listar o crear ramas se consulta, borrarlas está prohibido.

Dentro de cada lista, las reglas van agrupadas por tema y separadas por una línea en blanco, siempre en este orden: comandos del proyecto, Git, sistema de archivos, edición, lectura y web.

`defaultMode: "default"` significa que todo lo que no esté en ninguna lista también se consulta.

## 2. Lo que se permite sin preguntar (`allow`)

| Reglas | Por qué |
| :----- | :------ |
| `dotnet build`, `dotnet test`, `dotnet run`, `dotnet --version`, `dotnet --info`, `dotnet --list-sdks`, `dotnet list package` | Son los comandos de trabajo diario que `AGENTS.md` permite sin preguntar, más consultas que solo leen información del SDK y de los paquetes. |
| `curl` a `http://localhost:5080/` | Permite ejercitar la API local contra sus criterios de aceptación con peticiones reproducibles. No llega a ningún servidor externo. |
| `git status`, `git diff`, `git log`, `git show` | Solo leen el historial; no cambian nada. |
| `ls` | Solo lista archivos. |
| Edición de `specs/` y `docs/` | `AGENTS.md` permite editarlas sin preguntar. Incluye el contrato `docs/api/openapi.yaml`, que cambia antes que el código. |
| Edición de `_planificacion/00_producto/`, `10_epicas/` y `99_bitacora/` | El agente actualiza el registro y su backlog, las RDA y las auditorías, y escribe las bitácoras. `_planificacion/README.md` queda fuera: es un documento protegido. |
| `WebFetch` a `learn.microsoft.com` y `xunit.net` | Documentación oficial de .NET, ASP.NET Core, Blazor y xUnit, para verificar la sintaxis vigente en vez de recordarla. |

## 3. Lo que se consulta (`ask`)

| Reglas | Por qué |
| :----- | :------ |
| Edición de `src/`, `tests/`, `*.sln`, `*.csproj`, `global.json`, `Directory.Build.props` y `Directory.Packages.props` | `AGENTS.md` exige mostrar el plan y esperar aprobación antes de tocar el código, los tests y la configuración de la solución. |
| `dotnet add`, `dotnet remove`, `dotnet restore`, `dotnet tool`, `dotnet workload` | Agregan, quitan o descargan paquetes NuGet y herramientas. Cada dependencia tiene peso, mantenimiento y riesgo; el desarrollador decide. |
| `dotnet new`, `dotnet sln` | Crean proyectos o cambian la solución. |
| `dotnet ef` | Las migraciones cambian el esquema de la base de datos. |
| `git branch`, `checkout`, `switch`, `stash`, `add`, `restore`, `fetch` | Cambian el estado de la copia de trabajo o preparan un commit. Las ramas siguen el formato `iteracion/XX-YY-descripcion-corta`. |
| `rm`, `mv` | Borrar o mover archivos no se deshace fácil. |
| Edición de `.gitignore` y `.gitattributes` | Cambian qué versiona Git y cómo. |

## 4. Lo que está prohibido (`deny`)

| Reglas | Por qué |
| :----- | :------ |
| `dotnet publish`, `dotnet nuget push`, `dotnet nuget delete` | Publicar paquetes o artefactos de despliegue no es tarea del agente. |
| `dotnet ef database drop`, `psql` | Borran o tocan bases de datos directamente; las bases de datos remotas las maneja solo el desarrollador. |
| `git commit`, `push`, `merge`, `pull`, `rebase`, `cherry-pick`, `revert`, `reset`, `tag`, `clean`, `remote`, `config`, borrar ramas y `git -C` | Publican o reescriben historial. Los commits, pushes y merges los hace solo el desarrollador. `git -C` se bloquea porque permite ejecutar Git en otra carpeta y saltarse estas reglas. |
| `gh pr merge`, `gh repo`, `gh release` | Las mismas operaciones, hechas desde la CLI de GitHub. |
| `sudo`, `rm -rf` | Pueden dañar el sistema o borrar carpetas completas. |
| CLI de hosting y nube (`wrangler`, `vercel`, `netlify`, `railway`, `flyctl`, `heroku`, `gcloud`, `aws`, `az`, `kubectl`, `docker push`) | Desplegar y tocar la infraestructura lo hace solo el desarrollador. |
| Edición de `AGENTS.md`, `DESIGN.md`, `README.md` y `_planificacion/README.md` | Son los documentos protegidos del proyecto. El agente propone cambios y el desarrollador los aplica. |
| Edición de `.claude/`, `.git/`, `.env` y `.env.*` | El agente no cambia sus propios permisos, no toca la base de Git y no maneja secretos. Como `.env.*` incluye `.env.example` y `deny` gana sobre `ask`, ese archivo también lo edita el desarrollador: el agente le propone qué variables agregar. |
| Lectura de `.env` y `.env.*` | Contienen secretos que no deben llegar al contexto de un modelo. |

## 5. Ajustes personales

Si necesitas permisos distintos solo en tu equipo, usa `.claude/settings.local.json`. Ese archivo es personal, no se versiona (agrégalo a `.gitignore`) y se suma a las reglas de `settings.json`.

## 6. Instrucciones del proyecto: AGENTS.md

Claude Code carga `AGENTS.md` como instrucciones del proyecto solo si no encuentra un `CLAUDE.md` ni un `CLAUDE.local.md` en esta carpeta o más arriba. Si creas uno de esos archivos para instrucciones personales, su primera línea debe ser `@AGENTS.md`; si no, Claude Code deja de leer las reglas del proyecto sin avisar.

## 7. Comandos del proyecto

La carpeta `commands/` tiene atajos de Claude Code para los flujos de trabajo del proyecto. Cada comando es corto y remite a `_planificacion/README.md`, que es donde están los flujos completos: así los agentes que no usan estos atajos (Codex, Antigravity) siguen exactamente el mismo proceso.

| Comando | Qué hace |
| :------ | :------- |
| `/siguiente-epica` | Cierra la épica activa (revisión al cierre) y propone las iteraciones de la siguiente. Con tu aprobación, las genera todas de una vez. Sigue la sección 4.4 de `_planificacion/README.md`. |
