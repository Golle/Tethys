using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming

namespace Tethys.Platform.Windows.Win32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
}
