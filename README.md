# HuajiTech.CoolQ

用于酷Q应用的 .NET SDK。

**项目目前处于测试阶段，不建议用于生产环境。**

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

Github Issues[https://github.com/huajitech/coolq-dotnet-sdk/issues] 或 [QQ群](https://jq.qq.com/?_wv=1027&k=5HPLCyU)（1094829331）。

## 许可

本项目使用 LGPL v3 进行许可。
