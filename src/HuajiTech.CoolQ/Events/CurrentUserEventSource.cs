using HuajiTech.CoolQ.Utilities;
using HuajiTech.QQ;
using HuajiTech.QQ.Events;
using HuajiTech.UnmanagedExports;
using System;
using System.Runtime.InteropServices;

namespace HuajiTech.CoolQ.Events
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Usage", "CA1801:检查未使用的参数", Justification = "<挂起>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "CodeQuality", "IDE0051:删除未使用的私有成员", Justification = "<挂起>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Style", "IDE0060:删除未使用的参数", Justification = "<挂起>")]
    internal class CurrentUserEventSource : ICurrentUserEventSource
    {
        public static readonly CurrentUserEventSource Instance = new CurrentUserEventSource();

        private CurrentUserEventSource()
        {
        }

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public event EventHandler<AnonymousMessageReceivedEventArgs> AnonymousMessageReceived;

        public event EventHandler<EntranceInvitedEventArgs> EntranceInvited;

        public event EventHandler<ContactAddedEventArgs> ContactAdded;

        public event EventHandler<ContactRequestedEventArgs> ContactRequested;

        private static bool OnMessageReceived(
            int messageId, Chat source, QQ.User sender, string message)
        {
            var e = new MessageReceivedEventArgs(
                new Message(messageId, message), source, sender);

            Instance.MessageReceived?.Invoke(null, e);

            return e.Handled;
        }

        private static bool OnAnonymousMessageReceived(
            int messageId, QQ.Group source, QQ.AnonymousMember sender, string message)
        {
            var e = new AnonymousMessageReceivedEventArgs(
                new Message(messageId, message), source, sender);

            Instance.AnonymousMessageReceived?.Invoke(null, e);

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
            QQ.User sender = type switch
            {
                PrivateMessageSender.User => new User(senderNumber),
                PrivateMessageSender.Group => new Member(senderNumber, new Group(0)),
                PrivateMessageSender.Contact => new Contact(senderNumber),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
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

            if (senderAnonymousInfo.Length == 0)
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
        private static bool OnContactAdded(
            int type,
            int timestampAdded,
            long requesterNumber)
        {
            var e = new ContactAddedEventArgs(
                Timestamp.ToDateTime(timestampAdded), new Contact(requesterNumber));

            Instance.ContactAdded?.Invoke(null, e);

            return e.Handled;
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static bool OnContactRequested(
            int type,
            int timestampRequested,
            long requesterNumber,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string message,
            string requestToken)
        {
            var e = new ContactRequestedEventArgs(
                Timestamp.ToDateTime(timestampRequested),
                new User(requesterNumber),
                new ContactRequest(requestToken, message));

            Instance.ContactRequested?.Invoke(null, e);

            return e.Handled;
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static bool OnEntranceInvited(
            EntranceEventType type,
            int timestampRequested,
            long targetNumber,
            long inviterNumber,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string message,
            string requestToken)
        {
            if (type != EntranceEventType.Invite)
            {
                return false;
            }

            var e = new EntranceInvitedEventArgs(
                Timestamp.ToDateTime(timestampRequested),
                new Group(targetNumber),
                new User(inviterNumber),
                new EntranceInvitation(requestToken, message));

            Instance.EntranceInvited?.Invoke(null, e);

            return e.Handled;
        }
    }
}