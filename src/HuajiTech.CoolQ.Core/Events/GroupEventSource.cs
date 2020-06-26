using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using HuajiTech.CoolQ.Interop;
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
    public class GroupEventSource : IGroupEventSource
    {
        public static readonly GroupEventSource Instance = new GroupEventSource();

        private GroupEventSource()
        {
        }

        public event EventHandler<GroupEventArgs>? MemberJoined;

        public event EventHandler<GroupEventArgs>? MemberLeft;

        public event EventHandler<MembershipRequestedEventArgs>? MembershipRequested;

        public event EventHandler<MembershipInvitedEventArgs>? MembershipInvited;

        public event EventHandler<GroupMuteEventArgs>? GroupMuted;

        public event EventHandler<GroupMuteEventArgs>? GroupUnmuted;

        public event EventHandler<MemberMutedEventArgs>? MemberMuted;

        public event EventHandler<GroupEventArgs>? MemberUnmuted;

        public event EventHandler<GroupEventArgs>? AdministratorAdded;

        public event EventHandler<GroupEventArgs>? AdministratorRemoved;

        public event EventHandler<FileUploadedEventArgs>? FileUploaded;

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static bool OnAdministratorsChanged(
            AdministratorsChangedType type,
            int timestamp,
            long sourceNumber,
            long operateeNumber)
        {
            var ev = type switch
            {
                AdministratorsChangedType.Add => Instance.AdministratorAdded,
                AdministratorsChangedType.Remove => Instance.AdministratorRemoved,
                _ => throw new InvalidEnumArgumentException(nameof(type), (int)type, typeof(AdministratorsChangedType))
            };

            if (ev is null)
            {
                return false;
            }

            var source = new Group(sourceNumber);
            var e = new AdministratorsChangedEventArgs(
                Timestamp.ToDateTime(timestamp),
                source,
                new Member(operateeNumber, source));

            try
            {
                ev.Invoke(Instance, e);
            }
            catch (Exception ex)
            {
                Logger.LogUnhandledException(ex);
            }

            return e.Handled;
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static bool OnEntranceRequested(
            OperationKind kind,
            int timestamp,
            long sourceNumber,
            long requesterNumber,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string message,
            string requestToken)
        {
            return kind switch
            {
                OperationKind.Active => OnRequested(),
                OperationKind.Passive => OnInvited(),
                _ => throw new InvalidEnumArgumentException(nameof(kind), (int)kind, typeof(OperationKind))
            };

            bool OnRequested()
            {
                if (Instance.MembershipRequested is null)
                {
                    return false;
                }

                var e = new MembershipRequestedEventArgs(
                    Timestamp.ToDateTime(timestamp),
                    new Group(sourceNumber),
                    new User(requesterNumber),
                    new MembershipRequest(requestToken, message));

                try
                {
                    Instance.MembershipRequested.Invoke(Instance, e);
                }
                catch (Exception ex)
                {
                    Logger.LogUnhandledException(ex);
                }

                return e.Handled;
            }

            bool OnInvited()
            {
                if (Instance.MembershipInvited is null)
                {
                    return false;
                }

                var e = new MembershipInvitedEventArgs(
                    Timestamp.ToDateTime(timestamp),
                    new Group(sourceNumber),
                    new User(requesterNumber),
                    new MembershipInvitation(requestToken, message));

                try
                {
                    Instance.MembershipInvited.Invoke(Instance, e);
                }
                catch (Exception ex)
                {
                    Logger.LogUnhandledException(ex);
                }

                return e.Handled;
            }
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static bool OnMemberJoined(
            OperationKind kind,
            int timestamp,
            long sourceNumber,
            long operatorNumber,
            long operateeNumber)
        {
            if (Instance.MemberJoined is null)
            {
                return false;
            }

            var source = new Group(sourceNumber);
            var operatee = new Member(operateeNumber, source);

            var e = new GroupEventArgs(
                Timestamp.ToDateTime(timestamp),
                source,
                kind is OperationKind.Passive ? operatee : new Member(operatorNumber, source),
                operatee);

            try
            {
                Instance.MemberJoined.Invoke(Instance, e);
            }
            catch (Exception ex)
            {
                Logger.LogUnhandledException(ex);
            }

            return e.Handled;
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static bool OnMemberLeft(
            OperationKind kind,
            int timestamp,
            long sourceNumber,
            long operatorNumber,
            long operateeNumber)
        {
            if (Instance.MemberLeft is null)
            {
                return false;
            }

            var source = new Group(sourceNumber);
            var operatee = new Member(operateeNumber, source);

            var e = new GroupEventArgs(
                Timestamp.ToDateTime(timestamp),
                source,
                kind is OperationKind.Passive ? new Member(operatorNumber, source) : operatee,
                operatee);

            try
            {
                Instance.MemberLeft.Invoke(Instance, e);
            }
            catch (Exception ex)
            {
                Logger.LogUnhandledException(ex);
            }

            return e.Handled;
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static bool OnMuteStateChanged(
            Muting type,
            int timestamp,
            long sourceNumber,
            long operatorNumber,
            long operateeNumber,
            long secondsMuted)
        {
            return operateeNumber is 0 ? OnGroupMuteStateChanged() : OnMemberMuteStateChanged();

            bool OnGroupMuteStateChanged()
            {
                var ev = type switch
                {
                    Muting.Mute => Instance.GroupMuted,
                    Muting.Unmute => Instance.GroupUnmuted,
                    _ => throw new InvalidEnumArgumentException(nameof(type), (int)type, typeof(Muting))
                };

                if (ev is null)
                {
                    return false;
                }

                var source = new Group(sourceNumber);
                var e = new GroupMuteEventArgs(
                    Timestamp.ToDateTime(timestamp),
                    source,
                    new Member(operatorNumber, source));

                try
                {
                    ev.Invoke(Instance, e);
                }
                catch (Exception ex)
                {
                    Logger.LogUnhandledException(ex);
                }

                return e.Handled;
            }

            bool OnMemberMuteStateChanged()
            {
                return type switch
                {
                    Muting.Mute => OnMuted(),
                    Muting.Unmute => OnUnmuted(),
                    _ => throw new InvalidEnumArgumentException(nameof(type), (int)type, typeof(Muting))
                };

                bool OnMuted()
                {
                    if (Instance.MemberMuted is null)
                    {
                        return false;
                    }

                    var source = new Group(sourceNumber);
                    var e = new MemberMutedEventArgs(
                        Timestamp.ToDateTime(timestamp),
                        source,
                        new Member(operatorNumber, source),
                        new Member(operateeNumber, source),
                        TimeSpan.FromSeconds(secondsMuted));

                    try
                    {
                        Instance.MemberMuted.Invoke(Instance, e);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogUnhandledException(ex);
                    }

                    return e.Handled;
                }

                bool OnUnmuted()
                {
                    if (Instance.MemberUnmuted is null)
                    {
                        return false;
                    }

                    var source = new Group(sourceNumber);
                    var e = new GroupEventArgs(
                        Timestamp.ToDateTime(timestamp),
                        new Group(sourceNumber),
                        new Member(operatorNumber, source),
                        new Member(operateeNumber, source));

                    try
                    {
                        Instance.MemberUnmuted.Invoke(Instance, e);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogUnhandledException(ex);
                    }

                    return e.Handled;
                }
            }
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static bool OnFileUploaded(
            int type,
            int timestamp,
            long sourceNumber,
            long uploaderNumber,
            string fileInfo)
        {
            if (Instance.FileUploaded is null)
            {
                return false;
            }

            using var reader = new FileReader(fileInfo);
            var file = reader.Read();

            var source = new Group(sourceNumber);
            var e = new FileUploadedEventArgs(
                Timestamp.ToDateTime(timestamp), source, new Member(uploaderNumber, source), file);

            try
            {
                Instance.FileUploaded.Invoke(Instance, e);
            }
            catch (Exception ex)
            {
                Logger.LogUnhandledException(ex);
            }

            return e.Handled;
        }
    }
}