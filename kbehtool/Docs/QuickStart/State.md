# 查询按键状态

[上一页：获取原始按键数据](./RawData.md)

## 快速入门

使用 [KeyAction](../API/KeyAction.md) 类实时查询某个按键当前是否处于按下状态：

```csharp
using KBEHtool;

KBEH.Start();

if (KeyAction.IsKeyPressed(KeyCode.LeftShift))
{
    Console.WriteLine("左 Shift 正在按住");
}

if (KeyAction.IsKeyReleased(KeyCode.Space))
{
    Console.WriteLine("空格键没有被按下");
}
```

## 状态查询方法

| 方法 / 属性 | 说明 |
|-------------|------|
| `IsKeyPressed(KeyCode)` | 指定按键当前是否被按住 |
| `IsKeyReleased(KeyCode)` | 指定按键当前是否未被按下 |
| `IsAnykeyPressed` | 是否存在任意一个按键处于按下状态 |

## 在循环中轮询

状态查询不依赖监听回调，可在任何地方（包括循环）调用：

```csharp
while (true)
{
    if (KeyAction.IsAnykeyPressed)
    {
        Console.WriteLine("检测到有按键被按住");
    }

    if (KeyAction.IsKeyPressed(KeyCode.A) && KeyAction.IsKeyPressed(KeyCode.B))
    {
        Console.WriteLine("A 和 B 同时被按住");
    }

    Thread.Sleep(16);
}
```

## 扩展方法

[KeyCode](../API/KeyCode.md) 还提供了 `IsPressed()` 扩展方法，写法更简洁：

```csharp
if (KeyCode.F1.IsPressed())
{
    Console.WriteLine("F1 被按住");
}
```

> **注意**：按下状态由钩子维护，因此请先调用 `KBEH.Start()` 后再查询状态。

[上一页：获取原始按键数据](./RawData.md) · [下一页：模拟按键](./Simulate.md)