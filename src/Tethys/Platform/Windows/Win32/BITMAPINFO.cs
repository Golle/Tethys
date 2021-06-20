// ReSharper disable InconsistentNaming

using System.Runtime.InteropServices;

namespace Tethys.Platform.Windows.Win32
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct BITMAPINFO
    {
        public BITMAPINFOHEADER BmiHeader;
        public RGBQUAD bmiColors; // public RGBQUAD bmiColors[1];
    }
}
