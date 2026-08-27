# CiccioSoft.MariaDb.Tests

This project contains automated tests for the MariaDB Connector/C interop layer.

## Purpose

- Validate the behavior of the `CiccioSoft.MariaDb` wrapper.
- Test native API support, exception handling, and result operations.
- Ensure the interop layer maintains predictable behavior.

## Running the tests

```bash
dotnet test CiccioSoft.MariaDb.Tests/CiccioSoft.MariaDb.Tests.csproj
```

## Notes

- This project uses xUnit as the test framework.
- It depends on `CiccioSoft.MariaDb` as a referenced project.
