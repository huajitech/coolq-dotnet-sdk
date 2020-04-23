using HuajiTech.CoolQ.Utilities;
using HuajiTech.UnmanagedExports;
using System;
using System.Runtime.InteropServices;

namespace HuajiTech.CoolQ
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Usage", "CA1801:检查未使用的参数", Justification = "<挂起>")]
    public partial class CurrentUser
    {
        private static bool OnAnonymousMessageReceived(
            int messageId, Group source, AnonymousMember sender, string message)
        {
            var e = new AnonymousMessageReceivedEventArgs(
                new Message(messageId, message), source, sender);

            AnonymousMessageReceived?.Invoke(null, e);

            return e.Handled;
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

            ContactAdded?.Invoke(null, e);

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

            ContactRequested?.Invoke(null, e);

            return e.Handled;
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static bool OnEntranceInvited(
            EntranceType type,
            int timestampRequested,
            long targetNumber,
            long inviterNumber,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string message,
            string requestToken)
        {
            if (type != EntranceType.Invite)
            {
                return false;
            }

            var e = new EntranceInvitedEventArgs(
                Timestamp.ToDateTime(timestampRequested),
                new Group(targetNumber),
                new User(inviterNumber),
                new EntranceInvitation(requestToken, message));

            EntranceInvited?.Invoke(null, e);

            return e.Handled;
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

        private static bool OnMessageReceived(
            int messageId, Chat source, User sender, string message)
        {
            var e = new MessageReceivedEventArgs(
                new Message(messageId, message), source, sender);

            MessageReceived?.Invoke(null, e);

            return e.Handled;
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static bool OnPrivateMessageReceived(
            PrivateMessageType type,
            int messageId,
            long senderNumber,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string message,
            int font)
        {
            var sender = type switch
            {
                PrivateMessageType.User => new User(senderNumber),
                PrivateMessageType.Group => new Member(senderNumber, new Group(0)),
                PrivateMessageType.Contact => new Contact(senderNumber),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };

            return OnMessageReceived(messageId, sender, sender, message);
        }
    }
}