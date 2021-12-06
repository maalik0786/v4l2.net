// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using System;
using System.IO;
using System.Drawing.Imaging;
using Iot.Device.Media.Media;
using PixelFormat = Iot.Device.Media.Media.PixelFormat;

namespace V4l2.Samples;

internal class Program
{
	private static void Main(string[] args)
	{
		var settings = new VideoConnectionSettings(0)
		{
			CaptureSize = (2560, 1920),
			PixelFormat = PixelFormat.JPEG,
			ExposureType = ExposureType.Auto
		};
		using var device = VideoDevice.Create(settings);

		// Get the supported formats of the device
		foreach (var item in device.GetSupportedPixelFormats())
			Console.Write($"{item} ");
		Console.WriteLine();

		// Get the resolutions of the format
		foreach (var (width, height) in device.GetPixelFormatResolutions(PixelFormat.YUYV))
			Console.Write($"{width}x{height} ");
		Console.WriteLine();

		// Query v4l2 controls default and current value
		var value = device.GetVideoDeviceValue(VideoDeviceValueType.Rotate);
		Console.WriteLine(
			$"{value.Name} Min: {value.Minimum} Max: {value.Maximum} Step: {value.Step} Default: {value.DefaultValue} Current: {value.CurrentValue}");
		var path = Directory.GetCurrentDirectory();

		// Take photos
		device.Capture($"{path}/jpg_direct_output.jpg");

		// Change capture setting
		device.Settings.PixelFormat = PixelFormat.YUV420;

		// Convert pixel format
		var colors = VideoDevice.Yv12ToRgb(device.Capture(), settings.CaptureSize);
		var bitmap = VideoDevice.RgbToBitmap(settings.CaptureSize, colors);
		bitmap.Save($"{path}/yuyv_to_jpg.jpg", ImageFormat.Jpeg);
	}
}