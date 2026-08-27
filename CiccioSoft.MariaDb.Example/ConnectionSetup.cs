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

using CiccioSoft.MariaDb.Native;

namespace CiccioSoft.MariaDb.Example;

internal static class ConnectionSetup
{
    internal static MySql OpenDefaultConnection()
    {
        MySql mysql = MySql.Init();
        mysql.SetOption(MySqlOption.MYSQL_OPT_SSL_VERIFY_SERVER_CERT, false);
        mysql.SetOption(MySqlOption.MARIADB_OPT_MULTI_STATEMENTS, true);
        mysql.Connect("localhost", 3306, "root", "password", "test");

        ConsoleOutput.Section("Connessione aperta");
        ConsoleOutput.KeyValue("Server", mysql.GetServerInfo());
        ConsoleOutput.KeyValue("Client", MySql.GetClientInfo());
        ConsoleOutput.KeyValue("Host", mysql.GetHostInfo());
        ConsoleOutput.KeyValue("Thread", mysql.ThreadId());
        ConsoleOutput.KeyValue("Proto", mysql.GetProtoInfo());

        return mysql;
    }
}
