﻿using HuajiTech.CoolQ.Utilities;
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
    public class CurrentUserEventSource : ICurrentUserEventSource
    {
        public static readonly CurrentUserEventSource Instance = new CurrentUserEventSource();

        private CurrentUserEventSource()
        {
        }

        public event EventHandler<MessageReceivedEventArgs>? MessageReceived;

        public event EventHandler<AnonymousMessageReceivedEventArgs>? AnonymousMessageReceived;

        public event EventHandler<EntranceInvitedEventArgs>? EntranceInvited;

        public event EventHandler<FriendAddedEventArgs>? FriendAdded;

        public event EventHandler<FriendAddingEventArgs>? FriendAdding;

        private static bool OnMessageReceived(
            int messageId, IChattable source, IUser sender, string messageContent)
        {
            var e = new MessageReceivedEventArgs(
                new Message(messageId, messageContent), source, sender);

            Instance.MessageReceived?.Invoke(Instance, e);

            return e.Handled;
        }

        private static bool OnAnonymousMessageReceived(
            int messageId, IGroup source, IAnonymousMember sender, string messageContent)
        {
            var e = new AnonymousMessageReceivedEventArgs(
                new Message(messageId, messageContent), source, sender);

            Instance.AnonymousMessageReceived?.Invoke(Instance, e);

            return e.Handled;
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static bool OnPrivateMessageReceived(
            PrivateMessageSender senderType,
            int messageId,
            long senderNumber,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string messageContent,
            int font)
        {
            var sender = senderType switch
            {
                PrivateMessageSender.User => new User(senderNumber),
                PrivateMessageSender.Group => new Member(senderNumber, new Group(0)),
                PrivateMessageSender.Friend => new Friend(senderNumber),
                _ => throw new InvalidEnumArgumentException(nameof(senderType), (int)senderType, typeof(PrivateMessageSender))
            };

            return OnMessageReceived(messageId, sender, sender, messageContent);
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static bool OnGroupMessageReceived(
            int type,
            int messageId,
            long sourceNumber,
            long senderNumber,
            string senderAnonymousInfo,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string messageContent,
            int font)
        {
            var group = new Group(sourceNumber);

            if (senderAnonymousInfo.Length is 0)
            {
                return OnMessageReceived(
                    messageId, group, new Member(senderNumber, group), messageContent);
            }
            else
            {
                return OnAnonymousMessageReceived(
                    messageId, group, new AnonymousMember(senderAnonymousInfo, group), messageContent);
            }
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static bool OnFriendAdded(
            int type,
            int timestamp,
            long requesterNumber)
        {
            var e = new FriendAddedEventArgs(
                Timestamp.ToDateTime(timestamp), new Friend(requesterNumber));

            Instance.FriendAdded?.Invoke(Instance, e);

            return e.Handled;
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static bool OnFriendAdding(
            int type,
            int timestamp,
            long requesterNumber,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string message,
            string requestToken)
        {
            var e = new FriendAddingEventArgs(
                Timestamp.ToDateTime(timestamp),
                new User(requesterNumber),
                new FriendshipRequest(requestToken, message));

            Instance.FriendAdding?.Invoke(Instance, e);

            return e.Handled;
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static bool OnEntranceInvited(
            EntranceType type,
            int timestamp,
            long targetNumber,
            long inviterNumber,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string message,
            string requestToken)
        {
            if (!(type is EntranceType.Passive))
            {
                return false;
            }

            var e = new EntranceInvitedEventArgs(
                Timestamp.ToDateTime(timestamp),
                new Group(targetNumber),
                new User(inviterNumber),
                new EntranceInvitation(requestToken, message));

            Instance.EntranceInvited?.Invoke(Instance, e);

            return e.Handled;
        }
    }
}