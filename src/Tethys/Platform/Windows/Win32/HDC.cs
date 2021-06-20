using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace Tethys.Platform.Windows.Win32
{
    [SkipLocalsInit]
    [StructLayout(LayoutKind.Sequential)]
    internal struct HDC
    {
        public nint Value;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator nint(in HDC hwnd) => hwnd.Value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator HDC(nint hwnd) => new() {Value = hwnd};

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid() => Value != 0;
    }
}
