# KeyboardHook

### *Class*

底层全局键盘钩子（`WH_KEYBOARD_LL`）封装，命名空间为 `KBEHtool.Base`（完整名称 `KBEHtool.Base.KeyboardHook`）。通常由 [KBEH](./KBEH.md) 内部管理，一般无需直接使用。

## Events

| 名称 | 签名 | 说明 |
|------|------|------|
| `KeyDown` | `event Action<KeyCode>` | 按键按下（已做 KeyCode 转换） |
| `KeyUp` | `event Action<KeyCode>` | 按键松开（已做 KeyCode 转换） |
| `RawKeyDown` | `event Action<RawKeyEvent>` | 原始按下事件 |
| `RawKeyUp` | `event Action<RawKeyEvent>` | 原始松开事件 |

## Methods

| 名称 | 返回类型 | 说明 |
|------|----------|------|
| `AddKeyDownPreview(KeyPreviewHandler)` | `void` | 添加按下拦截处理器 |
| `RemoveKeyDownPreview(KeyPreviewHandler)` | `void` | 移除按下拦截处理器 |
| `AddKeyUpPreview(KeyPreviewHandler)` | `void` | 添加松开拦截处理器 |
| `RemoveKeyUpPreview(KeyPreviewHandler)` | `void` | 移除松开拦截处理器 |
| `Unhook()` | `void` | 卸载钩子 |

## Properties

| 名称 | 类型 | 说明 |
|------|------|------|
| `IsRunning` | `bool` | 钩子是否已安装 |

## 委托类型

```csharp
public delegate IntPtr HOOKPROC(int nCode, IntPtr wParam, IntPtr lParam);
public delegate bool KeyPreviewHandler(KeyCode keyCode, in RawKeyEvent rawData);
```

`KeyPreviewHandler` 与高级 API 的 `Func<KeyCode, bool>` 不同，它额外接收原始事件，返回 `true` 表示拦截：

```csharp
// 拦截扩展键（如方向键）
var handler = new KeyboardHook.KeyPreviewHandler((kc, raw) => raw.IsExtendedKey);

hook.AddKeyDownPreview(handler);
```

## 通过 KBEH.Hook 访问

```csharp
using KBEHtool;
using KBEHtool.Base;

KBEH.Start();

if (KBEH.Hook is { } hook)
{
    hook.KeyDown += keyCode => Console.WriteLine($"按下：{keyCode}");
    Console.WriteLine($"Hook running: {hook.IsRunning}");
}
```

## 注意事项

- 手动调用 `Unhook()` 前请确认不再使用；通常使用 `KBEH.Stop()` 更安全。
- 添加的 `KeyPreviewHandler` 在 `KBEH.Stop()` 后会被丢弃（钩子对象被重建）。