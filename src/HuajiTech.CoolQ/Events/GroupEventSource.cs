using HuajiTech.CoolQ.DataExchange;
using HuajiTech.CoolQ.Utilities;
using HuajiTech.QQ;
using HuajiTech.QQ.Events;
using HuajiTech.UnmanagedExports;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;

namespace HuajiTech.CoolQ.Events
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Usage", "CA1801:检查未使用的参数", Justification = "<挂起>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "CodeQuality", "IDE0051:删除未使用的私有成员", Justification = "<挂起>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Style", "IDE0060:删除未使用的参数", Justification = "<挂起>")]
    internal class GroupEventSource :
        IGroupMemberEventSource,
        IEntranceRequestEventSource,
        IGroupMuteEventSource,
        IMemberMuteEventSource
    {
        public static readonly GroupEventSource Instance = new GroupEventSource();

        private GroupEventSource()
        {
        }

        public event EventHandler<GroupMemberEventArgs> MemberJoined;

        public event EventHandler<GroupMemberEventArgs> MemberLeft;

        public event EventHandler<EntranceRequestedEventArgs> EntranceRequested;

        public event EventHandler<GroupMuteEventArgs> GroupMuted;

        public event EventHandler<GroupMuteEventArgs> GroupUnmuted;

        public event EventHandler<MemberMutedEventArgs> MemberMuted;

        public event EventHandler<GroupEventArgs> MemberUnmuted;

        public event EventHandler<GroupEventArgs> AdministratorAdded;

        public event EventHandler<GroupEventArgs> AdministratorRemoved;

        public event EventHandler<FileUploadedEventArgs> FileUploaded;

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static bool OnAdministratorsChanged(
            AdministratorEventType type,
            int timestampChanged,
            long sourceNumber,
            long affecteeNumber)
        {
            var source = new Group(sourceNumber);
            var e = new GroupEventArgs(
                Timestamp.ToDateTime(timestampChanged),
                source,
                source.GetMembers().Single(member => member.Role == MemberRole.Owner),
                new Member(affecteeNumber, source));

            var ev = type switch
            {
                AdministratorEventType.Add => Instance.AdministratorAdded,
                AdministratorEventType.Remove => Instance.AdministratorRemoved,
                _ => throw new InvalidEnumArgumentException(nameof(type), (int)type, typeof(AdministratorEventType))
            };

            ev?.Invoke(Instance, e);

            return e.Handled;
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static bool OnEntranceRequested(
            EntranceEventType type,
            int timestampRequested,
            long sourceNumber,
            long requesterNumber,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string message,
            string requestToken)
        {
            if (type != EntranceEventType.Request)
            {
                return false;
            }

            var e = new EntranceRequestedEventArgs(
                Timestamp.ToDateTime(timestampRequested),
                new Group(sourceNumber),
                new User(requesterNumber),
                new EntranceRequest(requestToken, message));

            Instance.EntranceRequested?.Invoke(Instance, e);

            return e.Handled;
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static bool OnFileUploaded(
            int type,
            int timestampUploaded,
            long sourceNumber,
            long uploaderNumber,
            string fileInfo)
        {
            using var reader = new FileReader(fileInfo);
            var file = reader.Read();

            var source = new Group(sourceNumber);
            var e = new FileUploadedEventArgs(
                Timestamp.ToDateTime(timestampUploaded), source, new Member(uploaderNumber, source), file);

            Instance.FileUploaded?.Invoke(Instance, e);

            return e.Handled;
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static bool OnGroupMuteStateChanged(
            MuteEventType type,
            int timestampChanged,
            long sourceNumber,
            long operatorNumber,
            long affecteeNumber,
            long secondsMuted)
        {
            if (affecteeNumber != default)
            {
                return false;
            }

            var source = new Group(sourceNumber);
            var e = new GroupMuteEventArgs(
                Timestamp.ToDateTime(timestampChanged),
                source,
                new Member(operatorNumber, source));

            var ev = type switch
            {
                MuteEventType.Mute => Instance.GroupMuted,
                MuteEventType.Unmute => Instance.GroupUnmuted,
                _ => throw new InvalidEnumArgumentException(nameof(type), (int)type, typeof(MuteEventType))
            };

            ev?.Invoke(Instance, e);

            return e.Handled;
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static bool OnMemberJoined(
            MemberEventType type,
            int timestampJoined,
            long sourceNumber,
            long operatorNumber,
            long affecteeNumber)
        {
            var source = new Group(sourceNumber);
            var isPassive = type == MemberEventType.Passive;

            var e = new GroupMemberEventArgs(
                type == MemberEventType.Passive,
                Timestamp.ToDateTime(timestampJoined),
                source,
                isPassive ? null : new Member(operatorNumber, source),
                new Member(affecteeNumber, source));

            Instance.MemberJoined?.Invoke(Instance, e);

            return e.Handled;
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static bool OnMemberLeft(
            MemberEventType type,
            int timestampLeft,
            long sourceNumber,
            long operatorNumber,
            long affecteeNumber)
        {
            var source = new Group(sourceNumber);
            var isPassive = type == MemberEventType.Passive;

            var e = new GroupMemberEventArgs(
                isPassive,
                Timestamp.ToDateTime(timestampLeft),
                source,
                isPassive ? new Member(operatorNumber, source) : null,
                new Member(affecteeNumber, source));

            Instance.MemberLeft?.Invoke(Instance, e);

            return e.Handled;
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static bool OnMemberMuteStateChanged(
            MuteEventType type,
            int timestampChanged,
            long sourceNumber,
            long operatorNumber,
            long affecteeNumber,
            long secondsMuted)
        {
            if (affecteeNumber == default)
            {
                return false;
            }

            var timeChanged = Timestamp.ToDateTime(timestampChanged);
            var source = new Group(sourceNumber);
            var @operator = new Member(operatorNumber, source);
            var affectee = new Member(affecteeNumber, source);

            switch (type)
            {
                case MuteEventType.Mute:
                    var eMute = new MemberMutedEventArgs(
                        timeChanged, source, @operator, affectee, TimeSpan.FromSeconds(secondsMuted));
                    Instance.MemberMuted?.Invoke(Instance, eMute);
                    return eMute.Handled;

                case MuteEventType.Unmute:
                    var eUnmute = new GroupEventArgs(
                        timeChanged, source, @operator, affectee);
                    Instance.MemberUnmuted?.Invoke(Instance, eUnmute);
                    return eUnmute.Handled;

                default:
                    throw new InvalidEnumArgumentException(nameof(type), (int)type, typeof(MuteEventType));
            }
        }
    }
}