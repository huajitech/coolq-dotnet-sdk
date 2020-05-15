namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 定义消息事件。
    /// </summary>
    public interface IMessageEventSource : INotifyAnonymousMessageReceived, INotifyMessageReceived
    {
    }
}