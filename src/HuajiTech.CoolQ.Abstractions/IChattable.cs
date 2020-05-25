using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 定义聊天。
    /// </summary>
    public interface IChattable : ISendee, IDisplayable, IEquatable<IChattable?>
    {
        /// <summary>
        /// 获取当前 <see cref="IChattable"/> 实例的号码。
        /// </summary>
        long Number { get; }
    }
}