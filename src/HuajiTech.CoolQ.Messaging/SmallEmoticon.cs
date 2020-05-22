using System.Collections.Generic;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示小表情的 <see cref="CQCode"/>。
    /// </summary>
    public class SmallEmoticon : CQCode
    {
        public SmallEmoticon()
            : base("sface")
        {
        }

        public SmallEmoticon(IDictionary<string, string> parameters)
            : base("sface", parameters)
        {
        }

        /// <summary>
        /// 获取或设置当前 <see cref="SmallEmoticon"/> 实例的 ID。
        /// </summary>
        public int Id
        {
            get => GetParameterAsInt32("id");
            set => SetParameter("id", value);
        }
    }
}