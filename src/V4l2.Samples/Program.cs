// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using System.Drawing.Imaging;
using System.IO;
using Iot.Device.Media.Media;
using PixelFormat = Iot.Device.Media.Media.PixelFormat;

namespace V4l2.Samples;

internal class Program
{
	private static void Main(string[] args)
	{
		var settings = new VideoConnectionSettings(0)
		{
			CaptureSize = (1920, 1080),
			PixelFormat = PixelFormat.MJPEG,
			ExposureType = ExposureType.Auto
		};
		using var device = VideoDevice.Create(settings);
		var path = Directory.GetCurrentDirectory();

		// Take photos
		device.Capture($"{path}/jpg_direct_output.jpg");

		// Change capture setting
		device.Settings.PixelFormat = PixelFormat.YUYV;

		// Get image stream, convert pixel format and save to file
		var ms = device.Capture();
		// Convert pixel format
		var colors = VideoDevice.Yv12ToRgb(ms, settings.CaptureSize);
		var bitmap = VideoDevice.RgbToBitmap(settings.CaptureSize, colors);
		bitmap.Save($"{path}/yuyv_to_jpg.jpg", ImageFormat.Jpeg);
	}
}