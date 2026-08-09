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

public sealed unsafe class MySqlResult : IDisposable
{
    private readonly MySqlResultHandle _handle;
    private MySqlField[]? _fieldsCache;

    internal MySqlResult(MySqlResultHandle handle)
    {
        _handle = handle;
    }


    #region Informazioni generali

    public ulong NumRows
    {
        get
        {
            EnsureNotDisposed();
            return MySqlNative.mysql_num_rows(_handle.AsStructPointer());
        }
    }

    public uint NumFields
    {
        get
        {
            EnsureNotDisposed();
            return MySqlNative.mysql_num_fields(_handle.AsStructPointer());
        }
    }

    #endregion


    #region Iterazione righe

    /// <summary>
    /// Advances to the next row.
    /// Returns <see langword="true"/> when a row is available; otherwise <see langword="false"/>.
    /// <para/>
    /// WARNING: internal pointers inside <see cref="MySqlRow"/> remain valid
    /// only until the next <c>FetchRow</c> call or <c>Dispose</c>.
    /// </summary>
    public bool FetchRow(out MySqlRow row)
    {
        EnsureNotDisposed();

        // byte** r = NativeMySql.mysql_fetch_row(_handle.AsStructPointer());
        nint r = MySqlNative.mysql_fetch_row(_handle.AsStructPointer());

        // if (r == null)
        if (r == 0)
        {
            row = default;
            return false;
        }

        uint* lengths = MySqlNative.mysql_fetch_lengths(_handle.AsStructPointer());
        // uint count = NumFields;
        // row = new MySqlRow(r, lengths, count);

        ReadOnlySpan<nint> rowSpan = new(r.ToPointer(), (int)NumFields);
        ReadOnlySpan<uint> lengthsSpan = new(lengths, (int)NumFields);
        row = new MySqlRow(rowSpan, lengthsSpan, FetchFields());

        return true;
    }

    /// <summary>
    /// Repositions the cursor at the beginning of the result set.
    /// </summary>
    public void DataSeek()
    {
        EnsureNotDisposed();
        MySqlNative.mysql_data_seek(_handle.AsStructPointer(), 0);
    }

    #endregion


    #region Metadati colonne

    /// <summary>
    /// Returns metadata for all columns.
    /// The result is cached: the native call is performed once.
    /// </summary>
    public MySqlField[] FetchFields()
    {
        EnsureNotDisposed();

        if (_fieldsCache != null)
            return _fieldsCache;

        uint count = NumFields;
        _fieldsCache = new MySqlField[count];

        st_mysql_field* ptr = MySqlNative.mysql_fetch_fields(_handle.AsStructPointer());
        Span<st_mysql_field> nativeFields = new(ptr, (int)count);  //MySqlFieldNative
        for (uint i = 0; i < count; i++)
        {
            ref st_mysql_field f = ref nativeFields[(int)i];
            _fieldsCache[i] = new MySqlField(f);
        }

        return _fieldsCache;
    }

    /// <summary>
    /// Metadata for a single column by index.
    /// </summary>
    public MySqlField FetchField(uint index)
    {
        EnsureNotDisposed();

        if (index >= NumFields)
            throw new ArgumentOutOfRangeException(nameof(index));

        st_mysql_field* ptr = MySqlNative.mysql_fetch_field_direct(_handle.AsStructPointer(), index);
        st_mysql_field nativeField = *ptr;
        return new MySqlField(nativeField);
    }

    #endregion


    #region Helper

    private void EnsureNotDisposed()
    {
        if (_handle.IsClosed || _handle.IsInvalid)
        {
            throw new ObjectDisposedException(nameof(MySqlResult));
        }
    }

    #endregion


    public void Dispose()
    {
        _handle.Dispose();
        GC.SuppressFinalize(this);
    }
}
