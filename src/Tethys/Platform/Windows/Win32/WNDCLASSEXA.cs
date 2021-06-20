using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming

namespace Tethys.Platform.Windows.Win32
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct WNDCLASSEXA
    {
        public uint CbSize;
        public WindowClassStyles Style;
        public unsafe delegate* unmanaged <HWND, WindowsMessage, nuint, nuint, nint> LpFnWndProc;
        public int CbClsExtra;
        public int CbWndExtra;
        public nint HInstance;
        public nint HIcon;
        public HCURSOR HCursor;
        public nint HbrBackground;
        public string LpszMenuName;
        public string LpszClassName;
        public nint HIconSm;
    }
}
