using System.Collections.Generic;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示表情的 <see cref="CQCode"/>。
    /// </summary>
    public class Emoticon : CQCode
    {
        public Emoticon()
            : base("face")
        {
        }

        public Emoticon(IDictionary<string, string> parameters)
            : base("face", parameters)
        {
        }

        /// <summary>
        /// 获取或设置当前 <see cref="Emoticon"/> 实例的 ID。
        /// </summary>
        public int Id
        {
            get => GetParameterAsInt32("id");
            set => SetParameter("id", value);
        }
    }
}