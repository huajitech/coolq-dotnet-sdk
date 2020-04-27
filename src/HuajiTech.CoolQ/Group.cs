using HuajiTech.CoolQ.DataExchange;
using HuajiTech.QQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示群聊。
    /// </summary>
    internal partial class Group : Chat, IGroup
    {
        private readonly string _name;
        private GroupInfo _info;

        /// <summary>
        /// 以指定的号码初始化一个 <see cref="Group"/> 类的新实例。
        /// </summary>
        internal Group(long number)
            : base(number)
        {
        }

        internal Group(long number, string name)
            : this(number)
        {
            _name = name;
        }

        /// <summary>
        /// 获取显示名称。
        /// 对于 <see cref="Group"/> 对象，为 <see cref="Name"/>。
        /// </summary>
        public override string DisplayName => Name;

        /// <summary>
        /// 获取一个值，指示当前 <see cref="Group"/> 对象是否含有信息。
        /// </summary>
        public virtual bool HasRequested => !(_info is null);

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
        /// 禁用当前 <see cref="Group"/> 对象的匿名聊天功能。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public void DisableAnonymous()
        {
            NativeMethods.SetGroupIsAnonymousEnabled(Bot.Instance.AuthCode, Number, false).CheckError();
        }

        /// <summary>
        /// 以异步操作禁用当前 <see cref="Group"/> 对象的匿名聊天功能。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task DisableAnonymousAsync()
        {
            return Task.Run(DisableAnonymous);
        }

        /// <summary>
        /// 启用当前 <see cref="Group"/> 对象的匿名聊天功能。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public void EnableAnonymous()
        {
            NativeMethods.SetGroupIsAnonymousEnabled(Bot.Instance.AuthCode, Number, true).CheckError();
        }

        /// <summary>
        /// 启用当前 <see cref="Group"/> 对象的匿名聊天功能。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task EnableAnonymousAsync()
        {
            return Task.Run(EnableAnonymous);
        }

        /// <summary>
        /// 获取当前 <see cref="Group"/> 对象的中的所有成员。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public IReadOnlyCollection<IMember> GetMembers()
        {
            using var reader = new MemberInfoReader(
                NativeMethods.GetGroupMembersBase64(Bot.Instance.AuthCode, Number));
            return reader.ReadAll()
                .Select(info => new Member(info))
                .ToList();
        }

        /// <summary>
        /// 以异步操作当前 <see cref="Group"/> 对象的中的所有成员。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task<IReadOnlyCollection<IMember>> GetMembersAsync()
        {
            return Task.Run(GetMembers);
        }

        /// <summary>
        /// 离开当前 <see cref="Group"/> 对象。
        /// </summary>
        /// <param name="disband">如果要在离开后解散当前 <see cref="Group"/> 对象，则为 <c>true</c>；否则为 <c>false</c>。</param>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public void Leave(bool disband = false)
        {
            NativeMethods.LeaveGroup(Bot.Instance.AuthCode, Number, disband).CheckError();
        }

        /// <summary>
        /// 以异步操作离开当前 <see cref="Group"/> 对象。
        /// </summary>
        /// <param name="disband">如果要在离开后解散当前 <see cref="Group"/> 对象，则为 <c>true</c>；否则为 <c>false</c>。</param>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task LeaveAsync(bool disband = false)
        {
            return Task.Run(() => Leave(disband));
        }

        /// <summary>
        /// 禁言当前 <see cref="Group"/> 对象。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public void Mute()
        {
            NativeMethods.SetGroupIsMuted(Bot.Instance.AuthCode, Number, true).CheckError();
        }

        /// <summary>
        /// 以异步操作禁言当前 <see cref="Group"/> 对象。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task MuteAsync()
        {
            return Task.Run(Mute);
        }

        public virtual void Refresh()
        {
            GetInfo(true, true);
        }

        public virtual Task RefreshAsync()
        {
            return Task.Run(Refresh);
        }

        public virtual void Request()
        {
            _info = null;
            GetInfo(true);
        }

        public virtual Task RequestAsync()
        {
            return Task.Run(Request);
        }

        /// <summary>
        /// 向当前 <see cref="Group"/> 对象发送消息。
        /// </summary>
        /// <param name="message">要发送的消息。</param>
        /// <returns>一个 <see cref="Message"/> 对象，表示已发送的消息。</returns>
        /// <exception cref="ArgumentException"><paramref name="message"/> 为 <c>null</c> 或 <see cref="string.Empty"/>。</exception>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public override IMessage Send(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException(Resources.FieldCannotBeEmpty, nameof(message));
            }

            var id = NativeMethods.SendGroupMessage(Bot.Instance.AuthCode, Number, message).CheckError();

            return new Message(id, message);
        }

        /// <summary>
        /// 将当前 <see cref="Group"/> 对象解除禁言。
        /// </summary>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public void Unmute()
        {
            NativeMethods.SetGroupIsMuted(Bot.Instance.AuthCode, Number, false).CheckError();
        }

        /// <summary>
        /// 以异步操作将当前 <see cref="Group"/> 对象解除禁言。
        /// </summary>
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
                        NativeMethods.GetGroupInfoBase64(Bot.Instance.AuthCode, Number, refresh));
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