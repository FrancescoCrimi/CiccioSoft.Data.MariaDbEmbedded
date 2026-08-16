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
using CiccioSoft.MariaDb.Native;

namespace CiccioSoft.MariaDb;

/// <summary>
/// Helper that builds an array of <c>MYSQL_BIND</c> entries consumed by
/// <c>mysql_stmt_bind_param</c> and <c>mysql_stmt_bind_result</c>.
/// </summary>
public sealed class MySqlBindBuilder : IDisposable
{
    private readonly int _count;
    private MySqlBind[] _mySqlBinds;

    public MySqlBind this[int index]
    {
        get { return _mySqlBinds[index]; }
    }

    /// <summary>
    /// Creates <paramref name="count"/> managed bind slots for a statement.
    /// </summary>
    public MySqlBindBuilder(int count)
    {
        _count = count;
        _mySqlBinds = new MySqlBind[count];
        for (int i = 0; i < count; i++)
            _mySqlBinds[i] = new MySqlBind();
    }

    internal st_mysql_bind[] GetNativeArray()
    {
        var binds = new st_mysql_bind[_count];
        for (int i = 0; i < _count; i++)
            binds[i] = _mySqlBinds[i].Native;
        return binds;
    }

    /// <summary>
    /// Releases every pinned bind buffer previously prepared for statement bind APIs.
    /// </summary>
    public void Dispose()
    {
        foreach (var bind in _mySqlBinds)
            bind.Dispose();
    }
}