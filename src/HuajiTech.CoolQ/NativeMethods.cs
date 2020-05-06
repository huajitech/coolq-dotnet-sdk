using HuajiTech.QQ;
using System.Runtime.InteropServices;

namespace HuajiTech.CoolQ
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Globalization", "CA2101:指定对 P/Invoke 字符串参数进行封送处理", Justification = "<挂起>")]
    internal static class NativeMethods
    {
        internal const UnmanagedType UnmanagedStringType = UnmanagedType.AnsiBStr;
        private const string DllName = "CQP.dll";

        #region 环境

        [DllImport(DllName, EntryPoint = "CQ_addLog")]
        public static extern int Log(
            int authCode,
            LogLevel level,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string type,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string message);

        [DllImport(DllName, EntryPoint = "CQ_setFatal")]
        public static extern int LogFatal(
            int authCode,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string message);

        [DllImport(DllName, EntryPoint = "CQ_getAppDirectory")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        public static extern string GetDataDirectory(int authCode);

        [DllImport(DllName, EntryPoint = "CQ_canSendImage")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCanSendImage(int authCode);

        [DllImport(DllName, EntryPoint = "CQ_canSendRecord")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCanSendRecord(int authCode);

        [DllImport(DllName, EntryPoint = "CQ_getCookiesV2")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        public static extern string GetCookies(
            int authCode,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string domain);

        [DllImport(DllName, EntryPoint = "CQ_getCsrfToken")]
        public static extern int GetCsrfToken(int authCode);

        [DllImport(DllName, EntryPoint = "CQ_getLoginQQ")]
        public static extern long GetCurrentUserNumber(int authCode);

        [DllImport(DllName, EntryPoint = "CQ_getLoginNick")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        public static extern string GetCurrentUserNickname(int authCode);

        [DllImport(DllName, EntryPoint = "CQ_getGroupList")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        public static extern string GetGroupsBase64(int authCode);

        [DllImport(DllName, EntryPoint = "CQ_getFriendList")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        public static extern string GetContactsBase64(
            int authCode,
            [MarshalAs(UnmanagedType.U1)] bool reservedParameter /* 总是 false */);

        #endregion 环境

        #region 用户

        [DllImport(DllName, EntryPoint = "CQ_sendLikeV2")]
        public static extern int GiveThumbsUp(
            int authCode,
            long userNumber,
            int count);

        [DllImport(DllName, EntryPoint = "CQ_setFriendAddRequest")]
        public static extern int RespondContactRequest(
            int authCode,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string token,
            Response response,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string alias);

        [DllImport(DllName, EntryPoint = "CQ_getStrangerInfo")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        public static extern string GetUserInfoBase64(
            int authCode,
            long userNumber,
            [MarshalAs(UnmanagedType.U1)] bool refresh);

        #endregion 用户

        #region 群聊

        [DllImport(DllName, EntryPoint = "CQ_setGroupWholeBan")]
        public static extern int SetGroupIsMuted(
            int authCode,
            long groupNumber,
            [MarshalAs(UnmanagedType.U1)] bool isMuted);

        [DllImport(DllName, EntryPoint = "CQ_setGroupAnonymous")]
        public static extern int SetGroupIsAnonymousEnabled(
            int authCode,
            long groupNumber,
            [MarshalAs(UnmanagedType.U1)] bool isEnabled);

        [DllImport(DllName, EntryPoint = "CQ_setGroupLeave")]
        public static extern int LeaveGroup(
            int authCode,
            long groupNumber,
            [MarshalAs(UnmanagedType.U1)] bool disband);

        [DllImport(DllName, EntryPoint = "CQ_getGroupMemberList")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        public static extern string GetGroupMembersBase64(
            int authCode,
            long groupNumber);

        [DllImport(DllName, EntryPoint = "CQ_getGroupInfo")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        public static extern string GetGroupInfoBase64(
            int authCode,
            long groupNumber,
            [MarshalAs(UnmanagedType.U1)] bool refresh);

        #endregion 群聊

        #region 成员

        [DllImport(DllName, EntryPoint = "CQ_setGroupKick")]
        public static extern int KickMember(
            int authCode,
            long groupNumber,
            long userNumber,
            [MarshalAs(UnmanagedType.U1)] bool disallowRejoin);

        [DllImport(DllName, EntryPoint = "CQ_setGroupBan")]
        public static extern int MuteMember(
            int authCode,
            long groupNumber,
            long userNumber,
            long seconds);

        [DllImport(DllName, EntryPoint = "CQ_setGroupAnonymousBan")]
        public static extern int MuteAnonymousMember(
            int authCode,
            long groupMember,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string anonymousInfo,
            long seconds);

        [DllImport(DllName, EntryPoint = "CQ_setGroupAdmin")]
        public static extern int SetMemberIsAdministrator(
            int authCode,
            long groupNumber,
            long userNumber,
            [MarshalAs(UnmanagedType.U1)] bool isAdministrator);

        [DllImport(DllName, EntryPoint = "CQ_setGroupSpecialTitle")]
        public static extern int SetMemberCustomTitle(
            int authCode,
            long groupNumber,
            long userNumber,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string title,
            long expirationSeconds);

        [DllImport(DllName, EntryPoint = "CQ_setGroupCard")]
        public static extern int SetMemberAlias(
            int authCode,
            long groupNumber,
            long userNumber,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string alias);

        [DllImport(DllName, EntryPoint = "CQ_setGroupAddRequestV2")]
        public static extern int RespondEntranceRequest(
            int authCode,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string token,
            MemberEventType requestType,
            Response response,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string rejectReason);

        [DllImport(DllName, EntryPoint = "CQ_getGroupMemberInfoV2")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        public static extern string GetMemberInfoBase64(
            int authCode,
            long groupNumber,
            long memberNumber,
            [MarshalAs(UnmanagedType.U1)] bool refresh);

        #endregion 成员

        #region 消息

        [DllImport(DllName, EntryPoint = "CQ_sendPrivateMsg")]
        public static extern int SendPrivateMessage(
            int authCode,
            long userNumber,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string message);

        [DllImport(DllName, EntryPoint = "CQ_sendGroupMsg")]
        public static extern int SendGroupMessage(
            int authCode,
            long groupNumber,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string message);

        [DllImport(DllName, EntryPoint = "CQ_deleteMsg")]
        public static extern int RecallMessage(
            int authCode,
            long messageId);

        [DllImport(DllName, EntryPoint = "CQ_getRecordV2")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        public static extern string GetRecord(
            int authCode,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string fileName,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string fileFormat);

        [DllImport(DllName, EntryPoint = "CQ_getImage")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        public static extern string GetImage(
            int authCode,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string fileName);

        #endregion 消息
    }
}