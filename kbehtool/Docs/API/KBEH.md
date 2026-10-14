# KBEH

### *Class*

KBEHtool 的入口类，负责管理全局键盘钩子的生命周期。

- 快速开始：[启动与停止监听](../QuickStart/Start.md)

## Methods

| 名称 | 返回类型 | 说明 |
|------|----------|------|
| `Start()` | `bool` | 安装全局键盘钩子 |
| `Stop()` | `bool` | 卸载钩子并清理状态 |

## Properties

| 名称 | 类型 | 说明 |
|------|------|------|
| `Hook` | `KeyboardHook?` | 当前的钩子实例，未启动时为 `null` |

## Start

```csharp
public static bool Start()
```

安装全局键盘钩子。

- 若尚未运行，创建 `KeyboardHook` 实例并绑定到 `KeyAction`。
- 已运行时直接返回 `true`，不会重复安装。
- 安装失败（抛异常）时返回 `false`。

```csharp
using KBEHtool;

bool ok = KBEH.Start();
Console.WriteLine(ok);   // true
```

## Stop

```csharp
public static bool Stop()
```

卸载钩子并清理：

- 卸载全局键盘钩子。
- 清空所有已注册的按键监听器。
- 重置所有按键的按下状态。

```csharp
KBEH.Stop();
```

## Hook

```csharp
public static KeyboardHook? Hook { get; }
```

返回当前钩子实例，通常无需直接使用。如需手动添加底层预览处理器或事件，可访问 [KeyboardHook](./KeyboardHook.md)：

```csharp
if (KBEH.Hook is { } hook)
{
    hook.KeyDown += keyCode => Console.WriteLine(keyCode);
}
```
