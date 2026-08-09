// CiccioSoft.Mariadb: A lightweight C# class library for high-performance MariaDB database access.
// Copyright (C) 2026  Francesco Crimi
// 
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; version 2 of the License.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.

using System.Runtime.InteropServices;
using CiccioSoft.Interop.MariaDb.Native;

namespace CiccioSoft.Interop.MariaDb;

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
