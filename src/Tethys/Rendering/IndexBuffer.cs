namespace Tethys.Rendering
{
    internal class IndexBuffer
    {
        private readonly uint[] _indices;
        public IndexBuffer(uint size)
        {
            _indices = new uint[size];
        }

        public ref uint this[uint index] => ref _indices[index];
    }
}
