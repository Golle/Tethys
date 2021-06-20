using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming

namespace Tethys.Platform.Windows.Win32
{
    internal static unsafe class GDI
    {
        private const string DllName = "Gdi32";
        public const uint DIB_RGB_COLORS = 0u;
        public const uint SRCCOPY = 13369376u;


        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int BitBlt(
            [In] HDC hdc,
            [In] int x,
            [In] int y,
            [In] int cx,
            [In] int cy,
            [In] HDC hdcSrc,
            [In] int x1,
            [In] int y1,
            [In] RasterOperations rop
        );

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int StretchDIBits(
            [In] HDC hdc,
            [In] int xDest,
            [In] int yDest,
            [In] int DestWidth,
            [In] int DestHeight,
            [In] int xSrc,
            [In] int ySrc,
            [In] int SrcWidth,
            [In] int SrcHeight,
            [In] void* lpBits,
            [In] BITMAPINFO* lpbmi,
            [In] uint iUsage,
            [In] RasterOperations rop
        );

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern HDC CreateCompatibleDC(
            [In] HDC hdc
        );


        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteDC(in HDC hdc);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern HBITMAP CreateDIBSection(
            HDC hdc,
            BITMAPINFO* pbmi,
            uint usage,
            void** ppvBits,
            nint hSection,
            uint offset
        );
    }
}
