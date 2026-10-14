using KBEHtool.Base;

namespace KBEHtool;

public class KeyAction
{
    internal static KeyboardHook? KeyboardHookInstance { get; set; }

    private static readonly Dictionary<Func<KeyCode, bool>, KeyboardHook.KeyPreviewHandler> _downPreviewMap = new();
    private static readonly Dictionary<Func<KeyCode, bool>, KeyboardHook.KeyPreviewHandler> _upPreviewMap = new();

    private static KeyboardHook.KeyPreviewHandler WrapHandler(Func<KeyCode, bool> handler)
    {
        return delegate(KeyCode kc, in RawKeyEvent raw) { return handler(kc); };
    }
    
    public static bool IsKeyPressed(KeyCode keyCode)
    {
        return KeyboardState.instance.GetKeyState(keyCode);
    }
    
    public static bool IsKeyReleased(KeyCode keyCode)
    {
        return !KeyboardState.instance.GetKeyState(keyCode);
    }

    public static bool IsAnykeyPressed
    {
        get
        {
            return KeyboardState.instance.IsStateTrue();
        }
    }
    
    public static void AddKeyDownListener(Action<KeyCode> action)
    {
        KeyboardEventCenter.AddKeyDownListener(action);   
    }

    public static void RemoveKeyDownListener(Action<KeyCode> action)
    {
        KeyboardEventCenter.RemoveKeyDownListener(action);
    }
    
    public static void AddKeyUpListener(Action<KeyCode> action)
    {
        KeyboardEventCenter.AddKeyUpListener(action);
    }

    public static void RemoveKeyUpListener(Action<KeyCode> action)
    {
        KeyboardEventCenter.RemoveKeyUpListener(action);
    }

    public static void RemoveAllKeyListener()
    {
        KeyboardEventCenter.RemoveAllListener();
    }
    
    // Preview (blocking) listeners - return true to block the key
    public static void AddKeyDownPreview(Func<KeyCode, bool> handler)
    {
        if (KeyboardHookInstance == null) return;
        if (_downPreviewMap.ContainsKey(handler)) return;
        _downPreviewMap[handler] = WrapHandler(handler);
        KeyboardHookInstance.AddKeyDownPreview(_downPreviewMap[handler]);
    }

    public static void RemoveKeyDownPreview(Func<KeyCode, bool> handler)
    {
        if (KeyboardHookInstance == null) return;
        if (_downPreviewMap.TryGetValue(handler, out var wrapped))
        {
            KeyboardHookInstance.RemoveKeyDownPreview(wrapped);
            _downPreviewMap.Remove(handler);
        }
    }

    public static void AddKeyUpPreview(Func<KeyCode, bool> handler)
    {
        if (KeyboardHookInstance == null) return;
        if (_upPreviewMap.ContainsKey(handler)) return;
        _upPreviewMap[handler] = WrapHandler(handler);
        KeyboardHookInstance.AddKeyUpPreview(_upPreviewMap[handler]);
    }

    public static void RemoveKeyUpPreview(Func<KeyCode, bool> handler)
    {
        if (KeyboardHookInstance == null) return;
        if (_upPreviewMap.TryGetValue(handler, out var wrapped))
        {
            KeyboardHookInstance.RemoveKeyUpPreview(wrapped);
            _upPreviewMap.Remove(handler);
        }
    }

    // Raw event listeners
    public static event Action<RawKeyEvent>? RawKeyDown
    {
        add { if (KeyboardHookInstance != null) KeyboardHookInstance.RawKeyDown += value; }
        remove { if (KeyboardHookInstance != null) KeyboardHookInstance.RawKeyDown -= value; }
    }

    public static event Action<RawKeyEvent>? RawKeyUp
    {
        add { if (KeyboardHookInstance != null) KeyboardHookInstance.RawKeyUp += value; }
        remove { if (KeyboardHookInstance != null) KeyboardHookInstance.RawKeyUp -= value; }
    }
    
    public static void PressKey(KeyCode keyCode, int delayTime = 0)
    {
        var bvk = KeyCodeConverter.ToVirtualKey(keyCode);
        ByteVirtualKey.keybd_event(bvk, 0, 0, 0);
        if (delayTime >= 0)
        {
            Thread.Sleep(delayTime);
            ByteVirtualKey.keybd_event(bvk, 0, 2, 0);
        }
    }

    public static void ReleaseKey(KeyCode keyCode)
    {
        var bvk = KeyCodeConverter.ToVirtualKey(keyCode);
        ByteVirtualKey.keybd_event(bvk, 0, 2, 0);
    }
    
    public static void PressKey(KeyCode[] keyCodes, int delayTime = 0)
    {
        foreach (var keyCode in keyCodes)
        {
            var bvk = KeyCodeConverter.ToVirtualKey(keyCode);
            ByteVirtualKey.keybd_event(bvk, 0, 0, 0);
        }
        if (delayTime >= 0)
        {
            Thread.Sleep(delayTime);
            foreach (var keyCode in keyCodes)
            {
                var bvk = KeyCodeConverter.ToVirtualKey(keyCode);
                ByteVirtualKey.keybd_event(bvk, 0, 2, 0);
            }
        }
    }

    public static void ReleaseKey(KeyCode[] keyCodes)
    {
        foreach (var keyCode in keyCodes)
        {
            var bvk = KeyCodeConverter.ToVirtualKey(keyCode);
            ByteVirtualKey.keybd_event(bvk, 0, 2, 0);
        }
    }
}
