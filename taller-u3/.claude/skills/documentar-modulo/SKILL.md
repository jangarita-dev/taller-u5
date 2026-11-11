---
name: documentar-modulo
description: "Documenta un módulo, servicio o clase de pedidos-web para el equipo en docs/modulos/<Modulo>.md, con propósito, API pública, ejemplo de uso y diagrama Mermaid. Úsala cuando pidan documentar, explicar o dejar por escrito un servicio o módulo del código, aunque no nombren la Skill."
---

# Documentar un módulo

## Pasos
1. Ubica el módulo en `src/` (por ejemplo `src/PedidosWeb.Core/Clientes/ClientesService.cs`) y lee el código completo, incluidas las clases que usa (modelos, datos de ejemplo).
2. Lee sus tests en `tests/PedidosWeb.Core.Tests/<Carpeta>/<Clase>Tests.cs`: muestran el comportamiento esperado y los casos borde.
3. Lista la API pública: cada método público con su firma, qué recibe, qué devuelve y qué pasa en los casos borde (id inexistente, inactivo, nulo).
4. Escribe `docs/modulos/<Modulo>.md` con las secciones de abajo, en ese orden.
5. Verifica cada afirmación contra el código y los tests: nombres de métodos, valores de retorno y ejemplos deben coincidir exactamente.
6. Al terminar, resume al usuario qué documentaste y qué observaciones encontraste.

## Qué debe tener docs/modulos/<Modulo>.md
- Propósito
- API pública
- Ejemplo de uso
- Diagrama Mermaid

## Reglas
- Escribe en español, con los nombres del dominio tal como están en el código.
- Documenta lo que el código **hace hoy**, no lo que debería hacer.
- Si encuentras rarezas o posibles errores, anótalos en una sección «Observaciones», sin corregirlos.
- Los ejemplos de uso deben salir de los tests o de los datos de ejemplo reales; no inventes valores.
- Los métodos privados se mencionan solo si ayudan a entender el flujo; la API pública es lo principal.
- El diagrama Mermaid debe ser simple (flujo o clases) y reflejar el código real.
- No modifiques `src/` ni `tests/`: esta Skill solo escribe en `docs/modulos/`.
- No hagas commit: lo hace una persona.
