// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using System.Runtime.InteropServices;

namespace Iot.Device.Media.Interop;

internal class Interop
{
	private const string LibcLibrary = "libc";

	[DllImport(LibcLibrary)]
	internal static extern int close(int fd);

	[DllImport(LibcLibrary, SetLastError = true)]
	internal static extern int ioctl(int fd, uint request, IntPtr argp);

	[DllImport(LibcLibrary, SetLastError = true)]
	internal static extern int ioctl(int fd, int request, IntPtr argp);

	[DllImport(LibcLibrary, SetLastError = true)]
	internal static extern int ioctl(int fd, uint request, ulong argp);

	[DllImport(LibcLibrary, SetLastError = true)]
	internal static extern IntPtr mmap(IntPtr addr, int length, MemoryMappedProtections prot,
		MemoryMappedFlags flags, int fd, int offset);

	[DllImport(LibcLibrary)]
	internal static extern int munmap(IntPtr addr, int length);

	[DllImport(LibcLibrary, SetLastError = true)]
	internal static extern int open([MarshalAs(UnmanagedType.LPStr)] string pathname,
		FileOpenFlags flags);

	[DllImport(LibcLibrary, SetLastError = true)]
	internal static extern int read(int fd, IntPtr buf, int count);

	[DllImport(LibcLibrary, SetLastError = true)]
	internal static extern int write(int fd, IntPtr buf, int count);

	// ReSharper disable InconsistentNaming
	private const int _IOC_NRBITS = 8;
	private const int _IOC_TYPEBITS = 8;
	private const int _IOC_SIZEBITS = 14;
	private const int _IOC_DIRBITS = 2;
	private const int _IOC_NRMASK = (1 << _IOC_NRBITS) - 1;
	private const int _IOC_TYPEMASK = (1 << _IOC_TYPEBITS) - 1;
	private const int _IOC_SIZEMASK = (1 << _IOC_SIZEBITS) - 1;
	private const int _IOC_DIRMASK = (1 << _IOC_DIRBITS) - 1;
	private const int _IOC_NRSHIFT = 0;
	private const int _IOC_TYPESHIFT = _IOC_NRSHIFT + _IOC_NRBITS;
	private const int _IOC_SIZESHIFT = _IOC_TYPESHIFT + _IOC_TYPEBITS;
	private const int _IOC_DIRSHIFT = _IOC_SIZESHIFT + _IOC_SIZEBITS;
	private const int _IOC_NONE = 0;
	private const int _IOC_WRITE = 1;
	private const int _IOC_READ = 2;

	private static int _IOC(int dir, int type, int nr, int size) =>
		(dir << _IOC_DIRSHIFT) | (type << _IOC_TYPESHIFT) | (nr << _IOC_NRSHIFT) |
		(size << _IOC_SIZESHIFT);

	internal static int _IO(int type, int nr) => _IOC(_IOC_NONE, type, nr, 0);

	internal static int _IOR(int type, int nr, Type size) =>
		_IOC(_IOC_READ, type, nr, _IOC_TYPECHECK(size));

	internal static int _IOW(int type, int nr, Type size) =>
		_IOC(_IOC_WRITE, type, nr, _IOC_TYPECHECK(size));

	internal static int _IOWR(int type, int nr, Type size) =>
		_IOC(_IOC_READ | _IOC_WRITE, type, nr, _IOC_TYPECHECK(size));

	private static int _IOC_TYPECHECK(Type t) => Marshal.SizeOf(t);
}