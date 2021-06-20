
// ReSharper disable InconsistentNaming

using System.Runtime.InteropServices;

namespace Tethys.Platform.Windows.Win32
{
    internal static unsafe class GDI
    {
        private const string DllName = "User32";

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern HDC GetDC(
            [In] HWND hwnds
        );
    }
}
