# 如何：处理消息

## 判断消息来源类型

```csharp
if (e.Source is Group) { }   // 群消息。
if (e.Source is User) { }    // 私聊消息（包括临时会话和好友消息）。
if (e.Source is Member) { }  // 群临时会话消息（Member.Group 为 Group(0)）。
if (e.Source is Contact) { } // 好友消息。
```

## 使用 @"HuajiTech.CoolQ.Messaging.ComplexMessage"（复合消息）

通过 @"HuajiTech.CoolQ.Messaging.Extensions.Parse(HuajiTech.QQ.Message)" 扩展方法解析消息。

```csharp
var message = e.Message.Parse();
```

获取消息中的所有纯文本拼接成的字符串。

```csharp
string.GetPlainText();
```

判断机器人是否被 @。

```csharp
if (message.Contains(CurrentUser.At()))
{
    [...]
}
```

使用 @"HuajiTech.CoolQ.Messaging.Extensions.At(HuajiTech.QQ.User)" @ 消息的发送者。

```csharp
e.Source.Send(e.Sender.At() + [...])
```

解构复合消息

```csharp
var (a, b, c) = message;
```

解构、模式匹配并获取剩余内容。

```csharp
if (message is (At at, Image image))
{
    var rest = message.Skip(2).ToComplexMessage();
    [...]
}
```

## 使用正则消息

通过 @"HuajiTech.CoolQ.Extensions.RegexDecode(HuajiTech.CoolQ.Message)" 扩展方法解码正则消息。

```csharp
var args = e.Message.RegexDecode();
```

获取 `Name` 键的值。

```csharp
args["Name"]
```
