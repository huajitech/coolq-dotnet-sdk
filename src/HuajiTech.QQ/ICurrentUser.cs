using HuajiTech.QQ.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    public interface ICurrentUser : IUser
    {
        IReadOnlyCollection<IContact> GetContacts();

        Task<IReadOnlyCollection<IContact>> GetContactsAsync();

        string GetCookies(string domain);

        int GetCsrfToken();

        IReadOnlyCollection<IGroup> GetGroups();

        Task<IReadOnlyCollection<IGroup>> GetGroupsAsync();
    }
}