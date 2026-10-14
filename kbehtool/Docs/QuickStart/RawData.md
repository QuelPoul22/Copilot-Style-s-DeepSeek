# 获取原始按键数据（RawKeyEvent）

[上一页：拦截按键](./Preview.md)

## 快速入门

通过 [KeyAction](../API/KeyAction.md) 的 `RawKeyDown` / `RawKeyUp` 事件获取操作系统级别的原始按键数据：

```csharp
using KBEHtool;

KBEH.Start();

KeyAction.RawKeyDown += raw =>
{
    Console.WriteLine(
        $"VK=0x{raw.VkCode:X2} " +
        $"SCAN=0x{raw.ScanCode:X2} " +
        $"EXT={raw.IsExtendedKey} " +
        $"ALT={raw.IsAltDown}");
};
```

## 原始事件

| 事件 | 说明 |
|------|------|
| `RawKeyDown` | 按键按下时触发，参数为 [RawKeyEvent](../API/RawKeyEvent.md) |
| `RawKeyUp` | 按键松开时触发，参数为 [RawKeyEvent](../API/RawKeyEvent.md) |

原始事件在**预览拦截之前**触发，因此即使按键最终被拦截，你依然能收到它的原始数据。

## 常见字段

| 字段 / 属性 | 说明 |
|-------------|------|
| `VkCode` | 虚拟键码（Virtual Key Code） |
| `ScanCode` | 硬件扫描码（Scan Code） |
| `IsExtendedKey` | 是否为扩展键（如方向键、小键盘区按键） |
| `IsAltDown` | 是否同时按下了 Alt 键 |
| `IsInjected` | 该输入是否由其他程序注入（模拟） |

`VkCode` 与 [KeyCode](../API/KeyCode.md) 之间的转换由内部完成，一般情况下无需手动处理原始数据。

## 与 KeyCode 的转换

如果需要在原始事件中还原为 [KeyCode](../API/KeyCode.md)，可直接使用 `KeyCodeExtensions.ToKeyCode`：

```csharp
KeyAction.RawKeyDown += raw =>
{
    KeyCode keyCode = raw.VkCode.ToKeyCode();
    Console.WriteLine($"{keyCode} (VK=0x{raw.VkCode:X2})");
};
```

[上一页：拦截按键](./Preview.md) · [下一页：查询按键状态](./State.md)