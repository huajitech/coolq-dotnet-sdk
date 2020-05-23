using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 定义可聊天的实例。
    /// </summary>
    public interface IChattable : ISendee, IDisplayable, IEquatable<IChattable?>
    {
        /// <summary>
        /// 获取当前 <see cref="IChattable"/> 实例的号码。
        /// </summary>
        long Number { get; }
    }
}