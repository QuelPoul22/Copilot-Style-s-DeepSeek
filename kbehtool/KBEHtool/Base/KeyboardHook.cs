using System.Runtime.InteropServices;

namespace KBEHtool.Base;

public readonly struct RawKeyEvent
{
    public int VkCode { get; }
    public int ScanCode { get; }
    public int Flags { get; }
    public int Time { get; }
    public IntPtr DwExtraInfo { get; }
    public bool IsExtendedKey => (Flags & 0x01) != 0;
    public bool IsAltDown => (Flags & 0x20) != 0;
    public bool IsInjected => (Flags & 0x10) != 0;

    internal RawKeyEvent(int vkCode, int scanCode, int flags, int time, IntPtr dwExtraInfo)
    {
        VkCode = vkCode;
        ScanCode = scanCode;
        Flags = flags;
        Time = time;
        DwExtraInfo = dwExtraInfo;
    }
}

public class KeyboardHook
{
    public int WM_KEYDOWN = 0x0100;
    public int WM_KEYUP = 0x0101;
    public int WM_SYSKEYDOWN = 0x0104;
    public int WM_SYSKEYUP = 0x0105;
    public int WH_KEYBOARD_LL = 13;

    private HOOKPROC _hookProc;
    private IntPtr _hookHandle;

    [StructLayout(LayoutKind.Sequential)]
    public struct KeyBoardHookStruct
    {
        public int vkCode;
        public int scanCode;
        public int flags;
        public int time;
        public IntPtr dwExtraInfo;
    }

    public delegate IntPtr HOOKPROC(int nCode, IntPtr wParam, IntPtr lParam);

    public delegate bool KeyPreviewHandler(KeyCode keyCode, in RawKeyEvent rawData);

    [DllImport("user32.dll")]
    public static extern IntPtr SetWindowsHookEx(int idHook, HOOKPROC lpfn, IntPtr hmod, uint dwThreadId);

    [DllImport("user32.dll")]
    public static extern bool UnhookWindowsHookEx(IntPtr idHook);

    [DllImport("user32.dll")]
    public static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll")]
    public static extern IntPtr GetModuleHandle(string lpModuleName);

    public event Action<KeyCode>? KeyDown;
    public event Action<KeyCode>? KeyUp;
    public event Action<RawKeyEvent>? RawKeyDown;
    public event Action<RawKeyEvent>? RawKeyUp;

    private readonly List<KeyPreviewHandler> _keyDownPreview = new();
    private readonly List<KeyPreviewHandler> _keyUpPreview = new();

    public void AddKeyDownPreview(KeyPreviewHandler handler) => _keyDownPreview.Add(handler);
    public void RemoveKeyDownPreview(KeyPreviewHandler handler) => _keyDownPreview.Remove(handler);
    public void AddKeyUpPreview(KeyPreviewHandler handler) => _keyUpPreview.Add(handler);
    public void RemoveKeyUpPreview(KeyPreviewHandler handler) => _keyUpPreview.Remove(handler);

    public IntPtr CallBackFunc(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0)
        {
            KeyBoardHookStruct s = Marshal.PtrToStructure<KeyBoardHookStruct>(lParam);
            var raw = new RawKeyEvent(s.vkCode, s.scanCode, s.flags, s.time, s.dwExtraInfo);
            KeyCode keyCode = KeyCodeConverter.ToKeyCode((byte)s.vkCode);

            bool isDown = wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN;
            bool isUp = wParam == (IntPtr)WM_KEYUP || wParam == (IntPtr)WM_SYSKEYUP;

            if (isDown)
            {
                RawKeyDown?.Invoke(raw);
                foreach (var handler in _keyDownPreview)
                {
                    if (handler(keyCode, raw))
                        return (IntPtr)1;
                }
                KeyboardEventCenter.TriggerKeyDown(keyCode);
                KeyboardState.instance.SetKeyState(keyCode, true);
                KeyDown?.Invoke(keyCode);
            }

            if (isUp)
            {
                RawKeyUp?.Invoke(raw);
                foreach (var handler in _keyUpPreview)
                {
                    if (handler(keyCode, raw))
                        return (IntPtr)1;
                }
                KeyboardEventCenter.TriggerKeyUp(keyCode);
                KeyboardState.instance.SetKeyState(keyCode, false);
                KeyUp?.Invoke(keyCode);
            }
        }

        return CallNextHookEx(_hookHandle, nCode, wParam, lParam);
    }

    public KeyboardHook()
    {
        _hookProc = new HOOKPROC(CallBackFunc);
        IntPtr hModule = GetModuleHandle(null!);
        _hookHandle = SetWindowsHookEx(WH_KEYBOARD_LL, _hookProc, hModule, 0);
    }

    public void Unhook()
    {
        if (_hookHandle != IntPtr.Zero)
        {
            UnhookWindowsHookEx(_hookHandle);
            _hookHandle = IntPtr.Zero;
        }
    }

    public bool IsRunning => _hookHandle != IntPtr.Zero;
}
