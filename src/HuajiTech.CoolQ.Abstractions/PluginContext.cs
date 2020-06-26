using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示插件上下文。
    /// 此类为抽象类。
    /// </summary>
    public abstract class PluginContext
    {
        private static PluginContext? _current;

        /// <summary>
        /// 获取或设置当前插件上下文。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> 为 <see langword="null"/>。</exception>
        public static PluginContext Current
        {
            get => _current ?? throw new InvalidOperationException(AbstractionResources.ContextNotInitialized);
            set => _current = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// 在派生类中重写时，获取当前 <see cref="PluginContext"/> 实例的 <see cref="IBot"/>。
        /// </summary>
        public abstract IBot Bot { get; }

        /// <summary>
        /// 在派生类中重写时，获取指定号码的用户。
        /// </summary>
        /// <param name="number">号码。</param>
        public abstract IUser GetUser(long number);

        /// <summary>
        /// 在派生类中重写时，获取指定号码的好友。
        /// </summary>
        /// <param name="number">号码。</param>
        public abstract IFriend GetFriend(long number);

        /// <summary>
        /// 在派生类中重写时，获取指定号码的群。
        /// </summary>
        /// <param name="number">号码。</param>
        public abstract IGroup GetGroup(long number);

        /// <summary>
        /// 在派生类中重写时，获取指定号码和群的成员。
        /// </summary>
        /// <param name="number">号码。</param>
        /// <param name="group">群。</param>
        public abstract IMember GetMember(long number, IGroup group);

        /// <summary>
        /// 获取指定号码和群号码的成员。
        /// </summary>
        /// <param name="number">号码。</param>
        /// <param name="groupNumber">群号码。</param>
        public virtual IMember GetMember(long number, long groupNumber)
            => GetMember(number, GetGroup(groupNumber));

        /// <summary>
        /// 获取号码为指定用户的号码，群为指定群的 <see cref="IMember"/> 实例。
        /// </summary>
        /// <param name="user">用户。</param>
        /// <param name="group">群。</param>
        public virtual IMember GetMember(IUser user, IGroup group)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return GetMember(user.Number, group);
        }

        /// <summary>
        /// 获取号码为指定用户的号码，群为指定群号码的群的 <see cref="IMember"/> 实例。
        /// </summary>
        /// <param name="user">用户。</param>
        /// <param name="groupNumber">群号码。</param>
        public virtual IMember GetMember(IUser user, long groupNumber) => GetMember(user, GetGroup(groupNumber));

        /// <summary>
        /// 在派生类中重写时，获取指定 ID 的 <see cref="Message"/> 实例。
        /// </summary>
        /// <param name="id">消息 ID。</param>
        public abstract Message GetMessage(int id);
    }
}