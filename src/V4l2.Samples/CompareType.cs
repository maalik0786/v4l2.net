/*using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iot.Device.Media.Interop.Unix.Libc;

namespace V4l2.Samples
{
	public class CompareType<T> where T : struct
	{
		public CompareType<T>()
		{
			var safe = Unsafe.SizeOf<T>();
			var marshal = Marshal.SizeOf<T>();
			Debug.WriteLine(typeof(T).Name + $"{safe.ToString()},{marshal.ToString()}");
		}

		public void Run()
		{
			CompareType<v4l2_capability>();
			CompareType<v4l2_fmtdesc>();
			CompareType<v4l2_requestbuffers>();
			CompareType<int>();
			CompareType<v4l2_control>();
			CompareType<v4l2_queryctrl>();
			CompareType<v4l2_cropcap>();
			CompareType<v4l2_crop>();
			CompareType<v4l2_format>();
			CompareType<v4l2_format_aligned>();
			CompareType<v4l2_frmsizeenum>();
			CompareType<v4l2_buffer>();
		}
	}
}*/