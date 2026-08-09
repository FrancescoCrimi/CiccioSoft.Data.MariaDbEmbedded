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

using System;
using CiccioSoft.Interop.MariaDb.Native;

namespace CiccioSoft.Interop.MariaDb;

/// <summary>
/// Metadata for a result-set column.
/// Managed wrapper for <c>MYSQL_FIELD</c>.
/// </summary>
public sealed class MySqlField
{
    public string Name { get; }
    public string OrgName { get; }
    public string Table { get; }
    public string OrgTable { get; }
    public string Database { get; }
    public string Catalog { get; }
    public string? Default { get; }   // null when not requested via mysql_list_fields

    public uint Length { get; }   // declared column width
    public uint MaxLength { get; }   // maximum width in the actual data
    public uint Flags { get; }
    public uint Decimals { get; }
    public uint CharsetNumber { get; }
    public MySqlFieldTypes Type { get; }

    // convenience flags
    public bool IsNotNull => (Flags & MariadbComNative.NOT_NULL_FLAG) != 0;
    public bool IsPrimaryKey => (Flags & MariadbComNative.PRI_KEY_FLAG) != 0;
    public bool IsUniqueKey => (Flags & MariadbComNative.UNIQUE_KEY_FLAG) != 0;
    public bool IsBlob => (Flags & MariadbComNative.BLOB_FLAG) != 0;
    public bool IsUnsigned => (Flags & MariadbComNative.UNSIGNED_FLAG) != 0;
    public bool IsAutoIncrement => (Flags & MariadbComNative.AUTO_INCREMENT_FLAG) != 0;
    public bool IsNumeric => (Flags & MariadbComNative.NUM_FLAG) != 0;

    internal unsafe MySqlField(st_mysql_field native)
    {
        Name = Utils.GetStringFromPointerBytes(native.name);
        OrgName = Utils.GetStringFromPointerBytes(native.org_name);
        Table = Utils.GetStringFromPointerBytes(native.table);
        OrgTable = Utils.GetStringFromPointerBytes(native.org_table);
        Database = Utils.GetStringFromPointerBytes(native.db);
        Catalog = Utils.GetStringFromPointerBytes(native.catalog);
        Default = native.def != null
                       ? Utils.GetStringFromPointerBytes(native.def)
                       : null;
        Length = native.length;
        MaxLength = native.max_length;
        Flags = native.flags;
        Decimals = native.decimals;
        CharsetNumber = native.charsetnr;
        Type = native.type;
    }

    public override string ToString() =>
        $"{Table}.{Name} ({Type}{(IsUnsigned ? " UNSIGNED" : "")}" +
        $"{(IsNotNull ? " NOT NULL" : "")})";
}