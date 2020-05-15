namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 定义群成员事件。
    /// </summary>
    public interface IMembershipEventSource : INotifyMemberJoined, INotifyMemberLeft
    {
    }
}