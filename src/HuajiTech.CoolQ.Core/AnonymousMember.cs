using System;
using HuajiTech.CoolQ.Interop;

namespace HuajiTech.CoolQ
{
    internal class AnonymousMember : IAnonymousMember
    {
        public static readonly TimeSpan MaxMuteDuration = TimeSpan.FromDays(30);

        private readonly string _rawInfo;
        private AnonymousMemberInfo? _info = null;

        public AnonymousMember(string rawInfo, IGroup group)
        {
            _rawInfo = rawInfo;
            Group = group;
        }

        public long Id => GetInfo().Id;

        public string? Name => GetInfo().Name;

        public string DisplayName => Name ?? ToString();

        public IGroup Group { get; }

        public bool Equals(IAnonymousMember? other) => other?.Id == Id && Group.Equals(other?.Group);

        public void Mute(TimeSpan duration)
        {
            if (duration <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(duration));
            }

            NativeMethods.AnonymousMember_Mute(
                Bot.Instance.AuthCode, Group.Number, _rawInfo, (long)duration.TotalSeconds).CheckError();
        }

        public void Mute() => Mute(MaxMuteDuration);

        public void Unmute()
            => NativeMethods.AnonymousMember_Mute(
                Bot.Instance.AuthCode, Group.Number, _rawInfo, 0).CheckError();

        private AnonymousMemberInfo GetInfo()
        {
            if (_info is null)
            {
                using var reader = new AnonymousMemberInfoReader(_rawInfo);
                _info = reader.Read();
            }

            return _info;
        }
    }
}