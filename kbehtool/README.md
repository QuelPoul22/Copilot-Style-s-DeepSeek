# KBEHtool
<u>**中文**</u> | [**English**](./README_en.md)

![NuGet](https://img.shields.io/nuget/v/KBEHtool.svg)

**K**ey**b**oard **Beh**aviour Library for Windows, a fun weekend project to prepare for later use.

当前库仍在测试，可能存在破坏性变更和未知的Bug

## 文档

[**Hello KBEHtool**](./Docs/KBEHtool.md)

- 快速开始
  - [启动与停止监听](./Docs/QuickStart/Start.md)
  - [监听按键事件](./Docs/QuickStart/Listen.md)
  - [拦截按键](./Docs/QuickStart/Preview.md)
  - [获取原始按键数据](./Docs/QuickStart/RawData.md)
  - [查询按键状态](./Docs/QuickStart/State.md)
  - [模拟按键](./Docs/QuickStart/Simulate.md)
- API
  - [KBEH](./Docs/API/KBEH.md)
  - [KeyAction](./Docs/API/KeyAction.md)
  - [KeyCode](./Docs/API/KeyCode.md)
  - [RawKeyEvent](./Docs/API/RawKeyEvent.md)
  - [KeyboardHook](./Docs/API/KeyboardHook.md)

## 示例Demo

```shell
./KBEHtool.Wpf.Demo/KBEHtool.Wpf.Demo.csproj
```

## 特性

KBEHtool  是一个只适用于Windows的键盘行为工具库，它支持：

- 全局监听键盘按键输入
- 模拟键盘按键按下、松起，支持同时按下多个按键

## 使用方法

### 启动与停止监听

```csharp
// 启动全局键盘钩子
KBEHtool.KBEH.Start();

// ... 你的应用逻辑 ...

// 停止钩子并清理
KBEHtool.KBEH.Stop();
```

### 监听按键事件

```csharp
// 监听按键按下
KBEHtool.KeyAction.AddKeyDownListener(keyCode =>
{
    if (keyCode == KBEHtool.KeyCode.F1)
        Console.WriteLine("F1 被按下");
});

// 监听按键松开
KBEHtool.KeyAction.AddKeyUpListener(keyCode =>
{
    if (keyCode == KBEHtool.KeyCode.Enter)
        Console.WriteLine("Enter 被松开");
});
```

### 拦截按键（Preview / 阻塞机制）

注册预览处理器，返回 `true` 即可**拦截该按键**，阻止其传递给其他程序：

```csharp
// 拦截 Copilot 键（Windows 11 专用键）
KBEHtool.KeyAction.AddKeyDownPreview(keyCode =>
{
    if (keyCode == KBEHtool.KeyCode.Copilot)
    {
        Console.WriteLine("Copilot 键被拦截");
        return true; // 拦截该按键
    }
    return false; // 放行其他按键
});
```

```csharp
// 拦截 LWin 键，防止误触开始菜单
KBEHtool.KeyAction.AddKeyDownPreview(kc => kc == KBEHtool.KeyCode.LeftWin);
```

### 获取原始按键数据（RawKeyEvent）

```csharp
KBEHtool.KeyAction.RawKeyDown += raw =>
{
    Console.WriteLine(
        $"VK=0x{raw.VkCode:X2} " +
        $"SCAN=0x{raw.ScanCode:X2} " +
        $"EXT={raw.IsExtendedKey} " +
        $"ALT={raw.IsAltDown}");
};
```

### 查询按键状态

```csharp
if (KBEHtool.KeyAction.IsKeyPressed(KBEHtool.KeyCode.LeftShift))
{
    Console.WriteLine("左 Shift 正在按住");
}

if (KBEHtool.KeyAction.IsKeyReleased(KBEHtool.KeyCode.Space))
{
    Console.WriteLine("空格键没有被按下");
}
```

### 模拟按键

```csharp
// 按下并松开 A 键
KBEHtool.KeyAction.PressKey(KBEHtool.KeyCode.A);

// 按住 Ctrl + C（复制），延迟 50ms 后松开
KBEHtool.KeyAction.PressKey(
    new[] { KBEHtool.KeyCode.LeftControl, KBEHtool.KeyCode.C }, 50);
```

## 打包为Nuget发行包

**Windows**

```shell
dotnet cake .\build.cake
```

## 开发平台

KBEHtool 目前支持的平台：

- .NET 8.0 (Windows)
- .NET 7.0 (Windows)
- .NET 6.0 (Windows)

## 开源许可协议

[中文翻译版](https://github.com/DanKE123abc/DanKE123abc/blob/main/%5B%E4%B8%AD%5D%20MIT%20License.txt)

```
MIT License

Copyright (c) 2023 - present DanKe

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

## 特别鸣谢

代码参考：

https://www.cnblogs.com/xielong/p/6763121.html

https://b23.tv/tIxlvbr

