using System.Collections.Generic;
using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 表示群。
    /// 此类为抽象类。
    /// </summary>
    public abstract class Group : Chat, IMuteable, IRequestable, IRefreshable
    {
        /// <summary>
        /// 以指定的号码初始化一个 <see cref="Group"/> 类的新实例。
        /// </summary>
        /// <param name="number">号码。</param>
        protected Group(long number)
            : base(number)
        {
        }

        public abstract bool HasRequested { get; }

        public abstract int MemberCapacity { get; }

        public abstract int MemberCount { get; }

        /// <summary>
        /// 获取当前 <see cref="Group"/> 对象的名称。
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// 禁用当前 <see cref="Group"/> 对象的匿名功能。
        /// </summary>
        public abstract void DisableAnonymous();

        /// <summary>
        /// 以异步操作禁用当前 <see cref="Group"/> 对象的匿名功能。
        /// </summary>
        public virtual Task DisableAnonymousAsync() => Task.Run(DisableAnonymous);

        /// <summary>
        /// 解散当前 <see cref="Group"/> 对象。
        /// </summary>
        public abstract void Disband();

        /// <summary>
        /// 以异步操作解散当前 <see cref="Group"/> 对象。
        /// </summary>
        public virtual void DisbandAsync() => Task.Run(Disband);

        /// <summary>
        /// 启用当前 <see cref="Group"/> 对象的匿名功能。
        /// </summary>
        public abstract void EnableAnonymous();

        /// <summary>
        /// 以异步操作启用当前 <see cref="Group"/> 对象的匿名功能。
        /// </summary>
        public virtual Task EnableAnonymousAsync() => Task.Run(EnableAnonymous);

        public abstract IReadOnlyCollection<Member> GetMembers();

        public virtual Task<IReadOnlyCollection<Member>> GetMembersAsync() => Task.Run(GetMembers);

        /// <summary>
        /// 离开当前 <see cref="Group"/> 对象。
        /// </summary>
        public abstract void Leave();

        /// <summary>
        /// 以异步操作离开当前 <see cref="Group"/> 对象。
        /// </summary>
        public virtual Task LeaveAsync() => Task.Run(Leave);

        public abstract void Mute();

        public virtual Task MuteAsync() => Task.Run(Mute);

        public abstract void Refresh();

        public virtual Task RefreshAsync() => Task.Run(Refresh);

        public abstract void Request();

        public virtual Task RequestAsync() => Task.Run(Request);

        public abstract void Unmute();

        public virtual Task UnmuteAsync() => Task.Run(Unmute);
    }
}