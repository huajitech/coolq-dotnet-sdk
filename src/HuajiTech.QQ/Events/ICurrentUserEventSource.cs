namespace HuajiTech.QQ.Events
{
    public interface ICurrentUserEventSource :
        IMessageEventSource,
        IEntranceInvitationEventSource,
        IContactEventSource
    {
    }
}