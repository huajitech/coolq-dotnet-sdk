using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示自定义头衔。
    /// </summary>
    public class CustomTitle
    {
        /// <summary>
        /// 以指定的文本和过期时间初始化一个 <see cref="CustomTitle"/> 类的实例。
        /// </summary>
        /// <param name="text">文本。</param>
        /// <param name="expirationTime">过期时间。</param>
        public CustomTitle(string text, DateTime? expirationTime = null)
        {
            Text = text;
            ExpirationTime = expirationTime;
        }

        /// <summary>
        /// 获取过期时间。
        /// 如果没有过期时间，则为 <c>null</c>。
        /// </summary>
        public DateTime? ExpirationTime { get; }

        /// <summary>
        /// 获取文本。
        /// </summary>
        public string Text { get; }
    }
}