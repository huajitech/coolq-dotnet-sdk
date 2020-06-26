using System.Collections.Generic;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示 Emoji 的 <see cref="CQCode"/>。
    /// </summary>
    public class Emoji : CQCode
    {
        public Emoji()
            : base("emoji")
        {
        }

        public Emoji(IDictionary<string, string> parameters)
            : base("emoji", parameters)
        {
        }

        /// <summary>
        /// 获取或设置当前 <see cref="Emoji"/> 实例的 ID。
        /// </summary>
        public int Id
        {
            get => GetParameterAsInt32("id");
            set => SetParameter("id", value);
        }

        public string ConvertToString() => char.ConvertFromUtf32(Id);
    }
}