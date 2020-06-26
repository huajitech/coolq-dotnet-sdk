using System;
using System.Linq;

namespace HuajiTech.CoolQ.Events
{
    internal class AdministratorsChangedEventArgs : GroupEventArgs
    {
        public AdministratorsChangedEventArgs(DateTime time, IGroup source, IMember operatee)
            : base(time, source, null!, operatee)
        {
        }

        public override IMember Operator
            => Source.GetMembers().First(member => member.Role is MemberRole.Owner);
    }
}