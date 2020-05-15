using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 定义文件上传事件。
    /// </summary>
    public interface INotifyFileUploaded
    {
        /// <summary>
        /// 在上传文件时引发。
        /// </summary>
        event EventHandler<FileUploadedEventArgs> FileUploaded;
    }
}