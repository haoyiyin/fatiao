using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace TSPlugLib
{
    public class TSPlugInterFace
    {
        private IntPtr _boundWindow = IntPtr.Zero;
        private bool _showErrorMsg = true;

        public int SetShowErrorMsg(int show)
        {
            _showErrorMsg = show != 0;
            return 1;
        }

        public int BindWindow(int hwnd, string display, string mouse, string keypad, int mode)
        {
            var handle = new IntPtr(hwnd);
            if (handle == IntPtr.Zero || !User32.IsWindow(handle))
            {
                return 0;
            }

            _boundWindow = handle;
            return 1;
        }

        public int UnBindWindow()
        {
            _boundWindow = IntPtr.Zero;
            return 1;
        }

        public int IsBind(int hwnd)
        {
            if (_boundWindow == IntPtr.Zero)
            {
                return 0;
            }

            if (hwnd == 0)
            {
                return 1;
            }

            return _boundWindow == new IntPtr(hwnd) ? 1 : 0;
        }

        public int GetSpecialWindow(int flag)
        {
            switch (flag)
            {
                case 0:
                    return ToInt(User32.GetDesktopWindow());
                case 1:
                    return ToInt(User32.GetForegroundWindow());
                default:
                    return ToInt(User32.GetDesktopWindow());
            }
        }

        public int FindWindow(string className, string title)
        {
            return ToInt(User32.FindWindow(className, title));
        }

        public int GetMousePointWindow()
        {
            User32.GetCursorPos(out var point);
            return ToInt(User32.WindowFromPoint(point));
        }

        public int GetPointWindow(int x, int y)
        {
            return ToInt(User32.WindowFromPoint(new User32.POINT { X = x, Y = y }));
        }

        public string GetWindowTitle(int hwnd)
        {
            var handle = new IntPtr(hwnd);
            var buffer = new StringBuilder(512);
            User32.GetWindowText(handle, buffer, buffer.Capacity);
            return buffer.ToString();
        }

        public int GetClientSize(int hwnd, out object width, out object height)
        {
            if (!TryGetClientRect(hwnd, out var rect))
            {
                width = 0;
                height = 0;
                return 0;
            }

            width = rect.Width;
            height = rect.Height;
            return 1;
        }

        public int GetClientRect(int hwnd, out object x1, out object y1, out object x2, out object y2)
        {
            if (!TryGetClientRectInScreen(hwnd, out var rect))
            {
                x1 = y1 = x2 = y2 = 0;
                return 0;
            }

            x1 = rect.Left;
            y1 = rect.Top;
            x2 = rect.Right;
            y2 = rect.Bottom;
            return 1;
        }

        public int GetWindowRect(int hwnd, out object x1, out object y1, out object x2, out object y2)
        {
            var handle = new IntPtr(hwnd);
            if (!User32.GetWindowRect(handle, out var rect))
            {
                x1 = y1 = x2 = y2 = 0;
                return 0;
            }

            x1 = rect.Left;
            y1 = rect.Top;
            x2 = rect.Right;
            y2 = rect.Bottom;
            return 1;
        }

        public int SetWindowSize(int hwnd, int width, int height)
        {
            var handle = new IntPtr(hwnd);
            return User32.SetWindowPos(handle, IntPtr.Zero, 0, 0, width, height, User32.SetWindowPosFlags.SWP_NOMOVE | User32.SetWindowPosFlags.SWP_NOZORDER) ? 1 : 0;
        }

        public int SetClientSize(int hwnd, int width, int height)
        {
            var handle = new IntPtr(hwnd);
            if (handle == IntPtr.Zero)
            {
                return 0;
            }

            var style = (uint)User32.GetWindowLong(handle, User32.GWL_STYLE);
            var exStyle = (uint)User32.GetWindowLong(handle, User32.GWL_EXSTYLE);
            var rect = new User32.RECT { Left = 0, Top = 0, Right = width, Bottom = height };
            if (!User32.AdjustWindowRectEx(ref rect, style, false, exStyle))
            {
                return 0;
            }

            var windowWidth = rect.Right - rect.Left;
            var windowHeight = rect.Bottom - rect.Top;
            return User32.SetWindowPos(handle, IntPtr.Zero, 0, 0, windowWidth, windowHeight, User32.SetWindowPosFlags.SWP_NOMOVE | User32.SetWindowPosFlags.SWP_NOZORDER) ? 1 : 0;
        }

        public int SetWindowState(int hwnd, int state)
        {
            var handle = new IntPtr(hwnd);
            if (handle == IntPtr.Zero)
            {
                return 0;
            }

            switch (state)
            {
                case 0:
                    User32.PostMessage(handle, User32.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                    return 1;
                case 2:
                    return User32.ShowWindow(handle, User32.SW_MINIMIZE) ? 1 : 0;
                case 4:
                    return User32.ShowWindow(handle, User32.SW_MAXIMIZE) ? 1 : 0;
                case 6:
                    return User32.ShowWindow(handle, User32.SW_HIDE) ? 1 : 0;
                case 7:
                    return User32.ShowWindow(handle, User32.SW_SHOW) ? 1 : 0;
                case 8:
                    return User32.SetWindowPos(handle, User32.HWND_TOPMOST, 0, 0, 0, 0, User32.SetWindowPosFlags.SWP_NOMOVE | User32.SetWindowPosFlags.SWP_NOSIZE) ? 1 : 0;
                case 9:
                    return User32.SetWindowPos(handle, User32.HWND_NOTOPMOST, 0, 0, 0, 0, User32.SetWindowPosFlags.SWP_NOMOVE | User32.SetWindowPosFlags.SWP_NOSIZE) ? 1 : 0;
                case 10:
                    return User32.EnableWindow(handle, false) ? 1 : 0;
                case 11:
                    return User32.EnableWindow(handle, true) ? 1 : 0;
                default:
                    return 0;
            }
        }

        public int MoveWindow(int hwnd, int x, int y)
        {
            var handle = new IntPtr(hwnd);
            if (!User32.GetWindowRect(handle, out var rect))
            {
                return 0;
            }

            var width = rect.Right - rect.Left;
            var height = rect.Bottom - rect.Top;
            return User32.MoveWindow(handle, x, y, width, height, true) ? 1 : 0;
        }

        public int SetWindowText(int hwnd, string text)
        {
            return User32.SetWindowText(new IntPtr(hwnd), text) ? 1 : 0;
        }

        public int SetWindowTransparent(int hwnd, int transparent)
        {
            var handle = new IntPtr(hwnd);
            if (handle == IntPtr.Zero)
            {
                return 0;
            }

            var exStyle = User32.GetWindowLong(handle, User32.GWL_EXSTYLE);
            if ((exStyle & User32.WS_EX_LAYERED) == 0)
            {
                User32.SetWindowLong(handle, User32.GWL_EXSTYLE, exStyle | User32.WS_EX_LAYERED);
            }

            var opacity = Math.Max(0, Math.Min(255, transparent));
            return User32.SetLayeredWindowAttributes(handle, 0, (byte)opacity, User32.LWA_ALPHA) ? 1 : 0;
        }

        public int GetWindowState(int hwnd, int state)
        {
            var handle = new IntPtr(hwnd);
            if (handle == IntPtr.Zero)
            {
                return 0;
            }

            switch (state)
            {
                case 0:
                    return User32.IsWindow(handle) ? 1 : 0;
                case 1:
                    return User32.GetForegroundWindow() == handle ? 1 : 0;
                case 3:
                    return User32.IsIconic(handle) ? 1 : 0;
                case 4:
                    return User32.IsZoomed(handle) ? 1 : 0;
                case 5:
                    return (User32.GetWindowLong(handle, User32.GWL_EXSTYLE) & User32.WS_EX_TOPMOST) != 0 ? 1 : 0;
                case 6:
                    return User32.IsHungAppWindow(handle) ? 1 : 0;
                default:
                    return 0;
            }
        }

        public int MoveTo(int x, int y)
        {
            var point = ToScreenPoint(x, y);
            return User32.SetCursorPos(point.X, point.Y) ? 1 : 0;
        }

        public int MoveR(int dx, int dy)
        {
            if (!User32.GetCursorPos(out var point))
            {
                return 0;
            }

            return User32.SetCursorPos(point.X + dx, point.Y + dy) ? 1 : 0;
        }

        public int LeftClick()
        {
            MouseEvent(User32.MOUSEEVENTF_LEFTDOWN | User32.MOUSEEVENTF_LEFTUP);
            return 1;
        }

        public int LeftDown()
        {
            MouseEvent(User32.MOUSEEVENTF_LEFTDOWN);
            return 1;
        }

        public int LeftUp()
        {
            MouseEvent(User32.MOUSEEVENTF_LEFTUP);
            return 1;
        }

        public int RightClick()
        {
            MouseEvent(User32.MOUSEEVENTF_RIGHTDOWN | User32.MOUSEEVENTF_RIGHTUP);
            return 1;
        }

        public int RightDown()
        {
            MouseEvent(User32.MOUSEEVENTF_RIGHTDOWN);
            return 1;
        }

        public int RightUp()
        {
            MouseEvent(User32.MOUSEEVENTF_RIGHTUP);
            return 1;
        }

        public int MiddleClick()
        {
            MouseEvent(User32.MOUSEEVENTF_MIDDLEDOWN | User32.MOUSEEVENTF_MIDDLEUP);
            return 1;
        }

        public int WheelUp()
        {
            MouseEvent(User32.MOUSEEVENTF_WHEEL, 120);
            return 1;
        }

        public int WheelDown()
        {
            MouseEvent(User32.MOUSEEVENTF_WHEEL, -120);
            return 1;
        }

        public int KeyDown(int keyCode)
        {
            User32.keybd_event((byte)keyCode, 0, 0, 0);
            return 1;
        }

        public int KeyUp(int keyCode)
        {
            User32.keybd_event((byte)keyCode, 0, User32.KEYEVENTF_KEYUP, 0);
            return 1;
        }

        public int KeyPress(int keyCode)
        {
            KeyDown(keyCode);
            KeyUp(keyCode);
            return 1;
        }

        public int SendString(int hwnd, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return 0;
            }

            if (hwnd != 0)
            {
                User32.SetForegroundWindow(new IntPtr(hwnd));
            }

            try
            {
                SendKeys.SendWait(text);
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public int Capture(int x1, int y1, int x2, int y2, string file)
        {
            var rect = NormalizeRegion(x1, y1, x2, y2);
            if (rect.Width <= 0 || rect.Height <= 0)
            {
                return 0;
            }

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(file) ?? ".");
                using (var bitmap = new Bitmap(rect.Width, rect.Height))
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(rect.Location, Point.Empty, rect.Size);
                    bitmap.Save(file, ImageFormat.Bmp);
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public int FindPic(int x1, int y1, int x2, int y2, string picName, string color, double sim, int dir, out object outX, out object outY)
        {
            outX = 0;
            outY = 0;
            if (!File.Exists(picName))
            {
                return -1;
            }

            var rect = NormalizeRegion(x1, y1, x2, y2);
            if (rect.Width <= 0 || rect.Height <= 0)
            {
                return -1;
            }

            try
            {
                using (var source = CaptureRegion(rect))
                using (var template = new Bitmap(picName))
                {
                    if (TryFindTemplate(source, template, sim, out var match))
                    {
                        outX = x1 + match.X;
                        outY = y1 + match.Y;
                        return 1;
                    }
                }
            }
            catch
            {
                if (_showErrorMsg)
                {
                    return -1;
                }
            }

            return -1;
        }

        public int FindColor(int x1, int y1, int x2, int y2, string color, double sim, int dir, out object outX, out object outY)
        {
            outX = 0;
            outY = 0;
            if (!TryParseColor(color, out var target))
            {
                return -1;
            }

            var rect = NormalizeRegion(x1, y1, x2, y2);
            if (rect.Width <= 0 || rect.Height <= 0)
            {
                return -1;
            }

            try
            {
                using (var source = CaptureRegion(rect))
                {
                    var tolerance = ToTolerance(sim);
                    for (var y = 0; y < source.Height; y++)
                    {
                        for (var x = 0; x < source.Width; x++)
                        {
                            var current = source.GetPixel(x, y);
                            if (IsColorMatch(current, target, tolerance))
                            {
                                outX = x1 + x;
                                outY = y1 + y;
                                return 1;
                            }
                        }
                    }
                }
            }
            catch
            {
                if (_showErrorMsg)
                {
                    return -1;
                }
            }

            return -1;
        }

        public int CmpColor(int x, int y, string color, double sim)
        {
            if (!TryParseColor(color, out var target))
            {
                return -1;
            }

            var point = ToScreenPoint(x, y);
            var current = GetScreenColor(point.X, point.Y);
            var tolerance = ToTolerance(sim);
            return IsColorMatch(current, target, tolerance) ? 1 : -1;
        }

        public string GetColor(int x, int y)
        {
            var point = ToScreenPoint(x, y);
            var color = GetScreenColor(point.X, point.Y);
            return ColorToHex(color);
        }

        private static int ToInt(IntPtr handle)
        {
            return unchecked((int)handle.ToInt64());
        }

        private void MouseEvent(uint flags, int data = 0)
        {
            if (_boundWindow != IntPtr.Zero)
            {
                User32.GetClientRect(_boundWindow, out var rect);
                var left = rect.Left;
                var top = rect.Top;
                var right = rect.Right;
                var bottom = rect.Bottom;
                var offset = new User32.POINT { X = 0, Y = 0 };
                User32.ClientToScreen(_boundWindow, ref offset);
                left += offset.X;
                top += offset.Y;
                right += offset.X;
                bottom += offset.Y;
                var width = Math.Max(1, right - left);
                var height = Math.Max(1, bottom - top);
                var normalizedX = (int)Math.Round((GetCursorX() - left) * 65535.0 / width);
                var normalizedY = (int)Math.Round((GetCursorY() - top) * 65535.0 / height);
                normalizedX = Math.Max(0, Math.Min(65535, normalizedX));
                normalizedY = Math.Max(0, Math.Min(65535, normalizedY));
                User32.mouse_event(flags | User32.MOUSEEVENTF_ABSOLUTE, normalizedX, normalizedY, data, 0);
                return;
            }

            User32.mouse_event(flags, 0, 0, data, 0);
        }

        private Rectangle NormalizeRegion(int x1, int y1, int x2, int y2)
        {
            var left = Math.Min(x1, x2);
            var top = Math.Min(y1, y2);
            var right = Math.Max(x1, x2);
            var bottom = Math.Max(y1, y2);

            if (_boundWindow != IntPtr.Zero)
            {
                var screenOrigin = ToScreenPoint(left, top);
                return new Rectangle(screenOrigin.X, screenOrigin.Y, right - left, bottom - top);
            }

            return new Rectangle(left, top, right - left, bottom - top);
        }

        private Point ToScreenPoint(int x, int y)
        {
            if (_boundWindow == IntPtr.Zero)
            {
                return new Point(x, y);
            }

            var point = new User32.POINT { X = x, Y = y };
            User32.ClientToScreen(_boundWindow, ref point);
            return new Point(point.X, point.Y);
        }

        private static Bitmap CaptureRegion(Rectangle rect)
        {
            var bitmap = new Bitmap(rect.Width, rect.Height);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(rect.Location, Point.Empty, rect.Size);
            }
            return bitmap;
        }

        private static bool TryFindTemplate(Bitmap source, Bitmap template, double sim, out Point match)
        {
            match = Point.Empty;
            if (template.Width > source.Width || template.Height > source.Height)
            {
                return false;
            }

            var tolerance = ToTolerance(sim);
            var maxMismatch = (int)Math.Floor(template.Width * template.Height * (1 - sim));

            for (var y = 0; y <= source.Height - template.Height; y++)
            {
                for (var x = 0; x <= source.Width - template.Width; x++)
                {
                    var mismatch = 0;
                    for (var ty = 0; ty < template.Height; ty++)
                    {
                        for (var tx = 0; tx < template.Width; tx++)
                        {
                            var sourceColor = source.GetPixel(x + tx, y + ty);
                            var templateColor = template.GetPixel(tx, ty);
                            if (!IsColorMatch(sourceColor, templateColor, tolerance))
                            {
                                mismatch++;
                                if (mismatch > maxMismatch)
                                {
                                    goto NextPosition;
                                }
                            }
                        }
                    }

                    match = new Point(x, y);
                    return true;

                NextPosition:
                    continue;
                }
            }

            return false;
        }

        private static int ToTolerance(double sim)
        {
            if (sim <= 0)
            {
                return 255;
            }

            if (sim >= 1)
            {
                return 0;
            }

            return (int)Math.Round((1 - sim) * 255);
        }

        private static bool TryParseColor(string value, out Color color)
        {
            color = Color.Empty;
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            var hex = value.Trim().TrimStart('#');
            if (hex.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                hex = hex.Substring(2);
            }

            var splitIndex = hex.IndexOf('-');
            if (splitIndex > 0)
            {
                hex = hex.Substring(0, splitIndex);
            }

            if (hex.Length < 6)
            {
                return false;
            }

            try
            {
                var r = Convert.ToInt32(hex.Substring(0, 2), 16);
                var g = Convert.ToInt32(hex.Substring(2, 2), 16);
                var b = Convert.ToInt32(hex.Substring(4, 2), 16);
                color = Color.FromArgb(r, g, b);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool TryGetClientRect(int hwnd, out Rectangle rect)
        {
            rect = Rectangle.Empty;
            var handle = new IntPtr(hwnd);
            if (handle == IntPtr.Zero)
            {
                return false;
            }

            if (!User32.GetClientRect(handle, out var clientRect))
            {
                return false;
            }

            rect = new Rectangle(0, 0, clientRect.Right - clientRect.Left, clientRect.Bottom - clientRect.Top);
            return true;
        }

        private static bool TryGetClientRectInScreen(int hwnd, out Rectangle rect)
        {
            rect = Rectangle.Empty;
            var handle = new IntPtr(hwnd);
            if (handle == IntPtr.Zero)
            {
                return false;
            }

            if (!User32.GetClientRect(handle, out var clientRect))
            {
                return false;
            }

            var origin = new User32.POINT { X = 0, Y = 0 };
            if (!User32.ClientToScreen(handle, ref origin))
            {
                return false;
            }

            var width = clientRect.Right - clientRect.Left;
            var height = clientRect.Bottom - clientRect.Top;
            rect = new Rectangle(origin.X, origin.Y, width, height);
            return true;
        }

        private static Color GetScreenColor(int x, int y)
        {
            var hdc = User32.GetDC(IntPtr.Zero);
            var pixel = Gdi32.GetPixel(hdc, x, y);
            User32.ReleaseDC(IntPtr.Zero, hdc);
            var r = pixel & 0x000000FF;
            var g = (pixel & 0x0000FF00) >> 8;
            var b = (pixel & 0x00FF0000) >> 16;
            return Color.FromArgb(r, g, b);
        }

        private static int GetCursorX()
        {
            return User32.GetCursorPos(out var point) ? point.X : 0;
        }

        private static int GetCursorY()
        {
            return User32.GetCursorPos(out var point) ? point.Y : 0;
        }

        private static string ColorToHex(Color color)
        {
            return $"{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        private static bool IsColorMatch(Color actual, Color target, int tolerance)
        {
            return Math.Abs(actual.R - target.R) <= tolerance
                && Math.Abs(actual.G - target.G) <= tolerance
                && Math.Abs(actual.B - target.B) <= tolerance;
        }

        private static class User32
        {
            public const int GWL_EXSTYLE = -20;
            public const int GWL_STYLE = -16;
            public const int WS_EX_LAYERED = 0x00080000;
            public const int WS_EX_TOPMOST = 0x00000008;
            public const int SW_HIDE = 0;
            public const int SW_SHOW = 5;
            public const int SW_MINIMIZE = 6;
            public const int SW_MAXIMIZE = 3;
            public const int LWA_ALPHA = 0x2;
            public const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
            public const uint MOUSEEVENTF_LEFTUP = 0x0004;
            public const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
            public const uint MOUSEEVENTF_RIGHTUP = 0x0010;
            public const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
            public const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
            public const uint MOUSEEVENTF_WHEEL = 0x0800;
            public const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
            public const uint KEYEVENTF_KEYUP = 0x0002;
            public const int WM_CLOSE = 0x0010;

            public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
            public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

            [Flags]
            public enum SetWindowPosFlags : uint
            {
                SWP_NOSIZE = 0x0001,
                SWP_NOMOVE = 0x0002,
                SWP_NOZORDER = 0x0004,
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int Left;
                public int Top;
                public int Right;
                public int Bottom;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct POINT
            {
                public int X;
                public int Y;
            }

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool IsWindow(IntPtr hWnd);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern IntPtr WindowFromPoint(POINT point);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool GetCursorPos(out POINT lpPoint);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool SetCursorPos(int x, int y);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool ClientToScreen(IntPtr hWnd, ref POINT lpPoint);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern IntPtr GetDesktopWindow();

            [DllImport("user32.dll", SetLastError = true)]
            public static extern IntPtr GetForegroundWindow();

            [DllImport("user32.dll", SetLastError = true)]
            public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool SetWindowText(IntPtr hWnd, string lpString);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool AdjustWindowRectEx(ref RECT lpRect, uint dwStyle, bool bMenu, uint dwExStyle);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool IsIconic(IntPtr hWnd);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool IsZoomed(IntPtr hWnd);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool IsHungAppWindow(IntPtr hWnd);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool SetForegroundWindow(IntPtr hWnd);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool PostMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern IntPtr GetDC(IntPtr hWnd);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern void mouse_event(uint dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        }

        private static class Gdi32
        {
            [DllImport("gdi32.dll", SetLastError = true)]
            public static extern int GetPixel(IntPtr hdc, int nXPos, int nYPos);
        }
    }
}
