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
        /// 以异步操作请求当前 <see cref="Image"/> 对象表示的文件。
        /// </summary>
        /// <returns>当前 <see cref="Image"/> 对象表示的文件。</returns>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public FileInfo RequestFile()
        {
            return Bot.RequestImage(FileName);
        }

        /// <summary>
        /// 以异步操作请求当前 <see cref="Image"/> 对象表示的文件。
        /// </summary>
        /// <returns>当前 <see cref="Image"/> 对象表示的文件。</returns>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task<FileInfo> RequestFileAsync()
        {
            return Task.Run(RequestFile);
        }
    }
}