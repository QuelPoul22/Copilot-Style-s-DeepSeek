# KeyAction

### *Class*

KBEHtool 的核心操作类，提供按键监听、拦截、状态查询与模拟输入等静态 API。

- 快速开始：[监听按键事件](../QuickStart/Listen.md) · [拦截按键](../QuickStart/Preview.md) · [查询按键状态](../QuickStart/State.md) · [模拟按键](../QuickStart/Simulate.md)

## Methods

| 名称 | 返回类型 | 说明 |
|------|----------|------|
| `IsKeyPressed(KeyCode)` | `bool` | 指定按键是否被按住 |
| `IsKeyReleased(KeyCode)` | `bool` | 指定按键是否未被按下 |
| `AddKeyDownListener(Action<KeyCode>)` | `void` | 注册按下监听器 |
| `RemoveKeyDownListener(Action<KeyCode>)` | `void` | 移除按下监听器 |
| `AddKeyUpListener(Action<KeyCode>)` | `void` | 注册松开监听器 |
| `RemoveKeyUpListener(Action<KeyCode>)` | `void` | 移除松开监听器 |
| `RemoveAllKeyListener()` | `void` | 移除全部监听器 |
| `AddKeyDownPreview(Func<KeyCode, bool>)` | `void` | 注册按下拦截处理器 |
| `RemoveKeyDownPreview(Func<KeyCode, bool>)` | `void` | 移除按下拦截处理器 |
| `AddKeyUpPreview(Func<KeyCode, bool>)` | `void` | 注册松开拦截处理器 |
| `RemoveKeyUpPreview(Func<KeyCode, bool>)` | `void` | 移除松开拦截处理器 |
| `PressKey(KeyCode, int)` / `PressKey(KeyCode[], int)` | `void` | 模拟按下并松开按键 |
| `ReleaseKey(KeyCode)` / `ReleaseKey(KeyCode[])` | `void` | 模拟松开按键 |

## Properties

| 名称 | 类型 | 说明 |
|------|------|------|
| `IsAnykeyPressed` | `bool` | 是否存在任意被按住的按键 |

## Events

| 名称 | 签名 | 说明 |
|------|------|------|
| `RawKeyDown` | `event Action<RawKeyEvent>` | 原始按下事件 |
| `RawKeyUp` | `event Action<RawKeyEvent>` | 原始松开事件 |

## IsKeyPressed / IsKeyReleased

```csharp
public static bool IsKeyPressed(KeyCode keyCode)
public static bool IsKeyReleased(KeyCode keyCode)
```

查询按键状态：

```csharp
if (KeyAction.IsKeyPressed(KeyCode.LeftShift))
{
    Console.WriteLine("左 Shift 被按住");
}

if (KeyAction.IsKeyReleased(KeyCode.Space))
{
    Console.WriteLine("空格未被按下");
}
```

## IsAnykeyPressed

```csharp
public static bool IsAnykeyPressed { get; }
```

只要存在任意被按住的按键即为 `true`。

## 监听按下 / 松开

```csharp
public static void AddKeyDownListener(Action<KeyCode> action)
public static void RemoveKeyDownListener(Action<KeyCode> action)
public static void AddKeyUpListener(Action<KeyCode> action)
public static void RemoveKeyUpListener(Action<KeyCode> action)
public static void RemoveAllKeyListener()
```

```csharp
KeyAction.AddKeyDownListener(keyCode =>
{
    Console.WriteLine($"按下：{keyCode}");
});

KeyAction.AddKeyUpListener(keyCode =>
{
    Console.WriteLine($"松开：{keyCode}");
});
```

## 拦截处理器（Preview）

```csharp
public static void AddKeyDownPreview(Func<KeyCode, bool> handler)
public static void RemoveKeyDownPreview(Func<KeyCode, bool> handler)
public static void AddKeyUpPreview(Func<KeyCode, bool> handler)
public static void RemoveKeyUpPreview(Func<KeyCode, bool> handler)
```

返回 `true` 拦截该按键：

```csharp
KeyAction.AddKeyDownPreview(kc => kc == KeyCode.LeftWin);
```

> 必须先 `KBEH.Start()` 才能注册预览处理器。

## 原始事件

```csharp
public static event Action<RawKeyEvent>? RawKeyDown
public static event Action<RawKeyEvent>? RawKeyUp
```

```csharp
KeyAction.RawKeyDown += raw =>
{
    Console.WriteLine($"VK=0x{raw.VkCode:X2}");
};
```

## 模拟按键

```csharp
public static void PressKey(KeyCode keyCode, int delayTime = 0)
public static void PressKey(KeyCode[] keyCodes, int delayTime = 0)
public static void ReleaseKey(KeyCode keyCode)
public static void ReleaseKey(KeyCode[] keyCodes)
```

`delayTime` 为按下到松开之间的毫秒数；`delayTime < 0` 时只按下不松开：

```csharp
// 按 A 键
KeyAction.PressKey(KeyCode.A);

// 按 Ctrl + C，100ms 后松开
KeyAction.PressKey(new[] { KeyCode.LeftControl, KeyCode.C }, 100);

// 只松开
KeyAction.ReleaseKey(KeyCode.A);
```