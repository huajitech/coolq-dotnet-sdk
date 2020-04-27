using System.IO;
using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 定义机器人。
    /// </summary>
    public interface IBot
    {
        bool CanSendImage { get; }

        bool CanSendRecord { get; }

        ICurrentUser CurrentUser { get; }

        ILogger Logger { get; }

        DirectoryInfo DataDirectory { get; }

        FileInfo RequestImage(string fileName);

        Task<FileInfo> RequestImageAsync(string fileName);

        FileInfo RequestRecord(string fileName, string fileFormat);

        Task<FileInfo> RequestRecordAsync(string fileName, string fileFormat);
    }
}