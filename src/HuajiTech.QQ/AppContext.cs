using System;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 表示应用上下文。
    /// 此类为抽象类。
    /// </summary>
    public abstract class AppContext
    {
        private static AppContext _currentContext;

        public static AppContext CurrentContext
        {
            get => _currentContext;
            set => _currentContext = value ?? throw new ArgumentNullException(nameof(value));
        }

        public abstract string AppId { get; }

        public abstract IBot Bot { get; }

        public abstract IUser GetUser(long number);

        public virtual IUser GetUser(IUser user)
        {
            return GetUser(user.Number);
        }

        public abstract IContact GetContact(long number);

        public virtual IContact GetContact(IContact contact)
        {
            return GetContact(contact.Number);
        }

        public abstract IGroup GetGroup(long number);

        public virtual IGroup GetGroup(IGroup group)
        {
            return GetGroup(group.Number);
        }

        public abstract IMember GetMember(long number, IGroup group);

        public virtual IMember GetMember(IMember member)
        {
            return GetMember(member.Number, member.Group);
        }

        public virtual IMember GetMember(long number, long groupNumber)
        {
            return GetMember(number, GetGroup(groupNumber));
        }

        public virtual IMember GetMember(IUser user, IGroup group)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return GetMember(user.Number, group);
        }

        public virtual IMember GetMember(IUser user, long groupNumber)
        {
            return GetMember(user, GetGroup(groupNumber));
        }
    }
}