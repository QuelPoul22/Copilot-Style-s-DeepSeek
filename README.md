# DeepSeek Copilot

用 DeepSeek 替代 Windows 11 Copilot 键的侧边栏助手。

国内 Copilot 键基本用不了？把那个键"夺舍"过来，按一下就弹出 DeepSeek 网页版侧边栏，随问随答。

> 本项目基于 [DanKE123abc/DeepSeekCopliot](https://github.com/DanKE123abc/DeepSeekCopliot) 修改，并集成了其配套的键盘钩子库 [DanKE123abc/KBEHtool](https://github.com/DanKE123abc/KBEHtool)。

---

## 功能特性

- **Copilot 键呼出侧边栏**：从屏幕右侧滑出，内嵌 DeepSeek 网页版（chat.deepseek.com），无需 Token。
- **短按 / 长按区分**
  - 短按 Copilot 键：显示 / 隐藏侧边栏
  - 长按 Copilot 键（500ms）：进入屏幕截图模式，框选区域后自动粘贴到 DeepSeek 视觉输入
- **Enter / Shift+Enter 交换**：默认 DeepSeek 网页是「Enter 发送、Shift+Enter 换行」，本项目将其交换为「Enter 换行、Shift+Enter 发送」。
- **WebView 常驻**：侧边栏隐藏后不销毁 WebView，输入框内容、生成中的回答都会被保留。因此调节释放WebView资源等待会报错，直接点继续就行。
- **外部调用接口**：提供命名管道，任何程序都能把问题丢给 DeepSeek 侧边栏。
- **主题 / 透明度 / 宽度可调**：设置界面可视化调整。
- **系统托盘**：左键切换侧边栏，右键菜单含设置、主题、开机自启等。

---

## 目录结构

.
├─ LICENSE.txt # 版权声明 + MIT 协议原文
├─ README.md # 本文件
├─ .gitignore
├─ DeepSeekCopliot/ # 主程序（WPF）
│ ├─ deepseek-copilot.csproj
│ ├─ App.xaml(.cs)
│ ├─ MainWindow.xaml(.cs)
│ ├─ SidebarWindow.xaml(.cs) # 侧边栏 + WebView + 按键交换注入
│ ├─ SettingsWindow.xaml(.cs)
│ ├─ ScreenCaptureWindow.xaml(.cs)
│ ├─ WindowManager.cs
│ ├─ KeyboardHookService.cs # 基于 KBEHtool 的键盘监听
│ ├─ ExternalInvokeService.cs # 命名管道服务
│ ├─ AppSettings.cs
│ └─ IconHelper.cs
│
├─ kbehtool/ # 键盘钩子库（KBEHtool）
  ├─ KBEHtool.sln
  └─ KBEHtool/
  ├─ KBEHtool.csproj
  ├─ KBEH.cs
  ├─ KeyAction.cs
  ├─ KeyCode.cs
  ├─ Base/
  │ ├─ KeyboardHook.cs
  │ ├─ KeyboardEventCenter.cs
  │ ├─ KeyboardState.cs
  │ ├─ KeyCodeConverter.cs
  │ └─ ByteVirtualKey.cs
  └─ Utils/
  ├─ EventCenter.cs
  └─ Singleton.cs

---

## 开发环境要求

| 组件 | 版本 | 说明 |
|---|---|---|
| Windows | 10 / 11 | 仅支持 Windows，因为用了键盘钩子 |
| .NET SDK | 10.0 | 编译 `DeepSeekCopliot`（`net10.0-windows`） |
| Inno Setup | 6.x | release里面的打包安装包的打包软件
| WebView2 Runtime | 任意 | 运行时依赖。Windows 11 自带；Windows 10 较新版本也自带 |

检查 .NET SDK：

```powershell
dotnet --list-sdks