using HuajiTech.QQ;
using System;

namespace HuajiTech.CoolQ
{
    internal class PluginContext : QQ.PluginContext
    {
        public PluginContext(IBot bot) => Bot = bot ?? throw new ArgumentNullException(nameof(bot));

        public override IBot Bot { get; }

        public override IFriend GetFriend(long number) => new Friend(number);

        public override IGroup GetGroup(long number) => new Group(number);

        public override IMember GetMember(long number, IGroup group) => new Member(number, group);

        public override IUser GetUser(long number) => new User(number);
    }
}