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

using System.Runtime.InteropServices;
using CiccioSoft.MariaDb.Native;

namespace CiccioSoft.MariaDb;

internal unsafe sealed class MySqlHandle : SafeHandle
{
    internal MySqlHandle(st_mysql* ptr) : base((nint)ptr, true) { }

    public override bool IsInvalid => handle == nint.Zero;

    internal st_mysql* AsStructPointer() => (st_mysql*)handle;

    protected override bool ReleaseHandle()
    {
        if (handle != 0)
            MySqlNative.mysql_close((st_mysql*)handle);
        return true;
    }
}

internal unsafe sealed class MySqlStmtHandle : SafeHandle
{
    internal MySqlStmtHandle(st_mysql_stmt* ptr) : base((nint)ptr, true) { }

    public override bool IsInvalid => handle == nint.Zero;

    internal st_mysql_stmt* AsStructPointer() => (st_mysql_stmt*)handle;

    protected override bool ReleaseHandle()
    {
        if (handle != 0)
            MariadbStmtNative.mysql_stmt_close((st_mysql_stmt*)handle);
        return true;
    }
}

internal unsafe sealed class MySqlResultHandle : SafeHandle
{
    internal MySqlResultHandle(st_mysql_res* ptr) : base((nint)ptr, true) { }

    public override bool IsInvalid => handle == nint.Zero;

    internal st_mysql_res* AsStructPointer() => (st_mysql_res*)handle;

    protected override bool ReleaseHandle()
    {
        if (handle != 0)
            MySqlNative.mysql_free_result((st_mysql_res*)handle);
        return true;
    }
}
