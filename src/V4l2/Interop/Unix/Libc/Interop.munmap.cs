// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using System;
using System.Runtime.InteropServices;

namespace Iot.Device.Media.Interop.Unix.Libc;

internal partial class Interop
{
	[DllImport(Unix.Interop.LibcLibrary)]
	internal static extern int munmap(IntPtr addr, int length);
}