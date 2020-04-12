# 如何：处理消息

`e` 为 @"HuajiTech.CoolQ.MessageReceivedEventArgs" 类的实例。

## 判断消息来源类型

```csharp
if (e.Source is Group) { }   // 群消息。
if (e.Source is User) { }    // 私聊消息（包括临时会话和好友消息）。
if (e.Source is Member) { }  // 群临时会话消息（Member.Group 为 null）。
if (e.Source is Contact) { } // 好友消息（Contact.Alias 为 null）。
```

## 使用 @"HuajiTech.CoolQ.AdvancedMessaging.ComplexMessage"（复合消息）

使用复合消息需要对 LINQ 有一定了解。

```csharp
using HuajiTech.CoolQ.AdvancedMessaging;
using System.Linq;
```
通过 @"HuajiTech.CoolQ.AdvancedMessaging.Extensions.Parse(HuajiTech.CoolQ.Message)" 扩展方法解析消息。

```csharp
var message = e.Message.Parse();
```

### 示例

- 获取消息中的所有纯文本拼接成的字符串。

  ```csharp
  message.GetPlainText();
  ```

- 获取消息中指定类型的元素。

  ```csharp
  var cqCodes = message.OfType<CQCode>();
  var images = message.OfType<Image>();
  var emojis = message.OfType<Emoji>();
  ```

- 如果消息包含对机器人的 At，发送一张图片和一个表情。

  ```csharp
  if (message.Contains(CurrentUser.AsUser().At()))
  {
      var image = new Image { FileName = "SmallYellowPicture.png" };
      e.Source.Send(e.Sender.At() + " " + image);
  	e.Source.Send(new Emoticon { Id = 178 });
  }

- 解析并请求录音文件。

  ```csharp
  if (message[0] is Record record)
  {
      var recordFile = record.RequestFile();
      // TODO: 对录音文件进行操作。
  }
```

## 使用正则消息

通过 @"HuajiTech.CoolQ.Extensions.RegexDecode(HuajiTech.CoolQ.Message)" 扩展方法解码正则消息。

```csharp
var args = e.Message.RegexDecode();
```

### 示例

- 获取 `Name` 键的值并发送问候。

  ```csharp
  e.Source.Send("你好，" + args["Name"]);
  ```
