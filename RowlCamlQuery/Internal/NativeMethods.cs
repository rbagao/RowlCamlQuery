using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RowlCamlQuery.Internal
{
    internal class NativeMethods
    {
        [DllImport("NetApi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern uint DavGetUNCFromHTTPPath(string HttpPath, StringBuilder UncPath, ref uint lpSize);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern int GetWindowLong(IntPtr hwnd, int index);
        [DllImport("user32.dll")]
        internal static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);
        [DllImport("user32.dll")]
        internal static extern bool SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int x, int y, int width, int height, uint flags);
        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);
    }
}
