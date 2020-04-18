using System.Collections.Generic;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示 At (@) 的 <see cref="CQCode"/>。
    /// </summary>
    public class At : CQCode
    {
        public At()
            : base("at")
        {
        }

        public At(IDictionary<string, string> arguments)
            : base("at", arguments)
        {
        }

        /// <summary>
        /// 获取或设置当前 <see cref="At"/> 对象的目标。
        /// </summary>
        public User Target
        {
            get => new User(GetParameterAsInt64("qq"));
            set => SetParameter("qq", value?.Number ?? default);
        }
    }
}