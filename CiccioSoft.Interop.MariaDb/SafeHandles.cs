// Copyright (c) 2026 Francesco Crimi
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

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
