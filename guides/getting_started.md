# 入门

## AppID

通过在程序集上应用 @"HuajiTech.CoolQ.AppIdAttribute"，可以指定 [AppID](https://docs.cqp.im/dev/v9/appid/)。
在同一个应用中，不允许多个 @"HuajiTech.CoolQ.AppIdAttribute"。

## 插件类

如果一个**可被实例化**的类实现了 @"HuajiTech.CoolQ.IPlugin" 接口，则它是一个插件类。
在同一个应用中，可以定义多个插件类。插件类只会被实例化一次。

通常情况下，插件类应以 @"HuajiTech.CoolQ.Plugin" 类作为基类。该类实现了 @"HuajiTech.CoolQ.IPlugin" 接口，并且提供了更易用的 API。

可通过 @"HuajiTech.CoolQ.PluginLoadStageAttribute" 特性指定插件类的加载阶段。

```csharp
[assembly: PluginLoadStage(typeof(MyPlugin), (int)AppLifecycle.Initializing)]
```

使用 @"HuajiTech.CoolQ.DefaultPluginLoadStageAttribute" 特性指定在未指定 @"HuajiTech.CoolQ.PluginLoadStageAttribute" 特性时的缺省值。
@"HuajiTech.CoolQ.DefaultPluginLoadStageAttribute" 特性不可重复。

```csharp
[assembly: DefaultPluginLoadStage((int)AppLifecycle.Enabled)]
```

> [!NOTE]
> 建议将 AppID 和插件加载阶段的设置项放置在 `Properties\AppInfo.cs` 内，但这不是强制的。

若在 @"HuajiTech.CoolQ.AppLifecycle.Enabled" 阶段加载，允许在构造函数内**调用 API**或**长时间阻塞线程**，并且可以显示引发的异常的详细信息。
**必须**在 `app.json` 中向酷Q**注册应用启用事件**才能在 Enabled 阶段加载。

通过将加载阶段设置为 @"HuajiTech.CoolQ.AppLifecycle.NotLoaded" (`0`)，可以禁止插件类的加载。

若在 @"HuajiTech.CoolQ.AppLifecycle.Initializing"  及其他非 @"HuajiTech.CoolQ.AppLifecycle.Enabled" 阶段加载，应遵循以下原则：

- ✔ 在插件类构造函数内对**类**进行初始化。
- ✔ 在 @"HuajiTech.CoolQ.Events.INotifyAppEnabled.AppEnabled" 事件中初始化**应用**。
- ✘ 不应在插件类构造函数内调用酷Q API。
- ✘ 插件类构造函数不应长时间阻塞线程。
- ✘ 插件类构造函数不应抛出异常。

有关详细信息，请参见[酷Q文库](https://docs.cqp.im/dev/v9/tips/#%E5%90%AF%E5%8A%A8-%E5%88%9D%E5%A7%8B%E5%8C%96)。

> [!WARNING]
> 获取 @"HuajiTech.CoolQ.ICurrentUser" 对象需要调用酷Q API。

## 事件

酷Q事件都被封装为了 .NET 事件。
当酷Q使用 StdCall 调用事件对应函数时，将会对事件数据进行封装并引发相应的事件。

✘ 不允许更改 `app.json` 中预定义的事件函数名。

部分酷Q事件被合并为一个或拆分为多个事件。若要获取详细信息，请参见[事件](events.md)。

### 事件源

事件源定义了一个或一组相关的事件。
事件源是接口类型。
定义了单个事件的事件源以 `Notify` 开头，如 @"HuajiTech.CoolQ.Events.INotifyMessageReceived" 接口。
定义了多个事件的事件源以 `EventSource` 结尾，如 @"HuajiTech.CoolQ.Events.IBotEventSource" 接口。

### 处理事件

在插件类构造函数中添加参数以获取事件源。通过向事件源对象附加事件处理程序来处理事件。

```csharp
public MyPlugin(
    INotifyMessageReceived notifyMessageReceived,
    IBotEventSource botEventSource)
{
    notifyMessageReceived.MessageReceived += ...
    botEventSource.AppEnabled += ...
}
```

> [!NOTE]
> 尽可能使用定义事件最少的事件源。例如，若插件只需处理消息接收事件，则应使用 @"HuajiTech.CoolQ.Events.INotifyMessageReceived"，而不是 @"HuajiTech.CoolQ.Events.ICurrentUserEventSource"。

### 路由

酷Q的所有事件数据类均派生自 @"HuajiTech.CoolQ.Events.RoutedEventArgs" 类。
将 @"HuajiTech.CoolQ.Events.RoutedEventArgs.Handled" 属性设置为 `true` 以阻断事件。

### 菜单和悬浮窗状态

菜单项被点击和悬浮窗状态更新均被视为事件。由于事件的数量无法确定，需要手动导出事件处理程序。

有关详细信息，请参见[菜单和悬浮窗状态](menus_and_statuses.md)。

## 调用酷Q API

- 通过事件数据。

  ```csharp
  private void OnMemberJoined(object sender, GroupEventArgs e)
  {
      e.Operatee.Mute(TimeSpan.FromMinutes(5));
      e.Source.Send("欢迎 " + e.Operatee.DisplayName);
  }
  ```

- 通过 @"HuajiTech.CoolQ.PluginContext" 类。对于 @"HuajiTech.CoolQ.PluginContext.Current"，建议使用 @"HuajiTech.CoolQ.CurrentPluginContext" 类。

  ```csharp
  PluginContext.Current.GetUser(114514).Send("Hello!");
  ```

- 通过 @"HuajiTech.CoolQ.Plugin" 类的 `protected` 实例方法。通常在插件类中使用此方法。

  ```csharp
  public class MyPlugin : Plugin
  {
      ...
      foreach (var member in Group(1919810).GetMembers())
      ...
  }
  ```

- 通过 @"HuajiTech.CoolQ.CurrentPluginContext" 类提供的静态方法。通常在非插件类中使用此方法。
  
  ```csharp
  using static HuajiTech.CoolQ.CurrentPluginContext;

  User(114514).Send("Hello!");
  ```

- 通过 @"HuajiTech.CoolQ.PluginContextExtensions" 类提供的常用扩展方法。

  ```csharp
  try
  {
      ...
      user.AsMemberOf(group);
      ...
  }
  catch (Exception ex)
  {
      ex.LogAsError();
  }
  ```

- 通过 @"HuajiTech.CoolQ.Messaging.Image" 和 @"HuajiTech.CoolQ.Messaging.Record" 类。

  ```csharp
  image.GetFile();
  ```

## CQ码

可以使用 @"HuajiTech.CoolQ.Messaging.ComplexMessage" 类处理CQ码。

有关详细信息，请参见[如何：处理消息](howto_handle_message.md)中的`使用 ComplexMessage`部分。

## .NET Core

目前，HuajiTech.CoolQ 暂不支持在 .NET Core 上运行。有计划在未来的版本添加对 .NET Core 的支持。
