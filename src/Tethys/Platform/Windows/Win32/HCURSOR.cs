using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming

namespace Tethys.Platform.Windows.Win32
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct HCURSOR
    {
        public nint Value;
    }
}
