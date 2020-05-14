# 入门

## AppID

通过在程序集上应用 @"HuajiTech.CoolQ.AppIdAttribute"，可以指定 [AppID](https://docs.cqp.im/dev/v9/appid/)。
在同一个应用中，不允许多个 @"HuajiTech.CoolQ.AppIdAttribute"。

## 插件类

如果一个**可被实例化**的类实现了 @"HuajiTech.QQ.IPlugin" 接口，则它是一个插件类。
在同一个应用中，可以定义多个插件类。插件类只会被实例化一次。

通常情况下，插件类应以 @"HuajiTech.QQ.Plugin" 类作为基类。该类实现了 @"HuajiTech.QQ.IPlugin" 接口，并且提供了更易用的 API。

可通过 @"HuajiTech.CoolQ.PluginLoadStageAttribute" 特性指定插件类的加载时机。若应用于程序集，则指定该项的缺省值。

```csharp
[PluginLoadStage((int)AppLifecycle.Initializing)]
class MyPlugin : Plugin
{
}
```

若在 @"HuajiTech.CoolQ.AppLifecycle.Enabled" 阶段加载，允许在构造函数内**调用 API**或**长时间阻塞线程**，并且抛出的异常不会导致应用模块崩溃。**必须**在 `app.json` 中向酷Q**注册应用启用事件**才能在 Enabled 阶段加载。

通过将加载阶段设置为 @"HuajiTech.CoolQ.AppLifecycle.NotLoaded" (`0`)，可以禁止插件类的加载。

若在 @"HuajiTech.CoolQ.AppLifecycle.Initializing"  及其他非 @"HuajiTech.CoolQ.AppLifecycle.Enabled" 阶段加载，应遵循以下原则：

- ✔ 在插件类构造函数内对**类**进行初始化。
- ✔ 在 @"HuajiTech.QQ.Events.IBotEventSource.AppEnabled" 事件中初始化**应用**。
- ✘ 不应在插件类构造函数内调用酷Q API。
- ✘ 插件类构造函数不应长时间阻塞线程。
- ✘ 插件类构造函数不应抛出异常。

有关详细信息，请参见[酷Q文库](https://docs.cqp.im/dev/v9/tips/#%E5%90%AF%E5%8A%A8-%E5%88%9D%E5%A7%8B%E5%8C%96)。

> [!WARNING]
> 获取 @"HuajiTech.QQ.ICurrentUser" 对象需要调用酷Q API。

## 事件

酷Q事件都被封装为了 .NET 事件。
当酷Q使用 StdCall 调用事件对应函数时，将会对事件数据进行封装并引发相应的事件。

✘ 不允许更改 `app.json` 中预定义的事件函数名。

部分酷Q事件被合并为一个或拆分为多个事件。若要获取详细信息，请参见[事件](events.md)。

### 事件源

事件源定义了一组相关的事件。事件源是接口类型，可以实现自定义事件源。

### 处理事件

通过在插件类构造函数中添加事件源类型的参数并向事件源附加事件处理程序可处理事件。
在插件类被实例化时，事件源的实现将会通过**依赖注入**提供为参数。

```csharp
public MyPlugin(
    IMessageEventSource messageEventSource,
    IBotEventSource botEventSource)
{
    messageEventSource.MessageReceived += [...]
    botEventSource.AppEnabled += [...]
}
```

### 路由

酷Q的所有事件数据类均派生自 @"HuajiTech.QQ.Events.RoutedEventArgs" 类。
通过设置 @"HuajiTech.QQ.Events.RoutedEventArgs.Handled" 属性，可阻断事件。

### 菜单和悬浮窗状态

菜单项被点击和悬浮窗状态更新均被视为事件。由于事件的数量无法确定，需要手动将事件处理程序导出。

有关详细信息，请参见[菜单和悬浮窗状态](menus_and_statuses.md)。

## 酷Q API

可以通过事件数据中提供的对象被动地调用酷Q API。

```csharp
private void OnMemberJoined(object sender, GroupMemberEventArgs e)
{
    e.Operatee.Mute(TimeSpan.FromMinutes(5));
    e.Source.Send("欢迎 " + e.Operatee.DisplayName);
}
```

@"HuajiTech.QQ.PluginContext" 类提供了主动调用酷Q API 的能力。

```csharp
PluginContext.Current.GetUser(114514).Send("Hello!");
```

对于在插件类中的使用，@"HuajiTech.QQ.Plugin" 类提供了更便捷的方法。

```csharp
public class MyPlugin : Plugin
{
    [...]
    foreach (var member in Group(1919810).GetMembers())
    [...]
}
```

@"HuajiTech.QQ.PluginContextExtensions" 类提供了一些常用的扩展方法。

```csharp
try
{
    [...]
    user.AsMemberOf(group);
    [...]
}
catch (Exception ex)
{
    ex.LogAsError();
}
```

@"HuajiTech.CoolQ.Messaging.Image" 和 @"HuajiTech.CoolQ.Messaging.Record" 类也提供了调用酷Q API 的方法。

```csharp
image.GetFile();
```

## CQ码

可以使用 @"HuajiTech.CoolQ.Messaging.ComplexMessage" 类处理CQ码。

有关详细信息，请参见[如何：处理消息](howto_handle_message.md)中的`使用 ComplexMessage`部分。

## .NET Core

目前，HuajiTech.CoolQ 暂不支持在 .NET Core 上运行。有计划在未来的版本添加对 .NET Core 的支持。
