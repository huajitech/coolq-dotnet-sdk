using HuajiTech.QQ;
using System;

namespace HuajiTech.CoolQ
{
    internal class PluginContext : QQ.PluginContext
    {
        public PluginContext(IBot bot) => Bot = bot ?? throw new ArgumentNullException(nameof(bot));

        public override IBot Bot { get; }

        public override QQ.Contact GetContact(long number) => new Contact(number);

        public override QQ.Group GetGroup(long number) => new Group(number);

        public override QQ.Member GetMember(long number, QQ.Group group) => new Member(number, group);

        public override QQ.User GetUser(long number) => new User(number);
    }
}