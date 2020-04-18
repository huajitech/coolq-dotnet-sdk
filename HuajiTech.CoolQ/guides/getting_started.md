# 入门

## 应用入口

如果一个类满足以下所有条件，则它是应用的入口类：

- 可以被实例化。
- 有公共无参构造函数。
- 应用了 @"HuajiTech.CoolQ.AppAttribute" 特性。

在同一个应用中，必须有且只有一个入口类。

在 `AppInfo` 函数被调用时，将会查找入口类并将 @"HuajiTech.CoolQ.AppAttribute.Id" 属性的值作为 [AppID](https://docs.cqp.im/dev/v9/appid/) 返回给酷Q。
在 `Initialize` 函数被调用时，将会创建入口类的实例。
所以，只应在构造方法内对类进行初始化，而不是对应用进行初始化。
若要获取详细信息，参见[酷Q文库](https://docs.cqp.im/dev/v9/tips/#%E5%90%AF%E5%8A%A8-%E5%88%9D%E5%A7%8B%E5%8C%96)。

## 事件

酷Q事件都被封装为了 .NET 静态事件。
当酷Q使用 StdCall 调用事件对应函数时，将会对事件数据进行封装并引发相应的事件。

部分酷Q事件被合并为一个或拆分为多个事件。若要获取详细信息，参见[事件](events.md)。

应在构造函数内将事件处理程序附加到事件。

### 路由

酷Q的所有事件数据类都派生自 @"HuajiTech.CoolQ.RoutedEventArgs" 类。
通过设置 @"HuajiTech.CoolQ.RoutedEventArgs.Handled" 属性，可结束处理事件。

### 菜单和悬浮窗状态

菜单项被点击和悬浮窗状态更新均被视为事件。但由于静态语言限制，需要手动将方法导出。

参见[如何：使用菜单和悬浮窗状态](howto_use_menus_and_statuses.md)。

## 酷Q API

酷Q的 API 被封装并放置在了多个不同的类中，以下是所有可以直接调用酷Q API 的类：

### 静态

- @"HuajiTech.CoolQ.Bot"
- @"HuajiTech.CoolQ.CurrentUser"

### 聊天

- @"HuajiTech.CoolQ.AnonymousMember"
- @"HuajiTech.CoolQ.Chat"
     - @"HuajiTech.CoolQ.Group"
     - @"HuajiTech.CoolQ.User"
         - @"HuajiTech.CoolQ.Contact"
         - @"HuajiTech.CoolQ.Member"

### 请求

- @"HuajiTech.CoolQ.EntranceInvitation"
- @"HuajiTech.CoolQ.EntranceRequest"

### CQ码

- @"HuajiTech.CoolQ.Messaging.Record"
- @"HuajiTech.CoolQ.Messaging.Image"
