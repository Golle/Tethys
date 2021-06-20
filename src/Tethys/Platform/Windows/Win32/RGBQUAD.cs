// ReSharper disable InconsistentNaming

using System.Runtime.InteropServices;

namespace Tethys.Platform.Windows.Win32
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct RGBQUAD
    {
        public byte RgbBlue;
        public byte RgbGreen;
        public byte RgbRed;
        public byte RgbReserver;
    }
}
