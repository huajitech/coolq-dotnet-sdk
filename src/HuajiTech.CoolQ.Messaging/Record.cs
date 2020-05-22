using System.Collections.Generic;

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
        /// 获取或设置当前 <see cref="Record"/> 实例的文件名。
        /// </summary>
        public string? FileName
        {
            get => this["file"];
            set => this["file"] = value;
        }

        /// <summary>
        /// 获取或设置一个值，指示当前 <see cref="Record"/> 实例是否经过变声。
        /// </summary>
        public bool IsVoiceChanged
        {
            get => GetParameterAsBoolean("magic");
            set => SetParameter("magic", value);
        }
    }
}