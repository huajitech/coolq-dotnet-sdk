using HuajiTech.CoolQ.DataExchange;
using HuajiTech.QQ;
using System;

namespace HuajiTech.CoolQ
{
    internal class AnonymousMember : IAnonymousMember
    {
        public static readonly TimeSpan MaxMuteDuration = TimeSpan.FromDays(30);

        private readonly string _rawInfo;
        private AnonymousMemberInfo _info;

        public AnonymousMember(string rawInfo, QQ.IGroup group)
        {
            _rawInfo = rawInfo;
            Group = group;
        }

        public long Id => GetInfo().Id;

        public string Name => GetInfo().Name;

        public IGroup Group { get; }

        public bool Equals(IAnonymousMember other) => other?.Id == Id && Group.Equals(other?.Group);

        public void Mute(TimeSpan duration)
        {
            if (duration <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(duration));
            }

            NativeMethods.MuteAnonymousMember(
                Bot.Instance.AuthCode, Group.Number, _rawInfo, (long)duration.TotalSeconds).CheckError();
        }

        public void Mute() => Mute(MaxMuteDuration);

        public void Unmute() =>
            NativeMethods.MuteAnonymousMember(
                Bot.Instance.AuthCode, Group.Number, _rawInfo, 0).CheckError();

        private AnonymousMemberInfo GetInfo()
        {
            if (_info is null)
            {
                try
                {
                    using var reader = new AnonymousMemberInfoReader(_rawInfo);
                    _info = reader.Read();
                }
                catch (CoolQException)
                {
                    return new AnonymousMemberInfo();
                }
            }

            return _info;
        }
    }
}