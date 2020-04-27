namespace HuajiTech.QQ.Events
{
    public interface IGroupEventSource :
        IGroupMemberEventSource,
        IEntranceRequestEventSource,
        IMuteEventSource,
        IGroupAdministratorEventSource,
        IFileEventSource
    {
    }
}