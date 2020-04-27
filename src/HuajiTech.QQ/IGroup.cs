using System.Collections.Generic;
using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    public interface IGroup : IChattable, IMuteable, IRequestable, IRefreshable
    {
        int MemberCapacity { get; }

        int MemberCount { get; }

        string Name { get; }

        void DisableAnonymous();

        Task DisableAnonymousAsync();

        void EnableAnonymous();

        Task EnableAnonymousAsync();

        IReadOnlyCollection<IMember> GetMembers();

        Task<IReadOnlyCollection<IMember>> GetMembersAsync();

        void Leave(bool disband = false);

        Task LeaveAsync(bool disband = false);
    }
}