using System.Collections.Generic;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示图片的 <see cref="CQCode"/>。
    /// </summary>
    public class Image : CQCode
    {
        public Image()
            : base("image")
        {
        }

        public Image(IDictionary<string, string> parameters)
            : base("image", parameters)
        {
        }

        /// <summary>
        /// 获取或设置当前 <see cref="Image"/> 对象的文件名。
        /// </summary>
        public string? FileName
        {
            get => this["file"];
            set => this["file"] = value;
        }
    }
}