using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示自定义头衔。
    /// </summary>
    public class CustomTitle
    {
        /// <summary>
        /// 以指定的文本和过期时间初始化一个 <see cref="CustomTitle"/> 类的新实例。
        /// </summary>
        /// <param name="text">文本。</param>
        /// <param name="expirationTime">过期时间。</param>
        /// <exception cref="ArgumentException"><paramref name="text"/> 为 <see langword="null"/>、<see cref="string.Empty"/> 或仅由空白字符组成。</exception>
        public CustomTitle(string text, DateTime? expirationTime = null)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException(AbstractionResources.FieldCannotBeEmptyOrWhiteSpace, nameof(text));
            }

            Text = text;
            ExpirationTime = expirationTime;
        }

        /// <summary>
        /// 获取当前 <see cref="CustomTitle"/> 实例的过期时间。
        /// 如果没有过期时间，则为 <see langword="null"/>。
        /// </summary>
        public DateTime? ExpirationTime { get; }

        /// <summary>
        /// 获取当前 <see cref="CustomTitle"/> 的文本。
        /// </summary>
        public string Text { get; }
    }
}