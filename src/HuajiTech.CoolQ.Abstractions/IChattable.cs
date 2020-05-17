using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 定义可聊天的对象。
    /// </summary>
    public interface IChattable : ISendee, INamed, IEquatable<IChattable?>
    {
        /// <summary>
        /// 获取当前 <see cref="IChattable"/> 对象的号码。
        /// </summary>
        long Number { get; }
    }
}