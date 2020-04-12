using HuajiTech.CoolQ.DataExchange;
using HuajiTech.CoolQ.Utilities;
using HuajiTech.UnmanagedExports;
using System;
using System.Runtime.InteropServices;

namespace HuajiTech.CoolQ
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Usage", "CA1801:检查未使用的参数", Justification = "<挂起>")]
    public partial class Group
    {
        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static bool OnAdministratorsChanged(
            AdministratorsChangingType type,
            int timestampChanged,
            long sourceNumber,
            long affecteeNumber)
        {
            var source = new Group(sourceNumber);
            var e = new AdministratorEventArgs(
                Timestamp.ToDateTime(timestampChanged), source, new Member(affecteeNumber, source));

            var ev = type switch
            {
                AdministratorsChangingType.Add => AdministratorAdded,
                AdministratorsChangingType.Remove => AdministratorRemoved,
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };

            ev?.Invoke(null, e);

            return e.Handled;
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static bool OnEntranceRequested(
            EntranceType type,
            int timestampRequested,
            long sourceNumber,
            long requesterNumber,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string message,
            string requestToken)
        {
            if (type != EntranceType.Request)
            {
                return false;
            }

            var e = new EntranceRequestedEventArgs(
                Timestamp.ToDateTime(timestampRequested),
                new Group(sourceNumber),
                new User(requesterNumber),
                new EntranceRequest(requestToken, message));

            EntranceRequested?.Invoke(null, e);

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

            FileUploaded?.Invoke(null, e);

            return e.Handled;
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static bool OnMemberJoined(
            MembersChangingType type,
            int timestampJoined,
            long sourceNumber,
            long operatorNumber,
            long affecteeNumber)
        {
            var source = new Group(sourceNumber);
            var isPassive = type == MembersChangingType.Passive;

            var e = new MemberJoinedEventArgs(
                type == MembersChangingType.Passive,
                Timestamp.ToDateTime(timestampJoined),
                source,
                isPassive ? null : new Member(operatorNumber, source),
                new Member(affecteeNumber, source));

            MemberJoined?.Invoke(null, e);

            return e.Handled;
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static bool OnMemberLeft(
            MembersChangingType type,
            int timestampLeft,
            long sourceNumber,
            long operatorNumber,
            long affecteeNumber)
        {
            var source = new Group(sourceNumber);
            var isPassive = type == MembersChangingType.Passive;

            var e = new MemberLeftEventArgs(
                isPassive,
                Timestamp.ToDateTime(timestampLeft),
                source,
                isPassive ? new Member(operatorNumber, source) : null,
                new User(affecteeNumber));

            MemberLeft?.Invoke(null, e);

            return e.Handled;
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static bool OnMuteStateChanged(
            MuteStateChangingType type,
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
                MuteStateChangingType.Mute => Muted,
                MuteStateChangingType.Unmute => Unmuted,
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };

            ev?.Invoke(null, e);

            return e.Handled;
        }
    }
}