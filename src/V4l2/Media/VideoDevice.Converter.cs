// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Iot.Device.Media.Media;

/// <summary>
/// The communications channel to a video device.
/// </summary>
public abstract partial class VideoDevice
{
	/// <summary>
	/// Convert YUV(YUV444) to RGB format.
	/// </summary>
	/// <param name="stream">YUV stream.</param>
	/// <returns>RGB format colors.</returns>
	public static Color[] YuvToRgb(MemoryStream stream)
	{
		int y, u, v;
		var colors = new List<Color>();
		while (stream.Position != stream.Length)
		{
			y = stream.ReadByte();
			u = stream.ReadByte();
			v = stream.ReadByte();
			colors.Add(YuvToRgb(y, u, v));
		}
		return colors.ToArray();
	}

	/// <summary>
	/// Convert YUYV(YUV422) to RGB format.
	/// </summary>
	/// <param name="stream">YUYV stream.</param>
	/// <returns>RGB format colors.</returns>
	public static Color[] YuyvToRgb(MemoryStream stream)
	{
		var colors = new List<Color>();
		while (stream.Position != stream.Length)
		{
			var y0 = stream.ReadByte();
			var u = stream.ReadByte();
			var y1 = stream.ReadByte();
			var v = stream.ReadByte();
			colors.Add(YuvToRgb(y0, u, v));
			colors.Add(YuvToRgb(y1, u, v));
		}
		return colors.ToArray();
	}

	/// <summary>
	/// Convert YV12(YUV420) to RGB format.
	/// </summary>
	/// <param name="stream">YV12 stream.</param>
	/// <param name="size">Image size in the stream.</param>
	/// <returns>RGB format colors.</returns>
	public static Color[] Yv12ToRgb(MemoryStream stream, (uint Width, uint Height) size)
	{
		int width = (int) size.Width, height = (int) size.Height;
		var total = width * height;
		var vShift = total / 4;
		var yuv = stream.ToArray();
		var colors = new List<Color>();
		for (var y = 0; y < height; y++)
		for (var x = 0; x < width; x++)
		{
			var shift = y / 2 * (width / 2) + x / 2;
			int y0 = yuv[y * width + x];
			int u = yuv[total + shift];
			int v = yuv[total + shift + vShift];
			colors.Add(YuvToRgb(y0, u, v));
		}
		return colors.ToArray();
	}

	/// <summary>
	/// Convert NV12(YUV420) to RGB format.
	/// </summary>
	/// <param name="stream">NV12 stream.</param>
	/// <param name="size">Image size in the stream.</param>
	/// <returns>RGB format colors.</returns>
	public static Color[] Nv12ToRgb(MemoryStream stream, (uint Width, uint Height) size)
	{
		int width = (int) size.Width, height = (int) size.Height;
		var total = width * height;
		var yuv = stream.ToArray();
		var colors = new List<Color>();
		for (var y = 0; y < height; y++)
		for (var x = 0; x < width; x++)
		{
			var shift = y / 2 * width + x - x % 2;
			int y0 = yuv[y * width + x];
			int u = yuv[total + shift];
			int v = yuv[total + shift + 1];
			colors.Add(YuvToRgb(y0, u, v));
		}
		return colors.ToArray();
	}

	/// <summary>
	/// Convert RGB format to bitmap
	/// </summary>
	/// <param name="size">Image size in the RGB data.</param>
	/// <param name="colors">RGB data.</param>
	/// <param name="format">Bitmap pixel format</param>
	/// <returns>Bitmap</returns>
	public static Bitmap RgbToBitmap((uint Width, uint Height) size, Color[] colors,
		System.Drawing.Imaging.PixelFormat format =
			System.Drawing.Imaging.PixelFormat.Format24bppRgb)
	{
		int width = (int) size.Width, height = (int) size.Height;
		var pic = new Bitmap(width, height, format);
		for (var x = 0; x < width; x++)
		for (var y = 0; y < height; y++)
			pic.SetPixel(x, y, colors[y * width + x]);
		return pic;
	}

	/// <summary>
	/// Convert single YUV pixel to RGB color.
	/// </summary>
	/// <param name="y">Y</param>
	/// <param name="u">U</param>
	/// <param name="v">V</param>
	/// <returns>RGB color.</returns>
	private static Color YuvToRgb(int y, int u, int v)
	{
		var r = (byte) (y + 1.4075 * (v - 128));
		var g = (byte) (y - 0.3455 * (u - 128) - 0.7169 * (v - 128));
		var b = (byte) (y + 1.7790 * (u - 128));
		return Color.FromArgb(r, g, b);
	}
}