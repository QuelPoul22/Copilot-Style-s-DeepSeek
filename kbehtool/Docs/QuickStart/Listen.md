# 监听按键事件

[上一页：启动与停止监听](./Start.md)

## 快速入门

使用 [KeyAction](../API/KeyAction.md) 类注册按键按下 / 松开的监听器：

```csharp
using KBEHtool;

KBEH.Start();

// 监听按键按下
KeyAction.AddKeyDownListener(keyCode =>
{
    if (keyCode == KeyCode.F1)
        Console.WriteLine("F1 被按下");
});

// 监听按键松开
KeyAction.AddKeyUpListener(keyCode =>
{
    if (keyCode == KeyCode.Enter)
        Console.WriteLine("Enter 被松开");
});
```

## 监听器类型

| 方法 | 说明 | 回调类型 |
|------|------|----------|
| `AddKeyDownListener` | 按键**按下**时触发 | `Action<KeyCode>` |
| `AddKeyUpListener` | 按键**松开**时触发 | `Action<KeyCode>` |

回调参数是需要维护命名空间前缀的 `KeyCode` 枚举值，详见 [KeyCode](../API/KeyCode.md)。

## 移除监听器

注册监听器时保留方法引用，即可在之后移除：

```csharp
void OnKeyDown(KeyCode keyCode)
{
    Console.WriteLine(keyCode);
}

KeyAction.AddKeyDownListener(OnKeyDown);

// ... 在需要时移除
KeyAction.RemoveKeyDownListener(OnKeyDown);
```

对应地还有 `RemoveKeyUpListener(Action<KeyCode>)` 用于移除松开监听器。

### 移除全部监听器

```csharp
// 清空所有按下/松开监听器
KeyAction.RemoveAllKeyListener();
```

> **注意**：`KBEH.Stop()` 也会清空所有监听器，因此重新 `Start()` 后需重新注册。

## 与 KeyCode 配合

用 `KeyCode` 枚举判断具体按键（更多按键见 [KeyCode](../API/KeyCode.md)）：

```csharp
KeyAction.AddKeyDownListener(keyCode =>
{
    switch (keyCode)
    {
        case KeyCode.A:
            Console.WriteLine("按下了 A");
            break;
        case KeyCode.LeftControl:
            Console.WriteLine("按下了左 Ctrl");
            break;
        case KeyCode.Copilot:
            Console.WriteLine("按下了 Copilot 键");
            break;
    }
});
```

[上一页：启动与停止监听](./Start.md) · [下一页：拦截按键](./Preview.md)