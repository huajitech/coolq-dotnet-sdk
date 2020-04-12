using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 为 <see cref="Group.FileUploaded"/> 事件提供数据。
    /// </summary>
    public class FileUploadedEventArgs : RoutedEventArgs
    {
        public FileUploadedEventArgs(
            DateTime time, Group source, Member uploader, File file)
        {
            Time = time;
            Source = source;
            Uploader = uploader;
            File = file;
        }

        /// <summary>
        /// 获取时间。
        /// </summary>
        public DateTime Time { get; }

        /// <summary>
        /// 获取来源群。
        /// </summary>
        public Group Source { get; }

        /// <summary>
        /// 获取上传用户。
        /// </summary>
        public Member Uploader { get; }

        /// <summary>
        /// 获取上传的文件。
        /// </summary>
        public File File { get; }
    }
}