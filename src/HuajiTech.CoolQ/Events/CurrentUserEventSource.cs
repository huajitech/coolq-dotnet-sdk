using HuajiTech.CoolQ.Utilities;
using HuajiTech.QQ;
using HuajiTech.QQ.Events;
using HuajiTech.UnmanagedExports;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace HuajiTech.CoolQ.Events
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Usage", "CA1801:检查未使用的参数", Justification = "<挂起>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "CodeQuality", "IDE0051:删除未使用的私有成员", Justification = "<挂起>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Style", "IDE0060:删除未使用的参数", Justification = "<挂起>")]
    internal class CurrentUserEventSource : IMessageEventSource, IFriendEventSource, IEntranceInviteEventSource
    {
        public static readonly CurrentUserEventSource Instance = new CurrentUserEventSource();

        private CurrentUserEventSource()
        {
        }

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public event EventHandler<AnonymousMessageReceivedEventArgs> AnonymousMessageReceived;

        public event EventHandler<EntranceInvitedEventArgs> EntranceInvited;

        public event EventHandler<FriendAddedEventArgs> FriendAdded;

        public event EventHandler<FriendshipRequestedEventArgs> FriendRequested;

        private static bool OnMessageReceived(
            int messageId, Chat source, IUser sender, string message)
        {
            var e = new MessageReceivedEventArgs(
                new Message(messageId, message), source, sender);

            Instance.MessageReceived?.Invoke(Instance, e);

            return e.Handled;
        }

        private static bool OnAnonymousMessageReceived(
            int messageId, IGroup source, IAnonymousMember sender, string message)
        {
            var e = new AnonymousMessageReceivedEventArgs(
                new Message(messageId, message), source, sender);

            Instance.AnonymousMessageReceived?.Invoke(Instance, e);

            return e.Handled;
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static bool OnPrivateMessageReceived(
            PrivateMessageSender type,
            int messageId,
            long senderNumber,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string message,
            int font)
        {
            var sender = type switch
            {
                PrivateMessageSender.User => new User(senderNumber),
                PrivateMessageSender.Group => new Member(senderNumber, new Group(0)),
                PrivateMessageSender.Friend => new Friend(senderNumber),
                _ => throw new InvalidEnumArgumentException(nameof(type), (int)type, typeof(PrivateMessageSender))
            };

            return OnMessageReceived(messageId, sender, sender, message);
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static bool OnGroupMessageReceived(
            int type,
            int messageId,
            long sourceNumber,
            long senderNumber,
            string senderAnonymousInfo,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string message,
            int font)
        {
            var group = new Group(sourceNumber);

            if (senderAnonymousInfo.Length is 0)
            {
                return OnMessageReceived(
                    messageId, group, new Member(senderNumber, group), message);
            }
            else
            {
                return OnAnonymousMessageReceived(
                    messageId, group, new AnonymousMember(senderAnonymousInfo, group), message);
            }
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static bool OnFriendAdded(
            int type,
            int timestampAdded,
            long requesterNumber)
        {
            var e = new FriendAddedEventArgs(
                Timestamp.ToDateTime(timestampAdded), new Friend(requesterNumber));

            Instance.FriendAdded?.Invoke(Instance, e);

            return e.Handled;
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static bool OnFriendRequested(
            int type,
            int timestampRequested,
            long requesterNumber,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string message,
            string requestToken)
        {
            var e = new FriendshipRequestedEventArgs(
                Timestamp.ToDateTime(timestampRequested),
                new User(requesterNumber),
                new FriendshipRequest(requestToken, message));

            Instance.FriendRequested?.Invoke(Instance, e);

            return e.Handled;
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static bool OnEntranceInvited(
            MemberEventType type,
            int timestampRequested,
            long targetNumber,
            long inviterNumber,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string message,
            string requestToken)
        {
            if (!(type is MemberEventType.Passive))
            {
                return false;
            }

            var e = new EntranceInvitedEventArgs(
                Timestamp.ToDateTime(timestampRequested),
                new Group(targetNumber),
                new User(inviterNumber),
                new EntranceInvitation(requestToken, message));

            Instance.EntranceInvited?.Invoke(Instance, e);

            return e.Handled;
        }
    }
}