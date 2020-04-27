using HuajiTech.CoolQ.DataExchange;
using HuajiTech.QQ;
using System;
using System.Threading.Tasks;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示匿名成员。
    /// 此类不能在外部被实例化。
    /// </summary>
    internal class AnonymousMember : IAnonymousMember
    {
        public static readonly TimeSpan MaxMuteDuration = TimeSpan.FromDays(30);

        private readonly string _rawInfo;
        private AnonymousMemberInfo _info;

        internal AnonymousMember(string rawInfo, IGroup group)
        {
            _rawInfo = rawInfo;
            Group = group;
        }

        /// <summary>
        /// 获取当前 <see cref="AnonymousMember"/> 的所属 <see cref="IGroup"/> 对象。
        /// </summary>
        public IGroup Group { get; }

        /// <summary>
        /// 获取当前 <see cref="AnonymousMember"/> 对象的 ID。
        /// </summary>
        public long Id => GetInfo().Id;

        /// <summary>
        /// 获取当前 <see cref="AnonymousMember"/> 对象的名称。
        /// </summary>
        public string Name => GetInfo().Name;

        public static bool operator !=(AnonymousMember left, AnonymousMember right)
        {
            return !(left == right);
        }

        public static bool operator ==(AnonymousMember left, AnonymousMember right)
        {
            return left?.Equals(right) ?? right is null;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as IAnonymousMember);
        }

        public bool Equals(IAnonymousMember other)
        {
            return base.Equals(other) || (other?.Id == Id && Group.Equals(other.Group));
        }

        public override int GetHashCode()
        {
            return (int)Id;
        }

        /// <summary>
        /// 禁言当前 <see cref="AnonymousMember"/> 对象。
        /// </summary>
        /// <param name="duration">禁言时长。</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="duration"/> 小于 <see cref="TimeSpan.Zero"/>。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public void Mute(TimeSpan duration)
        {
            if (duration <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(duration));
            }

            NativeMethods.MuteAnonymousMember(
                Bot.Instance.AuthCode, Group.Number, _rawInfo, (long)duration.TotalSeconds).CheckError();
        }

        public void Mute()
        {
            Mute(MaxMuteDuration);
        }

        /// <summary>
        /// 以异步操作禁言当前 <see cref="AnonymousMember"/>。
        /// </summary>
        /// <param name="duration">禁言时长。</param>
        /// /// <exception cref="ArgumentOutOfRangeException"><paramref name="duration"/> 小于 <see cref="TimeSpan.Zero"/>。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task MuteAsync(TimeSpan duration)
        {
            return Task.Run(() => Mute(duration));
        }

        public Task MuteAsync()
        {
            return MuteAsync(MaxMuteDuration);
        }

        public void Unmute()
        {
            NativeMethods.MuteAnonymousMember(
                Bot.Instance.AuthCode, Group.Number, _rawInfo, 0).CheckError();
        }

        public Task UnmuteAsync()
        {
            return Task.Run(Unmute);
        }

        public override string ToString()
        {
            return GetType().Name + $"({Id},{Group})";
        }

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