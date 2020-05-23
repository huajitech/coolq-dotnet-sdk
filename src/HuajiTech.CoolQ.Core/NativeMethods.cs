using System.Runtime.InteropServices;

namespace HuajiTech.CoolQ
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Globalization", "CA2101:指定对 P/Invoke 字符串参数进行封送处理", Justification = "<挂起>")]
    internal static class NativeMethods
    {
        private const string DllName = "CQP.dll";

        #region Bot

        [DllImport(DllName, EntryPoint = "CQ_addLog")]
        public static extern int Bot_Log(
            int authCode,
            LogLevel level,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string? type,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string? message);

        [DllImport(DllName, EntryPoint = "CQ_setFatal")]
        public static extern int Bot_LogFatal(
            int authCode,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string? message);

        [DllImport(DllName, EntryPoint = "CQ_getAppDirectory")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        public static extern string Bot_GetAppDirectory(int authCode);

        [DllImport(DllName, EntryPoint = "CQ_canSendImage")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Bot_GetCanSendImage(int authCode);

        [DllImport(DllName, EntryPoint = "CQ_canSendRecord")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Bot_GetCanSendRecord(int authCode);

        [DllImport(DllName, EntryPoint = "CQ_getRecordV2")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        public static extern string Bot_GetRecord(
            int authCode,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string fileName,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string fileFormat);

        [DllImport(DllName, EntryPoint = "CQ_getImage")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        public static extern string Bot_GetImage(
            int authCode,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string fileName);

        #endregion // Bot

        #region CurrentUser

        [DllImport(DllName, EntryPoint = "CQ_getCsrfToken")]
        public static extern int CurrentUser_GetCsrfToken(int authCode);

        [DllImport(DllName, EntryPoint = "CQ_getCookiesV2")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        public static extern string? CurrentUser_GetCookies(
            int authCode,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string domain);

        [DllImport(DllName, EntryPoint = "CQ_getLoginQQ")]
        public static extern long CurrentUser_GetNumber(int authCode);

        [DllImport(DllName, EntryPoint = "CQ_getLoginNick")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        public static extern string CurrentUser_GetNickname(int authCode);

        [DllImport(DllName, EntryPoint = "CQ_getGroupList")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        public static extern string? CurrentUser_GetGroups(int authCode);

        [DllImport(DllName, EntryPoint = "CQ_getFriendList")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        public static extern string? CurrentUser_GetFriends(
            int authCode,
            [MarshalAs(UnmanagedType.U1)] bool reserved = false);

        #endregion // CurrentUser

        #region User

        [DllImport(DllName, EntryPoint = "CQ_sendLikeV2")]
        public static extern int User_GiveThumbsUp(
            int authCode,
            long userNumber,
            int count);

        [DllImport(DllName, EntryPoint = "CQ_getStrangerInfo")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        public static extern string? User_GetInfo(
            int authCode,
            long userNumber,
            [MarshalAs(UnmanagedType.U1)] bool refresh);

        [DllImport(DllName, EntryPoint = "CQ_sendPrivateMsg")]
        public static extern int User_Send(
            int authCode,
            long userNumber,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string message);

        #endregion // User

        #region Group

        [DllImport(DllName, EntryPoint = "CQ_setGroupWholeBan")]
        public static extern int Group_SetIsMuted(
            int authCode,
            long groupNumber,
            [MarshalAs(UnmanagedType.U1)] bool isMuted);

        [DllImport(DllName, EntryPoint = "CQ_setGroupAnonymous")]
        public static extern int Group_SetIsAnonymousEnabled(
            int authCode,
            long groupNumber,
            [MarshalAs(UnmanagedType.U1)] bool isEnabled);

        [DllImport(DllName, EntryPoint = "CQ_setGroupLeave")]
        public static extern int Group_Leave(
            int authCode,
            long groupNumber,
            [MarshalAs(UnmanagedType.U1)] bool disband);

        [DllImport(DllName, EntryPoint = "CQ_getGroupMemberList")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        public static extern string? Group_GetMembers(
            int authCode,
            long groupNumber);

        [DllImport(DllName, EntryPoint = "CQ_getGroupInfo")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        public static extern string? Group_GetInfo(
            int authCode,
            long groupNumber,
            [MarshalAs(UnmanagedType.U1)] bool refresh);

        [DllImport(DllName, EntryPoint = "CQ_sendGroupMsg")]
        public static extern int Group_Send(
            int authCode,
            long groupNumber,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string message);

        #endregion // Group

        #region Member

        [DllImport(DllName, EntryPoint = "CQ_setGroupKick")]
        public static extern int Member_Kick(
            int authCode,
            long groupNumber,
            long userNumber,
            [MarshalAs(UnmanagedType.U1)] bool disallowRejoin);

        [DllImport(DllName, EntryPoint = "CQ_setGroupBan")]
        public static extern int Member_Mute(
            int authCode,
            long groupNumber,
            long userNumber,
            long seconds);

        [DllImport(DllName, EntryPoint = "CQ_setGroupAdmin")]
        public static extern int Member_SetIsAdministrator(
            int authCode,
            long groupNumber,
            long userNumber,
            [MarshalAs(UnmanagedType.U1)] bool isAdministrator);

        [DllImport(DllName, EntryPoint = "CQ_setGroupSpecialTitle")]
        public static extern int Member_SetCustomTitle(
            int authCode,
            long groupNumber,
            long userNumber,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string title,
            long expirationSeconds);

        [DllImport(DllName, EntryPoint = "CQ_setGroupCard")]
        public static extern int Member_SetAlias(
            int authCode,
            long groupNumber,
            long userNumber,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string alias);

        [DllImport(DllName, EntryPoint = "CQ_getGroupMemberInfoV2")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))]
        public static extern string? Member_GetInfo(
            int authCode,
            long groupNumber,
            long userNumber,
            [MarshalAs(UnmanagedType.U1)] bool refresh);

        #endregion // Member

        #region AnonymousMember

        [DllImport(DllName, EntryPoint = "CQ_setGroupAnonymousBan")]
        public static extern int AnonymousMember_Mute(
            int authCode,
            long groupMember,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string anonymousInfo,
            long seconds);

        #endregion // AnonymousMember

        #region Message

        [DllImport(DllName, EntryPoint = "CQ_deleteMsg")]
        public static extern int Message_Recall(
            int authCode,
            long messageId);

        #endregion // Message

        #region FriendshipRequest

        [DllImport(DllName, EntryPoint = "CQ_setFriendAddRequest")]
        public static extern int FriendshipRequest_Respond(
            int authCode,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string token,
            Response response,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string? alias);

        #endregion // FriendshipRequest

        #region EntranceRequest

        [DllImport(DllName, EntryPoint = "CQ_setGroupAddRequestV2")]
        public static extern int EntranceRequest_Respond(
            int authCode,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string token,
            EntranceType entranceType,
            Response response,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string? rejectReason);

        #endregion // EntranceRequest
    }
}