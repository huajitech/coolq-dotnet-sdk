using System.IO;
using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 定义机器人。
    /// </summary>
    public interface IBot
    {
        /// <summary>
        /// 获取一个值，指示当前 <see cref="IBot"/> 对象是否可以发送图片。
        /// </summary>
        bool CanSendImage { get; }

        /// <summary>
        /// 获取一个值，指示当前 <see cref="IBot"/> 对象是否可以发送录音。
        /// </summary>
        bool CanSendRecord { get; }

        /// <summary>
        /// 获取当前 <see cref="IBot"/> 对象的当前用户。
        /// </summary>
        CurrentUser CurrentUser { get; }

        /// <summary>
        /// 获取当前 <see cref="IBot"/> 对象的日志记录器。
        /// </summary>
        Logger Logger { get; }

        /// <summary>
        /// 获取当前 <see cref="IBot"/> 对象的数据目录。
        /// </summary>
        DirectoryInfo DataDirectory { get; }

        /// <summary>
        /// 请求指定文件名的图片。
        /// </summary>
        /// <param name="fileName">请求获取的图片的文件名。</param>
        FileInfo RequestImage(string fileName);

        /// <summary>
        /// 以异步操作请求指定文件名的图片。
        /// </summary>
        /// <param name="fileName">请求获取的图片的文件名。</param>
        Task<FileInfo> RequestImageAsync(string fileName);

        /// <summary>
        /// 请求指定文件名的录音。
        /// </summary>
        /// <param name="fileName">请求获取的录音的文件名。</param>
        /// <param name="fileFormat">请求获取的录音的格式。</param>
        FileInfo RequestRecord(string fileName, string fileFormat);

        /// <summary>
        /// 以异步操作请求指定文件名的录音。
        /// </summary>
        /// <param name="fileName">请求获取的录音的文件名。</param>
        /// <param name="fileFormat">请求获取的录音的格式。</param>
        Task<FileInfo> RequestRecordAsync(string fileName, string fileFormat);
    }
}