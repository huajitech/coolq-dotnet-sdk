using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 指定程序集的初始化器。
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class InitializerAttribute : Attribute
    {
        /// <summary>
        /// 使用指定的初始化器初始化一个 <see cref="InitializerAttribute"/> 的新实例。
        /// </summary>
        /// <param name="initializer">要使用的初始化器。</param>
        public InitializerAttribute(Type initializer)
            => Initializer = initializer ?? throw new ArgumentNullException(nameof(initializer));

        /// <summary>
        /// 使用指定的初始化器初始化一个 <see cref="InitializerAttribute"/> 的新实例。
        /// </summary>
        /// <param name="initializer">要使用的初始化器。</param>
        public InitializerAttribute(string initializer)
            : this(Type.GetType(initializer))
        {
        }

        /// <summary>
        /// 获取当前 <see cref="InitializerAttribute"/> 的初始化器。
        /// </summary>
        public Type Initializer { get; }
    }
}