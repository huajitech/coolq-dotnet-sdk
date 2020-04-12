using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HuajiTech.CoolQ.AdvancedMessaging
{
    /// <summary>
    /// 表示图片。
    /// </summary>
    public class Image : CQCode
    {
        public Image()
        {
        }

        public Image(IDictionary<string, string> arguments)
            : base(arguments)
        {
        }

        /// <summary>
        /// 获取或设置文件名。
        /// </summary>
        public string FileName
        {
            get => this["file"];
            set => this["file"] = value;
        }

        public override string Type => "image";

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