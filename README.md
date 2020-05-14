# HuajiTech.CoolQ

用于酷Q应用的 .NET SDK。

**项目目前处于测试阶段，不建议用于生产环境。**

## 特性

- 通过 StdCall 导出。
- 以 [NuGet 包](https://www.nuget.org/packages/HuajiTech.CoolQ/)发布。
- **高度封装。**

## 复读机示例

```csharp
using HuajiTech.QQ;
using HuajiTech.QQ.Events;

[assembly: HuajiTech.CoolQ.AppId("com.example.repeater")]

public class RepeaterPlugin : Plugin
{
    public RepeaterPlugin(IMessageEventSource source)
    {
        source.MessageReceived += (sender, e) => e.Source.Send(e.Message.Content);
    }
}
```

## 文档

[Github Pages](https://huajitech.github.io/coolq-dotnet-sdk/)（与代码库同步）

[HuajiTech](https://www.huajitech.net/docs/coolq-dotnet-sdk/)（与最新的 NuGet 包同步）

## 讨论

建议使用 Github Issues，
但也可以选择加入[QQ群](https://jq.qq.com/?_wv=1027&k=5HPLCyU)（1094829331）。

## 许可

本项目使用 LGPL v3 进行许可。
