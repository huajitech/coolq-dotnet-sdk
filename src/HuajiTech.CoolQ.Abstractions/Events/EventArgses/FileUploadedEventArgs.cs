using System;

namespace HuajiTech.CoolQ.Events
{
    /// <summary>
    /// 为 <see cref="INotifyFileUploaded.FileUploaded"/> 事件提供数据。
    /// </summary>
    public class FileUploadedEventArgs : TimedEventArgs
    {
        public FileUploadedEventArgs(
            DateTime time, IGroup source, IMember uploader, File file)
            : base(time)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Uploader = uploader ?? throw new ArgumentNullException(nameof(uploader));
            File = file ?? throw new ArgumentNullException(nameof(file));
        }

        /// <summary>
        /// 获取来源群。
        /// </summary>
        public IGroup Source { get; }

        /// <summary>
        /// 获取上传用户。
        /// </summary>
        public IMember Uploader { get; }

        /// <summary>
        /// 获取上传的文件。
        /// </summary>
        public File File { get; }
    }
}