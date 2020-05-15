namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 定义群事件。
    /// </summary>
    public interface IGroupEventSource :
        IMuteEventSource,
        IMembershipEventSource,
        IAdministratorAdjustmentEventSource,
        INotifyEntranceRequested
    {
    }
}