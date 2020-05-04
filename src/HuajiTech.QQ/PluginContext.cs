using System;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 表示插件上下文。
    /// 此类为抽象类。
    /// </summary>
    public abstract class PluginContext
    {
        private static PluginContext _current;

        /// <summary>
        /// 获取或设置当前插件的插件上下文。
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> 为 <c>null</c>。</exception>
        public static PluginContext CurrentContext
        {
            get => _current;
            set => _current = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// 获取当前 <see cref="PluginContext"/> 对象的 <see cref="IBot"/>。
        /// </summary>
        public abstract IBot Bot { get; }

        /// <summary>
        /// 获取指定号码的用户。
        /// </summary>
        /// <param name="number">号码。</param>
        public abstract IUser GetUser(long number);

        /// <summary>
        /// 获取号码为指定用户的号码的 <see cref="IUser"/> 对象。
        /// </summary>
        /// <param name="user">用户。</param>
        public virtual IUser GetUser(IUser user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return GetUser(user.Number);
        }

        /// <summary>
        /// 获取指定号码的联系人。
        /// </summary>
        /// <param name="number">号码。</param>
        public abstract IContact GetContact(long number);

        /// <summary>
        /// 获取号码为指定联系人的号码的 <see cref="IContact"/> 对象。
        /// </summary>
        /// <param name="contact">联系人。</param>
        public virtual IContact GetContact(IContact contact)
        {
            if (contact is null)
            {
                throw new ArgumentNullException(nameof(contact));
            }

            return GetContact(contact.Number);
        }

        /// <summary>
        /// 获取指定号码的群。
        /// </summary>
        /// <param name="number">号码。</param>
        public abstract IGroup GetGroup(long number);

        /// <summary>
        /// 获取号码为指定群的号码的 <see cref="IGroup"/> 对象。
        /// </summary>
        /// <param name="group">群。</param>
        public virtual IGroup GetGroup(IGroup group)
        {
            if (group is null)
            {
                throw new ArgumentNullException(nameof(group));
            }

            return GetGroup(group.Number);
        }

        /// <summary>
        /// 获取指定号码和群的成员。
        /// </summary>
        /// <param name="number">号码。</param>
        /// <param name="group">群。</param>
        public abstract IMember GetMember(long number, IGroup group);

        /// <summary>
        /// 获取号码和群为指定成员的号码和群的 <see cref="IMember"/> 对象。
        /// </summary>
        /// <param name="member">成员。</param>
        public virtual IMember GetMember(IMember member)
        {
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            return GetMember(member.Number, member.Group);
        }

        /// <summary>
        /// 获取指定号码和群号码的成员。
        /// </summary>
        /// <param name="number">号码。</param>
        /// <param name="groupNumber">群号码。</param>
        public virtual IMember GetMember(long number, long groupNumber) => GetMember(number, GetGroup(groupNumber));

        /// <summary>
        /// 获取号码为指定用户的号码，群为指定群的 <see cref="IMember"/> 对象。
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
        /// 获取号码为指定用户的号码，群为指定群号码的群的 <see cref="IMember"/> 对象。
        /// </summary>
        /// <param name="user">用户。</param>
        /// <param name="groupNumber">群号码。</param>
        public virtual IMember GetMember(IUser user, long groupNumber) => GetMember(user, GetGroup(groupNumber));
    }
}