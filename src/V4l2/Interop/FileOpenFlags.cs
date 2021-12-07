// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
namespace Iot.Device.Media.Interop;

internal enum FileOpenFlags
{
	// ReSharper disable InconsistentNaming
	O_RDONLY = 0x00,
	O_RDWR = 0x02,
	O_NONBLOCK = 0x800,
	O_SYNC = 0x101000
}