﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
namespace Iot.Device.Media.Media;

/// <summary>
/// The exposure type of a video device.
/// </summary>
public enum ExposureType
{
	/// <summary>
	/// Auto
	/// </summary>
	Auto = 0,
	/// <summary>
	/// Manual
	/// </summary>
	Manual = 1,
	/// <summary>
	/// Shutter Priority
	/// </summary>
	ShutterPriority = 2,
	/// <summary>
	/// Pperture Priority
	/// </summary>
	PperturePriority = 3
}