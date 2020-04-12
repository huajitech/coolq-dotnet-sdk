using HuajiTech.CoolQ.DataExchange;
using System;
using System.Threading.Tasks;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示一个匿名成员。
    /// </summary>
    public class AnonymousMember : IEquatable<AnonymousMember>
    {
        private readonly string _rawInfo;
        private AnonymousMemberInfo _info;

        internal AnonymousMember(string rawInfo, Group group)
        {
            _rawInfo = rawInfo;
            Group = group;
        }

        /// <summary>
        /// 获取所在群。
        /// </summary>
        public Group Group { get; }

        /// <summary>
        /// 获取 ID。
        /// </summary>
        public long Id => GetInfo().Id;

        /// <summary>
        /// 获取名称。
        /// </summary>
        public string Name => GetInfo().Name;

        public static bool operator !=(AnonymousMember left, AnonymousMember right)
        {
            return !(left == right);
        }

        public static bool operator ==(AnonymousMember left, AnonymousMember right)
        {
            return left?.Id == right?.Id && left?.Group == right?.Group;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj) || Equals(obj as AnonymousMember);
        }

        public bool Equals(AnonymousMember other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return (int)Id;
        }

        /// <summary>
        /// 禁言。
        /// </summary>
        /// <param name="duration">禁言时长。</param>
        public void Mute(TimeSpan duration)
        {
            NativeMethods.MuteAnonymousMember(
                Bot.AuthCode, Group.Number, _rawInfo, (long)duration.TotalSeconds).CheckError();
        }

        /// <summary>
        /// 以异步操作禁言。
        /// </summary>
        /// <param name="duration">禁言时长。</param>
        public Task MuteAsync(TimeSpan duration)
        {
            return Task.Run(() => Mute(duration));
        }

        public override string ToString()
        {
            return $"AnonymousMember({Id},{Group})";
        }

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