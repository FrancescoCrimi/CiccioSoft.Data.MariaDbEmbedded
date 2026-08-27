---
name: CiccioSoft.MariaDb.Tests
description: "Istruzioni specifiche per il progetto di test di CiccioSoft.MariaDb."
---

# Istruzioni per il progetto CiccioSoft. .MariaDb.Tests

## Scopo

- Questo progetto contiene i test dell’interoperabilità con MariaDB Connector/C.
- Verifica comportamenti, eccezioni e wrapper nativi del layer `CiccioSoft.MariaDb`.

## Cose importanti

- Concentrarsi su casi d’uso interop e casi limite nativi.
- Non scrivere test di ADO.NET qui; quelli sono nel progetto provider.

## Comandi utili

- Eseguire i test interop:
  - `dotnet test CiccioSoft.MariaDb.Tests/CiccioSoft.MariaDb.Tests.csproj`

## Lingua

- Rispondi in italiano quando discuti di questo progetto.
