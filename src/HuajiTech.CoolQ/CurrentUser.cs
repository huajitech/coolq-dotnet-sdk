using HuajiTech.CoolQ.DataExchange;
using HuajiTech.QQ;
using System.Collections.Generic;
using System.Linq;

namespace HuajiTech.CoolQ
{
    internal class CurrentUser : User, ICurrentUser
    {
        internal CurrentUser()
            : base(NativeMethods.GetCurrentUserNumber(Bot.Instance.AuthCode).CheckError())
        {
        }

        public override string Nickname => NativeMethods.GetCurrentUserNickname(Bot.Instance.AuthCode);

        internal static IReadOnlyCollection<FriendInfo> GetFriendInfos()
        {
            using var reader = new FriendInfoReader(
                NativeMethods.GetFriendsBase64(Bot.Instance.AuthCode, false).CheckError());

            return reader.ReadAll().ToList();
        }

        public IReadOnlyCollection<QQ.IFriend> GetFriends()
        {
            return GetFriendInfos()
                .Select(info => new Friend(info))
                .ToList();
        }

        public string GetCookies(string domain) => NativeMethods.GetCookies(Bot.Instance.AuthCode, domain).CheckError();

        public int GetCsrfToken() => NativeMethods.GetCsrfToken(Bot.Instance.AuthCode).CheckError();

        public IReadOnlyCollection<IGroup> GetGroups()
        {
            using var reader = new BasicGroupInfoReader(
                NativeMethods.GetGroupsBase64(Bot.Instance.AuthCode).CheckError());

            return reader.ReadAll()
                .Select(info => new Group(info.Number, info.Name!))
                .ToList();
        }
    }
}