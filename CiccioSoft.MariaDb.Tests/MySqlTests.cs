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
using System.Reflection;
using CiccioSoft.MariaDb.Native;
using Xunit;

namespace CiccioSoft.MariaDb.Tests;

public sealed class MySqlTests
{
    [Fact]
    public void MethodsThrowObjectDisposedExceptionWhenHandleIsZero()
    {
        using MySql sut = CreateDisposedClient();

        Assert.Throws<ObjectDisposedException>(() => sut.Ping());
        Assert.Throws<ObjectDisposedException>(() => sut.Query("SELECT 1"));
        Assert.Throws<ObjectDisposedException>(() => sut.Connect("localhost", 3306, "root", "root", "db"));
        Assert.Throws<ObjectDisposedException>(() => sut.SetOption(MySqlOption.MYSQL_OPT_CONNECT_TIMEOUT, 1u));
        Assert.Throws<ObjectDisposedException>(() => sut.SetOption(MySqlOption.MYSQL_OPT_RECONNECT, true));
        Assert.Throws<ObjectDisposedException>(() => sut.SetOption(MySqlOption.MYSQL_SET_CHARSET_NAME, "utf8mb4"));
        Assert.Throws<ObjectDisposedException>(() => MySql.GetClientInfo());
        Assert.Throws<ObjectDisposedException>(() => sut.GetServerInfo());
        Assert.Throws<ObjectDisposedException>(() => sut.Error());
    }

    [Fact]
    public void Dispose_IsNoOpWhenAlreadyDisposed()
    {
        using MySql sut = CreateDisposedClient();

        sut.Dispose();
    }

    [Fact]
    public void Query_ReturnType_IsVoid()
    {
        MethodInfo method = typeof(MySql).GetMethod(nameof(MySql.Query), [typeof(string)])
            ?? throw new InvalidOperationException("Unable to find Query(string) method.");

        Assert.Equal(typeof(void), method.ReturnType);
    }

    [Fact]
    public void Query_ThrowsMySqlExceptionOnError()
    {
        using MySql sut = CreateDisposedClient();

        Assert.Throws<MySqlException>(() => sut.Query("SELECT 1"));
    }

    // [Fact]
    // public void MySqlInteropException_DefaultConstructor_InitializesType()
    // {
    //     Exception sut = new MySqlInteropException();

    //     Assert.IsType<MySqlInteropException>(sut);
    // }

    // [Fact]
    // public void MySqlInteropException_MessageConstructor_SetsMessage()
    // {
    //     var sut = new MySqlInteropException("native call failed");

    //     Assert.Equal("native call failed", sut.Message);
    // }

    // [Fact]
    // public void MySqlInteropException_InnerExceptionConstructor_SetsAllValues()
    // {
    //     var inner = new InvalidOperationException("inner");
    //     var sut = new MySqlInteropException("native call failed", inner);

    //     Assert.Equal("native call failed", sut.Message);
    //     Assert.Same(inner, sut.InnerException);
    // }

    private static MySql CreateDisposedClient()
    {
        MySql client = MySql.Init();
        client.Dispose();
        return client;
    }
}
