# 模拟按键

[上一页：查询按键状态](./State.md)

## 快速入门

使用 [KeyAction](../API/KeyAction.md) 类的 `PressKey` / `ReleaseKey` 模拟键盘输入：

```csharp
using KBEHtool;

// 按下并松开 A 键
KeyAction.PressKey(KeyCode.A);

// 按住 Ctrl + C（复制），延迟 50ms 后松开
KeyAction.PressKey(
    new[] { KeyCode.LeftControl, KeyCode.C }, 50);
```

## 模拟单键

| 方法 | 说明 |
|------|------|
| `PressKey(KeyCode, delayTime = 0)` | 按下并（在 `delayTime` 毫秒后）松开 |
| `ReleaseKey(KeyCode)` | 只松开该按键 |

```csharp
// 只按住（不松开）
KeyAction.ReleaseKey(KeyCode.A);

// 按下后立即松开
KeyAction.PressKey(KeyCode.A);
```

## 模拟组合键

`PressKey` 也有接收 `KeyCode[]` 的重载，可同时按住多个按键再一起松开：

```csharp
// 模拟 Ctrl + Shift + Esc（任务管理器）
KeyAction.PressKey(
    new[] { KeyCode.LeftControl, KeyCode.LeftShift, KeyCode.Escape }, 100);
```

松开组合键则使用：

```csharp
KeyAction.ReleaseKey(new[] { KeyCode.LeftControl, KeyCode.LeftShift });
```

## delayTime 参数

`delayTime` 为按住与松开之间的间隔（毫秒）：

- `delayTime >= 0`：按下后 `Thread.Sleep(delayTime)`，再松开。
- `delayTime < 0`：只按下，**不松开**。

```csharp
// 一直按住 Ctrl（不自动松开）
KeyAction.PressKey(KeyCode.LeftControl, -1);
```

## 注意事项

- 模拟的按键由系统正常派发，你的应用自身的监听器也会收到。
- 频繁、无间隔地模拟按键可能被系统误判为 `IsInjected` 注入输入。
- 模拟输入与全局钩子无关，无需 `KBEH.Start()` 也能工作。

[上一页：查询按键状态](./State.md)