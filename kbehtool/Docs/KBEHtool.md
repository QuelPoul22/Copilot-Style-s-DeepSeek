# KBEHtool

KBEHtool（**K**ey**b**oard **Beh**aviour Library）是一个只适用于 **Windows** 平台的键盘行为工具库。

它通过 Windows 全局低级键盘钩子（`WH_KEYBOARD_LL`）实现：

- 全局监听键盘按键输入
- 拦截 / 屏蔽指定按键
- 模拟键盘按键按下与松起（支持多键组合）
- 查询任意按键的实时状态

## 目录

- [Hello KBEHtool](#hello-kbehtool)
- 快速开始
  - [启动与停止监听](./QuickStart/Start.md)
  - [监听按键事件](./QuickStart/Listen.md)
  - [拦截按键](./QuickStart/Preview.md)
  - [获取原始按键数据](./QuickStart/RawData.md)
  - [查询按键状态](./QuickStart/State.md)
  - [模拟按键](./QuickStart/Simulate.md)
- API
  - [KBEH](./API/KBEH.md)
  - [KeyAction](./API/KeyAction.md)
  - [KeyCode](./API/KeyCode.md)
  - [RawKeyEvent](./API/RawKeyEvent.md)
  - [KeyboardHook](./API/KeyboardHook.md)

## Hello KBEHtool

下面是最简单的使用示例。完整示例请参考上方「快速开始」中的各篇文档。

```csharp
using KBEHtool;

// 1. 启动全局键盘钩子
KBEH.Start();

// 2. 监听按键按下
KeyAction.AddKeyDownListener(keyCode =>
{
    if (keyCode == KeyCode.F1)
        Console.WriteLine("F1 被按下");
});

// 3. 监听按键松开
KeyAction.AddKeyUpListener(keyCode =>
{
    if (keyCode == KeyCode.Enter)
        Console.WriteLine("Enter 被松开");
});

// 4. 模拟按键（按 A 键）
KeyAction.PressKey(KeyCode.A);

// 5. 停止钩子并清理
KBEH.Stop();
```

## License

```
MIT License

Copyright (c) 2024 - present DanKe

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```
