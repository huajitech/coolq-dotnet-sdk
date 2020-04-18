using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

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

        public Image(IDictionary<string, string> arguments)
            : base("image", arguments)
        {
        }

        /// <summary>
        /// 获取或设置当前 <see cref="Image"/> 对象的文件名。
        /// </summary>
        public string FileName
        {
            get => this["file"];
            set => this["file"] = value;
        }

        /// <summary>
        /// 请求文件。
        /// </summary>
        /// <returns>请求到的文件。</returns>
        public FileInfo RequestFile()
        {
            return Bot.RequestImage(FileName);
        }

        /// <summary>
        /// 以异步操作请求文件。
        /// </summary>
        /// <returns>请求到的文件。</returns>
        public Task<FileInfo> RequestFileAsync()
        {
            return Task.Run(RequestFile);
        }
    }
}