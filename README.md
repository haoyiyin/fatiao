# 发条 (fatiao)

发条是一个基于 .NET Framework 4.8 的 Windows 自动化工具，包含主应用、动作库与小程序，用于编排与执行自动化流程。

## 功能概览

- 动作编排与执行（流程/变量/快捷命令）
- 窗口管理、鼠标键盘输入
- 截图、取色、找色、找图（位图模板匹配）
- 常用系统/文件/网络相关动作

## 目录结构

- `发条/`：主应用（WinForms），解决方案 `fatiao.sln`
- `动作/`：动作库（Class Library）
- `小程序/`：小程序（WinForms），解决方案 `winapp.sln`
- `图片素材/`：项目素材资源

## 环境要求

- Windows 10/11
- .NET Framework 4.8
- Visual Studio 2017+（支持 .NET Framework 4.8）
- NuGet（用于依赖包还原）

## 构建与运行

### 发条

1. 使用 Visual Studio 打开 `发条/fatiao.sln`。
2. 还原 NuGet 包。
3. Debug 配置建议选择 `x86` 平台。
4. 生成并运行。
5. 确保输出目录中存在 `steam_api.dll`。

### 小程序

1. 使用 Visual Studio 打开 `小程序/winapp.sln`。
2. 还原 NuGet 包。
3. 生成并运行。

### 动作库

`动作/动作库.csproj` 为类库项目，供主程序与小程序引用，无需单独启动。

## 备注（补充）

- 项目依赖 Win32/Windows Forms，macOS/Linux 不支持运行。
- 自动化操作通常要求目标窗口可交互，必要时请以管理员权限运行。

## 许可

MIT
