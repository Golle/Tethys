using System;
using System.Runtime.InteropServices;
using Tethys.Graphics;
using Tethys.Logging;
using Tethys.Platform.Windows.Win32;
using static Tethys.Platform.Windows.Win32.User32;

namespace Tethys.Platform.Windows
{
    internal class DeviceContext : IDisposable
    {
        public HDC Handle => _context;
        private readonly HWND _window;
        private readonly HDC _context;
        private readonly int _width;
        private readonly int _height;

        private DeviceContext(in HWND window, in HDC context, int width, int height)
        {
            _window = window;
            _context = context;
            _width = width;
            _height = height;
        }

        public static DeviceContext Create(Window window)
        {
            var context = GetDC(window.Handle);
            if (!context.IsValid())
            {
                Logger.Error($"GetDC failed with Win32Error {Marshal.GetLastWin32Error()}");
                return null;
            }
            return new DeviceContext(window.Handle, context, window.Width, window.Height);
        }
        
        public unsafe void Render(PixelBuffer pixelBuffer)
        {
            BITMAPINFO info = default;
            info.BmiHeader = new BITMAPINFOHEADER
            {
                BiSize = (uint) sizeof(BITMAPINFOHEADER),
                BiPlanes = 1,
                BiWidth = _width,
                BiHeight = _height,
                BiBitCount = 32,
                BiCompression = 0, //BI_RGB
            };
            // TODO: should use BitBlt later, so we can do offline rendering to a memory context

            GDI.StretchDIBits(
                _context,
                0, 0,
                _width, _height,
                0, 0,
                _width, _height,
                pixelBuffer.Buffer,
                &info,
                GDI.DIB_RGB_COLORS,
                RasterOperations.SRCCOPY
            );
        }

        public void Dispose()
        {
            Logger.Trace($"Disposing {nameof(DeviceContext)}");
            ReleaseDC(_window, _context);
        }
    }
}
