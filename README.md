# DeepSeek Copilot

用 DeepSeek 替代 Windows 11 Copilot 键的侧边栏助手。

国内 Copilot 键基本用不了？把那个键"夺舍"过来，按一下就弹出 DeepSeek 网页版侧边栏，随问随答。

> 本项目基于 [DanKE123abc/DeepSeekCopliot](https://github.com/DanKE123abc/DeepSeekCopliot) 修改，并集成了其配套的键盘钩子库 [DanKE123abc/KBEHtool](https://github.com/DanKE123abc/KBEHtool)。

---

## 下载

前往 [Releases](https://github.com/QuelPoul22/Copilot-Style-s-DeepSeek/releases) 下载最新安装包。

---

## 功能特性

- **Copilot 键呼出侧边栏**：从屏幕右侧滑出，内嵌 DeepSeek 网页版（chat.deepseek.com），无需 Token。
- **短按 / 长按区分**
  - 短按 Copilot 键：显示 / 隐藏侧边栏
  - 长按 Copilot 键（500ms）：进入屏幕截图模式，框选区域后自动粘贴到 DeepSeek 视觉输入
- **Enter / Shift+Enter 交换**：默认 DeepSeek 网页是「Enter 发送、Shift+Enter 换行」，本项目将其交换为「Enter 换行、Shift+Enter 发送」。
- **WebView 常驻**：侧边栏隐藏后不销毁 WebView，输入框内容、生成中的回答都会被保留。因此调节释放 WebView 资源等待会报错，直接点继续就行。
- **外部调用接口**：提供命名管道，任何程序都能把问题丢给 DeepSeek 侧边栏。
- **主题 / 透明度 / 宽度可调**：设置界面可视化调整。
- **系统托盘**：左键切换侧边栏，右键菜单含设置、主题、开机自启等。

---

## 目录结构

```
.
├─ LICENSE.txt
├─ README.md
├─ .gitignore
├─ DeepSeekCopliot/
│   ├─ deepseek-copilot.csproj
│   ├─ App.xaml(.cs)
│   ├─ MainWindow.xaml(.cs)
│   ├─ SidebarWindow.xaml(.cs)
│   ├─ SettingsWindow.xaml(.cs)
│   ├─ ScreenCaptureWindow.xaml(.cs)
│   ├─ WindowManager.cs
│   ├─ KeyboardHookService.cs
│   ├─ ExternalInvokeService.cs
│   ├─ AppSettings.cs
│   └─ IconHelper.cs
│
└─ kbehtool/
    ├─ KBEHtool.sln
    └─ KBEHtool/
        ├─ KBEHtool.csproj
        ├─ KBEH.cs
        ├─ KeyAction.cs
        ├─ KeyCode.cs
        ├─ Base/
        │   ├─ KeyboardHook.cs
        │   ├─ KeyboardEventCenter.cs
        │   ├─ KeyboardState.cs
        │   ├─ KeyCodeConverter.cs
        │   └─ ByteVirtualKey.cs
        └─ Utils/
            ├─ EventCenter.cs
            └─ Singleton.cs
```

---

## 开发环境要求

| 组件 | 版本 | 说明 |
|---|---|---|
| Windows | 10 / 11 | 仅支持 Windows，因为用了键盘钩子 |
| .NET SDK | 10.0 | 编译 `DeepSeekCopliot`（`net10.0-windows`） |
| WebView2 Runtime | 任意 | 运行时依赖。Windows 11 自带；Windows 10 较新版本也自带 |

检查 .NET SDK：

```powershell
dotnet --list-sdks
```

应看到 `10.x.x` 的条目。

---

## 编译

```powershell
cd DeepSeekCopliot
dotnet build
```

运行：

```powershell
dotnet run
```

或直接双击 `bin\Debug\net10.0-windows\win-x64\deepseek-copilot.exe`。程序启动后不显示主窗口，只在**系统托盘**出现一个图标。

---

## 发布（Release，自包含单文件）

```powershell
cd DeepSeekCopliot
dotnet publish -c Release
```

产物在：

```
DeepSeekCopliot\bin\Release\net10.0-windows\win-x64\publish\
```

该目录下的 `deepseek-copilot.exe` 约 200 MB，已包含 .NET 运行时，目标机器无需另装 .NET。

---

## 外部调用接口

程序运行后监听一个命名管道，外部程序可以把问题丢给侧边栏。

- **管道名**：`DeepSeekCopilot-Ask`
- **协议**：发送一行 UTF-8 文本（以 `\n` 结尾），可为纯文本或 JSON `{"question":"..."}`
- **回复**：`OK` / `FAILED` / `EMPTY`

### 命令行调用

```powershell
deepseek-copilot.exe "帮我写一段C#代码"
deepseek-copilot.exe --ask "帮我写一段C#代码"
```

### C# 调用

```csharp
using System.IO.Pipes;
using System.Text;

var question = "帮我写一段C#代码";

using var client = new NamedPipeClientStream(".", "DeepSeekCopilot-Ask", PipeDirection.InOut);
client.Connect(2000);

var bytes = Encoding.UTF8.GetBytes(question + "\n");
client.Write(bytes);
client.Flush();

var buf = new byte[256];
var sb = new StringBuilder();
while (true)
{
    var n = client.Read(buf, 0, buf.Length);
    if (n <= 0) break;
    sb.Append(Encoding.UTF8.GetString(buf, 0, n));
    if (sb.ToString().Contains('\n')) break;
}
Console.WriteLine(sb.ToString().Trim());
```

### PowerShell 调用

```powershell
$p = New-Object System.IO.Pipes.NamedPipeClientStream(".", "DeepSeekCopilot-Ask", [System.IO.Pipes.PipeDirection]::InOut)
$p.Connect(2000)
$w = New-Object System.IO.StreamWriter($p)
$r = New-Object System.IO.StreamReader($p)
$w.AutoFlush = $true
$w.WriteLine("帮我写一段C#代码")
Write-Host $r.ReadLine()
```

---

## 使用说明

### 呼出侧边栏

- 键盘有 Copilot 键：直接短按
- 没有 Copilot 键：点系统托盘图标（左键）

### 按键

| 操作 | 效果 |
|---|---|
| 短按 Copilot | 显示 / 隐藏侧边栏 |
| 长按 Copilot（500ms） | 截图并粘贴到 DeepSeek |
| **Enter** | 换行 |
| **Shift+Enter** | 发送 |
| 按住 Ctrl 再按 Copilot | 切换「Copilot 映射为右 Ctrl」开关 |
| Esc | 隐藏侧边栏 |

### 设置

托盘右键 → 设置，可调整侧边栏宽度、透明度、动画速度、主题、开机自启、Copilot 映射等。

---

## 常见问题

### 呼出侧边栏白屏

- 检查网络能否访问 `chat.deepseek.com`
- 检查系统是否安装了 WebView2 Runtime：

```powershell
Get-ItemProperty "HKLM:\SOFTWARE\WOW6432Node\Microsoft\EdgeUpdate\Clients\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}" -ErrorAction SilentlyContinue
```

无输出说明没装，去 [微软官网](https://developer.microsoft.com/microsoft-edge/webview2/) 下载安装。

### 编译报「找不到 KBEHtool」

确认目录结构是：

```
DC\
├─DeepSeekCopliot\
└─kbehtool\
   └─KBEHtool\
      └─KBEHtool.csproj
```

`DeepSeekCopliot\deepseek-copilot.csproj` 里的引用是：

```xml
<ProjectReference Include="..\kbehtool\KBEHtool\KBEHtool.csproj" />
```

### 按键交换不生效（Enter 仍然发送）

侧边栏里按 F12 打开 DevTools，Console 里执行：

```javascript
window.__dsCopilotEnterSwapped
```

- 返回 `true`：脚本已注入
- 返回 `undefined`：脚本未注入，检查 WebView2 是否正常加载

### 卸载后 `%LocalAppData%\DeepSeekCopilot` 还在

可能是 `msedgewebview2.exe` 进程未结束导致文件占用。重启后手动删掉即可。

---

## 技术栈

| 部分 | 使用 |
|---|---|
| 框架 | .NET 10 + WPF |
| UI 库 | iNKORE.UI.WPF / iNKORE.UI.WPF.Modern |
| 浏览器内核 | Microsoft.Web.WebView2 |
| SVG 渲染 | Svg.Skia |
| 键盘钩子 | KBEHtool |
| JSON | DanKeJson |
| 进程间通信 | 命名管道 |

---

## 许可证

本项目基于 MIT 协议发布，包含以下第三方代码：

- [DeepSeekCopliot](https://github.com/DanKE123abc/DeepSeekCopliot) — Copyright (c) 2026 DanKe
- [KBEHtool](https://github.com/DanKE123abc/KBEHtool) — Copyright (c) 2023 - present DanKe

两者的原始许可证均为 MIT，完整协议原文见仓库根目录的 [LICENSE.txt](LICENSE.txt)。

---

## 致谢

- [DanKE123abc](https://github.com/DanKE123abc) — 原始的 DeepSeekCopliot 与 KBEHtool 项目
- [DeepSeek](https://chat.deepseek.com) — AI 对话服务
- 所有上游依赖的开源作者
