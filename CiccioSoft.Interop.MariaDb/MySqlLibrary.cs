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
using System.Threading;
using CiccioSoft.Interop.MariaDb.Native;

namespace CiccioSoft.Interop.MariaDb;

/// <summary>
/// Process-wide MariaDB client/embedded library lifecycle wrapper.
/// Maps to <c>mysql_server_init</c> and <c>mysql_server_end</c>.
/// </summary>
public static class MySqlLibrary
{
    private static int _initialized = 0;

    /// <summary>
    /// Initializes the native library once for the current process via <c>mysql_server_init</c>.
    /// </summary>
    public static void Initialize()
    {
        EnsureInitialized();
    }

    internal static void EnsureInitialized()
    {
        if (Interlocked.CompareExchange(ref _initialized, 1, 0) != 0)
            return;

        unsafe
        {
            int rc = MySqlNative.mysql_server_init(0, null, null);
            if (rc != 0)
            {
                Interlocked.Exchange(ref _initialized, 0);
                throw new InvalidOperationException(
                    "mysql_library_init failed. Verificare che libmariadb sia installata.");
            }
        }
    }

    /// <summary>
    /// Shuts down the native library for the current process via <c>mysql_server_end</c>.
    /// </summary>
    public static void Shutdown()
    {
        if (Interlocked.CompareExchange(ref _initialized, 0, 1) != 1)
            return;

        MySqlNative.mysql_server_end();
    }
}