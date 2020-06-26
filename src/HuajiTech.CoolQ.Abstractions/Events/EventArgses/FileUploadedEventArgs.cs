using System;

namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 为 <see cref="INotifyFileUploaded.FileUploaded"/> 事件提供数据。
    /// </summary>
    public class FileUploadedEventArgs : TimedEventArgs
    {
        public FileUploadedEventArgs(DateTime time, IGroup source, IMember uploader, File file)
            : base(time)
        {
            Source = source;
            Uploader = uploader;
            File = file;
        }

        /// <summary>
        /// 获取来源群。
        /// </summary>
        public virtual IGroup Source { get; }

        /// <summary>
        /// 获取上传用户。
        /// </summary>
        public virtual IMember Uploader { get; }

        /// <summary>
        /// 获取上传的文件。
        /// </summary>
        public virtual File File { get; }
    }
}