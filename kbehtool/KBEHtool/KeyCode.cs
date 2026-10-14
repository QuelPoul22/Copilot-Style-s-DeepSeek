using System.Windows.Input;
using KBEHtool.Base;

namespace KBEHtool
{
    public enum KeyCode
    {
        None,

        // 字母
        A, B, C, D, E, F, G, H, I, J, K, L, M,
        N, O, P, Q, R, S, T, U, V, W, X, Y, Z,

        // 功能键
        F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12,
        F13, F14, F15, F16, F17, F18, F19, F20, F21, F22, F23, F24,

        // 主键盘数字
        Key1, Key2, Key3, Key4, Key5, Key6, Key7, Key8, Key9, Key0,

        // 小键盘数字
        Num1, Num2, Num3, Num4, Num5, Num6, Num7, Num8, Num9, Num0,

        // 方向键
        UpArrow, DownArrow, LeftArrow, RightArrow,

        // 修饰键
        LeftShift, RightShift,
        LeftControl, RightControl,
        LeftAlt, RightAlt,
        LeftWin, RightWin,

        // 符号键 (OEM)
        Oem1, Oem2, Oem3, Oem4, Oem5, Oem6, Oem7, Oem8,
        OemPlus, OemMinus,
        OemOpenBrackets, OemCloseBrackets,
        OemPipe, OemSemicolon, OemQuotes,
        OemComma, OemPeriod, OemQuestion, OemTilde,

        // 控制键
        Escape, Space, CapsLock, Tab, Enter,
        Backspace, Delete, Insert,
        Home, End, PageUp, PageDown,
        PrintScreen, ScrollLock, Pause,
        NumLock, Sleep,

        // 小键盘符号
        Divide, Multiply, Subtract, Add, Decimal, Separator,

        // 多媒体键
        VolumeMute, VolumeDown, VolumeUp,
        MediaNextTrack, MediaPrevTrack, MediaStop, MediaPlayPause,

        // 浏览器键
        BrowserBack, BrowserForward, BrowserRefresh,
        BrowserSearch, BrowserFavorites, BrowserHome,

        // 应用键
        LaunchMail, LaunchMedia, LaunchApp1, LaunchApp2,
        Apps,

        // Windows 11 Copilot 键 (VK=0x86, WPF=F23)
        Copilot,
    }

    public static class KeyCodeExtensions
    {
        public static Key ToKey(this KeyCode keyCode)
        {
            return KeyCodeConverter.ToKey(keyCode);
        }

        public static KeyCode ToKeyCode(this Key key)
        {
            return KeyCodeConverter.ToKeyCode(key);
        }

        public static bool IsPressed(this KeyCode keyCode)
        {
            return KeyboardState.instance.GetKeyState(keyCode);
        }
    }
}
