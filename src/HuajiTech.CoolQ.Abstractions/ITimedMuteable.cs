using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 提供用于禁言指定时间的方法。
    /// </summary>
    public interface ITimedMuteable : IMuteable
    {
        /// <summary>
        /// 将当前 <see cref="ITimedMuteable"/> 实例禁言指定时长。
        /// </summary>
        /// <param name="duration">要禁言的时长。</param>
        void Mute(TimeSpan duration);
    }
}