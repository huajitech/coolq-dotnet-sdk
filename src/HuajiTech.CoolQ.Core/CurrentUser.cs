using HuajiTech.CoolQ.DataExchange;
using System.Collections.Generic;
using System.Linq;

namespace HuajiTech.CoolQ
{
    internal class CurrentUser : User, ICurrentUser
    {
        public override long Number => NativeMethods.CurrentUser_GetNumber(Bot.Instance.AuthCode);

        public override string Nickname => NativeMethods.CurrentUser_GetNickname(Bot.Instance.AuthCode);

        internal static IReadOnlyCollection<FriendInfo> GetFriendInfos()
        {
            using var reader = new FriendInfoReader(
                NativeMethods.CurrentUser_GetFriends(Bot.Instance.AuthCode).CheckError());

            return reader.ReadAll().ToList();
        }

        public IReadOnlyCollection<IFriend> GetFriends()
        {
            return GetFriendInfos()
                .Select(info => new Friend(info))
                .ToList();
        }

        public string GetCookies(string domain) => NativeMethods.CurrentUser_GetCookies(Bot.Instance.AuthCode, domain).CheckError();

        public int GetCsrfToken() => NativeMethods.CurrentUser_GetCsrfToken(Bot.Instance.AuthCode).CheckError();

        public IReadOnlyCollection<IGroup> GetGroups()
        {
            using var reader = new BasicGroupInfoReader(
                NativeMethods.CurrentUser_GetGroups(Bot.Instance.AuthCode).CheckError());

            return reader.ReadAll()
                .Select(info => new Group(info.Number, info.Name!))
                .ToList();
        }
    }
}