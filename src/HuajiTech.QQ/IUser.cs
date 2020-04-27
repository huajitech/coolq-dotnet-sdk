using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 定义用户。
    /// </summary>
    public interface IUser : IChattable, IRequestable, IRefreshable
    {
        string Nickname { get; }

        void GiveThumbsUp(int count);

        Task GiveThumbsUpAsync(int count);
    }
}