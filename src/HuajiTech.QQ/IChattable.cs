using System;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 定义可聊天的对象。
    /// </summary>
    public interface IChattable : ISendable, IEquatable<IChattable?>
    {
        /// <summary>
        /// 获取当前 <see cref="IChattable"/> 对象的显示名称。
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// 获取当前 <see cref="IChattable"/> 对象的号码。
        /// </summary>
        long Number { get; }
    }
}