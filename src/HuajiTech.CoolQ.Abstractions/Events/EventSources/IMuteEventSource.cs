namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 定义禁言事件。
    /// </summary>
    public interface IMuteEventSource :
        INotifyMemberMuted,
        INotifyMemberUnmuted,
        INotifyGroupMuted,
        INotifyGroupUnmuted
    {
    }
}