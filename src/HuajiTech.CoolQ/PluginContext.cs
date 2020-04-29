using HuajiTech.QQ;
using System;

namespace HuajiTech.CoolQ
{
    internal class PluginContext : QQ.PluginContext
    {
        public PluginContext(IBot bot)
        {
            Bot = bot ?? throw new ArgumentNullException(nameof(bot));
        }

        public override IBot Bot { get; }

        public override QQ.Contact GetContact(long number)
        {
            return new Contact(number);
        }

        public override QQ.Group GetGroup(long number)
        {
            return new Group(number);
        }

        public override QQ.Member GetMember(long number, QQ.Group group)
        {
            return new Member(number, group);
        }

        public override QQ.User GetUser(long number)
        {
            return new User(number);
        }
    }
}