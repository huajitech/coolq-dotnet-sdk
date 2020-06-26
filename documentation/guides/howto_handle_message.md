# 如何：处理消息

## 判断消息来源类型

```csharp
if (e.Source is IGroup) { }  // 群消息。
if (e.Source is IUser) { }   // 私聊消息（包括临时会话和好友消息）。
if (e.Source is IMember) { } // 群临时会话消息（注意：由于酷Q限制，所在群为 Group(0)）。
if (e.Source is IFriend) { } // 好友消息。
```

## 使用 @"HuajiTech.CoolQ.Messaging.ComplexMessage"

通过 @"HuajiTech.CoolQ.Messaging.AbstractionExtensions.Parse(HuajiTech.CoolQ.Message,System.Boolean)" 扩展方法解析消息。

```csharp
var message = e.Message.Parse();
```

获取消息中的所有纯文本拼接成的字符串。

```csharp
message.GetPlainText();
```

判断机器人是否被提及（@）。

```csharp
if (message.Contains(CurrentUser.Mention()))
{
    ...
}
```

使用 @"HuajiTech.CoolQ.Messaging.AbstractionExtensions.Mention(HuajiTech.CoolQ.IUser)" 提及消息的发送者。

```csharp
e.Source.Send(e.Sender.Mention() + " How are you?");
// 或直接使用 Reply 扩展方法。
e.Reply("How are you?");
```

解构复合消息。

```csharp
var (a, b, c) = message;
```

解构、模式匹配并获取剩余内容。

```csharp
if (message is (Mention mention, Image image))
{
    var rest = message.Skip(2).ToComplexMessage();
    ...
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
