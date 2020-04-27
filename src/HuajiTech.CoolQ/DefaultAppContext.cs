using HuajiTech.QQ;
using System;

namespace HuajiTech.CoolQ
{
    internal class DefaultAppContext : QQ.AppContext
    {
        public DefaultAppContext(IBot bot)
        {
            Bot = bot ?? throw new ArgumentNullException(nameof(bot));
        }

        public override IBot Bot { get; }

        public override IContact GetContact(long number)
        {
            return new Contact(number);
        }

        public override IGroup GetGroup(long number)
        {
            return new Group(number);
        }

        public override IMember GetMember(long number, IGroup group)
        {
            return new Member(number, group);
        }

        public override IUser GetUser(long number)
        {
            return new User(number);
        }
    }
}