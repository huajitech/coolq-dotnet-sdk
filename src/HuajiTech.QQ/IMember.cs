using System;
using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    public interface IMember : IUser, ITimedMuteable, IEquatable<IMember>
    {
        int Age { get; }

        string Alias { get; }

        bool CanEditAlias { get; }

        CustomTitle CustomTitle { get; }

        DateTime EntranceTime { get; }

        Gender Gender { get; }

        IGroup Group { get; }

        bool HasBadRecord { get; }

        bool IsAdministrator { get; }

        DateTime LastSpeakTime { get; }

        string Level { get; }

        string Location { get; }

        MemberRole Role { get; }

        void Kick(bool disallowRejoin = false);

        Task KickAsync(bool disallowRejoin = false);

        void SetAlias(string alias);

        Task SetAliasAsync(string alias);

        void SetAsAdministrator();

        Task SetAsAdministratorAsync();

        void SetCustomTitle(CustomTitle title);

        Task SetCustomTitleAsync(CustomTitle title);

        void UnsetAsAdministrator();

        Task UnsetAsAdministratorAsync();
    }
}