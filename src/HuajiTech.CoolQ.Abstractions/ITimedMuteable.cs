using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 定义可以指定时间的可禁言对象。
    /// </summary>
    public interface ITimedMuteable : IMuteable
    {
        /// <summary>
        /// 将当前 <see cref="ITimedMuteable"/> 对象禁言指定时长。
        /// </summary>
        /// <param name="duration">要禁言的时长。</param>
        void Mute(TimeSpan duration);
    }
}