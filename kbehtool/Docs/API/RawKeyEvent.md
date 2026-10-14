# RawKeyEvent

### *struct*

操作系统底层键盘事件的原始数据封装，由全局键盘钩子产生，命名空间为 `KBEHtool.Base`（完整名称 `KBEHtool.Base.RawKeyEvent`）。

- 快速开始：[获取原始按键数据](../QuickStart/RawData.md)

## Properties

| 名称 | 类型 | 说明 |
|------|------|------|
| `VkCode` | `int` | 虚拟键码（Virtual Key Code） |
| `ScanCode` | `int` | 硬件扫描码（Scan Code） |
| `Flags` | `int` | 原始事件标志位 |
| `Time` | `int` | 事件发生的时间戳 |
| `DwExtraInfo` | `IntPtr` | 附加信息 |
| `IsExtendedKey` | `bool` | 是否为扩展键（如方向键、小键盘键） |
| `IsAltDown` | `bool` | 是否同时按下了 Alt 键 |
| `IsInjected` | `bool` | 输入是否由其他程序注入 |

## 使用示例

```csharp
using KBEHtool;
using KBEHtool.Base;

KBEH.Start();

KeyAction.RawKeyDown += raw =>
{
    Console.WriteLine(
        $"VK=0x{raw.VkCode:X2} " +
        $"SCAN=0x{raw.ScanCode:X2} " +
        $"EXT={raw.IsExtendedKey} " +
        $"ALT={raw.IsAltDown} " +
        $"INJ={raw.IsInjected}");
};
```

## 判断扩展键 / Alt / 注入

`Flags` 的位定义：

| 标志位 | 含义 |
|--------|------|
| `0x01` | 扩展键（`IsExtendedKey`） |
| `0x10` | 注入输入（`IsInjected`） |
| `0x20` | Alt 被按下（`IsAltDown`） |

这三个属性由 `Flags` 派生，可直接读取：

```csharp
if (raw.IsAltDown)
{
    Console.WriteLine("Alt 组合键");
}
```

## 与 KeyCode 转换

虚拟键码可通过扩展方法还原为 [KeyCode](./KeyCode.md)：

```csharp
KeyCode keyCode = raw.VkCode.ToKeyCode();
```
