using System;

namespace HuajiTech.QQ
{
    public interface IMessage : IEquatable<IMessage>
    {
        /// <summary>
        /// 获取当前 <see cref="IMessage"/> 对象的标识符。
        /// </summary>
        long Id { get; }

        /// <summary>
        /// 撤回当前 <see cref="IMessage"/> 对象。
        /// </summary>
        void Recall();
    }
}