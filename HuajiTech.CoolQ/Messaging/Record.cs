using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 表示录音的 <see cref="CQCode"/>。
    /// </summary>
    public class Record : CQCode
    {
        public Record()
            : base("record")
        {
        }

        public Record(IDictionary<string, string> arguments)
            : base("record", arguments)
        {
        }

        /// <summary>
        /// 获取或设置当前 <see cref="Record"/> 对象的文件名。
        /// </summary>
        public string FileName
        {
            get => this["file"];
            set => this["file"] = value;
        }

        /// <summary>
        /// 获取或设置一个值，指示当前 <see cref="Record"/> 对象是否经过变声。
        /// </summary>
        public bool IsVoiceChanged
        {
            get => GetArgumentAsBoolean("magic");
            set => SetArgument("magic", value);
        }

        /// <summary>
        /// 请求文件。
        /// </summary>
        /// <returns>请求到的文件。</returns>
        public FileInfo RequestFile(string format)
        {
            return Bot.RequestRecord(FileName, format);
        }

        /// <summary>
        /// 以异步操作请求文件。
        /// </summary>
        /// <returns>请求到的文件。</returns>
        public Task<FileInfo> RequestFileAsync(string format)
        {
            return Task.Run(() => RequestFile(format));
        }
    }
}