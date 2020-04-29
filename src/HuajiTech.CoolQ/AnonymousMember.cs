using HuajiTech.CoolQ.DataExchange;
using System;

namespace HuajiTech.CoolQ
{
    internal class AnonymousMember : QQ.AnonymousMember
    {
        public static readonly TimeSpan MaxMuteDuration = TimeSpan.FromDays(30);

        private readonly string _rawInfo;
        private AnonymousMemberInfo _info;

        internal AnonymousMember(string rawInfo, QQ.Group group)
            : base(group)
        {
            _rawInfo = rawInfo;
        }

        public override long Id => GetInfo().Id;

        public override string Name => GetInfo().Name;

        public override void Mute(TimeSpan duration)
        {
            if (duration <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(duration));
            }

            NativeMethods.MuteAnonymousMember(
                Bot.Instance.AuthCode, Group.Number, _rawInfo, (long)duration.TotalSeconds).CheckError();
        }

        public override void Mute() => Mute(MaxMuteDuration);

        public override void Unmute() => Mute(TimeSpan.Zero);

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