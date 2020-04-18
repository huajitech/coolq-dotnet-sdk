using HuajiTech.CoolQ.DataExchange;
using System;
using System.Threading.Tasks;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示用户。
    /// </summary>
    public class User : Chat
    {
        private UserInfo _info;

        /// <summary>
        /// 以指定的号码初始化一个 <see cref="User"/> 类的新实例。
        /// </summary>
        /// <param name="number">号码。</param>
        public User(long number)
            : base(number)
        {
        }

        internal User(UserInfo info)
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
        public override string DisplayName => Nickname;

        /// <summary>
        /// 获取昵称。
        /// </summary>
        public virtual string Nickname => GetInfo().Nickname;

        /// <summary>
        /// 点赞。
        /// </summary>
        /// <param name="count">数量。</param>
        public void GiveThumbsUp(int count)
        {
            NativeMethods.GiveThumbsUp(Bot.AuthCode, Number, count).CheckError();
        }

        /// <summary>
        /// 以异步操作点赞。
        /// </summary>
        /// <param name="count">数量。</param>
        public Task GiveThumbsUpAsync(int count)
        {
            return Task.Run(() => GiveThumbsUp(count));
        }

        /// <summary>
        /// 请求信息。
        /// </summary>
        /// <param name="refresh">是否刷新缓存。</param>
        public virtual void RequestInfo(bool refresh = false)
        {
            _info = null;
            GetInfo(false, refresh);
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

            var id = NativeMethods.SendPrivateMessage(Bot.AuthCode, Number, message).CheckError();

            return new Message(id, message);
        }

        private UserInfo GetInfo(bool handleException = true, bool refresh = false)
        {
            if (refresh || _info is null)
            {
                try
                {
                    using var reader = new UserInfoReader(
                        NativeMethods.GetUserInfoBase64(Bot.AuthCode, Number, refresh));
                    _info = reader.Read();
                }
                catch (CoolQException) when (handleException)
                {
                    return new UserInfo();
                }
            }

            return _info;
        }
    }
}