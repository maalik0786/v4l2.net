// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
namespace Iot.Device.Media.Interop;

[Flags]
internal enum MemoryMappedFlags
{
	// ReSharper disable InconsistentNaming
	MAP_SHARED = 0x01,
	MAP_PRIVATE = 0x02,
	MAP_FIXED = 0x10
}