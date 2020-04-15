using HuajiTech.CoolQ.DataExchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示群。
    /// </summary>
    public partial class Group : Chat
    {
        private readonly string _name;
        private GroupInfo _info;

        /// <summary>
        /// 以指定的号码初始化一个 <see cref="Group"/> 类的新实例。
        /// </summary>
        /// <param name="number">号码。</param>
        public Group(long number)
            : base(number)
        {
        }

        internal Group(long number, string name)
            : this(number)
        {
            _name = name;
        }

        internal Group(GroupInfo info)
            : this(info.Number)
        {
            _info = info;
        }

        /// <summary>
        /// 获取一个值，指示是否已请求信息。
        /// </summary>
        public virtual bool HasInfo => !(_info is null);

        /// <summary>
        /// 获取显示名称。
        /// </summary>
        public override string DisplayName => Name;

        /// <summary>
        /// 获取成员容量。
        /// </summary>
        public int MemberCapacity => GetInfo().MemberCapacity;

        /// <summary>
        /// 获取成员数。
        /// </summary>
        public int MemberCount => GetInfo().MemberCount;

        /// <summary>
        /// 获取名称。
        /// </summary>
        public string Name => _name ?? GetInfo().Name;

        /// <summary>
        /// 在添加管理员时引发。
        /// </summary>
        public static event EventHandler<AdministratorEventArgs> AdministratorAdded;

        /// <summary>
        /// 在移除管理员时引发。
        /// </summary>
        public static event EventHandler<AdministratorEventArgs> AdministratorRemoved;

        /// <summary>
        /// 在收到入群请求时引发。
        /// </summary>
        public static event EventHandler<EntranceRequestedEventArgs> EntranceRequested;

        /// <summary>
        /// 在上传文件时引发。
        /// </summary>
        public static event EventHandler<FileUploadedEventArgs> FileUploaded;

        /// <summary>
        /// 在成员加入时引发。
        /// </summary>
        public static event EventHandler<MemberJoinedEventArgs> MemberJoined;

        /// <summary>
        /// 在成员离开时引发。
        /// </summary>
        public static event EventHandler<MemberLeftEventArgs> MemberLeft;

        /// <summary>
        /// 在禁言时引发。
        /// </summary>
        public static event EventHandler<GroupMuteEventArgs> Muted;

        /// <summary>
        /// 在解除禁言时引发。
        /// </summary>
        public static event EventHandler<GroupMuteEventArgs> Unmuted;

        /// <summary>
        /// 禁用匿名。
        /// </summary>
        public void DisableAnonymous()
        {
            NativeMethods.SetGroupIsAnonymousEnabled(Bot.AuthCode, Number, false).CheckError();
        }

        /// <summary>
        /// 以异步操作禁用匿名。
        /// </summary>
        public Task DisableAnonymousAsync()
        {
            return Task.Run(DisableAnonymous);
        }

        /// <summary>
        /// 启用匿名。
        /// </summary>
        public void EnableAnonymous()
        {
            NativeMethods.SetGroupIsAnonymousEnabled(Bot.AuthCode, Number, true).CheckError();
        }

        /// <summary>
        /// 以异步操作启用匿名。
        /// </summary>
        public Task EnableAnonymousAsync()
        {
            return Task.Run(EnableAnonymous);
        }

        /// <summary>
        /// 获取所有成员。
        /// </summary>
        public IReadOnlyList<Member> GetMembers()
        {
            using var reader = new MemberInfoReader(
                NativeMethods.GetGroupMembersBase64(Bot.AuthCode, Number));
            return reader.ReadAll()
                .Select(info => new Member(info))
                .ToList();
        }

        /// <summary>
        /// 以异步操作获取所有成员。
        /// </summary>
        public Task<IReadOnlyList<Member>> GetMembersAsync()
        {
            return Task.Run(GetMembers);
        }

        /// <summary>
        /// 离开。
        /// </summary>
        /// <param name="disband">是否解散。</param>
        public void Leave(bool disband = false)
        {
            if (disband && CurrentUser.AsMemberOf(this).Role != MemberRole.Owner)
            {
                throw new InvalidOperationException(Resources.GroupCanOnlyBeDisbandedByOwnerWhenLeaving);
            }

            NativeMethods.LeaveGroup(Bot.AuthCode, Number, disband).CheckError();
        }

        /// <summary>
        /// 以异步操作离开。
        /// </summary>
        /// <param name="disband">是否解散。</param>
        public Task LeaveAsync(bool disband = false)
        {
            return Task.Run(() => Leave(disband));
        }

        /// <summary>
        /// 禁言。
        /// </summary>
        public void Mute()
        {
            NativeMethods.SetGroupIsMuted(Bot.AuthCode, Number, true).CheckError();
        }

        /// <summary>
        /// 以异步操作禁言。
        /// </summary>
        public Task MuteAsync()
        {
            return Task.Run(Mute);
        }

        /// <summary>
        /// 请求信息。
        /// </summary>
        /// <param name="refresh">是否刷新缓存。</param>
        public virtual void RequestInfo(bool refresh = false)
        {
            _info = null;
            GetInfo(refresh);
        }

        /// <summary>
        /// 以异步操作请求信息。
        /// </summary>
        /// <param name="refresh">是否刷新缓存。</param>
        public virtual Task RequestInfoAsync(bool refresh = false)
        {
            return Task.Run(() => RequestInfo(refresh));
        }

        /// <summary>
        /// 发送消息。
        /// </summary>
        /// <param name="message">要发送的消息。</param>
        /// <returns>发送的消息。</returns>
        public override Message Send(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException(Resources.FieldCannotBeEmpty, nameof(message));
            }

            var id = NativeMethods.SendGroupMessage(Bot.AuthCode, Number, message).CheckError();

            return new Message(id, message);
        }

        /// <summary>
        /// 解除禁言。
        /// </summary>
        public void Unmute()
        {
            NativeMethods.SetGroupIsMuted(Bot.AuthCode, Number, false).CheckError();
        }

        /// <summary>
        /// 以异步方式解除禁言。
        /// </summary>
        public Task UnmuteAsync()
        {
            return Task.Run(Unmute);
        }

        private GroupInfo GetInfo(bool refresh = false)
        {
            if (refresh || _info is null)
            {
                using var reader = new GroupInfoReader(
                    NativeMethods.GetGroupInfoBase64(Bot.AuthCode, Number, refresh));
                _info = reader.Read();
            }

            return _info;
        }
    }
}