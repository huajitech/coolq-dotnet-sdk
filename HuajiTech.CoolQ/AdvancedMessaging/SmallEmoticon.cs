using System.Collections.Generic;

namespace HuajiTech.CoolQ.AdvancedMessaging
{
    /// <summary>
    /// 表示小表情。
    /// </summary>
    public class SmallEmoticon : CQCode
    {
        public SmallEmoticon()
        {
        }

        public SmallEmoticon(IDictionary<string, string> arguments)
            : base(arguments)
        {
        }

        /// <summary>
        /// 获取或设置 ID。
        /// </summary>
        public int Id
        {
            get => GetParameterAsInt32("id");
            set => SetParameter("id", value);
        }

        public override string Type => "sface";
    }
}