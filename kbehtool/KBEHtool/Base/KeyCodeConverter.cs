using System.Windows.Input;

namespace KBEHtool.Base;

public static class KeyCodeConverter
{
    private static readonly Dictionary<KeyCode, Key> KeyCodeToKey = new();
    private static readonly Dictionary<Key, KeyCode> KeyToKeyCode = new();
    private static readonly Dictionary<KeyCode, byte> KeyCodeToVk = new();
    private static readonly Dictionary<byte, KeyCode> VkToKeyCode = new();

    static KeyCodeConverter()
    {
        Add(KeyCode.A, Key.A, ByteVirtualKey.vbKeyA);
        Add(KeyCode.B, Key.B, ByteVirtualKey.vbKeyB);
        Add(KeyCode.C, Key.C, ByteVirtualKey.vbKeyC);
        Add(KeyCode.D, Key.D, ByteVirtualKey.vbKeyD);
        Add(KeyCode.E, Key.E, ByteVirtualKey.vbKeyE);
        Add(KeyCode.F, Key.F, ByteVirtualKey.vbKeyF);
        Add(KeyCode.G, Key.G, ByteVirtualKey.vbKeyG);
        Add(KeyCode.H, Key.H, ByteVirtualKey.vbKeyH);
        Add(KeyCode.I, Key.I, ByteVirtualKey.vbKeyI);
        Add(KeyCode.J, Key.J, ByteVirtualKey.vbKeyJ);
        Add(KeyCode.K, Key.K, ByteVirtualKey.vbKeyK);
        Add(KeyCode.L, Key.L, ByteVirtualKey.vbKeyL);
        Add(KeyCode.M, Key.M, ByteVirtualKey.vbKeyM);
        Add(KeyCode.N, Key.N, ByteVirtualKey.vbKeyN);
        Add(KeyCode.O, Key.O, ByteVirtualKey.vbKeyO);
        Add(KeyCode.P, Key.P, ByteVirtualKey.vbKeyP);
        Add(KeyCode.Q, Key.Q, ByteVirtualKey.vbKeyQ);
        Add(KeyCode.R, Key.R, ByteVirtualKey.vbKeyR);
        Add(KeyCode.S, Key.S, ByteVirtualKey.vbKeyS);
        Add(KeyCode.T, Key.T, ByteVirtualKey.vbKeyT);
        Add(KeyCode.U, Key.U, ByteVirtualKey.vbKeyU);
        Add(KeyCode.V, Key.V, ByteVirtualKey.vbKeyV);
        Add(KeyCode.W, Key.W, ByteVirtualKey.vbKeyW);
        Add(KeyCode.X, Key.X, ByteVirtualKey.vbKeyX);
        Add(KeyCode.Y, Key.Y, ByteVirtualKey.vbKeyY);
        Add(KeyCode.Z, Key.Z, ByteVirtualKey.vbKeyZ);

        Add(KeyCode.F1, Key.F1, ByteVirtualKey.vbKeyF1);
        Add(KeyCode.F2, Key.F2, ByteVirtualKey.vbKeyF2);
        Add(KeyCode.F3, Key.F3, ByteVirtualKey.vbKeyF3);
        Add(KeyCode.F4, Key.F4, ByteVirtualKey.vbKeyF4);
        Add(KeyCode.F5, Key.F5, ByteVirtualKey.vbKeyF5);
        Add(KeyCode.F6, Key.F6, ByteVirtualKey.vbKeyF6);
        Add(KeyCode.F7, Key.F7, ByteVirtualKey.vbKeyF7);
        Add(KeyCode.F8, Key.F8, ByteVirtualKey.vbKeyF8);
        Add(KeyCode.F9, Key.F9, ByteVirtualKey.vbKeyF9);
        Add(KeyCode.F10, Key.F10, ByteVirtualKey.vbKeyF10);
        Add(KeyCode.F11, Key.F11, ByteVirtualKey.vbKeyF11);
        Add(KeyCode.F12, Key.F12, ByteVirtualKey.vbKeyF12);
        Add(KeyCode.F13, Key.F13, ByteVirtualKey.vbKeyF13);
        Add(KeyCode.F14, Key.F14, ByteVirtualKey.vbKeyF14);
        Add(KeyCode.F15, Key.F15, ByteVirtualKey.vbKeyF15);
        Add(KeyCode.F16, Key.F16, ByteVirtualKey.vbKeyF16);
        Add(KeyCode.F17, Key.F17, ByteVirtualKey.vbKeyF17);
        Add(KeyCode.F18, Key.F18, ByteVirtualKey.vbKeyF18);
        Add(KeyCode.F19, Key.F19, ByteVirtualKey.vbKeyF19);
        Add(KeyCode.F20, Key.F20, ByteVirtualKey.vbKeyF20);
        Add(KeyCode.F21, Key.F21, ByteVirtualKey.vbKeyF21);
        Add(KeyCode.F22, Key.F22, ByteVirtualKey.vbKeyF22);
        Add(KeyCode.F23, Key.F23, ByteVirtualKey.vbKeyF23);
        Add(KeyCode.F24, Key.F24, ByteVirtualKey.vbKeyF24);

        Add(KeyCode.Key1, Key.D1, ByteVirtualKey.vbKey1);
        Add(KeyCode.Key2, Key.D2, ByteVirtualKey.vbKey2);
        Add(KeyCode.Key3, Key.D3, ByteVirtualKey.vbKey3);
        Add(KeyCode.Key4, Key.D4, ByteVirtualKey.vbKey4);
        Add(KeyCode.Key5, Key.D5, ByteVirtualKey.vbKey5);
        Add(KeyCode.Key6, Key.D6, ByteVirtualKey.vbKey6);
        Add(KeyCode.Key7, Key.D7, ByteVirtualKey.vbKey7);
        Add(KeyCode.Key8, Key.D8, ByteVirtualKey.vbKey8);
        Add(KeyCode.Key9, Key.D9, ByteVirtualKey.vbKey9);
        Add(KeyCode.Key0, Key.D0, ByteVirtualKey.vbKey0);

        Add(KeyCode.Num1, Key.NumPad1, ByteVirtualKey.vbKeyNumpad1);
        Add(KeyCode.Num2, Key.NumPad2, ByteVirtualKey.vbKeyNumpad2);
        Add(KeyCode.Num3, Key.NumPad3, ByteVirtualKey.vbKeyNumpad3);
        Add(KeyCode.Num4, Key.NumPad4, ByteVirtualKey.vbKeyNumpad4);
        Add(KeyCode.Num5, Key.NumPad5, ByteVirtualKey.vbKeyNumpad5);
        Add(KeyCode.Num6, Key.NumPad6, ByteVirtualKey.vbKeyNumpad6);
        Add(KeyCode.Num7, Key.NumPad7, ByteVirtualKey.vbKeyNumpad7);
        Add(KeyCode.Num8, Key.NumPad8, ByteVirtualKey.vbKeyNumpad8);
        Add(KeyCode.Num9, Key.NumPad9, ByteVirtualKey.vbKeyNumpad9);
        Add(KeyCode.Num0, Key.NumPad0, ByteVirtualKey.vbKeyNumpad0);

        Add(KeyCode.UpArrow, Key.Up, ByteVirtualKey.vbKeyUp);
        Add(KeyCode.DownArrow, Key.Down, ByteVirtualKey.vbKeyDown);
        Add(KeyCode.LeftArrow, Key.Left, ByteVirtualKey.vbKeyLeft);
        Add(KeyCode.RightArrow, Key.Right, ByteVirtualKey.vbKeyRight);

        Add(KeyCode.LeftShift, Key.LeftShift, ByteVirtualKey.vbKeyLShift);
        Add(KeyCode.RightShift, Key.RightShift, ByteVirtualKey.vbKeyRShift);
        Add(KeyCode.LeftControl, Key.LeftCtrl, ByteVirtualKey.vbKeyLControl);
        Add(KeyCode.RightControl, Key.RightCtrl, ByteVirtualKey.vbKeyRControl);
        Add(KeyCode.LeftAlt, Key.LeftAlt, ByteVirtualKey.vbKeyLAlt);
        Add(KeyCode.RightAlt, Key.RightAlt, ByteVirtualKey.vbKeyRAlt);
        Add(KeyCode.LeftWin, Key.LWin, ByteVirtualKey.vbKeyLWin);
        Add(KeyCode.RightWin, Key.RWin, ByteVirtualKey.vbKeyRWin);

        Add(KeyCode.Escape, Key.Escape, ByteVirtualKey.vbKeyEscape);
        Add(KeyCode.Space, Key.Space, ByteVirtualKey.vbKeySpace);
        Add(KeyCode.CapsLock, Key.CapsLock, ByteVirtualKey.vbKeyCapital);
        Add(KeyCode.Tab, Key.Tab, ByteVirtualKey.vbKeyTab);
        Add(KeyCode.Enter, Key.Enter, ByteVirtualKey.vbKeyReturn);
        Add(KeyCode.Backspace, Key.Back, ByteVirtualKey.vbKeyBack);
        Add(KeyCode.Delete, Key.Delete, ByteVirtualKey.vbKeyDelete);
        Add(KeyCode.Insert, Key.Insert, ByteVirtualKey.vbKeyInsert);
        Add(KeyCode.Home, Key.Home, ByteVirtualKey.vbKeyHome);
        Add(KeyCode.End, Key.End, ByteVirtualKey.vbKeyEnd);
        Add(KeyCode.PageUp, Key.PageUp, ByteVirtualKey.vbKeyPageUp);
        Add(KeyCode.PageDown, Key.PageDown, ByteVirtualKey.vbKeyPageDown);
        Add(KeyCode.PrintScreen, Key.PrintScreen, ByteVirtualKey.vbKeySnapshot);
        Add(KeyCode.ScrollLock, Key.Scroll, ByteVirtualKey.vbKeyScroll);
        Add(KeyCode.Pause, Key.Pause, ByteVirtualKey.vbKeyPause);
        Add(KeyCode.NumLock, Key.NumLock, ByteVirtualKey.vbKeyNumlock);
        Add(KeyCode.Sleep, Key.Sleep, ByteVirtualKey.vbKeySleep);
        Add(KeyCode.Apps, Key.Apps, ByteVirtualKey.vbKeyApps);

        // OEM 符号
        Add(KeyCode.Oem1, Key.Oem1, ByteVirtualKey.vbKeyOem1);
        Add(KeyCode.Oem2, Key.Oem2, ByteVirtualKey.vbKeyOem2);
        Add(KeyCode.Oem3, Key.Oem3, ByteVirtualKey.vbKeyOem3);
        Add(KeyCode.Oem4, Key.Oem4, ByteVirtualKey.vbKeyOem4);
        Add(KeyCode.Oem5, Key.Oem5, ByteVirtualKey.vbKeyOem5);
        Add(KeyCode.Oem6, Key.Oem6, ByteVirtualKey.vbKeyOem6);
        Add(KeyCode.Oem7, Key.Oem7, ByteVirtualKey.vbKeyOem7);
        Add(KeyCode.Oem8, Key.Oem8, ByteVirtualKey.vbKeyOem8);
        Add(KeyCode.OemPlus, Key.OemPlus, ByteVirtualKey.vbKeyOemPlus);
        Add(KeyCode.OemMinus, Key.OemMinus, ByteVirtualKey.vbKeyOemMinus);
        Add(KeyCode.OemComma, Key.OemComma, ByteVirtualKey.vbKeyOemComma);
        Add(KeyCode.OemPeriod, Key.OemPeriod, ByteVirtualKey.vbKeyOemPeriod);
        Add(KeyCode.OemQuestion, Key.OemQuestion, ByteVirtualKey.vbKeyOem2);
        Add(KeyCode.OemTilde, Key.OemTilde, ByteVirtualKey.vbKeyOem3);
        Add(KeyCode.OemOpenBrackets, Key.OemOpenBrackets, ByteVirtualKey.vbKeyOem4);
        Add(KeyCode.OemCloseBrackets, Key.OemCloseBrackets, ByteVirtualKey.vbKeyOem6);
        Add(KeyCode.OemPipe, Key.OemPipe, ByteVirtualKey.vbKeyOem5);
        Add(KeyCode.OemSemicolon, Key.OemSemicolon, ByteVirtualKey.vbKeyOem1);
        Add(KeyCode.OemQuotes, Key.OemQuotes, ByteVirtualKey.vbKeyOem7);

        // 小键盘符号
        Add(KeyCode.Divide, Key.Divide, ByteVirtualKey.vbKeyDivide);
        Add(KeyCode.Multiply, Key.Multiply, ByteVirtualKey.vbKeyMultiply);
        Add(KeyCode.Subtract, Key.Subtract, ByteVirtualKey.vbKeySubtract);
        Add(KeyCode.Add, Key.Add, ByteVirtualKey.vbKeyAdd);
        Add(KeyCode.Decimal, Key.Decimal, ByteVirtualKey.vbKeyDecimal);
        Add(KeyCode.Separator, Key.Separator, ByteVirtualKey.vbKeySeparator);

        // 多媒体
        Add(KeyCode.VolumeMute, Key.VolumeMute, ByteVirtualKey.vbKeyVolumeMute);
        Add(KeyCode.VolumeDown, Key.VolumeDown, ByteVirtualKey.vbKeyVolumeDown);
        Add(KeyCode.VolumeUp, Key.VolumeUp, ByteVirtualKey.vbKeyVolumeUp);
        Add(KeyCode.MediaNextTrack, Key.MediaNextTrack, ByteVirtualKey.vbKeyMediaNext);
        Add(KeyCode.MediaPrevTrack, Key.MediaPreviousTrack, ByteVirtualKey.vbKeyMediaPrev);
        Add(KeyCode.MediaStop, Key.MediaStop, ByteVirtualKey.vbKeyMediaStop);
        Add(KeyCode.MediaPlayPause, Key.MediaPlayPause, ByteVirtualKey.vbKeyMediaPlayPause);

        // 浏览器
        Add(KeyCode.BrowserBack, Key.BrowserBack, ByteVirtualKey.vbKeyBrowserBack);
        Add(KeyCode.BrowserForward, Key.BrowserForward, ByteVirtualKey.vbKeyBrowserForward);
        Add(KeyCode.BrowserRefresh, Key.BrowserRefresh, ByteVirtualKey.vbKeyBrowserRefresh);
        Add(KeyCode.BrowserSearch, Key.BrowserSearch, ByteVirtualKey.vbKeyBrowserSearch);
        Add(KeyCode.BrowserFavorites, Key.BrowserFavorites, ByteVirtualKey.vbKeyBrowserFavorites);
        Add(KeyCode.BrowserHome, Key.BrowserHome, ByteVirtualKey.vbKeyBrowserHome);

        // 应用 (LaunchMedia, LaunchApp1, LaunchApp2 not available in WPF Key enum)
        Add(KeyCode.LaunchMail, Key.LaunchMail, ByteVirtualKey.vbKeyLaunchMail);

        // Copilot (VK=0x86 = WPF Key.F23)
        Add(KeyCode.Copilot, Key.F23, ByteVirtualKey.vbKeyCopilot);
    }

    private static void Add(KeyCode keyCode, Key key, byte vk)
    {
        KeyCodeToKey[keyCode] = key;
        KeyToKeyCode.TryAdd(key, keyCode);
        KeyCodeToVk[keyCode] = vk;
        VkToKeyCode[vk] = keyCode;
    }

    public static Key ToKey(KeyCode keyCode) =>
        KeyCodeToKey.TryGetValue(keyCode, out var key) ? key : Key.None;

    public static KeyCode ToKeyCode(Key key) =>
        KeyToKeyCode.TryGetValue(key, out var code) ? code : KeyCode.None;

    public static byte ToVirtualKey(KeyCode keyCode) =>
        KeyCodeToVk.TryGetValue(keyCode, out var vk) ? vk : (byte)0;

    public static KeyCode ToKeyCode(byte vk) =>
        VkToKeyCode.TryGetValue(vk, out var code) ? code : KeyCode.None;
}
