using System;
using System.Numerics;
using Tethys.Platform.Windows;

namespace Tethys.Graphics
{
    internal class RenderContext : IDisposable
    {
        private readonly int _width;
        private readonly int _height;
        private readonly PixelBuffer _buffer;
        

        public RenderContext(int width, int height)
        {
            _width = width;
            _height = height;

            _buffer = new PixelBuffer(_width, _height);
        }


        public void DrawLine(in Vector2 start, in Vector2 stop)
        {
            //_buffer.SetPixel(1023,767, Color.Red);
            //return;
            var x1 = (int)stop.X;
            var x0 = (int)start.X;

            var y1 = (int)stop.Y;
            var y0 = (int)start.Y;

            int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            int dy = Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2, e2;
            var color = new Color(255, 255, 0, 255);
            for (; ;)
            {
                _buffer.SetPixel(x0, y0, color);
                if (x0 == x1 && y0 == y1)
                    break;
                e2 = err;
                if (e2 > -dx)
                {
                    err -= dy; 
                    x0 += sx;
                }

                if (e2 < dy)
                {
                    err += dx; 
                    y0 += sy;
                }
            }

        }


        public void Render(DeviceContext context)
        {
            context.Render(_buffer);
        }

        public void Clear()
        {
            _buffer.Clear();
        }

        public void Dispose()
        {
            _buffer?.Dispose();
        }
    }


    internal struct Color
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;
        public Color(byte r, byte g, byte b, byte a = byte.MaxValue)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public static readonly Color Red = new (255, 255, 0);

        public readonly int ToInt() => R | G << 8 | B << 16 | A << 24;
    }
}

