using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using HuajiTech.CoolQ.Utilities;
using HuajiTech.UnmanagedExports;

namespace HuajiTech.CoolQ.Events
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Design", "CA1031:不捕获常规异常类型", Justification = "<挂起>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Usage", "CA1801:检查未使用的参数", Justification = "<挂起>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Style", "IDE0060:删除未使用的参数", Justification = "<挂起>")]
    public class CurrentUserEventSource : ICurrentUserEventSource
    {
        public static readonly CurrentUserEventSource Instance = new CurrentUserEventSource();

        private CurrentUserEventSource()
        {
        }

        public event EventHandler<UserMessageReceivedEventArgs>? UserMessageReceived;

        public event EventHandler<GroupMessageReceivedEventArgs>? GroupMessageReceived;

        public event EventHandler<AnonymousMessageReceivedEventArgs>? AnonymousMessageReceived;

        public event EventHandler<FriendAddedEventArgs>? FriendAdded;

        public event EventHandler<FriendshipRequestedEventArgs>? FriendshipRequested;

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static bool OnUserMessageReceived(
            MessageSender senderType,
            int messageId,
            long senderNumber,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string messageContent,
            int font)
        {
            if (Instance.UserMessageReceived is null)
            {
                return false;
            }

            var sender = senderType switch
            {
                MessageSender.User => new User(senderNumber),
                MessageSender.Member => new Member(senderNumber, Group.Empty),
                MessageSender.Friend => new Friend(senderNumber),
                _ => throw new InvalidEnumArgumentException(nameof(senderType), (int)senderType, typeof(MessageSender))
            };

            var e = new UserMessageReceivedEventArgs(new MessageCore(messageId, messageContent), sender, sender);

            try
            {
                Instance.UserMessageReceived.Invoke(Instance, e);
            }
            catch (Exception ex)
            {
                Logger.LogUnhandledException(ex);
            }

            return e.Handled;
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static bool OnGroupMessageReceived(
            int type,
            int messageId,
            long sourceNumber,
            long senderNumber,
            string senderAnonymousInfo,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string messageContent,
            int font)
        {
            if (Instance.GroupMessageReceived is null && Instance.AnonymousMessageReceived is null)
            {
                return false;
            }

            var group = new Group(sourceNumber);

            if (!(senderAnonymousInfo.Length is 0))
            {
                return OnAnonymousMessageReceived(
                    messageId,
                    new Group(sourceNumber),
                    new AnonymousMember(senderAnonymousInfo, group),
                    messageContent);
            }

            var e = new GroupMessageReceivedEventArgs(
                new MessageCore(messageId, messageContent),
                group,
                new Member(senderNumber, group));

            try
            {
                Instance.GroupMessageReceived?.Invoke(Instance, e);
            }
            catch (Exception ex)
            {
                Logger.LogUnhandledException(ex);
            }

            return e.Handled;
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static bool OnFriendAdded(
            int type,
            int timestamp,
            long requesterNumber)
        {
            if (Instance.FriendAdded is null)
            {
                return false;
            }

            var e = new FriendAddedEventArgs(
                Timestamp.ToDateTime(timestamp), new Friend(requesterNumber));

            try
            {
                Instance.FriendAdded.Invoke(Instance, e);
            }
            catch (Exception ex)
            {
                Logger.LogUnhandledException(ex);
            }

            return e.Handled;
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static bool OnFriendshipRequested(
            int type,
            int timestamp,
            long requesterNumber,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string message,
            string requestToken)
        {
            if (Instance.FriendshipRequested is null)
            {
                return false;
            }

            var e = new FriendshipRequestedEventArgs(
                Timestamp.ToDateTime(timestamp),
                new User(requesterNumber),
                new FriendshipRequest(requestToken, message));

            try
            {
                Instance.FriendshipRequested.Invoke(Instance, e);
            }
            catch (Exception ex)
            {
                Logger.LogUnhandledException(ex);
            }

            return e.Handled;
        }

        private static bool OnAnonymousMessageReceived(
            int messageId, IGroup source, IAnonymousMember sender, string messageContent)
        {
            var e = new AnonymousMessageReceivedEventArgs(
                new MessageCore(messageId, messageContent), source, sender);

            try
            {
                Instance.AnonymousMessageReceived?.Invoke(Instance, e);
            }
            catch (Exception ex)
            {
                Logger.LogUnhandledException(ex);
            }

            return e.Handled;
        }
    }
}