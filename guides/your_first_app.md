# 你的第一个应用

1. 创建一个新的 C# 类库项目，并选择 **.NET Framework 4.5** 及以上版本作为目标框架。
2. 安装 HuajiTech.CoolQ NuGet 包。
3. 在 `Properties\AssemblyInfo.cs` 中添加以下代码。

   ```csharp
   [assembly: System.Resources.NeutralResourcesLanguage("zh-CN")]
   [assembly: HuajiTech.CoolQ.AppId("com.example.myfirstapp")]
   ```

   > [!IMPORTANT]
   > 必须在程序集上应用 `[assembly: System.Resources.NeutralResourcesLanguage("zh-CN")]`，否则会发生致命错误。

4. 将以下代码粘贴到 `MyPlugin.cs`。

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
              e.Source.Send(e.Message);
          }
      }
   }
   ```

5. 编译。
6. 将 `{输出目录}\app.publish\` 中的文件复制到酷Q的 `dev\com.example.myfirstapp\` 目录。
7. 重载应用并启用。
