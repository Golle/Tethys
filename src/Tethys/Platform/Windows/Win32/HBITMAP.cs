using System.Runtime.CompilerServices;

namespace Tethys.Platform.Windows.Win32
{
    internal struct HBITMAP
    {
        public nint Value;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator nint(in HBITMAP bitmap) => bitmap.Value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator HBITMAP(nint bitmap) => new() { Value = bitmap };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid() => Value != 0;
    }
}
