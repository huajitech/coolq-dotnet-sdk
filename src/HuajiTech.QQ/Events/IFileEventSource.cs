using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 定义文件事件。
    /// </summary>
    public interface IFileEventSource
    {
        /// <summary>
        /// 在上传文件时引发。
        /// </summary>
        event EventHandler<FileUploadedEventArgs> FileUploaded;
    }
}