using System;
using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 定义聊天。
    /// </summary>
    public interface IChattable : IEquatable<IChattable>
    {
        string DisplayName { get; }

        long Number { get; }

        IMessage Send(string message);

        Task<IMessage> SendAsync(string message);
    }
}