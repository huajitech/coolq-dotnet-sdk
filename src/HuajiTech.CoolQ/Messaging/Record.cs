using HuajiTech.QQ;
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

        public Record(IDictionary<string, string> parameters)
            : base("record", parameters)
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
            get => GetParameterAsBoolean("magic");
            set => SetParameter("magic", value);
        }

        /// <summary>
        /// 请求当前 <see cref="Record"/> 对象表示的文件。
        /// </summary>
        /// <param name="format">返回的文件的格式。</param>
        /// <returns>当前 <see cref="Record"/> 对象表示的文件。</returns>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public FileInfo RequestFile(string format)
        {
            return AppContext.CurrentContext.Bot.RequestRecord(FileName, format);
        }

        /// <summary>
        /// 以异步操作请求当前 <see cref="Record"/> 对象表示的文件。
        /// </summary>
        /// <param name="format">返回的文件的格式。</param>
        /// <returns>当前 <see cref="Record"/> 对象表示的文件。</returns>
        /// <exception cref="CoolQException">酷Q返回了指示操作失败的值。</exception>
        public Task<FileInfo> RequestFileAsync(string format)
        {
            return Task.Run(() => RequestFile(format));
        }
    }
}