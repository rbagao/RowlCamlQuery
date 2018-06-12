using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Interop;

namespace RowlCamlQuery.Internal
{
    internal static class WindowUtility
    {
        const int GWL_EXSTYLE = -20;
        const int WS_EX_DLGMODALFRAME = 0x0001;
        const int SWP_NOSIZE = 0x0001;
        const int SWP_NOMOVE = 0x0002;
        const int SWP_NOZORDER = 0x0004;
        const int SWP_FRAMECHANGED = 0x0020;
        const uint WM_SETICON = 0x0080;
        const int ICON_SMALL = 0;
        const int ICON_BIG = 1;

        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2;

        internal static void RemoveIcon(this Window w)
        {
            IntPtr hwnd = new WindowInteropHelper(w).Handle;
            int extendedStyle = NativeMethods.GetWindowLong(hwnd, GWL_EXSTYLE);
            NativeMethods.SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_DLGMODALFRAME);
            NativeMethods.SendMessage(hwnd, WM_SETICON, (IntPtr)ICON_SMALL, IntPtr.Zero);
            NativeMethods.SendMessage(hwnd, WM_SETICON, (IntPtr)ICON_BIG, IntPtr.Zero);
            NativeMethods.SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED);
        }

        internal static void SetOwner(this Window w, DependencyObject ownerObject)
        {
            var owner = Window.GetWindow(ownerObject);
            if (owner == null)
            {
                w.SetProcessMainWindowOwner();
            }
            else
            {
                w.Owner = owner;
            }
        }

        internal static void SetProcessMainWindowOwner(this Window w)
        {
            var wih = new WindowInteropHelper(w);
            wih.Owner = Process.GetCurrentProcess().MainWindowHandle;
        }
    }
}
