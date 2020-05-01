using System;
using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 表示匿名成员。
    /// 此类为抽象类。
    /// </summary>
    public abstract class AnonymousMember : IMember
    {
        /// <summary>
        /// 以指定的群初始化一个 <see cref="AnonymousMember"/> 类的新实例。
        /// </summary>
        /// <param name="group">所属群。</param>
        /// <exception cref="ArgumentNullException"><paramref name="group"/> 为 <c>null</c>。</exception>
        protected AnonymousMember(Group group) => Group = group ?? throw new ArgumentNullException(nameof(group));

        /// <summary>
        /// 获取当前 <see cref="AnonymousMember"/> 对象的标识符。
        /// </summary>
        public abstract long Id { get; }

        /// <summary>
        /// 获取当前 <see cref="AnonymousMember"/> 对象的所属群。
        /// </summary>
        public Group Group { get; }

        /// <summary>
        /// 获取当前 <see cref="AnonymousMember"/> 对象的名称。
        /// </summary>
        public abstract string Name { get; }

        public bool Equals(IMember other) =>
            base.Equals(other) || (other is AnonymousMember member && member.Id == Id && member.Group == Group);

        public override bool Equals(object obj) => Equals(obj as IMember);

        public override int GetHashCode() => base.GetHashCode() ^ Group.GetHashCode();

        public abstract void Mute(TimeSpan duration);

        public abstract void Mute();

        public virtual Task MuteAsync(TimeSpan duration) => Task.Run(() => Mute(duration));

        public virtual Task MuteAsync() => Task.Run(Mute);

        public abstract void Unmute();

        public virtual Task UnmuteAsync() => Task.Run(Unmute);

        public override string ToString() => GetType().Name + $"({Id},{Group})";

        public static bool operator !=(AnonymousMember left, AnonymousMember right) => !(left == right);

        public static bool operator ==(AnonymousMember left, AnonymousMember right) => left?.Equals(right) ?? right is null;
    }
}