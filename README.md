# HuajiTech.CoolQ

用于*酷Q*应用的 .NET SDK。

## 项目状态

~~项目目前处于测试阶段，不建议用于生产环境。~~

酷Q已无法使用，**本项目不再更新**。

已有应用可通过其他平台对于酷Q的兼容层继续使用，**不建议将本项目用于任何新的开发**。

*如果事情出现巨大转机，不排除项目继续开发的可能性。*

## 特性

- 通过 StdCall 导出。
- 以 [NuGet 包](https://www.nuget.org/packages/HuajiTech.CoolQ/)发布。
- **高度封装。**

## 复读机示例

```csharp
using HuajiTech.CoolQ;
using HuajiTech.CoolQ.Events;

[assembly: AppId("com.example.repeater")]

[Plugin]
class RepeaterPlugin : Plugin
{
    public RepeaterPlugin(IMessageEventSource eventSource)
    {
        eventSource.AddMessageReceivedEventHandler((sender, e) => e.Source.Send(e.Message));
    }
}
```

## 文档

[Github Pages](https://huajitech.github.io/coolq-dotnet-sdk/)（与代码库同步）

[HuajiTech](https://www.huajitech.net/docs/coolq-dotnet-sdk/)（与最新的 NuGet 包同步）

## 讨论

~~[GitHub Issues](https://github.com/huajitech/coolq-dotnet-sdk/issues)~~ 或 [QQ群](https://jq.qq.com/?_wv=1027&k=5HPLCyU)（1094829331）。

\*QQ群已不再讨论本项目。

## 许可

本项目使用 LGPL v3 进行许可。
