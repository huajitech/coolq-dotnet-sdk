using System.Collections.Generic;
using System.IO;

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

        /// <summary>
        /// 获取当前 <see cref="Image"/> 对象表示的文件。
        /// </summary>
        /// <returns>当前 <see cref="Image"/> 对象表示的文件。</returns>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public FileInfo? GetFile()
        {
            if (FileName is null)
            {
                return null;
            }

            return QQ.PluginContext.CurrentContext.Bot.GetImage(FileName);
        }
    }
}