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
using System.Runtime.CompilerServices;
using System.Text;

namespace CiccioSoft.Interop.MariaDb;

internal static class Utils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)] // Encourages JIT inlining to eliminate call overhead
    internal unsafe static string GetStringFromPointerBytes(byte* pBytes)
    {
        if (pBytes == null)
            return string.Empty;

        int nbBytes = 0;
        while (pBytes[nbBytes] != 0)
            nbBytes++;

        ReadOnlySpan<byte> span = new(pBytes, nbBytes);

        return Encoding.UTF8.GetString(span);
    }

    internal static ReadOnlySpan<byte> BuildUtf8NullTerminated(string value)
    {
        Span<byte> buffer;

        if (string.IsNullOrEmpty(value))
        {
            buffer = new byte[1];
            buffer[0] = 0;
            return buffer;
        }

        int byteCount = Encoding.UTF8.GetByteCount(value);
        buffer = new byte[byteCount + 1];
        Encoding.UTF8.GetBytes(value, buffer);
        buffer[byteCount] = 0;
        return buffer;
    }
}
