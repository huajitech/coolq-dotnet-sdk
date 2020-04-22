using System.Collections.Generic;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示抖动的 <see cref="CQCode"/>。
    /// </summary>
    public class Shake : CQCode
    {
        public Shake()
            : base("shake")
        {
        }

        public Shake(IDictionary<string, string> parameters)
            : base("shake", parameters)
        {
        }

        /// <summary>
        /// 获取或设置当前 <see cref="Shake"/> 对象的ID。
        /// </summary>
        public int Id
        {
            get => GetArgumentAsInt32("id");
            set => SetArgument("id", value);
        }
    }
}