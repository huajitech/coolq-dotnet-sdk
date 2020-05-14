# 你的第一个应用

1. 创建一个新的 C# 类库项目，并选择 **.NET Framework 4.6.1** 及以上版本作为目标框架。
2. 安装 HuajiTech.CoolQ NuGet 包。
3. 将以下代码粘贴到 `MyPlugin.cs`。

   ```csharp
   using HuajiTech.QQ;
   using HuajiTech.QQ.Events;

   public class MyPlugin : Plugin
   {
      public MyPlugin(IMessageEventSource source)
      {
          source.MessageReceived += (sender, e) =>
          {
              // 向消息来源聊天发送消息的内容，即复读。
              e.Source.Send(e.Message.Content);
          }
      }
   }
   ```

4. 编译。
5. 将 `{输出目录}\app.publish\` 中的文件复制到酷Q的 `dev\com.example.app\` 目录。
6. 重载应用并启用。

## 接下来

[入门](getting_started.md)
