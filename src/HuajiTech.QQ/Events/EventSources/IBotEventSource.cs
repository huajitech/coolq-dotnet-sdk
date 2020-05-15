namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 定义机器人事件。
    /// </summary>
    public interface IBotEventSource :
        INotifyAppDisabling,
        INotifyAppEnabled,
        INotifyBotStarted,
        INotifyBotStopping
    {
    }
}