namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 定义消息事件。
    /// </summary>
    public interface IMessageEventSource :
        INotifyAnonymousMessageReceived,
        INotifyUserMessageReceived,
        INotifyGroupMessageReceived
    {
    }
}