# KeyCode

### *enum*

按键码枚举，KBEHtool 使用它表示所有可监听的按键。命名空间为 `KBEHtool`，完整名称 `KBEHtool.KeyCode`。

## 字母与数字

```csharp
A, B, C, D, E, F, G, H, I, J, K, L, M,
N, O, P, Q, R, S, T, U, V, W, X, Y, Z,

// 主键盘数字
Key1, Key2, Key3, Key4, Key5, Key6, Key7, Key8, Key9, Key0,

// 小键盘数字
Num1, Num2, Num3, Num4, Num5, Num6, Num7, Num8, Num9, Num0,
```

## 功能键

```csharp
F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12,
F13, F14, F15, F16, F17, F18, F19, F20, F21, F22, F23, F24,
```

## 方向键

```csharp
UpArrow, DownArrow, LeftArrow, RightArrow,
```

## 修饰键

```csharp
LeftShift, RightShift,
LeftControl, RightControl,
LeftAlt, RightAlt,
LeftWin, RightWin,
```

## 符号键（OEM）

```csharp
Oem1, Oem2, Oem3, Oem4, Oem5, Oem6, Oem7, Oem8,
OemPlus, OemMinus,
OemOpenBrackets, OemCloseBrackets,
OemPipe, OemSemicolon, OemQuotes,
OemComma, OemPeriod, OemQuestion, OemTilde,
```

## 控制键

```csharp
Escape, Space, CapsLock, Tab, Enter,
Backspace, Delete, Insert,
Home, End, PageUp, PageDown,
PrintScreen, ScrollLock, Pause,
NumLock, Sleep,
```

## 小键盘符号

```csharp
Divide, Multiply, Subtract, Add, Decimal, Separator,
```

## 多媒体键

```csharp
VolumeMute, VolumeDown, VolumeUp,
MediaNextTrack, MediaPrevTrack, MediaStop, MediaPlayPause,
```

## 浏览器键

```csharp
BrowserBack, BrowserForward, BrowserRefresh,
BrowserSearch, BrowserFavorites, BrowserHome,
```

## 应用键

```csharp
LaunchMail, LaunchMedia, LaunchApp1, LaunchApp2, Apps,
```

## Windows 11 Copilot 键

```csharp
Copilot,   // VK=0x86，映射到 WPF 的 F23
```

## KeyCodeExtensions

### *Class*

`KeyCode` 的扩展方法类，位于命名空间 `KBEHtool`。

| 方法 | 返回类型 | 说明 |
|------|----------|------|
| `ToKey()` | `System.Windows.Input.Key` | 转换为 WPF 的 `Key` |
| `ToKeyCode()` | `KeyCode` | 将 WPF 的 `Key` 转换为 `KeyCode` |
| `IsPressed()` | `bool` | 该按键当前是否被按住 |

```csharp
using KBEHtool;

// 查询状态
if (KeyCode.F1.IsPressed())
{
    Console.WriteLine("F1 被按住");
}

// 与 WPF Key 互转
System.Windows.Input.Key wpfKey = KeyCode.Enter.ToKey();
KeyCode code = wpfKey.ToKeyCode();
```
