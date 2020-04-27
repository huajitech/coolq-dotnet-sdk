using HuajiTech.CoolQ.DataExchange;
using HuajiTech.QQ;
using System;
using System.Threading.Tasks;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示用户。
    /// </summary>
    internal class User : Chat, IUser
    {
        private UserInfo _info;

        /// <summary>
        /// 以指定的号码初始化一个 <see cref="User"/> 类的新实例。
        /// </summary>
        internal User(long number)
            : base(number)
        {
        }

        internal User(UserInfo info)
            : this(info.Number)
        {
            _info = info;
        }

        /// <summary>
        /// 获取一个值，指示当前 <see cref="User"/> 对象是否含有信息。
        /// </summary>
        public virtual bool HasRequested => !(_info is null);

        /// <summary>
        /// 获取当前 <see cref="User"/> 对象的显示名称。
        /// 对于 <see cref="User"/> 对象，为 <see cref="Nickname"/>。
        /// </summary>
        public override string DisplayName => Nickname;

        /// <summary>
        /// 获取当前 <see cref="User"/> 对象的昵称。
        /// </summary>
        public virtual string Nickname => GetInfo().Nickname;

        /// <summary>
        /// 给予当前 <see cref="User"/> 对象指定数量的赞。
        /// </summary>
        /// <param name="count">赞的数量。</param>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public void GiveThumbsUp(int count)
        {
            NativeMethods.GiveThumbsUp(Bot.Instance.AuthCode, Number, count).CheckError();
        }

        /// <summary>
        /// 以异步操作给予当前 <see cref="User"/> 对象指定数量的赞。
        /// </summary>
        /// <param name="count">赞的数量。</param>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task GiveThumbsUpAsync(int count)
        {
            return Task.Run(() => GiveThumbsUp(count));
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

        public virtual void Refresh()
        {
            GetInfo(true, true);
        }

        public virtual Task RefreshAsync()
        {
            return Task.Run(Refresh);
        }

        /// <summary>
        /// 向当前 <see cref="User"/> 对象发送消息。
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

            var id = NativeMethods.SendPrivateMessage(Bot.Instance.AuthCode, Number, message).CheckError();

            return new Message(id, message);
        }

        private UserInfo GetInfo(bool throwException = false, bool refresh = false)
        {
            if (refresh || _info is null)
            {
                try
                {
                    using var reader = new UserInfoReader(
                        NativeMethods.GetUserInfoBase64(Bot.Instance.AuthCode, Number, refresh));
                    _info = reader.Read();
                }
                catch (CoolQException) when (!throwException)
                {
                    return new UserInfo();
                }
            }

            return _info;
        }
    }
}