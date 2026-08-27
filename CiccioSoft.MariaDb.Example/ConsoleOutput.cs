// CiccioSoft.Mariadb: A lightweight C# class library for high-performance MariaDB database access.
// Copyright (C) 2026  Francesco Crimi.
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, see https://www.gnu.org/licenses/.

using System;

namespace CiccioSoft.MariaDb.Example;

internal static class ConsoleOutput
{
    internal static void Section(string title) => Console.WriteLine($"\n=== {title} ===");

    internal static void Message(string message) => Console.WriteLine($"  {message}");

    internal static void KeyValue(string key, object? value) => Console.WriteLine($"  {key,-8}: {value}");
}
