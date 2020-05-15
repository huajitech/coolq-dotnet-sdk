namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 定义群禁言事件。
    /// </summary>
    public interface IGroupMuteEventSource : INotifyGroupMuted, INotifyGroupUnmuted
    {
    }
}