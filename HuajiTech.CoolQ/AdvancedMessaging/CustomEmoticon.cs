using System.Collections.Generic;

namespace HuajiTech.CoolQ.AdvancedMessaging
{
    /// <summary>
    /// 表示自定义表情。
    /// </summary>
    public class CustomEmoticon : CQCode
    {
        public CustomEmoticon()
        {
        }

        public CustomEmoticon(IDictionary<string, string> arguments)
            : base(arguments)
        {
        }

        /// <summary>
        /// 获取或设置当前 <see cref="CustomEmoticon"/> 对象的 ID。
        /// </summary>
        public int Id
        {
            get => GetArgumentAsInt32("id");
            set => SetArgument("id", value);
        }

        public override string Type => "bface";
    }
}