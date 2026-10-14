using System.Windows.Interop;
using KBEHtool.Base;
using KBEHtool.Utils;

namespace KBEHtool;

public class KBEH
{
    private static bool isRunning = false;
    private static KeyboardHook? keyboardHook = null;

    public static KeyboardHook? Hook => keyboardHook;

    public static bool Start()
    {
        if (!isRunning)
        {
            try
            {
                keyboardHook = new KeyboardHook();
                KeyAction.KeyboardHookInstance = keyboardHook;
                isRunning = true;
                return true;
            }
            catch
            {
                return false;
            }
        }
        return true;
    }

    public static bool Stop()
    {
        if (isRunning)
        {
            try
            {
                keyboardHook?.Unhook();
                keyboardHook = null;
                KeyAction.KeyboardHookInstance = null;
                EventCenter.instance.Clear();
                KeyboardState.instance.ClearAllKeys();
                isRunning = false;
                return true;
            }
            catch
            {
                return false;
            }
        }
        return true;
    }
}
