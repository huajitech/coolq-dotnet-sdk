using HuajiTech.CoolQ.DataExchange;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HuajiTech.CoolQ
{
    internal partial class CurrentUser : QQ.CurrentUser
    {
        private static string _nickname;

        internal CurrentUser()
            : base(NativeMethods.GetCurrentUserNumber(Bot.Instance.AuthCode).CheckError())
        {
        }

        public override string Nickname
        {
            get
            {
                Request();
                return _nickname;
            }
        }

        public override bool HasRequested => throw new NotImplementedException();

        public override string DisplayName => throw new NotImplementedException();

        internal static IReadOnlyCollection<ContactInfo> GetContactInfos()
        {
            using var reader = new ContactInfoReader(
                NativeMethods.GetContactsBase64(Bot.Instance.AuthCode, false).CheckError());

            return reader.ReadAll().ToList();
        }

        public override IReadOnlyCollection<QQ.Contact> GetContacts()
        {
            return GetContactInfos()
                .Select(info => new Contact(info))
                .ToList();
        }

        public override string GetCookies(string domain)
        {
            return NativeMethods.GetCookies(Bot.Instance.AuthCode, domain).CheckError();
        }

        public override int GetCsrfToken()
        {
            return NativeMethods.GetCsrfToken(Bot.Instance.AuthCode).CheckError();
        }

        public override IReadOnlyCollection<QQ.Group> GetGroups()
        {
            using var reader = new BasicGroupInfoReader(
                NativeMethods.GetGroupsBase64(Bot.Instance.AuthCode).CheckError());

            return reader.ReadAll()
                .Select(info => new Group(info.Number, info.Name))
                .ToList();
        }

        public override void GiveThumbsUp(int count) =>
            throw new NotSupportedException(Resources.CannotGiveThumbsUpToCurrentUser);

        public override void Refresh()
        {
            _nickname = null;
            Request();
        }

        public override void Request() => _nickname ??= NativeMethods.GetCurrentUserNickname(Bot.Instance.AuthCode);

        public override QQ.Message Send(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException(Resources.FieldCannotBeEmpty, nameof(message));
            }

            var id = NativeMethods.SendPrivateMessage(Bot.Instance.AuthCode, Number, message).CheckError();

            return new Message(id, message);
        }
    }
}