using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HuajiTech.CoolQ.AdvancedMessaging
{
    /// <summary>
    /// 表示录音。
    /// </summary>
    public class Record : CQCode
    {
        public Record()
        {
        }

        public Record(IDictionary<string, string> arguments)
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

        /// <summary>
        /// 获取或设置一个值，指示是否变声。
        /// </summary>
        public bool IsVoiceChanged
        {
            get => GetArgumentAsBoolean("magic");
            set => SetArgument("magic", value);
        }

        public override string Type => "record";

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