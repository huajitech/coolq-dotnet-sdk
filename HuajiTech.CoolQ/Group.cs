using HuajiTech.CoolQ.DataExchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示群聊。
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
        /// 获取一个值，指示当前 <see cref="Group"/> 对象是否含有信息。
        /// </summary>
        public virtual bool HasInfo => !(_info is null);

        /// <summary>
        /// 获取显示名称。
        /// 对于 <see cref="Group"/> 对象，为 <see cref="Name"/>。
        /// </summary>
        public override string DisplayName => Name;

        /// <summary>
        /// 获取当前 <see cref="Group"/> 对象的成员容量。
        /// </summary>
        public int MemberCapacity => GetInfo().MemberCapacity;

        /// <summary>
        /// 获取当前 <see cref="Group"/> 对象的成员数量。
        /// </summary>
        public int MemberCount => GetInfo().MemberCount;

        /// <summary>
        /// 获取当前 <see cref="Group"/> 对象的名称。
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
        /// 禁用当前 <see cref="Group"/> 对象的匿名聊天功能。
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="Bot.CurrentUser"/> 不是当前 <see cref="Group"/> 对象的管理员。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public void DisableAnonymous()
        {
            if (!Bot.CurrentUser.AsMemberOf(this).IsAdministrator)
            {
                throw new InvalidOperationException(Resources.AdministratorOnlyOperation);
            }

            NativeMethods.SetGroupIsAnonymousEnabled(Bot.AuthCode, Number, false).CheckError();
        }

        /// <summary>
        /// 以异步操作禁用当前 <see cref="Group"/> 对象的匿名聊天功能。
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="Bot.CurrentUser"/> 不是当前 <see cref="Group"/> 对象的管理员。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task DisableAnonymousAsync()
        {
            return Task.Run(DisableAnonymous);
        }

        /// <summary>
        /// 启用当前 <see cref="Group"/> 对象的匿名聊天功能。
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="Bot.CurrentUser"/> 不是当前 <see cref="Group"/> 对象的管理员。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public void EnableAnonymous()
        {
            if (!Bot.CurrentUser.AsMemberOf(this).IsAdministrator)
            {
                throw new InvalidOperationException(Resources.AdministratorOnlyOperation);
            }

            NativeMethods.SetGroupIsAnonymousEnabled(Bot.AuthCode, Number, true).CheckError();
        }

        /// <summary>
        /// 启用当前 <see cref="Group"/> 对象的匿名聊天功能。
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="Bot.CurrentUser"/> 不是当前 <see cref="Group"/> 对象的管理员。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task EnableAnonymousAsync()
        {
            return Task.Run(EnableAnonymous);
        }

        /// <summary>
        /// 获取当前 <see cref="Group"/> 对象的中的所有成员。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public IReadOnlyCollection<Member> GetMembers()
        {
            using var reader = new MemberInfoReader(
                NativeMethods.GetGroupMembersBase64(Bot.AuthCode, Number));
            return reader.ReadAll()
                .Select(info => new Member(info))
                .ToList();
        }

        /// <summary>
        /// 以异步操作当前 <see cref="Group"/> 对象的中的所有成员。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task<IReadOnlyCollection<Member>> GetMembersAsync()
        {
            return Task.Run(GetMembers);
        }

        /// <summary>
        /// 离开当前 <see cref="Group"/> 对象。
        /// </summary>
        /// <param name="disband">如果要在离开后解散当前 <see cref="Group"/> 对象，则为 <c>true</c>；否则为 <c>false</c>。</param>
        /// <exception cref="InvalidOperationException"><see cref="Bot.CurrentUser"/> 不是当前 <see cref="Group"/> 的群主。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public void Leave(bool disband = false)
        {
            if (disband && Bot.CurrentUser.AsMemberOf(this).Role != MemberRole.Owner)
            {
                throw new InvalidOperationException(Resources.OwnerOnlyOperation);
            }

            NativeMethods.LeaveGroup(Bot.AuthCode, Number, disband).CheckError();
        }

        /// <summary>
        /// 以异步操作离开当前 <see cref="Group"/> 对象。
        /// </summary>
        /// <param name="disband">如果要在离开后解散当前 <see cref="Group"/> 对象，则为 <c>true</c>；否则为 <c>false</c>。</param>
        /// <exception cref="InvalidOperationException"><see cref="Bot.CurrentUser"/> 不是当前 <see cref="Group"/> 对象的群主。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task LeaveAsync(bool disband = false)
        {
            return Task.Run(() => Leave(disband));
        }

        /// <summary>
        /// 禁言当前 <see cref="Group"/> 对象。
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="Bot.CurrentUser"/> 不是当前 <see cref="Group"/> 对象的管理员。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public void Mute()
        {
            if (!Bot.CurrentUser.AsMemberOf(this).IsAdministrator)
            {
                throw new InvalidOperationException(Resources.AdministratorOnlyOperation);
            }

            NativeMethods.SetGroupIsMuted(Bot.AuthCode, Number, true).CheckError();
        }

        /// <summary>
        /// 以异步操作禁言当前 <see cref="Group"/> 对象。
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="Bot.CurrentUser"/> 不是当前 <see cref="Group"/> 对象的管理员。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task MuteAsync()
        {
            return Task.Run(Mute);
        }

        /// <summary>
        /// 请求当前 <see cref="Group"/> 对象的信息。
        /// </summary>
        /// <param name="refresh">如果要求酷Q不使用缓存的信息，则为 <c>true</c>；否则为 <c>false</c>。</param>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public virtual void RequestInfo(bool refresh = false)
        {
            _info = null;
            GetInfo(false, refresh);
        }

        /// <summary>
        /// 以异步操作请求当前 <see cref="Group"/> 对象的信息。
        /// </summary>
        /// <param name="refresh">如果要求酷Q不使用缓存的信息，则为 <c>true</c>；否则为 <c>false</c>。</param>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public virtual Task RequestInfoAsync(bool refresh = false)
        {
            return Task.Run(() => RequestInfo(refresh));
        }

        /// <summary>
        /// 向当前 <see cref="Group"/> 对象发送消息。
        /// </summary>
        /// <param name="message">要发送的消息。</param>
        /// <returns>一个 <see cref="Message"/> 对象，表示已发送的消息。</returns>
        /// <exception cref="ArgumentException"><paramref name="message"/> 为 <c>null</c> 或 <see cref="string.Empty"/>。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
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
        /// 将当前 <see cref="Group"/> 对象解除禁言。
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="Bot.CurrentUser"/> 不是当前 <see cref="Group"/> 对象的管理员。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public void Unmute()
        {
            if (!Bot.CurrentUser.AsMemberOf(this).IsAdministrator)
            {
                throw new InvalidOperationException(Resources.AdministratorOnlyOperation);
            }

            NativeMethods.SetGroupIsMuted(Bot.AuthCode, Number, false).CheckError();
        }

        /// <summary>
        /// 以异步操作将当前 <see cref="Group"/> 对象解除禁言。
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="Bot.CurrentUser"/> 不是当前 <see cref="Group"/> 对象的管理员。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task UnmuteAsync()
        {
            return Task.Run(Unmute);
        }

        private GroupInfo GetInfo(bool throwException = false, bool refresh = false)
        {
            if (refresh || _info is null)
            {
                try
                {
                    using var reader = new GroupInfoReader(
                        NativeMethods.GetGroupInfoBase64(Bot.AuthCode, Number, refresh));
                    _info = reader.Read();
                }
                catch (CoolQException) when (!throwException)
                {
                    return new GroupInfo();
                }
            }

            return _info;
        }
    }
}