using HuajiTech.CoolQ.Utilities;
using HuajiTech.UnmanagedExports;
using System;
using System.Runtime.InteropServices;

namespace HuajiTech.CoolQ
{
    public partial class Member
    {
        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static bool OnAdministratorsChanged(
            AdministratorsChangeType type,
            int timestampChanged,
            long sourceNumber,
            long affecteeNumber)
        {
            var source = new Group(sourceNumber);
            var e = new AdministratorEventArgs(
                Timestamp.ToDateTime(timestampChanged), source, new Member(affecteeNumber, source));

            var ev = type switch
            {
                AdministratorsChangeType.Add => AdministratorSet,
                AdministratorsChangeType.Remove => AdministratorUnset,
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };

            ev?.Invoke(null, e);

            return e.Handled;
        }

        [DllExport]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static bool OnMemberMuteStateChanged(
            MuteStateChangeType type,
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
                case MuteStateChangeType.Mute:
                    var eMute = new MemberMutedEventArgs(
                        timeChanged, source, @operator, affectee, TimeSpan.FromSeconds(secondsMuted));
                    Muted?.Invoke(null, eMute);
                    return eMute.Handled;

                case MuteStateChangeType.Unmute:
                    var eUnmute = new MemberUnmutedEventArgs(
                        timeChanged, source, @operator, affectee);
                    Unmuted?.Invoke(null, eUnmute);
                    return eUnmute.Handled;

                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }
        }
    }
}