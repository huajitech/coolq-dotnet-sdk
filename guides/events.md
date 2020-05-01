# 事件

| 类型 | 说明 | 事件 |
| --: | :-- | :-- |
| 1 | 私聊消息处理 | @"HuajiTech.QQ.Events.IMessageEventSource.MessageReceived" |
| 2 | 群消息处理 | @"HuajiTech.QQ.Events.IMessageEventSource.MessageReceived" |
| 11 | 群文件上传事件处理 | @"HuajiTech.QQ.Events.IFileEventSource.FileUploaded" |
| 101 | 群管理变动事件处理 | @"HuajiTech.QQ.Events.IGroupAdministratorEventSource.AdministratorAdded"、@"HuajiTech.QQ.Events.IGroupAdministratorEventSource.AdministratorRemoved" |
| 102 | 群成员减少事件处理 | @"HuajiTech.QQ.Events.IGroupMemberEventSource.MemberLeft" |
| 103 | 群成员增加事件处理 | @"HuajiTech.QQ.Events.IGroupMemberEventSource.MemberJoined" |
| 104 | 群禁言事件处理 | @"HuajiTech.QQ.Events.IMemberMuteEventSource.MemberMuted"、@"HuajiTech.QQ.Events.IMemberMuteEventSource.MemberUnmuted"、@"HuajiTech.QQ.Events.IGroupMuteEventSource.GroupMuted"、@"HuajiTech.QQ.Events.IGroupMuteEventSource.GroupUnmuted" |
| 201 | 好友已添加事件处理 | @"HuajiTech.QQ.Events.IContactEventSource.ContactAdded" |
| 301 | 好友添加请求处理 | @"HuajiTech.QQ.Events.IContactEventSource.ContactRequested" |
| 302 | 群添加请求处理 | @"HuajiTech.QQ.Events.IEntranceInviteEventSource.EntranceInvited"、@"HuajiTech.QQ.Events.IEntranceRequestEventSource.EntranceRequested" |
| 1001 | 酷Q启动事件 | @"HuajiTech.QQ.Events.IBotEventSource.BotStarted" |
| 1002 | 酷Q关闭事件 | @"HuajiTech.QQ.Events.IBotEventSource.BotStopping" |
| 1003 | 应用已被启用 | @"HuajiTech.QQ.Events.IBotEventSource.AppEnabled" |
| 1004 | 应用将被停用 | @"HuajiTech.QQ.Events.IBotEventSource.AppDisabling" |
