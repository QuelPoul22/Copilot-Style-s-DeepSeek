# 启动与停止监听

[上一页：Hello KBEHtool](../KBEHtool.md)

## 快速入门

使用 [KBEH](../API/KBEH.md) 类的 `Start()` 与 `Stop()` 方法控制全局键盘钩子的生命周期：

```csharp
using KBEHtool;

// 启动全局键盘钩子
bool started = KBEH.Start();

// ... 你的应用逻辑 ...

// 停止钩子并清理
bool stopped = KBEH.Stop();
```

`Start()` 与 `Stop()` 都返回 `bool`，表示操作是否成功。

## 幂等性

`Start()` / `Stop()` 是幂等的：

- 重复调用 `Start()` 不会重复安装钩子，始终返回 `true`。
- 重复调用 `Stop()` 不会重复卸载钩子，始终返回 `true`。

```csharp
KBEH.Start();   // true，安装钩子
KBEH.Start();   // true，已运行，直接返回
KBEH.Stop();    // true，卸载钩子
KBEH.Stop();    // true，已停止，直接返回
```

## 清理时机

`Stop()` 除了卸载钩子外，还会：

- 清空已注册的全部按键监听器（`AddKeyDownListener` / `AddKeyUpListener`）。
- 重置所有按键的按下状态。

因此，再次 `Start()` 后需要重新注册你需要的监听器。

## 在 WPF 应用中使用

KBEHtool 依赖 WPF 的 `System.Windows.Interop`，因此请确保项目已启用 WPF（`<UseWPF>true</UseWPF>`）。示例见仓库中的 `KBEHtool.Wpf.Demo` 项目。

[下一页：监听按键事件](./Listen.md)
