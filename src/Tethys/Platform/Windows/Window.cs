using System;
using System.Runtime.InteropServices;
using Tethys.Logging;
using Tethys.Platform.Windows.Win32;
using static Tethys.Platform.Windows.Win32.User32;
using static Tethys.Platform.Windows.Win32.WindowClassStyles;
using static Tethys.Platform.Windows.Win32.WindowsMessage;

namespace Tethys.Platform.Windows
{
    internal class Window : IDisposable
    {
        private static Window _activeWindow;
        public int Width { get; }
        public int Height { get; }
        
        public Window(in HWND handle, int width, int height)
        {
            Handle = handle;
            Width = width;
            Height = height;
            _activeWindow = this;
        }

        public HWND Handle { get; }

        public static unsafe Window Create(int width,  int height, string title)
        {
            if (_activeWindow != null)
            {
                throw new InvalidOperationException("Only a single window can be active.");
            }

            var className = typeof(Window).FullName;
            // Create the Window Class EX
            var wndClassExA = new WNDCLASSEXA
            {
                CbClsExtra = 0,
                CbSize = (uint)Marshal.SizeOf<WNDCLASSEXA>(),
                HCursor = default,
                HIcon = 0,
                LpFnWndProc = &WndProc,
                CbWndExtra = 0,
                HIconSm = 0,
                HInstance = Marshal.GetHINSTANCE(typeof(Window).Module),
                HbrBackground = 0,
                LpszClassName = className,
                Style = CS_HREDRAW | CS_OWNDC | CS_VREDRAW
            };
            Logger.Trace($"RegisterClass {className}");
            if (RegisterClassExA(wndClassExA) == 0)
            {
                Logger.Error($"RegisterClassExA failed with Win32Error {Marshal.GetLastWin32Error()}");
                return null;
            }

            // Adjust the window size to take into account for the menu etc
            const WindowStyles wsStyle = WindowStyles.OverlappedWindow | WindowStyles.Visible;
            const int windowOffset = 100;
            var windowRect = new RECT
            {
                Left = windowOffset,
                Top = windowOffset,
                Right = width + windowOffset,
                Bottom = height + windowOffset
            };
            AdjustWindowRect(ref windowRect, wsStyle, false);

            Logger.Trace($"Create window with size Width: {width} Height: {height}");
            // Create the Window
            var handle = CreateWindowExA(
                0,
                className,
                title,
                wsStyle,
                -1,
                -1,
                windowRect.Right - windowRect.Left,
                windowRect.Bottom - windowRect.Top,
                0,
                0,
                wndClassExA.HInstance,
                null
            );

            if (!handle.IsValid)
            {
                Logger.Error($"CreateWindowExA failed with Win32Error {Marshal.GetLastWin32Error()}");
                return null;
            }

            return new Window(handle, width, height);
        }

        public bool Update()
        {
            while (PeekMessageA(out var msg, 0, 0, 0, 1))
            {
                if (msg.Message == WM_QUIT)
                {
                    return false;
                }
                TranslateMessage(msg);
                DispatchMessage(msg);
            }
            return true;
        }

        [UnmanagedCallersOnly]
        public static nint WndProc(HWND hWnd, WindowsMessage message, nuint wParam, nuint lParam)
        {
            switch (message)
            {
                case WM_KILLFOCUS:
                    break;
                case WM_SETFOCUS:
                    break;
                case WM_KEYDOWN:
                case WM_SYSKEYDOWN:
                    break;
                case WM_KEYUP:
                case WM_SYSKEYUP:
                    break;
                case WM_CHAR:
                    break;
                case WM_PAINT:
                    Logger.Info("Paint");
                    break;
                case WM_SIZE:
                    //if (_activeWindow != null)
                    //{
                    //    var window = _activeWindow;
                    //    var width = (uint)(lParam & 0xffff);
                    //    var height = (uint)((lParam >> 16) & 0xffff);

                    //    window.Height = height;
                    //    window.Width = width;
                    //    window._center = new POINT((int)(window.Width / 2), (int)(window.Height / 2));
                    //}
                    //else
                    //{
                    //    Logger.Warning<Window>("No active window, changing window size will be ignored.");
                    //}
                    break;
                case WM_EXITSIZEMOVE:
                    //WindowEventHandler.OnWindowResize(_activeWindow.Width, _activeWindow.Height);
                    break;
                case WM_LBUTTONDOWN:
                    break;
                case WM_LBUTTONUP:
                    break;
                case WM_RBUTTONDOWN:
                    break;
                case WM_RBUTTONUP:
                    break;
                case WM_MOUSELEAVE:
                    break;
                case WM_MOUSEWHEEL:
                    break;
                case WM_CREATE:
                    break;
                case WM_CLOSE:
                    PostQuitMessage(0);
                    return 0;
            }
            return DefWindowProcA(hWnd, message, wParam, lParam);
        }

        public void Dispose()
        {
            Logger.Trace($"Disposing {nameof(Window)}");
            UnregisterClassA(typeof(Window).FullName, Marshal.GetHINSTANCE(typeof(Window).Module));
            DestroyWindow(Handle);
            _activeWindow = null;
        }
    }
}
