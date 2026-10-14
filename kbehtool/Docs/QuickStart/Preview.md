# 拦截按键（Preview / 阻塞机制）

[上一页：监听按键事件](./Listen.md)

## 快速入门

注册预览处理器，返回 `true` 即可**拦截该按键**，阻止其传递给其他程序：

```csharp
using KBEHtool;

KBEH.Start();

// 拦截 Copilot 键（Windows 11 专用键）
KeyAction.AddKeyDownPreview(keyCode =>
{
    if (keyCode == KeyCode.Copilot)
    {
        Console.WriteLine("Copilot 键被拦截");
        return true; // 拦截该按键
    }
    return false; // 放行其他按键
});
```

## 预览处理器

预览处理器类型为 `Func<KeyCode, bool>`：

- 返回 `true`：该按键被拦截，不会继续传递给其他程序。
- 返回 `false`：放行该按键。

```csharp
// 拦截 LWin 键，防止误触开始菜单
KeyAction.AddKeyDownPreview(kc => kc == KeyCode.LeftWin);
```

## 按下 / 松开分别拦截

| 方法 | 说明 |
|------|------|
| `AddKeyDownPreview(Func<KeyCode, bool>)` | 在按下事件中拦截 |
| `AddKeyUpPreview(Func<KeyCode, bool>)` | 在松开事件中拦截 |

## 移除预览处理器

与监听器一样，保留方法引用即可移除：

```csharp
bool BlockCopilot(KeyCode keyCode) => keyCode == KeyCode.Copilot;

KeyAction.AddKeyDownPreview(BlockCopilot);

// ... 在需要时移除
KeyAction.RemoveKeyDownPreview(BlockCopilot);
```

对应地还有 `RemoveKeyUpPreview(Func<KeyCode, bool>)`。

## 注意事项

- **必须在 `KBEH.Start()` 之后**注册预览处理器，否则会因钩子尚未安装而直接忽略。
- 多个预览处理器会按注册顺序依次执行，只要有一个返回 `true`，该按键即被拦截。
- 拦截只影响**后续程序**收到按键，不会阻止你的应用自身通过 `KeyDownListener` 收到该按键。
- `Stop()` 之后，所有预览处理器会被清空（底层钩子对象被重建）。

[上一页：监听按键事件](./Listen.md) · [下一页：获取原始按键数据](./RawData.md)