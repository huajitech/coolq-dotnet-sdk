namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 定义群禁言事件。
    /// </summary>
    public interface IGroupMuteEventSource : INotifyGroupMuted, INotifyGroupUnmuted
    {
    }
}