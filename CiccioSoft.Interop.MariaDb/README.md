# CiccioSoft.Interop.MariaDb

![License: MIT](https://img.shields.io/badge/license-MIT-blue.svg)
![.NET Version](https://img.shields.io/badge/.NET-10.0-purple.svg)
![Language](https://img.shields.io/badge/language-C%23-brightgreen.svg)

Low-level library exposing an idiomatic, OOP wrapper for MariaDB Connector/C.

## Current scope

This project introduces the first building blocks for a native MariaDB Embedded interop package:

- native binding surface (`NativeMySqlClient`) for core C API entry points
- low-level managed wrapper (`MySql`) around `MYSQL*`
- custom exception (`MySqlInteropException`) for native failures

## Example

```csharp
using var client = MySql.Init().Connect("127.0.0.1", 3306, "root", "secret", "mydb");
client.Ping();
```

## Notes

- This is an initial scaffold and intentionally minimal.
- Runtime requires a compatible `libmysqlclient` (or equivalent MariaDB client library) on the host system.

## License
CiccioSoft.Mariadb: A lightweight C# class library for high-performance MariaDB database access.
Copyright (C) 2026  Francesco Crimi

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation; version 2 of the License.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License along
with this program; if not, write to the Free Software Foundation, Inc.,
51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
