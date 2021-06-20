using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Tethys.Logging;

namespace Tethys.Graphics
{
    internal unsafe class PixelBuffer : IDisposable
    {
        private readonly int _width;
        private readonly int _height;
        private int* _pixels;
        private readonly int _size;
        public void* Buffer => _pixels;

        public PixelBuffer(int width, int height)
        {
            _width = width;
            _height = height;
            const int bytesPerPixel = 4;

            _size = height * width * bytesPerPixel;
            _pixels = (int*) Marshal.AllocHGlobal(_size);
        }

        public void Clear()
        {
            Unsafe.InitBlock(_pixels, 0, (uint)_size);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetPixel(int x, int y, in Color color) => _pixels[y*_width + x] = color.ToInt();

        public void Dispose()
        {

            
            Logger.Trace($"Disposing {nameof(PixelBuffer)}");
            if (_pixels != null)
            {
                Marshal.FreeHGlobal((nint)_pixels);
                _pixels = null;
            }
        }
    }
}
