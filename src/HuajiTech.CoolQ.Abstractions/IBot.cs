using System.IO;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 定义机器人。
    /// </summary>
    public interface IBot
    {
        /// <summary>
        /// 获取一个值，指示当前 <see cref="IBot"/> 实例是否可以发送图片。
        /// </summary>
        bool CanSendImage { get; }

        /// <summary>
        /// 获取一个值，指示当前 <see cref="IBot"/> 实例是否可以发送录音。
        /// </summary>
        bool CanSendRecord { get; }

        /// <summary>
        /// 获取当前 <see cref="IBot"/> 实例的当前用户。
        /// </summary>
        ICurrentUser CurrentUser { get; }

        /// <summary>
        /// 获取当前 <see cref="IBot"/> 实例的日志记录器。
        /// </summary>
        ILogger Logger { get; }

        /// <summary>
        /// 获取当前 <see cref="IBot"/> 实例的数据目录。
        /// </summary>
        DirectoryInfo DataDirectory { get; }

        /// <summary>
        /// 请求指定文件名的图片。
        /// </summary>
        /// <param name="fileName">请求获取的图片的文件名。</param>
        FileInfo GetImage(string fileName);

        /// <summary>
        /// 请求指定文件名的录音。
        /// </summary>
        /// <param name="fileName">请求获取的录音的文件名。</param>
        /// <param name="fileFormat">请求获取的录音的格式。</param>
        FileInfo GetRecord(string fileName, string fileFormat);
    }
}