using System;
using System.Runtime.InteropServices;

namespace Tethys.Rendering
{
    internal unsafe class VertexBuffer<T> : IDisposable where T : unmanaged
    {
        private static readonly int VertexSize = sizeof(T);
        private readonly T* _buffer;

        public VertexBuffer(uint count)
        {
            _buffer = (T*)Marshal.AllocHGlobal((int) (count * VertexSize));
        }
       
        public ref T this[uint index] => ref _buffer[index];

        public void Dispose()
        {
            Marshal.FreeHGlobal((nint)_buffer);
        }
    }
}
